using System;
using System.Collections.Generic;
using System.Text;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Enums;
using Dev2.Communication;
using Dev2.Data;
using Dev2.Runtime.ESB.Management.Services;
using Dev2.Runtime.ServiceModel.Data;
using Dev2.Workspaces;
using NUnit.Framework;
using Moq;

namespace Dev2.Tests.Runtime.Services
{
    [TestFixture]
    public class FetchAllTestsTest
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category("GetResourceID")]
        public void GetResourceID_GivenArgsWithResourceId_ShouldReturnResourceId()
        {
            //------------Setup for test--------------------------
            var fetchAllTests = new FetchAllTests();
            var stringBuilder = new StringBuilder();
            var resId = Guid.NewGuid();
            stringBuilder.Append(resId);
            //------------Execute Test---------------------------
            var requestArgs = new Dictionary<string, StringBuilder>
            {
                { "resourceID", stringBuilder }
            };
            //------------Execute Test---------------------------
            var resourceID = fetchAllTests.GetResourceID(requestArgs);
            //------------Assert Results-------------------------
            Assert.AreEqual(resId, resourceID);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("GetResourceID")]
        public void GetResourceID_ShouldReturnEmptyGuid()
        {
            //------------Setup for test--------------------------
            var fetchAllTests = new FetchAllTests();
            //------------Execute Test---------------------------
            var resId = fetchAllTests.GetResourceID(new Dictionary<string, StringBuilder>());
            //------------Assert Results-------------------------
            Assert.AreEqual(Guid.Empty, resId);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("GetResourceID")]
        public void GetAuthorizationContextForService_ShouldReturnContext()
        {
            //------------Setup for test--------------------------
            var fetchAllTests = new FetchAllTests();
            //------------Execute Test---------------------------
            var resId = fetchAllTests.GetAuthorizationContextForService();
            //------------Assert Results-------------------------
            Assert.AreEqual(AuthorizationContext.Contribute, resId);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("FetchAllTests_HandlesType")]
        public void FetchTests_HandlesType_ExpectName()
        {
            //------------Setup for test--------------------------
            var fetchAllTests = new FetchAllTests();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            Assert.AreEqual("FetchAllTests", fetchAllTests.HandlesType());
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("FetchAllTests_Execute")]
        public void FetchTests_Execute_NullValues_ErrorResult()
        {
            //------------Setup for test--------------------------
            var fetchAllTests = new FetchAllTests();
            var serializer = new Dev2JsonSerializer();
            //------------Execute Test---------------------------
            var jsonResult = fetchAllTests.Execute(null, null);
            var result = serializer.Deserialize<CompressedExecuteMessage>(jsonResult);
            //------------Assert Results-------------------------
            Assert.IsFalse(result.HasError);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("FetchAllTests_Execute")]
        public void FetchTests_Execute_ResourceIDNotPresent_ErrorResult()
        {
            //------------Setup for test--------------------------
            var values = new Dictionary<string, StringBuilder> { { "item", new StringBuilder() } };
            var fetchAllTests = new FetchAllTests();
            var serializer = new Dev2JsonSerializer();
            //------------Execute Test---------------------------
            var jsonResult = fetchAllTests.Execute(values, null);
            var result = serializer.Deserialize<CompressedExecuteMessage>(jsonResult);
            //------------Assert Results-------------------------
            Assert.IsFalse(result.HasError);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("FetchAllTests_Execute")]
        public void FetchTests_Execute_ResourceIDNotGuid_ErrorResult()
        {
            //------------Setup for test--------------------------
            var values = new Dictionary<string, StringBuilder> { { "resourceID", new StringBuilder("ABCDE") } };
            var fetchAllTests = new FetchAllTests();
            var serializer = new Dev2JsonSerializer();
            //------------Execute Test---------------------------
            var jsonResult = fetchAllTests.Execute(values, null);
            var result = serializer.Deserialize<CompressedExecuteMessage>(jsonResult);
            //------------Assert Results-------------------------
            Assert.IsFalse(result.HasError);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("FetchTests_Execute")]
        public void FetchAllTests_Execute_ExpectTestList()
        {
            //------------Setup for test--------------------------
            var fetchAllTests = new FetchAllTests();

            var listOfTests = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    AuthenticationType = AuthenticationType.Public,
                    Enabled = true,
                    TestName = "Test MyWF"
                }
            };
            var repo = new Mock<ITestCatalog>();
            var ws = new Mock<IWorkspace>();
            var resID = Guid.Empty;
            repo.Setup(a => a.FetchAllTests()).Returns(listOfTests).Verifiable();

            var serializer = new Dev2JsonSerializer();
            fetchAllTests.TestCatalog = repo.Object;
            //------------Execute Test---------------------------
            var res = fetchAllTests.Execute(null, ws.Object);
            var msg = serializer.Deserialize<CompressedExecuteMessage>(res);
            var testModels = serializer.Deserialize<List<IServiceTestModelTO>>(msg.GetDecompressedMessage());
            //------------Assert Results-------------------------
            repo.Verify(a => a.FetchAllTests());
            Assert.AreEqual(listOfTests.Count, testModels.Count);
            Assert.AreEqual(listOfTests[0].TestName, testModels[0].TestName);
        }
    }
}
