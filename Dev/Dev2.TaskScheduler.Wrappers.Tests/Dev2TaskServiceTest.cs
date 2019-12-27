/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Linq;
using NUnit.Framework;
using Microsoft.Win32.TaskScheduler;

namespace Dev2.TaskScheduler.Wrappers.Test
{
    [TestFixture]
    public class Dev2TaskServiceTest
    {
        TaskFolder _folder;
        TaskService _service;
        [SetUp]
        public void Init()
        {

            _service = new TaskService();
            _folder = _service.RootFolder.SubFolders.All(a => a.Name != "WarewolfTestFolder") ? _service.RootFolder.CreateFolder("WarewolfTestFolder") : _service.GetFolder("WarewolfTestFolder");
            var task = _service.NewTask();
            task.Actions.Add(new ExecAction("Notepad.exe"));
            _folder.RegisterTaskDefinition("TestTask", task, TaskCreation.Create, "LocalSchedulerAdmin", "987Sched#@!", TaskLogonType.None);

        }

        [TearDown]
        public void Cleanup()
        {

            _folder = _service.GetFolder("WarewolfTestFolder");
            foreach (var task in _folder.Tasks)
            {
                _folder.DeleteTask(task.Name, false);
            }

            _service.RootFolder.DeleteFolder("WarewolfTestFolder");
            _service.Dispose();
            _folder.Dispose();

        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("TaskShedulerWrapper_Dev2TaskServiceTest_Constructor")]
        public void TaskShedulerWrapper_Dev2TaskServiceTest_Constructor()
        {
            var service = new Dev2TaskService(new TaskServiceConvertorFactory());
            Assert.IsNotNull(service.Instance);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("TaskShedulerWrapper_Dev2TaskServiceTest_PassThrough")]
        public void TaskShedulerWrapper_Dev2TaskServiceTest_PassThrough()
        {
            using (var service = new Dev2TaskService(new TaskServiceConvertorFactory()))
            {
                Assert.AreEqual(service.RootFolder.Instance.Name, _service.RootFolder.Name);
            }
            Assert.IsTrue(_service.Connected);
           
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("TaskShedulerWrapper_Dev2TaskServiceTest_GetUnexistingFolder")]
        public void TaskShedulerWrapper_Dev2TaskServiceTest_GetUnexistingFolder()
        {
            using (var service = new Dev2TaskService(new TaskServiceConvertorFactory()))
            {
                const string NewFolder = "SomethingThatDoesNotExist";
                Assert.IsNotNull(service.GetFolder(NewFolder).Instance);
                Assert.AreEqual(service.GetFolder(NewFolder).Instance.Name, NewFolder);
            }
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("TaskShedulerWrapper_Dev2TaskServiceTest_GetFolder")]
        public void TaskShedulerWrapper_Dev2TaskServiceTest_GetFolder()
        {
            using (var service = new Dev2TaskService(new TaskServiceConvertorFactory()))
            {
                Assert.AreEqual(service.GetFolder("WarewolfTestFolder").Instance.Name,_folder.Name);
            }
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("TaskShedulerWrapper_Dev2TaskServiceTest_GetTask")]
        public void TaskShedulerWrapper_Dev2TaskServiceTest_GetTask()
        {
            using (var service = new Dev2TaskService(new TaskServiceConvertorFactory()))
            {
                Assert.AreEqual(service.GetTask("\\WarewolfTestFolder\\TestTask").Instance.Name, "TestTask");
            }
        }
    }
}
