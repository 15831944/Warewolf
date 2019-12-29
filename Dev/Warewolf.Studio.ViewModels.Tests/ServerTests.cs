using System;
using System.Collections.Generic;
using System.Net;
using System.Network;
using Caliburn.Micro;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Enums;
using Dev2.Common.Interfaces.Infrastructure;
using Dev2.Common.Interfaces.Infrastructure.Events;
using Dev2.Common.Interfaces.Security;
using Dev2.Common.Interfaces.Toolbox;
using Dev2.Communication;
using Dev2.Core.Tests.Environments;
using Dev2.Explorer;
using Dev2.Network;
using Dev2.Providers.Events;
using Dev2.Services.Events;
using Dev2.Services.Security;
using Dev2.Studio.Core.Models;
using Dev2.Studio.Interfaces;
using Dev2.Threading;
using NUnit.Framework;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    public class ServerTests
    {
        Mock<IServer> _env;
        Mock<IEnvironmentConnection> _envConnection;
        Mock<IExplorerRepository> _proxyLayer;
        Guid _serverId;
        Mock<IAuthorizationService> _authorizationService;

        [SetUp]
        public void Initialize()
        {
            var envId = new Guid();
            _serverId = new Guid();
            _env = new Mock<IServer>();
            _authorizationService = new Mock<IAuthorizationService>();
            _envConnection = new Mock<IEnvironmentConnection>();
            _proxyLayer = new Mock<IExplorerRepository>();
            _envConnection.Setup(connection => connection.ServerID).Returns(_serverId);
            _envConnection.Setup(connection => connection.DisplayName).Returns("EnvName");
            _env.Setup(model => model.Connection).Returns(_envConnection.Object);
            _env.Setup(model => model.EnvironmentID).Returns(envId);
        }
        

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void Server_GivenNewInstance_IsNotNull()
        {
            //------------Setup for test--------------------------
            var mockConnection = new Mock<IEnvironmentConnection>();
            mockConnection.Setup(connection => connection.DisplayName).Returns("TestConnection");
            var server = new Server(Guid.Empty,mockConnection.Object);
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(server);
            NUnit.Framework.Assert.IsNotNull(server.Connection);
            NUnit.Framework.Assert.IsNotNull(server.EnvironmentID);
            NUnit.Framework.Assert.IsFalse(string.IsNullOrEmpty(server.DisplayName));
        }
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void CanDeployTo_GivenIsAuthorizedDeployToIsTrue_ShouldReturnTrue()
        {
            //------------Setup for test--------------------------
            _authorizationService.Setup(model => model.IsAuthorized(AuthorizationContext.DeployTo,null)).Returns(true);
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, new Mock<IEnvironmentConnection>().Object);
            server.AuthorizationService = _authorizationService.Object;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(server.CanDeployTo);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void CanDeployFrom_GivenIsAuthorizedDeployFromIsTrue_ShouldReturnTrue()
        {
            //------------Setup for test--------------------------
            _authorizationService.Setup(model => model.IsAuthorized(AuthorizationContext.DeployFrom, null)).Returns(true);
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, new Mock<IEnvironmentConnection>().Object);
            server.AuthorizationService = _authorizationService.Object;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(server.CanDeployFrom);            
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void ServerID_GivenEnvironmentConnectionServerID_ShouldReturnValue()
        {
            //------------Setup for test--------------------------
            var mockConnection = new Mock<IEnvironmentConnection>();
            mockConnection.Setup(connection => connection.ServerID).Returns(Guid.Empty);
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, mockConnection.Object);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(server.ServerID);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void EnvironmentModel_GivenConnection_ShouldReturnValue()
        {
            //------------Setup for test--------------------------
            var mockConnection = new Mock<IEnvironmentConnection>();
            mockConnection.Setup(connection => connection.ServerID).Returns(Guid.Empty);
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, mockConnection.Object);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(server.Connection);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void UpdateRepository_GivenNewServerInstane_ShouldReturnValue()
        {
            //------------Setup for test--------------------------
            var mockConnection = new Mock<IEnvironmentConnection>();
            mockConnection.Setup(connection => connection.ServerID).Returns(Guid.Empty);
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, mockConnection.Object);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(server.UpdateRepository);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void Permissions_GivenIsAuthorizedDeployFromIsTrue_ShouldReturnTrue()
        {
            //------------Setup for test--------------------------
            var auth = new Mock<IAuthorizationService>();
            auth.Setup(service => service.GetResourcePermissions(It.IsAny<Guid>()))
                .Returns(Permissions.Administrator);
            var mockConnection = new Mock<IEnvironmentConnection>();
            mockConnection.Setup(connection => connection.ServerID).Returns(Guid.Empty);
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, mockConnection.Object);
            server.AuthorizationService = auth.Object;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(server.GetPermissions(It.IsAny<Guid>()));
            NUnit.Framework.Assert.AreEqual(Permissions.Administrator, server.GetPermissions(It.IsAny<Guid>()));
        }   
        
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void GetServerInformation_GivenServerIsNotConnected_ShouldReturnTrue()
        {
            var valueFunction = new Dictionary<string, string> { { "some key", "some value" } };
            //------------Setup for test--------------------------
            _envConnection.Setup(connection => connection.IsConnected).Returns(false);
            var proxyLayer = new Mock<IExplorerRepository>();

            var adminManager = new Mock<IAdminManager>();
            adminManager.Setup(manager => manager.GetServerInformation()).Returns(valueFunction);
            proxyLayer.SetupGet(repository => repository.AdminManagerProxy).Returns(adminManager.Object);
            var server = new Server(Guid.Empty, _envConnection.Object);
            server.ProxyLayer = proxyLayer.Object;
            //------------Assert Precondition-------------------------
            //------------Execute Test---------------------------
            var serverInformation = server.GetServerInformation();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(serverInformation);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void GetServerVersion_GivenServerIsNotConnected_ShouldReturnTrue()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(connection => connection.IsConnected).Returns(false);
            var server = new Server(Guid.Empty, _envConnection.Object);
            var adminManager = new Mock<IAdminManager>();
            adminManager.Setup(manager => manager.GetServerVersion()).Returns("2.0.0.0");
            _proxyLayer.SetupGet(repository => repository.AdminManagerProxy).Returns(adminManager.Object);
            server.ProxyLayer = _proxyLayer.Object;
            //------------Assert Precondition-------------------------
            //------------Execute Test---------------------------
            var serverVersion = server.GetServerVersion();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(string.IsNullOrEmpty(serverVersion));
        }
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void GetMinSupportedVersion_GivenServerIsNotConnected_ShouldReturnTrue()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(connection => connection.IsConnected).Returns(false);
            var adminManager = new Mock<IAdminManager>();
            adminManager.Setup(manager => manager.GetMinSupportedServerVersion()).Returns("1.5.0.9");
            _proxyLayer.SetupGet(repository => repository.AdminManagerProxy).Returns(adminManager.Object);
            var server = new Server(Guid.Empty,_envConnection.Object);
            server.ProxyLayer = _proxyLayer.Object;
            //------------Assert Precondition-------------------------
            //------------Execute Test---------------------------
            var minSupportedVersion = server.GetMinSupportedVersion();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(string.IsNullOrEmpty(minSupportedVersion));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void Connect_GivenServerIsNotConnected_ShouldReturnTrue()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(connection => connection.IsConnected).Returns(false);
            var query = new Mock<IAdminManager>();
            query.Setup(manager => manager.GetMinSupportedServerVersion()).Returns("2.0.0.0");
            _proxyLayer.Setup(repository => repository.AdminManagerProxy).Returns(query.Object);
            var server = new Server(Guid.Empty,_envConnection.Object);
            server.ProxyLayer = _proxyLayer.Object;
            //------------Assert Precondition-------------------------
            //------------Execute Test---------------------------
            var minSupportedVersion = server.GetMinSupportedVersion();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(string.IsNullOrEmpty(minSupportedVersion));
            NUnit.Framework.Assert.AreEqual("2.0.0.0", minSupportedVersion);
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void IsConnected_GivenEnvironmentConnectionIsConnected_ShouldReturnTrue()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(connection => connection.IsConnected).Returns(true);
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------            
            var server = new Server(Guid.Empty, _envConnection.Object);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(server.IsConnected);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void AllowEdit_GivenServerIsNotLocalHost_ShouldReturnTrue()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, _envConnection.Object);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(server.AllowEdit);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void DisplayName_GivenEnvirnmentConnectionIsConnected_ShouldHaveValue()
        {
            const string serverName = "Localhost";
            const string serverNameConnected = "Localhost (Connected)";
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(true);
            _envConnection.Setup(model => model.IsConnected).Returns(true);
            _envConnection.Setup(model => model.DisplayName).Returns(serverName);
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, _envConnection.Object);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(string.IsNullOrEmpty(server.DisplayName));
            NUnit.Framework.Assert.AreEqual(serverNameConnected, server.DisplayName);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void ToString_GivenDisplyaName_ShouldDisplayName()
        {
            const string serverName = "Localhost";
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(true);
            _envConnection.Setup(model => model.DisplayName).Returns(serverName);
            var server = new Server(Guid.Empty,_envConnection.Object);
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var toString = server.DisplayName;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(serverName, toString);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void DisplayName_GivenEnvirnmentConnectionIsNotConnected_ShouldHaveValue()
        {
            const string serverName = "Localhost";
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(true);
            _envConnection.Setup(model => model.IsConnected).Returns(false);
            _envConnection.Setup(model => model.DisplayName).Returns(serverName);
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, _envConnection.Object);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(string.IsNullOrEmpty(server.DisplayName));
            NUnit.Framework.Assert.AreEqual(serverName, server.DisplayName);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void DisplayName_GivenNoEnvirnmentConnection_ShouldHaveValueOfNewRemoteServer()
        {
            const string serverName = "Default Name";
            //------------Setup for test--------------------------
            var server = new Server(Guid.Empty,_envConnection.Object);
            //------------Assert Precondition--------------------
            NUnit.Framework.Assert.IsFalse(string.IsNullOrEmpty(server.DisplayName));            
            //------------Execute Test---------------------------
            server.Connection = null;
            //------------Assert Results-------------------------            
            NUnit.Framework.Assert.AreEqual(serverName, server.DisplayName);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void ExplorerRepository_GivenProxyLayer_ShouldNotBeNull()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            _proxyLayer.Setup(repository => repository.QueryManagerProxy).Returns(new Mock<IQueryManager>().Object);

            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var server = new Server(Guid.Empty, _envConnection.Object);
            server.ProxyLayer = _proxyLayer.Object;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(server.ExplorerRepository);
            NUnit.Framework.Assert.IsNotNull(server.QueryProxy);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void Connect_GivenEnvironmentConnectionIsNoConnected_ShouldConnect()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            _envConnection.Setup(model => model.IsConnected).Returns(false);
            _envConnection.Setup(model => model.Connect(It.IsAny<Guid>()));
            var server = new Server(Guid.Empty, _envConnection.Object);
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            server.Connect();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(server);
            _envConnection.Verify(connection => connection.Connect(It.IsAny<Guid>()), Times.AtLeastOnce);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void ConnectAsync_GivenEnvironmentConnectionIsNoConnected_ShouldConnect()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            _envConnection.Setup(model => model.IsConnected).Returns(false);
            _envConnection.Setup(model => model.ConnectAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            var server = new Server(Guid.Empty, _envConnection.Object);
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var connectAsync = server.ConnectAsync();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(connectAsync.Result);
            _envConnection.Verify(connection => connection.ConnectAsync(It.IsAny<Guid>()), Times.AtLeastOnce);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void Disconnect_GivenEnvironmentConnectionIsNoConnected_ShouldConnect()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            _envConnection.Setup(model => model.IsConnected).Returns(true);
            _envConnection.Setup(model => model.Disconnect());
            var server = new Server(Guid.Empty, _envConnection.Object);
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            server.Disconnect();
            //------------Assert Results-------------------------
            _envConnection.Verify(connection => connection.Disconnect(), Times.AtLeastOnce);
        }
        
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void LoadTools_ShouldReturnTools()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            var query = new Mock<IQueryManager>();
            var toolDescriptors = new List<IToolDescriptor>
            {
                new Mock<IToolDescriptor>().Object
            };
            query.Setup(manager => manager.FetchTools()).Returns(toolDescriptors);
            _proxyLayer.Setup(repository => repository.QueryManagerProxy).Returns(query.Object);
            var server = new Server(Guid.Empty, _envConnection.Object);
            server.ProxyLayer = _proxyLayer.Object;
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var tools = server.LoadTools();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(tools);
            NUnit.Framework.Assert.AreEqual(1 , tools.Count);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void LoadExplorer_ShouldReturnTools()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            _proxyLayer.Setup(repository => repository.LoadExplorer(false)).ReturnsAsync(new ServerExplorerItem());
            var server = new Server(Guid.Empty, _envConnection.Object);
            server.ProxyLayer = _proxyLayer.Object;
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var loadExplorer = server.LoadExplorer();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(loadExplorer);
            NUnit.Framework.Assert.IsTrue(server.HasLoaded);
            _proxyLayer.Verify(repository => repository.LoadExplorer(false), Times.AtLeastOnce);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Sanele Mthembu")]
        public void LoadExplorerDuplicates_ShouldReturnTools()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            var duplicates = new List<string>()
            {
                "SomeDuplicateResource"
            };
            _proxyLayer.Setup(repository => repository.LoadExplorerDuplicates()).ReturnsAsync(duplicates);
            var server = new Server(Guid.Empty, _envConnection.Object);
            server.ProxyLayer = _proxyLayer.Object;
            //------------Assert Precondition--------------------
            //------------Execute Test---------------------------
            var explorerDuplicates = server.LoadExplorerDuplicates();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(explorerDuplicates);
            NUnit.Framework.Assert.IsTrue(server.HasLoaded);
            _proxyLayer.Verify(repository => repository.LoadExplorerDuplicates(), Times.AtLeastOnce);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void GetServerInformation_Given_ServerInformation_IsNotNUll_Returns_ServerInformation()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            var server = new Server(Guid.Empty, _envConnection.Object);
            var privateObj = new PrivateObject(server);
            var info = new Dictionary<string, string>();
            info.Add("information", "value for inforamtion");
            privateObj.SetField("_serverInformation", info);
            var information = server.GetServerInformation();
            NUnit.Framework.Assert.IsNotNull(information);
            NUnit.Framework.Assert.AreEqual(info, information);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void GetServerVersion_Given_ServerVersion_IsNotNUll_Returns_ServerVersion()
        {
            //------------Setup for test--------------------------
            _envConnection.Setup(model => model.IsLocalHost).Returns(false);
            var server = new Server(Guid.Empty, _envConnection.Object);
            var privateObj = new PrivateObject(server);
            privateObj.SetField("_version", "0.0.0.1");
            var version = server.GetServerVersion();
            NUnit.Framework.Assert.IsNotNull(version);
            NUnit.Framework.Assert.AreEqual("0.0.0.1", version);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [UnitTestAttributes.ExpectedException(typeof(ArgumentNullException))]
        public void EnvironmentModel_Constructor_NullConnection_ThrowsArgumentNullException()
        {
            new Server(Guid.NewGuid(), null);
            
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void EnvironmentModel_Constructor_ConnectionAndWizardEngine_InitializesConnectionAndResourceRepository()
        {

            var connection = CreateConnection();
            //, wizard.Object
            var env = new Server(Guid.NewGuid(), connection.Object);
            NUnit.Framework.Assert.IsNotNull(env.Connection);
            NUnit.Framework.Assert.IsNotNull(env.ResourceRepository);
            NUnit.Framework.Assert.AreSame(connection.Object, env.Connection);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [UnitTestAttributes.ExpectedException(typeof(ArgumentNullException))]
        public void EnvironmentModel_Constructor_ConnectionAndNullResourceRepository_ThrowsArgumentNullException()
        {
            var connection = CreateConnection();
            
            new Server(Guid.NewGuid(), connection.Object, null);
            
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void EnvironmentModel_Constructor_ConnectionAndResourceRepository_InitializesConnectionAndResourceRepository()
        {
            var connection = CreateConnection();
            var repo = new Mock<IResourceRepository>();
            var env = new Server(Guid.NewGuid(), connection.Object, repo.Object);

            NUnit.Framework.Assert.IsNotNull(env.Connection);
            NUnit.Framework.Assert.IsNotNull(env.ResourceRepository);
            NUnit.Framework.Assert.AreSame(connection.Object, env.Connection);
            NUnit.Framework.Assert.AreSame(repo.Object, env.ResourceRepository);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Hagashen Naidu")]
        [Category("EnvironmentModel_DisplayName")]
        public void EnvironmentModel_DisplayName_WithConnection_ContainsConnectionAddress()
        {
            //------------Setup for test--------------------------
            var connection = CreateConnection();
            var repo = new Mock<IResourceRepository>();
            var env = new Server(Guid.NewGuid(), connection.Object, repo.Object);
            const string expectedDisplayName = "localhost";
            //------------Execute Test---------------------------
            var displayName = env.DisplayName;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(expectedDisplayName, displayName);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Connect")]
        [UnitTestAttributes.ExpectedException(typeof(ArgumentException))]
        public void EnvironmentModel_Connect_IsNotConnectedAndNameIsEmpty_ThrowsArgumentException()
        {
            var connection = CreateConnection();
            connection.Setup(c => c.IsConnected).Returns(false);
            connection.Setup(c => c.DisplayName).Returns("");

            var env = CreateEnvironmentModel(Guid.NewGuid(), connection.Object);

            env.Connect();
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Connect")]
        public void EnvironmentModel_Connect_IsNotConnectedAndNameIsNotEmpty_DoesInvokeConnection()
        {
            var connection = CreateConnection();
            connection.Setup(c => c.IsConnected).Returns(false);
            connection.Setup(c => c.DisplayName).Returns("Test");
            connection.Setup(c => c.Connect(It.IsAny<Guid>())).Verifiable();

            var env = CreateEnvironmentModel(Guid.NewGuid(), connection.Object);

            env.Connect();

            connection.Verify(c => c.Connect(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Connect")]
        public void EnvironmentModel_Connect_IsConnected_DoesNotInvokeConnection()
        {
            var connection = CreateConnection();
            connection.Setup(c => c.IsConnected).Returns(true);
            connection.Setup(c => c.DisplayName).Returns("Test");
            connection.Setup(c => c.Connect(It.IsAny<Guid>())).Verifiable();

            var env = CreateEnvironmentModel(Guid.NewGuid(), connection.Object);

            env.Connect();

            connection.Verify(c => c.Connect(It.IsAny<Guid>()), Times.Never());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentModel_Connect")]
        [UnitTestAttributes.ExpectedException(typeof(ArgumentNullException))]
        public void EnvironmentModel_ConnectOther_Null_ThrowsArgumentNullException()
        {
            var connection = CreateConnection();

            var env = CreateEnvironmentModel(Guid.NewGuid(), connection.Object);

            env.Connect(null);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentModel_Connect")]
        public void EnvironmentModel_ConnectOther_NonNullAndConnected_DoesNotInvokeOthersConnect()
        {
            var c1 = CreateConnection();
            c1.Setup(c => c.DisplayName).Returns("Test");
            c1.Setup(c => c.Connect(It.IsAny<Guid>())).Verifiable();

            var c2 = CreateConnection();
            c2.Setup(c => c.DisplayName).Returns("Other");
            c2.Setup(c => c.Connect(It.IsAny<Guid>())).Verifiable();
            c2.Setup(c => c.IsConnected).Returns(true);

            var e1 = CreateEnvironmentModel(Guid.NewGuid(), c1.Object);
            var e2 = CreateEnvironmentModel(Guid.NewGuid(), c2.Object);

            e1.Connect(e2);

            c2.Verify(c => c.Connect(It.IsAny<Guid>()), Times.Never());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentModel_Connect")]
        public void EnvironmentModel_ConnectOther_NonNullAndConnected_InvokesThisConnect()
        {
            var c1 = CreateConnection();
            c1.Setup(c => c.DisplayName).Returns("Test");
            c1.Setup(c => c.Connect(It.IsAny<Guid>())).Verifiable();

            var c2 = CreateConnection();
            c2.Setup(c => c.DisplayName).Returns("Other");
            c2.Setup(c => c.Connect(It.IsAny<Guid>())).Verifiable();
            c2.Setup(c => c.IsConnected).Returns(true);

            var e1 = CreateEnvironmentModel(Guid.NewGuid(), c1.Object);
            var e2 = CreateEnvironmentModel(Guid.NewGuid(), c2.Object);

            e1.Connect(e2);

            c1.Verify(c => c.Connect(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Connect")]
        public void EnvironmentModel_ConnectOther_NonNullAndNotConnected_InvokesOtherConnect()
        {
            var c1 = CreateConnection();
            c1.Setup(c => c.DisplayName).Returns("Test");
            c1.Setup(c => c.Connect(It.IsAny<Guid>())).Verifiable();

            var c2 = CreateConnection();
            c2.Setup(c => c.DisplayName).Returns("Other");
            c2.Setup(c => c.IsConnected).Returns(false);
            c2.Setup(c => c.Connect(It.IsAny<Guid>())).Callback(() => c2.Setup(c => c.IsConnected).Returns(true)).Verifiable();

            var e1 = CreateEnvironmentModel(Guid.NewGuid(), c1.Object);
            var e2 = CreateEnvironmentModel(Guid.NewGuid(), c2.Object);

            e1.Connect(e2);

            c2.Verify(c => c.Connect(It.IsAny<Guid>()), Times.Once());
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Connect")]
        [UnitTestAttributes.ExpectedException(typeof(InvalidOperationException))]
        public void EnvironmentModel_ConnectOther_NonNullAndNotConnectedFails_ThrowsInvalidOperationException()
        {
            var c1 = CreateConnection();
            c1.Setup(c => c.DisplayName).Returns("Test");
            c1.Setup(c => c.Connect(It.IsAny<Guid>())).Verifiable();

            var c2 = CreateConnection();
            c2.Setup(c => c.DisplayName).Returns("Other");
            c2.Setup(c => c.IsConnected).Returns(false);

            var e1 = CreateEnvironmentModel(Guid.NewGuid(), c1.Object);
            var e2 = CreateEnvironmentModel(Guid.NewGuid(), c2.Object);

            e1.Connect(e2);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentModel_NetworkStateChanged")]
        public void EnvironmentModel_NetworkStateChanged_Offline_DoesPublishEnvironmentDisconnectedMessage()
        {
            TestConnectionEvents(NetworkState.Offline);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentModel_NetworkStateChanged")]
        public void EnvironmentModel_NetworkStateChanged_Online_DoesPublishEnvironmentConnectedMessage()
        {
            TestConnectionEvents(NetworkState.Online);
        }

        static void TestConnectionEvents(NetworkState toState)
        {

            var environmentConnection = new Mock<IEnvironmentConnection>();
            environmentConnection.Setup(connection => connection.IsConnected).Returns(true);
            environmentConnection.Setup(connection => connection.ServerEvents).Returns(EventPublishers.Studio);

            var repo = new Mock<IResourceRepository>();
            var envModel = new Server(Guid.NewGuid(), environmentConnection.Object, repo.Object);

            envModel.IsConnectedChanged += (sender, args) => NUnit.Framework.Assert.AreEqual(toState == NetworkState.Online, args.IsConnected);

            environmentConnection.Raise(c => c.NetworkStateChanged += null, new NetworkStateEventArgs(NetworkState.Connecting, toState));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void EnvironmentModel_ForceLoadResources_InvokesForceLoadOnResourceRepository()
        {
            var resourceRepo = new Mock<IResourceRepository>();
            resourceRepo.Setup(r => r.Load(true)).Verifiable();

            var connection = CreateConnection();
            connection.Setup(c => c.DisplayName).Returns("Test");
            connection.Setup(c => c.IsConnected).Returns(true);

            var env = new Server(Guid.NewGuid(), connection.Object, resourceRepo.Object);

            NUnit.Framework.Assert.IsTrue(env.CanStudioExecute);

            env.ForceLoadResources();

            resourceRepo.Verify(r => r.Load(true), Times.Once());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void EnvironmentModel_LoadResources_ShouldLoadTrue_InvokesLoadOnResourceRepository()
        {
            var resourceRepo = new Mock<IResourceRepository>();
            resourceRepo.Setup(r => r.Load(false)).Verifiable();

            var connection = CreateConnection();
            connection.Setup(c => c.DisplayName).Returns("Test");
            connection.Setup(c => c.IsConnected).Returns(true);

            var env = new Server(Guid.NewGuid(), connection.Object, resourceRepo.Object);

            NUnit.Framework.Assert.IsTrue(env.CanStudioExecute);

            env.LoadResources();

            resourceRepo.Verify(r => r.UpdateWorkspace(), Times.Once());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void EnvironmentModel_LoadResources_ShouldLoadFalse_NotInvokeLoadOnResourceRepository()
        {
            var resourceRepo = new Mock<IResourceRepository>();
            resourceRepo.Setup(r => r.Load(false)).Verifiable();

            var connection = CreateConnection();
            connection.Setup(c => c.DisplayName).Returns("Test");
            connection.Setup(c => c.IsConnected).Returns(true);

            var env = new Server(Guid.NewGuid(), connection.Object, resourceRepo.Object) { CanStudioExecute = false };

            env.LoadResources();

            resourceRepo.Verify(r => r.Load(false), Times.Never());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("EnvironmentModel_Load")]
        public void EnvironmentModel_Load_Loads_SetsLoaded()
        {
            var resourceRepo = new Mock<IResourceRepository>();
            resourceRepo.Setup(r => r.Load(false)).Verifiable();

            var connection = CreateConnection();
            connection.Setup(c => c.DisplayName).Returns("Test");
            connection.Setup(c => c.IsConnected).Returns(true);

            var env = new Server(Guid.NewGuid(), connection.Object, resourceRepo.Object) { CanStudioExecute = true };

            env.LoadResources();
            NUnit.Framework.Assert.IsTrue(env.HasLoadedResources);

        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("EnvironmentModel_Load")]
        public void EnvironmentModel_Load_DoesNotLoads_SetsLoaded()
        {
            var resourceRepo = new Mock<IResourceRepository>();
            resourceRepo.Setup(r => r.Load(false)).Verifiable();

            var connection = CreateConnection();
            connection.Setup(c => c.DisplayName).Returns("Test");
            connection.Setup(c => c.IsConnected).Returns(true);

            var env = new Server(Guid.NewGuid(), connection.Object, resourceRepo.Object) { CanStudioExecute = false };

            env.LoadResources();
            NUnit.Framework.Assert.IsFalse(env.HasLoadedResources);

        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("EnvironmentModel_Load")]
        public void EnvironmentModel_Load_CallsLoadedEvent()
        {
            var resourceRepo = new Mock<IResourceRepository>();
            resourceRepo.Setup(r => r.Load(false)).Verifiable();

            var connection = CreateConnection();
            connection.Setup(c => c.DisplayName).Returns("Test");
            connection.Setup(c => c.IsConnected).Returns(true);

            var env = new Server(Guid.NewGuid(), connection.Object, resourceRepo.Object);
            env.ResourcesLoaded += (sender, args) => NUnit.Framework.Assert.AreEqual(args.Model, env);
            env.CanStudioExecute = false;

            env.LoadResources();
            NUnit.Framework.Assert.IsFalse(env.HasLoadedResources);

        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentModel_IsLocalHost")]
        public void EnvironmentModel_IsLocalHost_IsLocalHost_True()
        {
            var conn = CreateConnection();
            conn.SetupProperty(c => c.DisplayName, "localhost");
            conn.Setup(connection => connection.IsLocalHost).Returns(conn.Object.DisplayName == "localhost");
            var env = CreateEnvironmentModel(Guid.NewGuid(), conn.Object);
            var isLocalHost = env.IsLocalHost;
            NUnit.Framework.Assert.IsTrue(isLocalHost);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentModel_IsLocalHost")]
        public void EnvironmentModel_IsLocalHost_IsNotLocalHost_False()
        {
            var conn = CreateConnection();
            conn.Setup(c => c.DisplayName).Returns("notlocalhost");
            var env = CreateEnvironmentModel(Guid.NewGuid(), conn.Object);
            var isLocalHost = env.IsLocalHost;
            NUnit.Framework.Assert.IsFalse(isLocalHost);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_AuthorizationService")]
        public void EnvironmentModel_AuthorizationService_Constructor_PropertyInitialized()
        {
            //------------Setup for test--------------------------
            var connection = CreateConnection();

            //------------Execute Test---------------------------
            var env = new Server(Guid.NewGuid(), connection.Object, new Mock<IResourceRepository>().Object);
            connection.Raise(environmentConnection => environmentConnection.NetworkStateChanged += null, new NetworkStateEventArgs(NetworkState.Offline, NetworkState.Online));
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(env.AuthorizationService);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Connection")]
        public void EnvironmentModel_Connection_PermissionsChanged_IsAuthorizedChanged()
        {
            //------------Setup for test--------------------------
            var connection = CreateConnection();
            connection.Setup(c => c.IsAuthorized).Returns(false);

            var envModel = new TestServer(new Mock<IEventAggregator>().Object, Guid.NewGuid(), connection.Object, new Mock<IResourceRepository>().Object, false);
            NUnit.Framework.Assert.IsFalse(envModel.IsAuthorized);

            //------------Execute Test---------------------------
            connection.Setup(c => c.IsAuthorized).Returns(true);
            connection.Raise(c => c.PermissionsChanged += null, EventArgs.Empty);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(envModel.IsAuthorized);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Connection")]
        public void EnvironmentModel_Connection_PermissionsChanged_IsDeployFromChanged()
        {
            //------------Setup for test--------------------------
            var connection = CreateConnection();
            connection.Setup(c => c.IsAuthorized).Returns(true);

            var envModel = new TestServer(new Mock<IEventAggregator>().Object, Guid.NewGuid(), connection.Object, new Mock<IResourceRepository>().Object, false);
            connection.Raise(environmentConnection => environmentConnection.NetworkStateChanged += null, new NetworkStateEventArgs(NetworkState.Offline, NetworkState.Online));
            NUnit.Framework.Assert.IsFalse(envModel.IsAuthorizedDeployFrom);

            //------------Execute Test---------------------------
            envModel.AuthorizationServiceMock.Setup(service => service.IsAuthorized(AuthorizationContext.DeployFrom, null)).Returns(true);
            connection.Raise(c => c.PermissionsChanged += null, EventArgs.Empty);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(envModel.IsAuthorizedDeployFrom);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Connection")]
        public void EnvironmentModel_Connection_PermissionsChanged_IsDeployToChanged()
        {
            //------------Setup for test--------------------------
            var connection = CreateConnection();
            connection.Setup(c => c.IsAuthorized).Returns(true);

            var envModel = new TestServer(new Mock<IEventAggregator>().Object, Guid.NewGuid(), connection.Object, new Mock<IResourceRepository>().Object, false);
            connection.Raise(environmentConnection => environmentConnection.NetworkStateChanged += null, new NetworkStateEventArgs(NetworkState.Offline, NetworkState.Online));
            NUnit.Framework.Assert.IsFalse(envModel.IsAuthorizedDeployTo);

            //------------Execute Test---------------------------
            envModel.AuthorizationServiceMock.Setup(service => service.IsAuthorized(AuthorizationContext.DeployTo, null)).Returns(true);
            connection.Raise(c => c.PermissionsChanged += null, EventArgs.Empty);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(envModel.IsAuthorizedDeployTo);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_AuthorizationService")]
        public void EnvironmentModel_AuthorizationService_PermissionsChanged_IsAuthorizedDeployToAndIsAuthorizedDeployFromChanged()
        {
            //------------Setup for test--------------------------
            var connection = CreateConnection();

            var envModel = new TestServer(new Mock<IEventAggregator>().Object, Guid.NewGuid(), connection.Object, new Mock<IResourceRepository>().Object, false);
            connection.Raise(environmentConnection => environmentConnection.NetworkStateChanged += null, new NetworkStateEventArgs(NetworkState.Offline, NetworkState.Online));
            envModel.AuthorizationServiceMock.Setup(a => a.IsAuthorized(AuthorizationContext.DeployFrom, null)).Returns(false).Verifiable();
            envModel.AuthorizationServiceMock.Setup(a => a.IsAuthorized(AuthorizationContext.DeployTo, null)).Returns(false).Verifiable();

            NUnit.Framework.Assert.IsFalse(envModel.IsAuthorizedDeployFrom);
            NUnit.Framework.Assert.IsFalse(envModel.IsAuthorizedDeployTo);

            //------------Execute Test---------------------------
            envModel.AuthorizationServiceMock.Setup(a => a.IsAuthorized(AuthorizationContext.DeployFrom, null)).Returns(true).Verifiable();
            envModel.AuthorizationServiceMock.Setup(a => a.IsAuthorized(AuthorizationContext.DeployTo, null)).Returns(true).Verifiable();
            envModel.AuthorizationServiceMock.Raise(a => a.PermissionsChanged += null, EventArgs.Empty);

            //------------Assert Results-------------------------
            envModel.AuthorizationServiceMock.Verify(a => a.IsAuthorized(AuthorizationContext.DeployFrom, null));
            envModel.AuthorizationServiceMock.Verify(a => a.IsAuthorized(AuthorizationContext.DeployTo, null));
            NUnit.Framework.Assert.IsTrue(envModel.IsAuthorizedDeployFrom);
            NUnit.Framework.Assert.IsTrue(envModel.IsAuthorizedDeployTo);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Equals")]
        public void EnvironmentModel_Equals_OtherIsNull_False()
        {
            //------------Setup for test--------------------------
            var environment = CreateEqualityEnvironmentModel(Guid.NewGuid(), "Test", new Guid(), "https://myotherserver1:3143");

            //------------Execute Test---------------------------
            var actual = environment.Equals(null);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(actual);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Equals")]
        public void EnvironmentModel_Equals_OtherIsSame_True()
        {
            //------------Setup for test--------------------------
            var resourceID = Guid.NewGuid();
            var serverID = Guid.NewGuid();
            const string ServerUri = "https://myotherserver1:3143";
            const string Name = "test";

            var environment1 = CreateEqualityEnvironmentModel(resourceID, Name, serverID, ServerUri);
            var environment2 = CreateEqualityEnvironmentModel(resourceID, Name, serverID, ServerUri);

            //------------Execute Test---------------------------
            var actual = environment1.Equals(environment2);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(actual);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Equals")]
        public void EnvironmentModel_Equals_OtherHasDifferentID_False()
        {
            //------------Setup for test--------------------------
            const string Name = "test";
            var serverID = Guid.NewGuid();
            const string ServerUri = "https://myotherserver1:3143";

            var environment1 = CreateEqualityEnvironmentModel(Guid.NewGuid(), Name, serverID, ServerUri);
            var environment2 = CreateEqualityEnvironmentModel(Guid.NewGuid(), Name, serverID, ServerUri);

            //------------Execute Test---------------------------
            var actual = environment1.Equals(environment2);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(actual);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Equals")]
        public void EnvironmentModel_Equals_OtherHasDifferentName_True()
        {
            //------------Setup for test--------------------------
            var resourceID = Guid.NewGuid();
            var serverID = Guid.NewGuid();
            const string ServerUri = "https://myotherserver1:3143";
            const string Name = "test";

            var environment1 = CreateEqualityEnvironmentModel(resourceID, Name + "1", serverID, ServerUri);
            var environment2 = CreateEqualityEnvironmentModel(resourceID, Name + "1", serverID, ServerUri);

            //------------Execute Test---------------------------
            var actual = environment1.Equals(environment2);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(actual);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Disconnect")]
        public void EnvironmentModel_Disconnect_IsConnected_DoesInvokeDisconnectOnConnection()
        {
            //------------Setup for test--------------------------
            var connection = CreateConnection();
            connection.Setup(c => c.IsConnected).Returns(true);
            connection.Setup(c => c.Disconnect()).Verifiable();

            var environment = CreateEnvironmentModel(Guid.NewGuid(), connection.Object);

            //------------Execute Test---------------------------
            environment.Disconnect();

            //------------Assert Results-------------------------
            connection.Verify(c => c.Disconnect());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Trevor Williams-Ros")]
        [Category("EnvironmentModel_Disconnect")]
        public void EnvironmentModel_Disconnect_IsNotConnected_DoesNotInvokeDisconnectOnConnection()
        {
            //------------Setup for test--------------------------
            var connection = CreateConnection();
            connection.Setup(c => c.IsConnected).Returns(false);
            connection.Setup(c => c.Disconnect()).Verifiable();

            var environment = CreateEnvironmentModel(Guid.NewGuid(), connection.Object);

            //------------Execute Test---------------------------
            environment.Disconnect();

            //------------Assert Results-------------------------
            connection.Verify(c => c.Disconnect(), Times.Never());
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentTreeViewModel_PermissionsChanged")]
        [Author("Leon Rajindrasomething")]
        public void EnvironmentTreeViewModel_PermissionsChanged_MemoIDEqualsEnvironmentServerId_UserPermissionChanges()
        {
            //------------Setup for test--------------------------
            var resourceID = Guid.NewGuid();
            var memoServerID = Guid.NewGuid();

            var pubMemo = new PermissionsModifiedMemo { ServerID = memoServerID };

            pubMemo.ModifiedPermissions.Add(new WindowsGroupPermission { ResourceID = resourceID, Permissions = Permissions.Execute });
            pubMemo.ModifiedPermissions.Add(new WindowsGroupPermission { ResourceID = resourceID, Permissions = Permissions.DeployTo });

            var eventPublisher = new EventPublisher();
            var connection = new Mock<IEnvironmentConnection>();
            connection.Setup(e => e.ServerEvents).Returns(eventPublisher);
            connection.SetupGet(c => c.ServerID).Returns(memoServerID);

            connection.Setup(a => a.DisplayName).Returns("localhost");
            //------------Execute Test---------------------------
            eventPublisher.Publish(pubMemo);
        }
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category("EnvironmentTreeViewModel_PermissionsChanged")]
        [Author("Leon Rajindrasomething")]
        public void EnvironmentTreeViewModel_PermissionsChanged_MemoIDEqualsEnvironmentServerId_UserPermissionChangesNonLocalHost()
        {
            //------------Setup for test--------------------------
            var resourceID = Guid.NewGuid();
            var memoServerID = Guid.NewGuid();

            var pubMemo = new PermissionsModifiedMemo { ServerID = memoServerID };

            pubMemo.ModifiedPermissions.Add(new WindowsGroupPermission { ResourceID = resourceID, Permissions = Permissions.Execute });
            pubMemo.ModifiedPermissions.Add(new WindowsGroupPermission { ResourceID = resourceID, Permissions = Permissions.DeployTo });

            var eventPublisher = new EventPublisher();
            var connection = new Mock<IEnvironmentConnection>();
            connection.Setup(e => e.ServerEvents).Returns(eventPublisher);
            connection.SetupGet(c => c.ServerID).Returns(memoServerID);

            connection.Raise(environmentConnection => environmentConnection.NetworkStateChanged += null, new NetworkStateEventArgs(NetworkState.Offline, NetworkState.Online));
            connection.Setup(a => a.DisplayName).Returns("bob");
            //------------Execute Test---------------------------
            eventPublisher.Publish(pubMemo);
        }

        static Mock<IEnvironmentConnection> CreateConnection()
        {
            var conn = new Mock<IEnvironmentConnection>();
            conn.Setup(c => c.ServerEvents).Returns(new Mock<IEventPublisher>().Object);
            conn.Setup(connection => connection.WebServerUri).Returns(new Uri("http://localhost:3142"));
            conn.Setup(connection => connection.DisplayName).Returns("localhost");
            return conn;
        }

        static Server CreateEnvironmentModel(Guid id, IEnvironmentConnection connection)
        {
            var repo = new Mock<IResourceRepository>();

            return new Server(id, connection, repo.Object);
        }

        public static IServer CreateEqualityEnvironmentModel(Guid resourceID, string resourceName, Guid serverID, string serverUri)
        {
            var proxy = new TestEqualityConnection(serverID, serverUri);
            return new Server(resourceID, proxy) { Name = resourceName };
        }
    }

    public class TestEqualityConnection : ServerProxy
    {
        public TestEqualityConnection(Guid serverID, string serverUri)
            : base(serverUri, CredentialCache.DefaultCredentials, new SynchronousAsyncWorker())
        {
            ServerID = serverID;
        }
    }

}
