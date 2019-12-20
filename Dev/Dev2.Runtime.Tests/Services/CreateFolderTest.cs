/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Collections.Generic;
using System.Text;
using Dev2.Common;
using Dev2.Common.Interfaces.Enums;
using Dev2.Common.Interfaces.Explorer;
using Dev2.Common.Interfaces.Hosting;
using Dev2.Common.Interfaces.Infrastructure;
using Dev2.Common.Interfaces.Security;
using Dev2.Communication;
using Dev2.Explorer;
using Dev2.Runtime.ESB.Management.Services;
using Dev2.Workspaces;
using NUnit.Framework;
using Moq;


namespace Dev2.Tests.Runtime.Services
{
    [TestFixture]
    [SetUpFixture]
    public class CreateFolderTest
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("GetResourceID")]
        public void GetResourceID_ShouldReturnEmptyGuid()
        {
            //------------Setup for test--------------------------
            var service = new AddFolderService();

            //------------Execute Test---------------------------
            var resId = service.GetResourceID(new Dictionary<string, StringBuilder>());
            //------------Assert Results-------------------------
            Assert.AreEqual(Guid.Empty, resId);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("GetResourceID")]
        public void GetAuthorizationContextForService_ShouldReturnContext()
        {
            //------------Setup for test--------------------------
            var service = new AddFolderService();

            //------------Execute Test---------------------------
            var resId = service.GetAuthorizationContextForService();
            //------------Assert Results-------------------------
            Assert.AreEqual(AuthorizationContext.Contribute, resId);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CreateFolder_HandlesType")]
        public void CreateFolder_HandlesType_ExpectName()
        {
            //------------Setup for test--------------------------
            var addFolder = new AddFolderService();


            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual("AddFolderService", addFolder.HandlesType());
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CreateFolder_Execute")]
        public void CreateFolder_Execute_ExpectCreateCalled()
        {
            //------------Setup for test--------------------------
            var createFolderService = new AddFolderService();

            var item = new ServerExplorerItem("a", Guid.NewGuid(), "Folder", null, Permissions.DeployFrom, "");
            item.ResourcePath = @"root\";
            var repo = new Mock<IExplorerServerResourceRepository>();
            var ws = new Mock<IWorkspace>();
            repo.Setup(a => a.AddItem(item, It.IsAny<Guid>())).Returns(new ExplorerRepositoryResult(ExecStatus.Fail, "noddy"));
            var serializer = new Dev2JsonSerializer();
            var inputs = new Dictionary<string, StringBuilder>
            {
                { "itemToAdd", serializer.SerializeToBuilder(item) }
            };
            ws.Setup(a => a.ID).Returns(Guid.Empty);
            createFolderService.ServerExplorerRepo = repo.Object;
            //------------Execute Test---------------------------
            createFolderService.Execute(inputs, ws.Object);
            //------------Assert Results-------------------------
            repo.Verify(a => a.AddItem(It.IsAny<IExplorerItem>(), It.IsAny<Guid>()));
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CreateFolder_HandlesType")]
        public void CreateFolder_CreateServiceEntry_ExpectProperlyFormedDynamicService()
        {
            //------------Setup for test--------------------------
            var createFolder = new AddFolderService();


            //------------Execute Test---------------------------
            var a = createFolder.CreateServiceEntry();
            //------------Assert Results-------------------------
            var b = a.DataListSpecification.ToString();
            Assert.AreEqual("<DataList><itemToAdd ColumnIODirection=\"Input\"/><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>", b);
        }
    }
}
