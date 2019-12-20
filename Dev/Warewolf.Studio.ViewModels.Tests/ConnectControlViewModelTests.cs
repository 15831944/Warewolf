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
using System.Threading.Tasks;
using System.Windows;
using Dev2;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Help;
using Dev2.Common.Interfaces.Studio.Controller;
using Dev2.Core.Tests.Environments;
using Dev2.Instrumentation;
using Dev2.Studio.Core;
using Dev2.Studio.Core.Models;
using Dev2.Studio.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using NUnit.Framework;
using Moq;
using Warewolf.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class ConnectControlViewModelTests
    {
        Mock<IServer> _serverMock;
        Mock<IServer> _serverIIMock;

        Mock<IEventAggregator> _eventAggregatorMock;

        Mock<IStudioUpdateManager> _updateRepositoryMock;

        List<string> _changedProperties;

        Guid _serverEnvironmentId;

        ConnectControlViewModel _target;

        private Mock<IApplicationTracker> _applicationTrackerMock;

        [SetUp]
        public void TestInitialize()
        {
            _serverMock = new Mock<IServer>();
            _serverIIMock = new Mock<IServer>();
            _eventAggregatorMock = new Mock<IEventAggregator>();
            _updateRepositoryMock = new Mock<IStudioUpdateManager>();
            _updateRepositoryMock.SetupProperty(manager => manager.ServerSaved);
            _serverMock.SetupGet(it => it.UpdateRepository).Returns(_updateRepositoryMock.Object);
            _serverMock.SetupGet(it => it.DisplayName).Returns("some text");
            _serverEnvironmentId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(_serverEnvironmentId);

            var mockEnvironmentConnection = SetupMockConnection();
            _serverMock.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);

            _changedProperties = new List<string>();
            var serverRepo = new Mock<IServerRepository>();
            serverRepo.Setup(repository => repository.ActiveServer).Returns(_serverMock.Object);
            serverRepo.Setup(repository => repository.Source).Returns(_serverMock.Object);
            serverRepo.Setup(repository => repository.All()).Returns(new List<IServer>()
            {
                _serverMock.Object
            });
            serverRepo.Setup(repository => repository.Get(It.IsAny<Guid>())).Returns(_serverMock.Object);
            CustomContainer.Register(serverRepo.Object);

            _applicationTrackerMock = new Mock<IApplicationTracker>();
            _applicationTrackerMock.Setup(controller => controller.TrackEvent(It.IsAny<string>(), It.IsAny<string>()));
            CustomContainer.Register(_applicationTrackerMock.Object);

            _target = new ConnectControlViewModel(_serverMock.Object, _eventAggregatorMock.Object) { ShouldUpdateActiveEnvironment = true };
            _target.ShouldUpdateActiveEnvironment = true;
            _target.PropertyChanged += Target_PropertyChanged;
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [NUnit.Framework.ExpectedException(typeof(ArgumentNullException))]
        public void TestConnectControlViewModelServerNull()
        {
            //act
            var connectControlViewModel = new ConnectControlViewModel(null, _eventAggregatorMock.Object);
            NUnit.Framework.Assert.IsNotNull(connectControlViewModel);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [NUnit.Framework.ExpectedException(typeof(ArgumentNullException))]
        public void TestConnectControlViewModelEventAggregatorNull()
        {
            //act
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, null);
            NUnit.Framework.Assert.IsNotNull(connectControlViewModel);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [NUnit.Framework.ExpectedException(typeof(ArgumentNullException))]
        public void TestConnectControlViewModelExpectedProperties()
        {
            //act
            var connectControlViewModel = new ConnectControlViewModel(null, _eventAggregatorMock.Object);
            NUnit.Framework.Assert.IsNotNull(connectControlViewModel);
            NUnit.Framework.Assert.IsTrue(connectControlViewModel.CanEditServer);
            NUnit.Framework.Assert.IsTrue(connectControlViewModel.CanCreateServer);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestEditConnectionCommandCantbeExecuted()
        {
            //arrange
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.Empty);

            //act
            NUnit.Framework.Assert.IsFalse(_target.EditConnectionCommand.CanExecute(null));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestEditConnectionCommandSelectedConnectionNull()
        {
            //arrange
            _target.SelectedConnection = null;

            //act
            _target.EditConnectionCommand.Execute(null);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestShouldTrackSelectedConnectionWarewolfStore()
        {
            var uri = new Uri("https://store.warewolf.io:3143/dsf");
            var mockEnvironmentConnection = new Mock<IEnvironmentConnection>();

            mockEnvironmentConnection.Setup(a => a.AppServerUri).Returns(uri);
            mockEnvironmentConnection.Setup(a => a.AuthenticationType).Returns(Dev2.Runtime.ServiceModel.Data.AuthenticationType.Public);
            mockEnvironmentConnection.Setup(a => a.WebServerUri).Returns(uri);
            mockEnvironmentConnection.Setup(a => a.ID).Returns(Guid.Empty);

            var newSelectedConnection = new Mock<IServer>();
            var newSelectedConnectionEnvironmentId = Guid.NewGuid();
            newSelectedConnection.SetupGet(it => it.DisplayName).Returns("Warewolf Store");
            newSelectedConnection.SetupGet(it => it.EnvironmentID).Returns(newSelectedConnectionEnvironmentId);
            newSelectedConnection.SetupGet(it => it.HasLoaded).Returns(true);
            newSelectedConnection.SetupGet(it => it.IsConnected).Returns(true);
            newSelectedConnection.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);
            //arrange
            _target.SelectedConnection = newSelectedConnection.Object;

            //act
            _applicationTrackerMock.Verify(controller => controller.TrackEvent(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestShouldNotTrackSelectedConnectionWarewolfStore()
        {
            var mockEnvironmentConnection = SetupMockConnection();

            var newSelectedConnection = new Mock<IServer>();
            var newSelectedConnectionEnvironmentId = Guid.NewGuid();
            newSelectedConnection.SetupGet(it => it.DisplayName).Returns("Other Server");
            newSelectedConnection.SetupGet(it => it.EnvironmentID).Returns(newSelectedConnectionEnvironmentId);
            newSelectedConnection.SetupGet(it => it.HasLoaded).Returns(true);
            newSelectedConnection.SetupGet(it => it.IsConnected).Returns(true);
            newSelectedConnection.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);
            //arrange
            _target.SelectedConnection = newSelectedConnection.Object;

            //act
            _applicationTrackerMock.Verify(controller => controller.TrackEvent(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestEditConnectionCommand()
        {
            //arrange
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());
            _serverMock.SetupGet(it => it.AllowEdit).Returns(true);

            //act
            NUnit.Framework.Assert.IsTrue(_target.EditConnectionCommand.CanExecute(null));
            _target.EditConnectionCommand.Execute(null);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewConnectionCommand()
        {
            //arrange
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());
            _serverMock.SetupGet(it => it.AllowEdit).Returns(true);

            var mainViewModelMock = new Mock<IShellViewModel>();
            CustomContainer.Register(mainViewModelMock.Object);

            //act
            NUnit.Framework.Assert.IsTrue(_target.NewConnectionCommand.CanExecute(null));
            _target.NewConnectionCommand.Execute(null);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSelectedConnectionNonLocalhostLabel()
        {
            //arrange
            var mainViewModelMock = new Mock<IShellViewModel>();
            CustomContainer.Register(mainViewModelMock.Object);
            var newSelectedConnection = new Mock<IServer>();
            var newSelectedConnectionEnvironmentId = Guid.NewGuid();
            newSelectedConnection.SetupGet(it => it.DisplayName).Returns("Nonlocalhost");
            newSelectedConnection.SetupGet(it => it.EnvironmentID).Returns(newSelectedConnectionEnvironmentId);
            newSelectedConnection.SetupGet(it => it.HasLoaded).Returns(true);
            newSelectedConnection.SetupGet(it => it.IsConnected).Returns(true);

            var mockEnvironmentConnection = SetupMockConnection();
            newSelectedConnection.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);

            _changedProperties.Clear();
            var isSelectedEnvironmentChangedRaised = false;
            _target.SelectedEnvironmentChanged += (sender, args) =>
                {
                    isSelectedEnvironmentChangedRaised = sender == _target && args == _serverEnvironmentId;
                };
            var isCanExecuteChangedRaised = false;
            _target.EditConnectionCommand.CanExecuteChanged += (sender, args) =>
                {
                    isCanExecuteChangedRaised = true;
                };
            //act
            _target.SelectedConnection = newSelectedConnection.Object;
            var value = _target.SelectedConnection;

            //assert
            NUnit.Framework.Assert.AreSame(value, newSelectedConnection.Object, "The SelectedConnection does not match with the expected.");
            mainViewModelMock.Verify(it => it.SetActiveServer(newSelectedConnectionEnvironmentId));
            NUnit.Framework.Assert.IsTrue(_target.SelectedConnection.IsConnected, "The SelectedConnection is not connected as expected.");
            NUnit.Framework.Assert.IsTrue(_changedProperties.Contains("SelectedConnection"), "Changed properties does not conatin SelectedConnection.");
            NUnit.Framework.Assert.IsFalse(isSelectedEnvironmentChangedRaised, "ConnectControlViewModel SelectedEnvironmentChanged event did not execute as expected.");
            NUnit.Framework.Assert.IsTrue(isCanExecuteChangedRaised, "ConnectControlViewModel EditConnectionCommand CanExecuteChanged event did not execute as expected.");
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSelectedConnectionLocalhostLabel()
        {
            //arrange
            var mainViewModelMock = new Mock<IShellViewModel>();
            CustomContainer.Register(mainViewModelMock.Object);
            var newSelectedConnection = new Mock<IServer>();
            var newSelectedConnectionEnvironmentId = Guid.NewGuid();
            newSelectedConnection.SetupGet(it => it.DisplayName).Returns("localhost");
            newSelectedConnection.SetupGet(it => it.EnvironmentID).Returns(newSelectedConnectionEnvironmentId);
            newSelectedConnection.SetupGet(it => it.HasLoaded).Returns(true);
            newSelectedConnection.SetupGet(it => it.IsConnected).Returns(true);

            var mockEnvironmentConnection = SetupMockConnection();
            newSelectedConnection.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);

            _changedProperties.Clear();
            var isSelectedEnvironmentChangedRaised = false;
            _target.SelectedEnvironmentChanged += (sender, args) =>
                {
                    isSelectedEnvironmentChangedRaised = sender == _target && args == _serverEnvironmentId;
                };
            var isCanExecuteChangedRaised = false;
            _target.EditConnectionCommand.CanExecuteChanged += (sender, args) =>
                {
                    isCanExecuteChangedRaised = true;
                };

            //act
            _target.SelectedConnection = newSelectedConnection.Object;

            //assert
            mainViewModelMock.Verify(it => it.SetActiveServer(newSelectedConnectionEnvironmentId));
            NUnit.Framework.Assert.IsTrue(_target.SelectedConnection.IsConnected);
            NUnit.Framework.Assert.IsTrue(_changedProperties.Contains("SelectedConnection"));
            NUnit.Framework.Assert.IsFalse(isSelectedEnvironmentChangedRaised);
            NUnit.Framework.Assert.IsTrue(isCanExecuteChangedRaised, "Selecting a new target server in the deploy did not raise event 'Can execute changed' on edit connection command.");
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestIsLoading()
        {
            //arrange
            _changedProperties.Clear();
            const bool ExpectedValue = true;

            //act
            _target.IsLoading = ExpectedValue;
            var value = _target.IsLoading;

            //assert
            NUnit.Framework.Assert.AreEqual(ExpectedValue, value);
            NUnit.Framework.Assert.IsTrue(_changedProperties.Contains("IsLoading"));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestToggleConnectionToolTip()
        {
            //assert
            NUnit.Framework.Assert.IsTrue(!string.IsNullOrEmpty(_target.NewConnectionToolTip));

        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestEditConnectionToolTip()
        {
            //assert
            NUnit.Framework.Assert.IsTrue(!string.IsNullOrEmpty(_target.EditConnectionToolTip));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestConnectionsToolTip()
        {
            //assert
            NUnit.Framework.Assert.IsTrue(!string.IsNullOrEmpty(_target.ConnectionsToolTip));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_TestUpdateHelpDescriptor()
        {
            //arrange
            var mainViewModelMock = new Mock<IShellViewModel>();
            var helpViewModelMock = new Mock<IHelpWindowViewModel>();
            mainViewModelMock.SetupGet(it => it.HelpViewModel).Returns(helpViewModelMock.Object);
            CustomContainer.Register(mainViewModelMock.Object);
            const string HelpText = "someText";

            //act
            _target.UpdateHelpDescriptor(HelpText);

            //assert
            helpViewModelMock.Verify(it => it.UpdateHelpText(HelpText));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_TestConnectNullArgument()
        {
            //act
            var result = _target.TryConnectAsync(null);
            result.Wait();

            //assert
            NUnit.Framework.Assert.IsFalse(result.Result);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_TestConnectException()
        {
            //arrange
            var serverMock = new Mock<IServer>();
            serverMock.Setup(it => it.ConnectAsync()).ThrowsAsync(new Exception());

            //act
            var result = _target.TryConnectAsync(serverMock.Object);
            result.Wait();

            //assert
            NUnit.Framework.Assert.IsFalse(result.Result);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sipohamandla Dube")]
        public void ConnectControlViewModel_TestConnectSuccessful()
        {
            //arrange
            var mockServer = new Mock<IServer>();
            var mockShellViewModel = new Mock<IShellViewModel>();
            var mockEventAggregator = new Mock<IEventAggregator>();
            var mockPopupController = new Mock<IPopupController>();
            var mockEnvironmentConnection = new Mock<IEnvironmentConnection>();

            var serverEnvironmentId = Guid.NewGuid();
            var serverConnectedRaised = false;
            var rand = new Random();

            mockEnvironmentConnection.Setup(o => o.IsConnected).Returns(true);
            mockEnvironmentConnection.Setup(o => o.AppServerUri).Returns((new Uri($"http://127.0.0.{rand.Next(1, 100)}:{rand.Next(1, 100)}/dsf")));

            mockServer.Setup(it => it.ConnectAsync()).ReturnsAsync(true);
            mockServer.SetupGet(it => it.EnvironmentID).Returns(serverEnvironmentId);
            mockServer.SetupGet(it => it.DisplayName).Returns("display name");
            mockServer.SetupGet(it => it.IsConnected).Returns(true);
            mockServer.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);
            mockServer.SetupGet(o => o.Connection.AppServerUri).Returns((new Uri($"http://127.0.0.{rand.Next(1, 100)}:{rand.Next(1, 100)}/dsf")));

            var connectControlViewModel = new ConnectControlViewModel(mockServer.Object, mockEventAggregator.Object) { ShouldUpdateActiveEnvironment = true };

            connectControlViewModel.ServerConnected = (sender, e) =>
            {
                serverConnectedRaised = sender == connectControlViewModel && Equals(e, mockServer.Object);
            };

            CustomContainer.Register(mockShellViewModel.Object);

            mockPopupController.Setup(it => it.ShowConnectionTimeoutConfirmation("DisplayName"))
                .Returns<string>(
                    dispName =>
                    {
                        mockServer.Setup(it => it.ConnectAsync()).ReturnsAsync(true);
                        return MessageBoxResult.Yes;
                    });

            CustomContainer.Register(mockPopupController.Object);
            //act
            connectControlViewModel.SelectedConnection = new Server(serverEnvironmentId, mockEnvironmentConnection.Object);
            var result = connectControlViewModel.TryConnectAsync(mockServer.Object);

            //assert
            NUnit.Framework.Assert.IsTrue(result.Result);
            NUnit.Framework.Assert.IsTrue(serverConnectedRaised);
            mockServer.Verify(it => it.ConnectAsync());
            mockShellViewModel.Verify(it => it.SetActiveServer(mockServer.Object.EnvironmentID));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_TestConnectUnsuccessful()
        {
            //arrange
            var serverMock = new Mock<IServer>();
            serverMock.Setup(it => it.ConnectAsync()).ReturnsAsync(false);
            var serverEnvironmentId = Guid.NewGuid();
            serverMock.SetupGet(it => it.EnvironmentID).Returns(serverEnvironmentId);
            serverMock.SetupGet(it => it.DisplayName).Returns("DisplayName");
            _changedProperties.Clear();
            var serverConnectedRaised = false;
            _target.ServerConnected = (sender, e) =>
            {
                serverConnectedRaised = sender == _target && Equals(e, serverMock.Object);
            };
            var mainViewModelMock = new Mock<IShellViewModel>();
            CustomContainer.Register(mainViewModelMock.Object);
            var popupControllerMock = new Mock<IPopupController>();
            popupControllerMock.Setup(it => it.ShowConnectionTimeoutConfirmation("DisplayName"))
                .Returns<string>(
                    dispName =>
                    {
                        serverMock.Setup(it => it.ConnectAsync()).ReturnsAsync(true);
                        return MessageBoxResult.Yes;
                    });
            CustomContainer.Register(popupControllerMock.Object);

            //act
            var result = _target.TryConnectAsync(serverMock.Object);
            result.Wait();

            //assert
            NUnit.Framework.Assert.IsFalse(result.Result);
            NUnit.Framework.Assert.IsFalse(serverConnectedRaised);
        }
        
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_TestOnServerOnNetworkStateChanged()
        {
            //arrange
            var serverConnectionMock = new Mock<IServer>();
            var serverConnectionEnvironmentId = Guid.NewGuid();
            var serverArg = new Mock<IServer>();
            var serverDisconnectedRaised = false;
            serverArg.SetupGet(it => it.IsConnected).Returns(false);
            serverArg.SetupGet(it => it.EnvironmentID).Returns(serverConnectionEnvironmentId);
            serverConnectionMock.SetupGet(it => it.EnvironmentID).Returns(serverConnectionEnvironmentId);
            serverConnectionMock.SetupGet(server => server.EnvironmentID).Returns(Guid.NewGuid);
            serverConnectionMock.SetupGet(it => it.DisplayName).Returns("someName");
            serverConnectionMock.SetupGet(it => it.DisplayName).Returns("My display name(Connected)");

            var mockEnvironmentConnection = SetupMockConnection();
            serverConnectionMock.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);

            _target.ServerHasDisconnected = (obj, arg) => { serverDisconnectedRaised = obj == _target && Equals(arg, serverArg.Object); };

            _updateRepositoryMock.SetupGet(manager => manager.ServerSaved).Returns(new Mock<Action<Guid, bool>>().Object);
            _updateRepositoryMock.Object.ServerSaved.Invoke(serverConnectionMock.Object.EnvironmentID, false);
            var argsMock = new Mock<INetworkStateChangedEventArgs>();
            argsMock.SetupGet(it => it.State).Returns(ConnectionNetworkState.Disconnected);
            _target.SelectedConnection = serverConnectionMock.Object;
            _changedProperties.Clear();

            //act
            serverConnectionMock.Raise(it => it.NetworkStateChanged += null, argsMock.Object, serverArg.Object);

            //assert
            NUnit.Framework.Assert.IsFalse(_target.Servers.Contains(serverConnectionMock.Object));
            NUnit.Framework.Assert.IsFalse(_target.IsConnected);
            NUnit.Framework.Assert.IsNotNull(_target.ServerHasDisconnected);            
            NUnit.Framework.Assert.IsFalse(serverDisconnectedRaised);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_TestOnServerOnNetworkStateChangedConnected()
        {
            //arrange
            var serverConnectionMock = new Mock<IServer>();
            var serverArg = new Mock<IServer>();
            var serverReconnectedRaised = false;
            serverConnectionMock.SetupGet(it => it.DisplayName).Returns("someName");
            serverConnectionMock.SetupGet(server => server.EnvironmentID).Returns(Guid.NewGuid);

            var mockEnvironmentConnection = SetupMockConnection();
            serverConnectionMock.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);

            _target.ServerReConnected = (obj, arg) => { serverReconnectedRaised = obj == _target && Equals(arg, serverArg.Object); };
            _updateRepositoryMock.SetupGet(manager => manager.ServerSaved).Returns(new Mock<Action<Guid, bool>>().Object);
            _updateRepositoryMock.Object.ServerSaved.Invoke(serverConnectionMock.Object.EnvironmentID, false);
            var argsMock = new Mock<INetworkStateChangedEventArgs>();
            argsMock.SetupGet(it => it.State).Returns(ConnectionNetworkState.Connected);
            _target.SelectedConnection = serverConnectionMock.Object;
            _changedProperties.Clear();

            //act
            serverConnectionMock.Raise(it => it.NetworkStateChanged += null, argsMock.Object, serverArg.Object);

            //assert
            NUnit.Framework.Assert.IsNotNull(_target.ServerReConnected);
            NUnit.Framework.Assert.IsFalse(serverReconnectedRaised);
        }
        
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_TestEditSelectedConnectionShouldSetSelectedConnection()
        {
            //arrange
            var serverConnectionMock = new Mock<IServer>();
            var serverConnectionEnvironmentId = Guid.NewGuid();
            serverConnectionMock.SetupGet(it => it.EnvironmentID).Returns(serverConnectionEnvironmentId);

            _updateRepositoryMock.SetupGet(manager => manager.ServerSaved).Returns(new Mock<Action<Guid, bool>>().Object);
            _updateRepositoryMock.Object.ServerSaved.Invoke(serverConnectionMock.Object.EnvironmentID, false);
            var server1Mock = new Mock<IServer>();
            server1Mock.SetupGet(it => it.EnvironmentID).Returns(serverConnectionEnvironmentId);
            server1Mock.SetupGet(it => it.DisplayName).Returns("server1MockDisplayName");
            var newEnvironmentID = Guid.NewGuid();
            var server2Mock = new Mock<IServer>();
            server2Mock.SetupGet(it => it.EnvironmentID).Returns(newEnvironmentID);
            server1Mock.SetupGet(it => it.DisplayName).Returns("server2MockDisplayName");

            var mockEnvironmentConnection = SetupMockConnection();
            server2Mock.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);

            _target.SelectedConnection = server2Mock.Object;
            //act
            _target.EditConnectionCommand.Execute(null);

            //assert
            NUnit.Framework.Assert.IsNotNull(_target.SelectedConnection);
        }
        
        private static Mock<IEnvironmentConnection> SetupMockConnection()
        {
            var uri = new Uri("http://bravo.com/");
            var mockEnvironmentConnection = new Mock<IEnvironmentConnection>();
            mockEnvironmentConnection.Setup(a => a.AppServerUri).Returns(uri);
            mockEnvironmentConnection.Setup(a => a.AuthenticationType).Returns(Dev2.Runtime.ServiceModel.Data.AuthenticationType.Public);
            mockEnvironmentConnection.Setup(a => a.WebServerUri).Returns(uri);
            mockEnvironmentConnection.Setup(a => a.ID).Returns(Guid.Empty);
            return mockEnvironmentConnection;
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Pieter Terblanche")]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_EditServerServerIDMatchIsTrue()
        {
            var serverGuid = Guid.NewGuid();
            var uri = new Uri("http://bravo.com/");

            var mockShellViewModel = new Mock<IShellViewModel>();
            var mockExplorerRepository = new Mock<IExplorerRepository>();
            var mockEnvironmentConnection = new Mock<IEnvironmentConnection>();

            mockEnvironmentConnection.Setup(a => a.AppServerUri).Returns(uri);
            mockEnvironmentConnection.Setup(a => a.UserName).Returns("johnny");
            mockEnvironmentConnection.Setup(a => a.Password).Returns("bravo");
            mockEnvironmentConnection.Setup(a => a.WebServerUri).Returns(uri);
            mockEnvironmentConnection.Setup(a => a.ID).Returns(serverGuid);

            mockExplorerRepository.Setup(
                repository => repository.CreateFolder(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));
            mockExplorerRepository.Setup(
                repository => repository.Rename(It.IsAny<IExplorerItemViewModel>(), It.IsAny<string>())).Returns(true);

            var server = new ServerForTesting(mockExplorerRepository);
            var server2 = new Server(serverGuid, mockEnvironmentConnection.Object);
            server.EnvironmentID = serverGuid;
            server.ResourceName = "mr_J_bravo";
            server.Connection = mockEnvironmentConnection.Object;
            mockShellViewModel.Setup(a => a.ActiveServer).Returns(server);
            mockShellViewModel.Setup(model => model.LocalhostServer).Returns(server);

            CustomContainer.Register<IServer>(server);
            CustomContainer.Register(mockShellViewModel.Object);

            var environmentModel = new Mock<IServer>();
            environmentModel.SetupGet(a => a.Connection).Returns(mockEnvironmentConnection.Object);
            environmentModel.SetupGet(a => a.IsConnected).Returns(true);

            var e1 = new Server(serverGuid, mockEnvironmentConnection.Object);
            var repo = new TestServerRespository(environmentModel.Object, e1) { ActiveServer = e1 };
            var environmentRepository = new ServerRepository(repo);
            NUnit.Framework.Assert.IsNotNull(environmentRepository);

            var passed = false;
            mockShellViewModel.Setup(a => a.OpenResource(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<IServer>()))
                .Callback((Guid id1,Guid id2,  IServer a) =>
                {
                    passed = a.EnvironmentID == serverGuid;
                });
            //------------Setup for test--------------------------
            var connectControlViewModel = new ConnectControlViewModel(server, new EventAggregator());
            var p = new PrivateObject(connectControlViewModel);
            p.SetField("_selectedConnection", server2);
            //------------Execute Test---------------------------
            connectControlViewModel.Edit();

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(passed);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_CheckVersionConflict_GivenConnectedServer_ResultServerIsConnecting()
        {
            //------------Setup for test--------------------------
            _serverMock.Setup(server1 => server1.IsConnected).Returns(true);
            _serverMock.Setup(server1 => server1.GetServerVersion()).Returns("1.0.0.0");
            var popupController = new Mock<IPopupController>();
            popupController.Setup(controller => controller.ShowConnectServerVersionConflict(It.IsAny<string>(), It.IsAny<string>()));
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator(), popupController.Object);
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(connectControlViewModel);
            privateObject.Invoke("CheckVersionConflictAsync");
            //------------Assert Results-------------------------
            popupController.Verify(controller => controller.ShowConnectServerVersionConflict(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        public void ConnectControlViewModel_CheckVersionConflict_ThrowsException()
        {
            _serverMock.Setup(server1 => server1.IsConnected).Returns(true);
            _serverMock.Setup(server1 => server1.GetServerVersion()).Throws(new Exception());
            var popupController = new Mock<IPopupController>();
            popupController.Setup(controller => controller.ShowConnectServerVersionConflict(It.IsAny<string>(), It.IsAny<string>()));
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator(), popupController.Object);
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(connectControlViewModel);
            privateObject.Invoke("CheckVersionConflictAsync");
            //------------Assert Results-------------------------
        }
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_CheckVersionConflict_GivenNoVersionConflicts()
        {
            //------------Setup for test--------------------------
            _serverMock.Setup(server1 => server1.IsConnected).Returns(true);
            _serverMock.Setup(server1 => server1.GetServerVersion()).Returns("0.0.0.5");
            var popupController = new Mock<IPopupController>();
            popupController.Setup(controller => controller.ShowConnectServerVersionConflict(It.IsAny<string>(), It.IsAny<string>()));
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator(), popupController.Object);
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(connectControlViewModel);
            privateObject.Invoke("CheckVersionConflictAsync");
            //------------Assert Results-------------------------
            popupController.Verify(controller => controller.ShowConnectServerVersionConflict(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_ConnectOrDisconnect_GivenServerNull()
        {
            //------------Setup for test--------------------------
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator());
            connectControlViewModel.SelectedConnection = null;
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(_target);
            privateObject.SetField("_selectedConnection", value:null);
            privateObject.Invoke("ConnectOrDisconnectAsync");
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(_target.IsConnecting);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_ConnectOrDisconnect_GivenServerIsConnectAndServerHasNotLoaded()
        {
            //------------Setup for test--------------------------
            _serverMock.Setup(server1 => server1.IsConnected).Returns(true);
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(_target);
            privateObject.Invoke("ConnectOrDisconnectAsync");
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(_target.IsConnecting);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_ConnectOrDisconnect_GivenServerIsConnectAndServerHasLoaded()
        {
            //------------Setup for test--------------------------
            _serverMock.Setup(server1 => server1.IsConnected).Returns(true);
            _serverMock.Setup(server1 => server1.HasLoaded).Returns(true);
            _target.ServerDisconnected = (obj, arg) => { };
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(_target);
            privateObject.Invoke("ConnectOrDisconnectAsync");
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(_target.ServerDisconnected);
            NUnit.Framework.Assert.IsFalse(_target.IsConnecting);
            NUnit.Framework.Assert.IsFalse(_target.IsConnected);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_Connect_GivenShowConnectionTimeoutConfirmation_MessageBox_Yes()
        {
            //------------Setup for test--------------------------
            var popupController = new Mock<IPopupController>();
            popupController.SetupSequence(controller => controller.ShowConnectionTimeoutConfirmation(_serverMock.Object.DisplayName))
                .Returns(MessageBoxResult.Yes)                
                .Returns(MessageBoxResult.None);
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator(), popupController.Object);
            //------------Execute Test---------------------------
            var result = connectControlViewModel.TryConnectAsync(_serverMock.Object);
            result.Wait();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(result.Result);
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_LoadServers_GivenSelectedServer_ResultIsSelectedServer()
        {
            //------------Setup for test--------------------------
            _serverMock.Setup(server => server.IsConnected).Returns(true);
            var store = new Mock<IServer>();
            store.Setup(server => server.DisplayName).Returns("WarewolfStore");

            var mockEnvironmentConnection = SetupMockConnection();

            var intergration = new Mock<IServer>();
            intergration.Setup(server => server.DisplayName).Returns("RemoteIntergration");
            var intergrationId = Guid.NewGuid();
            intergration.Setup(server => server.EnvironmentID).Returns(intergrationId);
            intergration.SetupGet(it => it.Connection).Returns(mockEnvironmentConnection.Object);
            
            
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator());
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(connectControlViewModel);
            privateObject.SetField("_selectedId", intergrationId);
            connectControlViewModel.LoadServers();
            connectControlViewModel.SelectedConnection = intergration.Object;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(intergrationId, connectControlViewModel.SelectedConnection.EnvironmentID);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_OnServerOnNetworkStateChanged_GivenLocalhostAndIsNotConnecting_ResultServerDisconnected()
        {
            //------------Setup for test--------------------------
            _serverMock.Setup(server => server.IsConnected).Returns(false);
            var args = new Mock<INetworkStateChangedEventArgs>();
            args.Setup(eventArgs => eventArgs.State).Returns(ConnectionNetworkState.Disconnected);
            var localhost = new Mock<IServer>();
            localhost.Setup(server => server.IsConnected).Returns(false);
            localhost.Setup(server => server.DisplayName).Returns("localhost (Connected)");
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator());
            //------------Execute Test---------------------------
            var popupController = new Mock<IPopupController>();
            popupController.Setup(
                controller =>
                    controller.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButton.OK, MessageBoxImage.Warning,
                        It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(),
                        It.IsAny<bool>(), It.IsAny<bool>()));
            var privateObject = new PrivateObject(connectControlViewModel);
            privateObject.SetProperty("IsConnecting", false);
            privateObject.Invoke("OnServerOnNetworkStateChanged", args.Object, _serverMock.Object);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(connectControlViewModel.IsConnected);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_UpdateRepositoryOnServerSaved_GivenEmptyGuid_Result()
        {
            //------------Setup for test--------------------------
            _serverMock.Setup(server => server.IsConnected).Returns(true);
            var args = new Mock<INetworkStateChangedEventArgs>();
            args.Setup(eventArgs => eventArgs.State).Returns(ConnectionNetworkState.Disconnected);
            var localhost = new Mock<IServer>();
            localhost.Setup(server => server.IsConnected).Returns(false);
            localhost.Setup(server => server.EnvironmentID).Returns(new Guid());
            localhost.Setup(server => server.DisplayName).Returns("localhost (Connected)");
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator());
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(connectControlViewModel);
            privateObject.SetProperty("IsConnecting", false);
            privateObject.Invoke("UpdateRepositoryOnServerSaved", Guid.Empty, false);
            //------------Assert Results-------------------------
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Sanele Mthembu")]
        public void ConnectControlViewModel_UpdateRepositoryOnServerSaved_GivenLocalhost_Result()
        {            
            //------------Setup for test--------------------------
            var store = new Mock<IServer>();
            store.Setup(server => server.DisplayName).Returns("WarewolfStore");
            var intergration = new Mock<IServer>();
            intergration.Setup(server => server.DisplayName).Returns("RemoteIntergration");
            intergration.Setup(server => server.IsConnected).Returns(true);
            var intergrationId = Guid.NewGuid();
            intergration.Setup(server => server.EnvironmentID).Returns(intergrationId);
            
            var connectControlViewModel = new ConnectControlViewModel(_serverMock.Object, new EventAggregator());
            //------------Execute Test---------------------------
            var privateObject = new PrivateObject(connectControlViewModel);
            privateObject.SetProperty("IsConnecting", false);
            privateObject.Invoke("UpdateRepositoryOnServerSaved", intergrationId, false);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(connectControlViewModel.SelectedConnection);
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Category(nameof(ConnectControlViewModel))]
        [Author("Pieter Terblanche")]
        public void ConnectControlViewModel_UpdateRepositoryOnServerSaved()
        {
            var serverGuid = Guid.NewGuid();
            var uri = new Uri("http://bravo.com/");
            var serverDisplayName = "johnnyBravoServer";

            var mockShellViewModel = new Mock<IShellViewModel>();
            var mockExplorerRepository = new Mock<IExplorerRepository>();
            var mockEnvironmentConnection = new Mock<IEnvironmentConnection>();

            mockEnvironmentConnection.Setup(a => a.AppServerUri).Returns(uri);
            mockEnvironmentConnection.Setup(a => a.UserName).Returns("johnny");
            mockEnvironmentConnection.Setup(a => a.Password).Returns("bravo");
            mockEnvironmentConnection.Setup(a => a.WebServerUri).Returns(uri);
            mockEnvironmentConnection.Setup(a => a.ID).Returns(serverGuid);
            mockEnvironmentConnection.Setup(a => a.IsConnected).Returns(false);
            mockEnvironmentConnection.SetupProperty(a => a.DisplayName);
            mockEnvironmentConnection.Object.DisplayName = serverDisplayName;

            mockExplorerRepository.Setup(
                repository => repository.CreateFolder(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()));
            mockExplorerRepository.Setup(
                repository => repository.Rename(It.IsAny<IExplorerItemViewModel>(), It.IsAny<string>())).Returns(true);

            var server = new ServerForTesting(mockExplorerRepository);
            server.EnvironmentID = serverGuid;
            server.ResourceName = "mr_J_bravo";
            server.Connection = mockEnvironmentConnection.Object;
            mockShellViewModel.Setup(a => a.ActiveServer).Returns(server);
            mockShellViewModel.Setup(model => model.LocalhostServer).Returns(server);

            CustomContainer.Register<IServer>(server);
            CustomContainer.Register(mockShellViewModel.Object);

            var environmentModel = new Mock<IServer>();
            environmentModel.SetupGet(a => a.Connection).Returns(mockEnvironmentConnection.Object);
            environmentModel.SetupGet(a => a.IsConnected).Returns(true);

            var e1 = new Server(serverGuid, mockEnvironmentConnection.Object);
            var repo = new TestServerRespository(environmentModel.Object, e1) { ActiveServer = e1 };
            var environmentRepository = new ServerRepository(repo);
            NUnit.Framework.Assert.IsNotNull(environmentRepository);

            var passed = false;
            mockShellViewModel.Setup(a => a.OpenResource(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<IServer>()))
                .Callback((Guid id1, Guid id2, IServer a) =>
                {
                    passed = a.EnvironmentID == serverGuid;
                });
            //------------Setup for test--------------------------
            var connectControlViewModel = new ConnectControlViewModel(server, new EventAggregator());
            var privateObject = new PrivateObject(connectControlViewModel);
            privateObject.SetProperty("IsConnecting", false);
            privateObject.Invoke("UpdateRepositoryOnServerSaved", serverGuid, false);
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual("johnnyBravoServer", mockEnvironmentConnection.Object.DisplayName);
        }

        void Target_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _changedProperties.Add(e.PropertyName);
        }
    }
}
