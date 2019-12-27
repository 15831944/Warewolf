using System;
using System.Collections.Generic;
using System.Text;
using Dev2.Common.Interfaces.Enums;
using Dev2.Common.Interfaces.Explorer;
using Dev2.Common.Interfaces.Security;
using Dev2.Common.Interfaces.Versioning;
using Dev2.Communication;
using Dev2.Explorer;
using Dev2.Runtime.ESB.Management.Services;
using Dev2.Workspaces;
using NUnit.Framework;
using Moq;

namespace Dev2.Tests.Runtime.Services
{
         [TestFixture]
    public class GetServerVersionTest
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("GetResourceID")]
        public void GetResourceID_ShouldReturnEmptyGuid()
        {
            //------------Setup for test--------------------------
            var serverVersion = new GetServerVersion();

            //------------Execute Test---------------------------
            var resId = serverVersion.GetResourceID(new Dictionary<string, StringBuilder>());
            //------------Assert Results-------------------------
            Assert.AreEqual(Guid.Empty, resId);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("GetResourceID")]
        public void GetAuthorizationContextForService_ShouldReturnContext()
        {
            //------------Setup for test--------------------------
            var serverVersion = new GetServerVersion();

            //------------Execute Test---------------------------
            var resId = serverVersion.GetAuthorizationContextForService();
            //------------Assert Results-------------------------
            Assert.AreEqual(AuthorizationContext.Any, resId);
        }

        [Test]
            [Author("Leon Rajindrapersadh")]
            [Category("GetVersions_HandlesType")]
            
            public void GetVersions_HandlesType_ExpectName()
            
            {
                //------------Setup for test--------------------------
                var getVersions = new GetServerVersion();


                //------------Execute Test---------------------------

                //------------Assert Results-------------------------
                Assert.AreEqual("GetServerVersion", getVersions.HandlesType());
            }

            [Test]
            [Author("Leon Rajindrapersadh")]
            [Category("GetVersions_Execute")]
            public void GetVersions_Execute_NullValuesParameter_ErrorResult()
            {
                //------------Setup for test--------------------------
                var getVersions = new GetServerVersion();
                var serializer = new Dev2JsonSerializer();
                //------------Execute Test---------------------------
                var jsonResult = getVersions.Execute(null, null);
            var result = serializer.Deserialize<string>(jsonResult);
                //------------Assert Results-------------------------
                Assert.IsNotNull(result);
            }

            [Test]
            [Author("Leon Rajindrapersadh")]
            [Category("GetVersions_HandlesType")]
            public void GetVersions_Execute_ExpectName()
            {
                //------------Setup for test--------------------------
                var getVersions = new GetVersions();
                var resourceId = Guid.NewGuid();
                var item = new ServerExplorerItem("a", Guid.NewGuid(), "Folder", null, Permissions.DeployFrom, "");
            var repo = new Mock<IServerVersionRepository>();
                var ws = new Mock<IWorkspace>();
                repo.Setup(a => a.GetVersions(resourceId)).Returns(new List<IExplorerItem> {item});
                var serializer = new Dev2JsonSerializer();
                ws.Setup(a => a.ID).Returns(Guid.Empty);
                getVersions.ServerVersionRepo = repo.Object;
                //------------Execute Test---------------------------
                var ax = getVersions.Execute(new Dictionary<string, StringBuilder> {{"resourceId",new StringBuilder( resourceId.ToString())}}, ws.Object);
                //------------Assert Results-------------------------
                repo.Verify(a => a.GetVersions(It.IsAny<Guid>()));
                Assert.AreEqual(serializer.Deserialize<IList<IExplorerItem>>(ax.ToString())[0].ResourceId, item.ResourceId);
            }

            [Test]
            [Author("Leon Rajindrapersadh")]
            [Category("GetVersions_HandlesType")]
            public void GetVersions_CreateServiceEntry_ExpectProperlyFormedDynamicService()
            {
                //------------Setup for test--------------------------
                var getVersions = new GetServerVersion();


                //------------Execute Test---------------------------
                var a = getVersions.CreateServiceEntry();
                //------------Assert Results-------------------------
                var b = a.DataListSpecification.ToString();
                Assert.AreEqual("<DataList><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>", b);
            }
        }
    }




