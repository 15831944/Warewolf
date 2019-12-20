using System;
using System.Linq;
using ActivityUnitTests;
using Dev2.Activities.Scripting;
using Dev2.Common.Interfaces.Enums;
using NUnit.Framework;
using Unlimited.Applications.BusinessDesignStudio.Activities;
using Warewolf.Core;


namespace Dev2.Tests.Activities.ActivityTests.Scripting
{
    [TestFixture]
    [SetUpFixture]
    public class DsfPythonActivityTests : BaseActivityUnitTest
    {
        [OneTimeTearDown]
        public static void Cleaner()
        {
            try
            {
            }
            catch (Exception)
            {
                //supress exceptio
            }
        }
        
      
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Attribute_GivenIsNew_ShouldhaveCorrectValues()
        {
            //---------------Set up test pack-------------------
            var act = new DsfPythonActivity();
            //---------------Assert Precondition----------------
            Assert.IsNotNull(act);
            //---------------Execute Test ----------------------
            var toolDescriptorInfo = typeof(DsfPythonActivity).GetCustomAttributes(typeof(ToolDescriptorInfo), false).Single() as ToolDescriptorInfo;
            //---------------Test Result -----------------------
            
            Assert.AreEqual("Scripting", toolDescriptorInfo.Category );
            Assert.AreEqual("python script", toolDescriptorInfo.FilterTag );
            Assert.AreEqual("Scripting-Python", toolDescriptorInfo.Icon );
            Assert.AreEqual("Python", toolDescriptorInfo.Name );
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void OnConstruction_GivenType_ShouldInheritCorrectly()
        {
            //---------------Set up test pack-------------------
            var act = new DsfPythonActivity();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            Assert.IsInstanceOf(act.GetType(), typeof(DsfActivityAbstract<string>));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_Given_GivenIsNew_ShouldSetJavascript()
        {
            //---------------Set up test pack-------------------
            var act = new DsfPythonActivity();
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(act.GetType(), typeof(DsfActivityAbstract<string>));
            //---------------Execute Test ----------------------
            var displayName = act.DisplayName;
            //---------------Test Result -----------------------
            Assert.AreEqual("Python", displayName);

        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Script_GivenIsNew_ShouldBeEmpty()
        {
            //---------------Set up test pack-------------------
            var act = new DsfPythonActivity();
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(act.GetType(), typeof(DsfActivityAbstract<string>));
            //---------------Execute Test ----------------------
            var displayName = act.Script;
            //---------------Test Result -----------------------
            Assert.AreEqual("", displayName);

        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ScriptType_GivenIsNew_ShouldSetJavascript()
        {
            //---------------Set up test pack-------------------
            var act = new DsfPythonActivity();
            //---------------Assert Precondition----------------
            Assert.AreEqual("Python", act.DisplayName);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.AreEqual(enScriptType.Python, act.ScriptType);

        }


       

    }
}