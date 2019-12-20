using System;
using System.Linq;
using System.Xml.Linq;
using Dev2.Common;
using Dev2.Runtime.ServiceModel;
using Dev2.Runtime.ServiceModel.Data;
using Dev2.Tests.Runtime.XML;
using NUnit.Framework;



namespace Dev2.Tests.Runtime.ServiceModel
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class ComPluginServicesTest
    {

        #region CTOR

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComPluginServicesContructorWithNullResourceCatalogExpectedThrowsArgumentNullException()
        {
            new ComPluginServices(null, null);
        }

        #endregion

        #region DeserializeService

        class ComPluginServicesMock : ComPluginServices
        {
            public new Service DeserializeService(string args)
            {
                return base.DeserializeService(args);
            }
            public new Service DeserializeService(XElement xml, string resourceType)
            {
                return base.DeserializeService(xml, resourceType);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComPluginServicesDeserializeServiceWithNullJsonExpectedThrowsArgumentNullException()
        {
            var services = new ComPluginServicesMock();
            services.DeserializeService(null);
        }

        [Test]
        public void ComPluginServicesDeserializeServiceWithInvalidJsonExpectedReturnsNewPluginService()
        {
            var services = new ComPluginServicesMock();
            var result = services.DeserializeService("{'root' : 'hello' }");
            NUnit.Framework.Assert.AreEqual(result.ResourceID, Guid.Empty);
        }

        [Test]
        public void ComPluginServicesDeserializeServiceWithValidJsonExpectedReturnsPluginService()
        {
            var xml = XmlResource.Fetch("ComPluginService");
            var service = new ComPluginService(xml);

            var services = new ComPluginServicesMock();
            var result = services.DeserializeService(service.ToString());

            NUnit.Framework.Assert.AreEqual(Guid.Parse("89098b76-ac11-40b2-b3e8-b175314cb3bb"), service.ResourceID);
            NUnit.Framework.Assert.AreEqual("ComPluginService", service.ResourceType);
            NUnit.Framework.Assert.AreEqual(Guid.Parse("00746beb-46c1-48a8-9492-e2d20817fcd5"), service.Source.ResourceID);
            NUnit.Framework.Assert.AreEqual("ComPluginTesterSource", service.Source.ResourceName);
            NUnit.Framework.Assert.AreEqual("Dev2.Terrain.Mountain", service.Namespace);
            NUnit.Framework.Assert.AreEqual("Echo", service.Method.Name);

            NUnit.Framework.Assert.AreEqual("<root>hello</root>", service.Method.Parameters.First(p => p.Name == "text").DefaultValue);

            NUnit.Framework.Assert.AreEqual("reverb", service.Recordsets[0].Fields.First(f => f.Name == "echo").Alias);
        }

        [Test]
        public void ComPluginServicesDeserializeServiceWithNullXmlExpectedReturnsNewPluginService()
        {
            var services = new ComPluginServicesMock();
            var result = (ComPluginService)services.DeserializeService(null, "ComPluginService");

            NUnit.Framework.Assert.AreEqual(result.ResourceID, Guid.Empty);
        }

        [Test]
        public void ComPluginServicesDeserializeServiceWithValidXmlExpectedReturnsPluginService()
        {
            var xml = XmlResource.Fetch("ComPluginService");

            var services = new ComPluginServicesMock();
            var service = (ComPluginService)services.DeserializeService(xml, "ComPluginService");

            NUnit.Framework.Assert.AreEqual(Guid.Parse("89098b76-ac11-40b2-b3e8-b175314cb3bb"), service.ResourceID);
            NUnit.Framework.Assert.AreEqual("ComPluginService", service.ResourceType);
            NUnit.Framework.Assert.AreEqual(Guid.Parse("00746beb-46c1-48a8-9492-e2d20817fcd5"), service.Source.ResourceID);
            NUnit.Framework.Assert.AreEqual("ComPluginTesterSource", service.Source.ResourceName);
            NUnit.Framework.Assert.AreEqual("Dev2.Terrain.Mountain", service.Namespace);
            NUnit.Framework.Assert.AreEqual("Echo", service.Method.Name);

            NUnit.Framework.Assert.AreEqual("<root>hello</root>", service.Method.Parameters.First(p => p.Name == "text").DefaultValue);

            NUnit.Framework.Assert.AreEqual("reverb", service.Recordsets[0].Fields.First(f => f.Name == "echo").Alias);
        }

        #endregion

        #region Namespaces

        [Test]
        public void ComPluginServicesNamespacesWithNullArgsExpectedReturnsEmptyList()
        {
            var services = new ComPluginServices();
            var result = services.Namespaces(null, Guid.Empty, Guid.Empty);
            NUnit.Framework.Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ComPluginServicesNamespacesWithInvalidArgsExpectedReturnsEmptyList()
        {
            var services = new ComPluginServices();
            var result = services.Namespaces(new ComPluginSource(), Guid.Empty, Guid.Empty);
            NUnit.Framework.Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ComPluginServicesNamespacesWithValidArgsExpectedReturnsList()
        {
            var source = CreatePluginSource();
            var args = source.ToString();
            var workspaceID = Guid.NewGuid();

            EnvironmentVariables.GetWorkspacePath(workspaceID);

            var services = new ComPluginServices();
            var result = services.Namespaces(source, workspaceID, Guid.Empty);

            NUnit.Framework.Assert.IsTrue(result.Count > 0);
        }

        #endregion

        #region Methods

        [Test]
        public void ComPluginServicesMethodsWithNullArgsExpectedReturnsEmptyList()
        {
            var services = new ComPluginServices();
            var result = services.Methods(null, Guid.Empty, Guid.Empty);
            NUnit.Framework.Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ComPluginServicesMethodsWithInvalidArgsExpectedReturnsEmptyList()
        {
            var services = new ComPluginServices();
            var result = services.Methods(new ComPluginService(), Guid.Empty, Guid.Empty);
            NUnit.Framework.Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ComPluginServicesMethodsWithValidArgsExpectedReturnsList()
        {
            var service = CreatePluginService();
            var workspaceID = Guid.NewGuid();

            EnvironmentVariables.GetWorkspacePath(workspaceID);

            var services = new ComPluginServices();
            var result = services.Methods(service, workspaceID, Guid.Empty);

            NUnit.Framework.Assert.IsTrue(result.Count > 5, "Not enough items in COM server method list.");
        }

        #endregion

        #region Test
       

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("ComPluginServices_Test")]
        public void ComPluginServices_Test_WhenTestingPluginReturningJsonString_ExpectValidPaths()
        {
            var pluginServices = new ComPluginServices();
            var serviceDef = JsonResource.Fetch("ComPrimitivePluginReturningJsonString");
            //------------Execute Test---------------------------
            try
            {
                pluginServices.Test(serviceDef, out string serializedResult);
            }
            catch(Exception e)
            {
                //Calls the execution correctly;
                
                NUnit.Framework.Assert.AreEqual("[Microsoft][ODBC Driver Manager] Data source name not found and no default driver specified", e.InnerException.Message);
                
            }
            
          
        }

        [Test]
        public void PluginServicesTestWithNullArgsExpectedReturnsRecordsetWithError()
        {
            //------------Setup for test--------------------------
            var services = new ComPluginServicesMock();
            //------------Execute Test---------------------------
            var result = services.Test(null, out string serializedResult);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result[0].HasErrors);
        }

        [Test]
        public void PluginServicesTestWithInvalidArgsExpectedReturnsRecordsetWithError()
        {
            //------------Setup for test--------------------------
            var services = new ComPluginServicesMock();
            //------------Execute Test---------------------------
            var result = services.Test("xxx", out string serializedResult);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result[0].HasErrors);
        }

        #endregion

        #region CreatePluginService

        public static ComPluginService CreatePluginService()
        {
            return CreatePluginService(new ServiceMethod
            {
                Name = "DummyMethod"
            });
        }

        public static ComPluginService CreatePluginService(ServiceMethod method)
        {

            var source = CreatePluginSource();
            var service = new ComPluginService
            {
                ResourceID = Guid.NewGuid(),
                ResourceName = "DummyPluginService",
                ResourceType = "ComPluginService",
                Namespace = "Namespace",
                Method = method,
                Source = source,
                
            };
            return service;
        }

        #endregion

        #region CreatePluginSource

        static ComPluginSource CreatePluginSource()
        {
            return new ComPluginSource
            {

                ResourceID = Guid.NewGuid(),
                ResourceName = "Dummy",
                ResourceType = "ComPluginSource",
                ClsId = clsId
            };
        }
        const string clsId = "00000514-0000-0010-8000-00AA006D2EA4";
        #endregion
    }
}