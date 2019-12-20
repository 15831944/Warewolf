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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Dev2;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Infrastructure;
using Dev2.Common.Interfaces.PopupController;
using Dev2.Common.Interfaces.Security;
using Dev2.Common.Interfaces.Threading;
using Dev2.Common.Interfaces.Versioning;
using Dev2.Studio.Interfaces;
using Dev2.Threading;
using NUnit.Framework;
using Moq;
using IPopupController = Dev2.Common.Interfaces.Studio.Controller.IPopupController;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class ExplorerItemViewModelTests
    {
        #region Fields

        ExplorerItemViewModel _target;
        Mock<IServer> _serverMock;
        Mock<IStudioUpdateManager> _updateManager;
        Mock<IExplorerTreeItem> _explorerTreeItemMock;
        Mock<IShellViewModel> _shellViewModelMock;
        Mock<IExplorerRepository> _explorerRepositoryMock;
        Mock<IPopupController> _popupControllerMock;
        Mock<IWindowsGroupPermission> _windowsGroupPermissionsMock;
        Mock<IExplorerTooltips> _explorerTooltips;

        #endregion Fields

        #region Test initialize

        [SetUp]
        public void TestInitialize()
        {
            _serverMock = new Mock<IServer>();
            _updateManager = new Mock<IStudioUpdateManager>();
            _serverMock.Setup(a => a.UpdateRepository).Returns(_updateManager.Object);

            _explorerTreeItemMock = new Mock<IExplorerTreeItem>();
            _shellViewModelMock = new Mock<IShellViewModel>();
            _explorerRepositoryMock = new Mock<IExplorerRepository>();
            _windowsGroupPermissionsMock = new Mock<IWindowsGroupPermission>();
            _serverMock.SetupGet(it => it.ExplorerRepository).Returns(_explorerRepositoryMock.Object);
            _serverMock.SetupGet(it => it.Permissions)
                .Returns(new List<IWindowsGroupPermission> { _windowsGroupPermissionsMock.Object });
            _popupControllerMock = new Mock<IPopupController>();
            _explorerTooltips = new Mock<IExplorerTooltips>();
            CustomContainer.Register(_explorerTooltips.Object);
            _target = new ExplorerItemViewModel(_serverMock.Object, _explorerTreeItemMock.Object,
                a => { }, _shellViewModelMock.Object, _popupControllerMock.Object);
        }

        #endregion Test initialize

        #region Test commands

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestRollbackCommand()
        {
            _target.ResourceId = Guid.NewGuid();
            _target.VersionNumber = Guid.NewGuid().ToString();
            var outputDisplayName = Guid.NewGuid().ToString();
            var rollbackResultMock = new Mock<IRollbackResult>();
            _explorerRepositoryMock.Setup(it => it.Rollback(_target.ResourceId, _target.VersionNumber)).Returns(rollbackResultMock.Object);
            rollbackResultMock.SetupGet(it => it.DisplayName).Returns(outputDisplayName);

            _popupControllerMock.Setup(it => it.ShowRollbackVersionMessage(It.IsAny<string>())).Returns(MessageBoxResult.Yes);

            //act
            _target.RollbackCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.RollbackCommand.CanExecute(null));

            //assert
            _explorerTreeItemMock.VerifySet(it => it.AreVersionsVisible = true);
            _explorerTreeItemMock.VerifySet(it => it.ResourceName = outputDisplayName);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeployCommand()
        {
            //arrange
            //act
            _target.DeployCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DeployCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(
                it =>
                    it.AddDeploySurface(
                        It.Is<IEnumerable<IExplorerTreeItem>>(
                            x => x.All(xitem => (_target.AsList().Union(new[] { _target })).Contains(xitem)))));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestLostFocusCommand()
        {
            //arrange
            _target.IsRenaming = true;

            //act
            _target.LostFocus.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.LostFocus.CanExecute(null));

            //assert
            NUnit.Framework.Assert.IsFalse(_target.IsRenaming);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewServerCommand()
        {
            //arrange
            _target.ResourceType = "ServerSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewServerCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewServerCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewServerSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewSqlServerSourceCommand()
        {
            //arrange
            _target.ResourceType = "DbSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewSqlServerSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewSqlServerSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewSqlServerSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewMySqlSourceCommand()
        {
            //arrange
            _target.ResourceType = "DbSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewMySqlSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewMySqlSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewMySqlSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestIsMergeVisibleFalse()
        {
            //assert
            NUnit.Framework.Assert.IsFalse(_target.IsMergeVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestIsMergeVisibleTrue()
        {
            _target.IsSaveDialog = false;
            _target.IsMergeVisible = true;
            var id1 = Guid.NewGuid();
            var v1 = new Mock<IVersionInfo>();
            v1.SetupAllProperties();
            v1.Setup(info => info.Reason).Returns("a");
            v1.Setup(info => info.ResourceId).Returns(id1);
            var versionInfos = new List<IVersionInfo>()
            {
                v1.Object
            };
            _serverMock.Setup(server => server.GetPermissions(Guid.Empty)).Returns(Permissions.View | Permissions.DeployTo);
            var explorerRepositoryMock = new Mock<IExplorerRepository>();
            explorerRepositoryMock.Setup(it => it.GetVersions(It.IsAny<Guid>())).Returns(versionInfos);
            _serverMock.SetupGet(it => it.ExplorerRepository).Returns(explorerRepositoryMock.Object);
            _explorerRepositoryMock.Setup(it => it.GetVersions(It.IsAny<Guid>())).Returns(versionInfos);
            _target.ShowVersionHistory.Execute(_target);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(_target.ShowVersionHistory.CanExecute(null));
            NUnit.Framework.Assert.IsTrue(_target.AreVersionsVisible);
            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsMergeVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewPostgreSqlSourceCommand()
        {
            //arrange
            _target.ResourceType = "DbSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewPostgreSqlSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewPostgreSqlSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewPostgreSqlSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewOracleSourceCommand()
        {
            //arrange
            _target.ResourceType = "DbSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewOracleSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewOracleSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewOracleSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewOdbcSourceCommand()
        {
            //arrange
            _target.ResourceType = "DbSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewOdbcSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewOdbcSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewOdbcSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewPluginSourceCommand()
        {
            //arrange
            _target.ResourceType = "PluginSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewPluginSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewPluginSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewPluginSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewWebSourceCommand()
        {
            //arrange
            _target.ResourceType = "WebSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewWebSourceSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewWebSourceSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewWebSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewRedisSourceCommand()
        {
            //arrange
            _target.ResourceType = "RedisSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewRedisSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewRedisSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewRedisSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewEmailSourceCommand()
        {
            //arrange
            _target.ResourceType = "EmailSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewEmailSourceSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewEmailSourceSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewEmailSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewExchangeSourceCommand()
        {
            //arrange
            _target.ResourceType = "ExchangeSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewExchangeSourceSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewExchangeSourceSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewExchangeSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewRabbitMqSourceCommand()
        {
            //arrange
            _target.ResourceType = "RabbitMqSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewRabbitMqSourceSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewRabbitMqSourceSourceCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewRabbitMQSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCreateNewTestCommand()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.ResourceId = Guid.NewGuid();
            _target.IsService = true;
            _target.CanCreateTest = true;
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.CreateTestCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.CreateTestCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.CreateTest(_target.ResourceId));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewSharepointSourceCommand()
        {
            //arrange
            _target.ResourceType = "SharepointSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewSharepointSourceSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewServerCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewSharepointSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewDropboxSourceCommand()
        {
            //arrange
            _target.ResourceType = "DropboxSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewDropboxSourceSourceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewServerCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewDropboxSource(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNewServiceCommand()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.NewServiceCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewServerCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.NewService(_target.ResourcePath));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestContextMenuDebugCommand()
        {
            //arrange
            _target.ResourceId = Guid.NewGuid();

            //act
            _target.DebugCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DebugCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.OpenResource(_target.ResourceId, _target.Server.EnvironmentID, _target.Server));
            _shellViewModelMock.Verify(it => it.Debug());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDebugStudioCommand()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.DebugStudioCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DebugStudioCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.StudioDebug(_target.ResourceId, _target.Server));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void DebugBrowserCommand()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.DebugBrowserCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DebugBrowserCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.BrowserDebug(_target.ResourceId, _target.Server));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void ScheduleCommand()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.ScheduleCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.ScheduleCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.NewSchedule(_target.ResourceId));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void ExplorerItemViewModel_QueueEventCommand()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.QueueEventCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.QueueEventCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.NewQueueEvent(_target.ResourceId));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void RunAllTestsCommand()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.RunAllTestsCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.RunAllTestsCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.RunAllTests(null, _target.ResourceId));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestViewSwaggerCommand()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.ViewSwaggerCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewServerCommand.CanExecute(null));

            //assert

            _shellViewModelMock.Verify(it => it.ViewSwagger(_target.ResourceId, _target.Server));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestApisJsonCommand()
        {
            //arrange
            _target.ResourceType = "Folder";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());
            var connection = new Mock<IEnvironmentConnection>();
            connection.SetupGet(it => it.ID).Returns(Guid.NewGuid());
            //act
            var mock = new Mock<IServer>();
            mock.SetupGet(it => it.Connection).Returns(connection.Object);
            mock.SetupGet(it => it.Connection.WebServerUri).Returns(new Uri("http://localhost:3142"));
            _target.Server = mock.Object;
            _target.ViewApisJsonCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.NewServerCommand.CanExecute(null));

            //assert

            _shellViewModelMock.Verify(it => it.ViewApisJson(_target.ResourcePath, new Uri("http://localhost:3142")));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestOpenCommand()
        {
            //arrange
            _target.ResourceType = "DbSource";
            _target.ResourceId = Guid.NewGuid();
            _serverMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());

            //act
            _target.OpenCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.OpenCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.SetActiveServer(_target.Server.EnvironmentID));
            _shellViewModelMock.Verify(it => it.OpenResource(_target.ResourceId, _target.Server.EnvironmentID, _target.Server));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDebugCommand()
        {
            //arrange
            _target.ResourceId = Guid.NewGuid();

            //act
            _target.DebugCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DebugCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.OpenResource(_target.ResourceId, _target.Server.EnvironmentID, _target.Server));
            _shellViewModelMock.Verify(it => it.Debug());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestRenameCommand()
        {
            //arrange
            _target.IsRenaming = false;

            //act
            _target.RenameCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.RenameCommand.CanExecute(null));

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsRenaming);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestShowDependenciesCommand()
        {
            //arrange
            _target.ResourceId = Guid.NewGuid();

            //act
            _target.ShowDependenciesCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.ShowDependenciesCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.ShowDependencies(_target.ResourceId, _target.Server, _target.IsSource));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestShowVersionHistory()
        {
            //arrange
            _target.AreVersionsVisible = false;
            var explorerRepositoryMock = new Mock<IExplorerRepository>();
            explorerRepositoryMock.Setup(it => it.GetVersions(It.IsAny<Guid>())).Returns(Enumerable.Empty<IVersionInfo>().ToList());
            _serverMock.SetupGet(it => it.ExplorerRepository).Returns(explorerRepositoryMock.Object);
            _explorerRepositoryMock.Setup(it => it.GetVersions(It.IsAny<Guid>())).Returns(new List<IVersionInfo>());

            //act
            _target.ShowVersionHistory.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.ShowVersionHistory.CanExecute(null));

            //Changed to False as there is now a count on the return of versions
            //assert
            NUnit.Framework.Assert.IsFalse(_target.AreVersionsVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        [Category("ExplorerItemViewModel_ShowVersionHistory")]
        public void ExplorerItemViewModel_ShowVersionHistory_HasChildren_SetPermissions()
        {
            //------------Setup for test--------------------------
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            var v1 = new Mock<IVersionInfo>();
            v1.SetupAllProperties();
            v1.Setup(info => info.Reason).Returns("a");
            v1.Setup(info => info.ResourceId).Returns(id1);
            var v2 = new Mock<IVersionInfo>();
            v2.SetupAllProperties();
            v2.Setup(info => info.Reason).Returns("a");
            v2.Setup(info => info.ResourceId).Returns(id2);
            var v3 = new Mock<IVersionInfo>();
            v3.SetupAllProperties();
            v3.Setup(info => info.Reason).Returns("a");
            v3.Setup(info => info.ResourceId).Returns(id3);
            var versionInfos = new List<IVersionInfo>()
            {
                v1.Object,
                v2.Object,
                v3.Object,
            };
            _serverMock.Setup(server => server.GetPermissions(Guid.Empty)).Returns(Permissions.View | Permissions.DeployTo);
            var explorerRepositoryMock = new Mock<IExplorerRepository>();
            explorerRepositoryMock.Setup(it => it.GetVersions(It.IsAny<Guid>())).Returns(versionInfos);
            _serverMock.SetupGet(it => it.ExplorerRepository).Returns(explorerRepositoryMock.Object);
            _explorerRepositoryMock.Setup(it => it.GetVersions(It.IsAny<Guid>())).Returns(versionInfos);
            //------------Execute Test---------------------------
           
             _target.ShowVersionHistory.Execute(_target);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(_target.ShowVersionHistory.CanExecute(null));
            NUnit.Framework.Assert.IsTrue(_target.AreVersionsVisible);
            _serverMock.Verify(server => server.GetPermissions(Guid.Empty), Times.Exactly(3));
            var canViewresource1 = _target.Children[0].CanView;
            var canViewresource2 = _target.Children[1].CanView;
            var canViewresource3 = _target.Children[2].CanView;
            NUnit.Framework.Assert.IsTrue(canViewresource1);
            NUnit.Framework.Assert.IsTrue(canViewresource2);
            NUnit.Framework.Assert.IsTrue(canViewresource3);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeleteCommandResourceTypeVersion()
        {
            //arrange
            _target.ResourceType = "Version";
            _target.IsResourceVersion = true;
            _target.ResourceName = Guid.NewGuid().ToString();
            _target.Server = new Mock<IServer>().Object;
            _explorerTreeItemMock.SetupGet(it => it.Children)
                .Returns(new ObservableCollection<IExplorerItemViewModel> { _target });

            _popupControllerMock.Setup(it => it.ShowDeleteVersionMessage(It.IsAny<string>())).Returns(MessageBoxResult.Yes);

            //act
            _target.DeleteCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DeleteCommand.CanExecute(null));

            //assert
            _explorerRepositoryMock.Verify(it => it.TryDelete(_target));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeleteCommandResourceTypeVersionUserDeclined()
        {
            //arrange
            _target.ResourceType = "Version";
            _target.IsResourceVersion = true;
            _popupControllerMock.Setup(it => it.ShowDeleteVersionMessage(It.IsAny<string>())).Returns(MessageBoxResult.No);

            //act
            _target.DeleteCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DeleteCommand.CanExecute(null));

            //assert
            _explorerRepositoryMock.Verify(it => it.TryDelete(It.IsAny<IExplorerItemViewModel>()), Times.Never);
            _explorerTreeItemMock.Verify(it => it.RemoveChild(_target), Times.Never);
            //assert
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeleteCommandResourceTypeServerSourceDeleteSuccess()
        {
            //arrange
            var environmentModelMock = new Mock<IServer>();
            environmentModelMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());
            _explorerRepositoryMock.Setup(it => it.TryDelete(_target)).Returns(new DeletedFileMetadata { IsDeleted = true });
            var studioManagerUpdateMock = new Mock<IStudioUpdateManager>();
            environmentModelMock.SetupGet(it => it.UpdateRepository).Returns(studioManagerUpdateMock.Object);
            _target.Server = environmentModelMock.Object;
            _target.ResourceType = "ServerSource";
            _target.ResourceId = Guid.NewGuid();
            _popupControllerMock.Setup(it => it.Show(It.IsAny<IPopupMessage>())).Returns(MessageBoxResult.Yes);

            //act
            NUnit.Framework.Assert.IsTrue(_target.DeleteCommand.CanExecute(null));
            _target.DeleteCommand.Execute(null);

            //assert
            _shellViewModelMock.Verify(it => it.CloseResource(_target.ResourceId, environmentModelMock.Object.EnvironmentID));
            _explorerRepositoryMock.Verify(it => it.TryDelete(_target));
            _explorerTreeItemMock.Verify(it => it.RemoveChild(_target));
            studioManagerUpdateMock.Verify(it => it.FireServerSaved(It.IsAny<Guid>(), It.IsAny<bool>()));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeleteCommandResourceTypeServerDeleteSuccess()
        {
            //arrange
            var environmentModelMock = new Mock<IServer>();
            environmentModelMock.SetupGet(it => it.EnvironmentID).Returns(Guid.NewGuid());
            _explorerRepositoryMock.Setup(it => it.TryDelete(_target)).Returns(new DeletedFileMetadata { IsDeleted = true });
            var studioManagerUpdateMock = new Mock<IStudioUpdateManager>();
            environmentModelMock.SetupGet(it => it.UpdateRepository).Returns(studioManagerUpdateMock.Object);
            _target.Server = environmentModelMock.Object;
            _target.ResourceType = "Server";
            _target.IsServer = true;
            _target.ResourceId = Guid.NewGuid();
            _popupControllerMock.Setup(it => it.Show(It.IsAny<IPopupMessage>())).Returns(MessageBoxResult.Yes);

            //act
            _target.DeleteCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DeleteCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.CloseResource(_target.ResourceId, environmentModelMock.Object.EnvironmentID));
            _explorerRepositoryMock.Verify(it => it.TryDelete(_target));
            _explorerTreeItemMock.Verify(it => it.RemoveChild(_target));
            studioManagerUpdateMock.Verify(it => it.FireServerSaved(It.IsAny<Guid>(), It.IsAny<bool>()));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestOpenVersionCommandResourceTypeVersion()
        {
            //arrange
            _target.ResourceType = "Version";
            _target.IsResourceVersion = true;
            _target.IsVersion = true;
            var versionInfoMock = new Mock<IVersionInfo>();
            _target.VersionInfo = versionInfoMock.Object;
            _explorerTreeItemMock.Setup(it => it.ResourceId).Returns(Guid.NewGuid());

            //act
            _target.OpenCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.OpenCommand.CanExecute(null));

            //assert
            _shellViewModelMock.Verify(it => it.OpenVersion(_target.Parent.ResourceId, _target.VersionInfo));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestExpandCommandResourceTypeFolderSingleClick()
        {
            //arrange
            _target.IsExpanded = false;
            _target.ResourceType = "Folder";

            //act
            _target.Expand.Execute(1);
            NUnit.Framework.Assert.IsTrue(_target.Expand.CanExecute(1));

            //assert
            NUnit.Framework.Assert.IsFalse(_target.IsExpanded);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestExpandCommandResourceTypeFolderDoubleClick()
        {
            //arrange
            _target.IsExpanded = false;
            _target.ResourceType = "Folder";
            _target.IsFolder = true;
            //act
            _target.Expand.Execute(2);
            NUnit.Framework.Assert.IsTrue(_target.Expand.CanExecute(2));

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsExpanded);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestExpandCommandResourceTypeWorkflowServiceSingleClick()
        {
            //arrange
            _target.IsExpanded = false;
            _target.ResourceType = "WorkflowService";

            //act
            _target.Expand.Execute(1);
            NUnit.Framework.Assert.IsTrue(_target.Expand.CanExecute(1));

            //assert
            NUnit.Framework.Assert.IsFalse(_target.IsExpanded);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestExpandCommandResourceTypeWorkflowServiceDoubleClick()
        {
            //arrange
            _target.IsExpanded = true;
            _target.ResourceType = "WorkflowService";

            //act
            _target.Expand.Execute(2);
            NUnit.Framework.Assert.IsTrue(_target.Expand.CanExecute(2));

            //assert
            NUnit.Framework.Assert.IsFalse(_target.IsExpanded);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCreateFolderCommandResourceTypeFolder()
        {
            _serverMock.Setup(server => server.UserPermissions)
                       .Returns(Permissions.Administrator);

            _serverMock.Setup(server => server.GetPermissions(It.IsAny<Guid>())).Returns(Permissions.Administrator);

            //arrange
            _target.IsExpanded = false;
            _target.ResourceType = "Folder";
            _target.IsFolder = true;
            _target.Children.Clear();
            _target.AllowResourceCheck = true;
            _target.IsResourceChecked = true;
            _target.IsResourceUnchecked = false;
            _target.IsFolderChecked = true;
            _target.CanCreateFolder = true;
            _target.CanCreateSource = true;
            _target.CanShowVersions = true;
            _target.CanRename = true;
            _target.CanDuplicate = true;
            _target.CanCreateTest = true;
            _target.CanDeploy = true;
            _target.CanShowDependencies = true;
            _target.ResourcePath = Guid.NewGuid().ToString();
            _target.CanCreateWorkflowService = true;
            _target.ShowContextMenu = true;

            //act
            _target.CreateFolderCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.CreateFolderCommand.CanExecute(null));

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsExpanded);
            var createdFolder = _target.Children.Single();
            NUnit.Framework.Assert.AreEqual("New Folder", createdFolder.ResourceName);
            NUnit.Framework.Assert.AreEqual("Folder", createdFolder.ResourceType);
            NUnit.Framework.Assert.AreEqual(_target.AllowResourceCheck, createdFolder.AllowResourceCheck);
            NUnit.Framework.Assert.AreEqual(_target.IsResourceChecked, createdFolder.IsResourceChecked);
            NUnit.Framework.Assert.AreEqual(_target.IsResourceUnchecked, createdFolder.IsResourceUnchecked);
            NUnit.Framework.Assert.AreEqual(_target.IsFolderChecked, createdFolder.IsFolderChecked);
            NUnit.Framework.Assert.AreEqual(_target.CanCreateFolder, createdFolder.CanCreateFolder);
            NUnit.Framework.Assert.AreEqual(_target.CanCreateSource, createdFolder.CanCreateSource);
            NUnit.Framework.Assert.AreEqual(_target.CanShowVersions, createdFolder.CanShowVersions);
            NUnit.Framework.Assert.AreEqual(_target.CanRename, createdFolder.CanRename);
            NUnit.Framework.Assert.AreEqual(_target.CanDuplicate, createdFolder.CanDuplicate);
            NUnit.Framework.Assert.AreEqual(_target.CanCreateTest, createdFolder.CanCreateTest);
            NUnit.Framework.Assert.AreEqual(_target.CanRollback, createdFolder.CanRollback);
            NUnit.Framework.Assert.AreEqual(_target.CanDeploy, createdFolder.CanDeploy);
            NUnit.Framework.Assert.AreEqual(_target.CanShowDependencies, createdFolder.CanShowDependencies);
            NUnit.Framework.Assert.AreEqual(_target.ResourcePath + "\\" + createdFolder.ResourceName, createdFolder.ResourcePath);
            NUnit.Framework.Assert.AreEqual(_target.CanCreateWorkflowService, createdFolder.CanCreateWorkflowService);
            NUnit.Framework.Assert.AreEqual(_target.ShowContextMenu, createdFolder.ShowContextMenu);
            NUnit.Framework.Assert.IsTrue(createdFolder.IsRenaming);
            _explorerTooltips.Verify(it => it.SetSourceTooltips(_target.CanCreateSource));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanShowServerVersion()
        {
            //arrange
            _target.CanShowServerVersion = false;

            //act
            _target.CanShowServerVersion = !_target.CanShowServerVersion;

            //assert
            NUnit.Framework.Assert.IsTrue(_target.CanShowServerVersion);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCreateFolderCommandResourceTypeDbService()
        {
            //arrange
            _target.IsExpanded = false;
            _target.ResourceType = "DbService";
            _target.Children.Clear();

            //act
            _target.CreateFolderCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.CreateFolderCommand.CanExecute(null));

            //assert
            NUnit.Framework.Assert.IsFalse(_target.IsExpanded);
            NUnit.Framework.Assert.IsFalse(_target.Children.Any());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeleteVersionCommand()
        {
            //arrange
            _target.ResourceType = "Version";
            _target.ResourceName = Guid.NewGuid().ToString();
            _explorerTreeItemMock.SetupGet(it => it.Children)
                .Returns(new ObservableCollection<IExplorerItemViewModel>() { _target });
            //if (_popupController.ShowDeleteVersionMessage(ResourceName) == MessageBoxResult.Yes)

            _popupControllerMock.Setup(it => it.ShowDeleteVersionMessage(It.IsAny<string>())).Returns(MessageBoxResult.Yes);

            //act
            _target.DeleteVersionCommand.Execute(null);
            NUnit.Framework.Assert.IsTrue(_target.DeleteVersionCommand.CanExecute(null));

            //assert
            _explorerRepositoryMock.Verify(it => it.TryDelete(_target));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeleteFolderCommandExpectException()
        {
            //arrange
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.SetupGet(it => it.ResourceType).Returns("WorkflowService");
            childMock.SetupGet(it => it.ResourceName).Returns("Message");
            childMock.SetupGet(it => it.IsVisible).Returns(true);
            _target.Server = new Mock<IServer>().Object;
            _target.ResourceName = "someResource";
            _target.ResourceType = "Folder";
            _target.IsFolder = true;
            _target.AddChild(childMock.Object);

            _explorerTreeItemMock.SetupGet(it => it.Children)
                .Returns(new ObservableCollection<IExplorerItemViewModel>() { _target });

            _popupControllerMock.Setup(it => it.Show(It.IsAny<IPopupMessage>())).Returns(MessageBoxResult.Yes);

            //act
            NUnit.Framework.Assert.IsTrue(_target.DeleteCommand.CanExecute(null));
            _target.DeleteCommand.Execute(null);

            //assert
            _explorerRepositoryMock.Verify(it => it.TryDelete(_target));
            _explorerTreeItemMock.Verify(it => it.RemoveChild(_target), Times.Never());

            NUnit.Framework.Assert.AreEqual(1, _target.ChildrenCount);
            _explorerTreeItemMock.Verify(it => it.AddChild(_target), Times.Never);
        }

        #endregion Test commands

        #region Test equality

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestEquals()
        {
            var otherSameId = new ExplorerItemViewModel(_serverMock.Object, _explorerTreeItemMock.Object, a => { },
                _shellViewModelMock.Object, _popupControllerMock.Object)
            { ResourceId = Guid.NewGuid() };
            var otherDifferentId = new ExplorerItemViewModel(_serverMock.Object, _explorerTreeItemMock.Object, a => { },
                _shellViewModelMock.Object, _popupControllerMock.Object)
            { ResourceId = Guid.NewGuid() };
            var otherDifferentType = new object();
            _target.ResourceId = otherSameId.ResourceId;
            _target.ResourceName = "Some res name";

            NUnit.Framework.Assert.IsFalse(_target.Equals(null));
            NUnit.Framework.Assert.IsTrue(_target.Equals(_target));
            NUnit.Framework.Assert.IsTrue(_target.Equals(otherSameId));
            NUnit.Framework.Assert.IsFalse(_target.Equals(otherDifferentId));

            NUnit.Framework.Assert.IsFalse(_target.Equals((object)null));
            NUnit.Framework.Assert.IsTrue(_target.Equals((object)_target));
            NUnit.Framework.Assert.IsTrue(_target.Equals((object)otherSameId));
            NUnit.Framework.Assert.IsFalse(_target.Equals((object)otherDifferentId));
            NUnit.Framework.Assert.IsFalse(_target.Equals(otherDifferentType));

            NUnit.Framework.Assert.IsFalse(_target == null);
            
            NUnit.Framework.Assert.IsTrue(Equals(_target, _target));
            NUnit.Framework.Assert.IsFalse(_target == otherSameId);
            NUnit.Framework.Assert.IsFalse(_target == otherDifferentId);
            NUnit.Framework.Assert.IsFalse(ReferenceEquals(_target, otherDifferentType));

            NUnit.Framework.Assert.IsTrue(_target != null);
            
            NUnit.Framework.Assert.IsFalse(!Equals(_target, _target));
            NUnit.Framework.Assert.IsTrue(_target != otherSameId);
            NUnit.Framework.Assert.IsTrue(_target != otherDifferentId);
#pragma warning disable 252,253
            NUnit.Framework.Assert.IsTrue(_target != otherDifferentType);
#pragma warning restore 252,253
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestHashcode()
        {
            _target.ResourceId = Guid.NewGuid();
            _target.ResourceName = "Some res name";

            NUnit.Framework.Assert.AreEqual(_target.GetHashCode(), _target.ResourceId.GetHashCode());
        }

        #endregion Test equality

        #region Test properties

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestExecuteToolTipGet()
        {
            //assert
            NUnit.Framework.Assert.IsNotNull(_target.ExecuteToolTip);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestEditToolTipGet()
        {
            //assert
            NUnit.Framework.Assert.IsNotNull(_target.EditToolTip);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanDropGetFolder()
        {
            //arrange
            _target.ResourceType = "Folder";
            _target.CanDrop = true;
            _target.IsFolder = true;
            //act
            var actual = _target.CanDrop;
            //assert
            NUnit.Framework.Assert.IsTrue(actual);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanDropGetOther()
        {
            //arrange
            _target.ResourceType = "Server";
            _target.CanDrop = true;
            //act
            var actual = _target.CanDrop;
            //assert
            NUnit.Framework.Assert.IsFalse(actual);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanDropSet()
        {
            //arrange
            var isCanDropChanged = false;
            _target.PropertyChanged += (sender, e) =>
            {
                isCanDropChanged = isCanDropChanged || e.PropertyName == "CanDrop";
            };

            //act
            _target.CanDrop = true;

            //assert
            NUnit.Framework.Assert.IsTrue(isCanDropChanged);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanDragGetFolder()
        {
            //arrange
            _target.ResourceType = "Folder";
            _target.CanDrag = true;
            _target.IsFolder = true;
            //act
            var actual = _target.CanDrag;
            //assert
            NUnit.Framework.Assert.IsTrue(actual);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCommands()
        {
            //arrange
            var canCreateNewServiceCommand = _target.NewServiceCommand.CanExecute(null);
            var canCreateNewServerCommand = _target.NewServerCommand.CanExecute(null);
            var canCreateNewSqlServerSourceCommand = _target.NewSqlServerSourceCommand.CanExecute(null);
            var canCreateNewMySqlSourceCommand = _target.NewMySqlSourceCommand.CanExecute(null);
            var canCreateNewPostgreSqlSourceCommand = _target.NewPostgreSqlSourceCommand.CanExecute(null);
            var canCreateNewOracleSourceCommand = _target.NewOracleSourceCommand.CanExecute(null);
            var canCreateNewOdbcSourceCommand = _target.NewOdbcSourceCommand.CanExecute(null);
            var canCreateNewPluginSourceCommand = _target.NewPluginSourceCommand.CanExecute(null);
            var canCreateNewWebSourceSourceCommand = _target.NewWebSourceSourceCommand.CanExecute(null);
            var canCreateNewRedisSourceCommand = _target.NewRedisSourceCommand.CanExecute(null);
            var canCreateNewEmailSourceSourceCommand = _target.NewEmailSourceSourceCommand.CanExecute(null);
            var canCreateNewExchangeSourceSourceCommand = _target.NewExchangeSourceSourceCommand.CanExecute(null);
            var canCreateNewSharepointSourceSourceCommand = _target.NewSharepointSourceSourceCommand.CanExecute(null);
            var canCreateNewDropboxSourceSourceCommand = _target.NewDropboxSourceSourceCommand.CanExecute(null);
            var canCreateNewRabbitMqSourceSourceCommand = _target.NewRabbitMqSourceSourceCommand.CanExecute(null);
            var canViewSwaggerCommand = _target.ViewSwaggerCommand.CanExecute(null);
            var canViewApisJsonCommand = _target.ViewApisJsonCommand.CanExecute(null);

            //act

            //assert
            NUnit.Framework.Assert.IsTrue(canCreateNewServiceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewServerCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewSqlServerSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewMySqlSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewPostgreSqlSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewOracleSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewOdbcSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewPluginSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewWebSourceSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewRedisSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewEmailSourceSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewExchangeSourceSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewSharepointSourceSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewDropboxSourceSourceCommand);
            NUnit.Framework.Assert.IsTrue(canCreateNewRabbitMqSourceSourceCommand);
            NUnit.Framework.Assert.IsTrue(canViewSwaggerCommand);
            NUnit.Framework.Assert.IsTrue(canViewApisJsonCommand);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanDragGetOther()
        {
            //arrange
            _target.ResourceType = "Server";
            _target.CanDrag = true;
            //act
            var actual = _target.CanDrag;
            //assert
            NUnit.Framework.Assert.IsTrue(actual);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanDragSet()
        {
            //arrange
            var isCanDragChanged = false;
            _target.PropertyChanged += (sender, e) =>
            {
                isCanDragChanged = isCanDragChanged || e.PropertyName == "CanDrag";
            };

            //act
            _target.CanDrag = true;

            //assert
            NUnit.Framework.Assert.IsTrue(isCanDragChanged);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestIsExpanderVisibleNoChildren()
        {
            //arrange
            _target.Children.Clear();
            //act
            var isExpanderVisible = _target.IsExpanderVisible;
            //assert
            NUnit.Framework.Assert.IsFalse(isExpanderVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestIsExpanderVisibleChildrenAreVersionsVisible()
        {
            //arrange
            _target.Children.Clear();
            var childMock = new Mock<IExplorerItemViewModel>();
            _target.Children.Add(childMock.Object);
            _explorerRepositoryMock.Setup(it => it.GetVersions(It.IsAny<Guid>()))
                .Returns(new List<IVersionInfo>());
            _target.AreVersionsVisible = true;
            //act
            var isExpanderVisible = _target.IsExpanderVisible;
            //assert
            NUnit.Framework.Assert.IsFalse(isExpanderVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestIsExpanderVisibleChildrenAreVersionsInvisible()
        {
            //arrange
            _target.Children.Clear();
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.Setup(model => model.IsVisible).Returns(true);
            _target.AreVersionsVisible = false;
            _target.AddChild(childMock.Object);
            //act
            var isExpanderVisible = _target.IsExpanderVisible;
            //assert
            NUnit.Framework.Assert.IsTrue(isExpanderVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestAreVersionsVisibleTrue()
        {
            //arrange
            var versionMock = new Mock<IVersionInfo>();
            var isAreVersionsVisibleChanged = false;
            var isChildrenChanged = false;
            var collection = new List<IVersionInfo>()
            {
                versionMock.Object
            };
            versionMock.SetupGet(it => it.VersionNumber).Returns("someVerNum");
            versionMock.SetupGet(it => it.DateTimeStamp).Returns(new DateTime(2013, 2, 2));
            versionMock.SetupGet(it => it.Reason).Returns("gfedew.xml");
            _target.ResourceId = Guid.NewGuid();
            _target.PropertyChanged += (sender, e) =>
            {
                isAreVersionsVisibleChanged = isAreVersionsVisibleChanged || e.PropertyName == "AreVersionsVisible";
                isChildrenChanged = isChildrenChanged || e.PropertyName == "Children";
            };
            _target.CanDelete = true;
            _explorerRepositoryMock.Setup(it => it.GetVersions(It.IsAny<Guid>())).Returns(collection);
            //act
            _target.AreVersionsVisible = true;
            //assert
            var version = (VersionViewModel)_target.Children[0];
            NUnit.Framework.Assert.AreEqual(Resources.Languages.Core.HideVersionHistoryLabel, _target.VersionHeader);
            NUnit.Framework.Assert.IsTrue(version.IsVersion);
            NUnit.Framework.Assert.AreEqual("v.someVerNum 02022013 000000 gfedew", version.ResourceName);
            NUnit.Framework.Assert.AreEqual(_target.ResourceId, version.ResourceId);
            NUnit.Framework.Assert.AreEqual(versionMock.Object.VersionNumber, version.VersionNumber);
            NUnit.Framework.Assert.AreSame(versionMock.Object, version.VersionInfo);
            NUnit.Framework.Assert.IsFalse(version.CanEdit);
            NUnit.Framework.Assert.IsFalse(version.CanCreateWorkflowService);
            NUnit.Framework.Assert.IsTrue(version.ShowContextMenu);
            NUnit.Framework.Assert.IsFalse(version.CanCreateSource);
            NUnit.Framework.Assert.IsFalse(version.AllowResourceCheck);
            NUnit.Framework.Assert.IsTrue(version.IsResourceChecked.HasValue && !version.IsResourceChecked.Value);
            NUnit.Framework.Assert.AreEqual(_target.CanDelete, version.CanDelete);
            NUnit.Framework.Assert.AreEqual("Version", version.ResourceType);
            NUnit.Framework.Assert.IsTrue(_target.IsExpanded);
            NUnit.Framework.Assert.IsTrue(isAreVersionsVisibleChanged);
            NUnit.Framework.Assert.IsTrue(isChildrenChanged);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeployIsResourceCheckedDisabled()
        {
            //arrange
            var windowsGroupPermissionMock = new Mock<IWindowsGroupPermission>();
            windowsGroupPermissionMock.SetupGet(it => it.IsServer).Returns(true);
            windowsGroupPermissionMock.SetupGet(it => it.View).Returns(false);
            windowsGroupPermissionMock.SetupGet(it => it.DeployFrom).Returns(true);
            _serverMock.SetupGet(it => it.Permissions).Returns(new List<IWindowsGroupPermission>()
            {
                windowsGroupPermissionMock.Object
            });
            _target.SetPermission(Permissions.DeployFrom);
            _target.IsFolder = false;
            _target.IsResourceChecked = false;
            //act
            //assert
            NUnit.Framework.Assert.IsFalse(_target.CanDeploy);
            NUnit.Framework.Assert.AreEqual(_target.CanDeploy, _target.IsResourceCheckedEnabled);
            NUnit.Framework.Assert.AreEqual(_target.DeployResourceCheckboxTooltip, Resources.Languages.Core.DeployResourceCheckboxViewPermissionError);
        }
        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeployIsResourceCheckedEnabled_GivenView_ThenChangedToAdministrator()
        {
            //arrange
            var windowsGroupPermissionMock = new Mock<IWindowsGroupPermission>();
            windowsGroupPermissionMock.SetupGet(it => it.IsServer).Returns(true);
            windowsGroupPermissionMock.SetupGet(it => it.View).Returns(true);
            windowsGroupPermissionMock.SetupGet(it => it.Administrator).Returns(true);
            _serverMock.SetupGet(it => it.Permissions).Returns(new List<IWindowsGroupPermission>()
            {
                windowsGroupPermissionMock.Object
            });
            _target.SetPermission(Permissions.Administrator);
            _target.IsFolder = false;
            _target.IsResourceChecked = false;
            //act
            //assert
            NUnit.Framework.Assert.IsTrue(_target.CanDeploy);
            NUnit.Framework.Assert.AreEqual(_target.CanDeploy, _target.IsResourceCheckedEnabled);
            NUnit.Framework.Assert.AreEqual(_target.DeployResourceCheckboxTooltip, Resources.Languages.Core.DeployResourceCheckbox);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeployIsResourceCheckedEnabled_GivenAdministrator_ThenChangedToView()
        {
            //arrange
            var windowsGroupPermissionMock = new Mock<IWindowsGroupPermission>();
            windowsGroupPermissionMock.SetupGet(it => it.IsServer).Returns(true);
            windowsGroupPermissionMock.SetupGet(it => it.Administrator).Returns(true);
            windowsGroupPermissionMock.SetupGet(it => it.View).Returns(true);
            _serverMock.SetupGet(it => it.Permissions).Returns(new List<IWindowsGroupPermission>()
            {
                windowsGroupPermissionMock.Object
            });
            _target.SetPermission(Permissions.Administrator);
            _target.IsFolder = false;
            _target.IsResourceChecked = false;
            //act
            //assert
            NUnit.Framework.Assert.IsTrue(_target.CanDeploy);
            NUnit.Framework.Assert.AreEqual(_target.CanDeploy, _target.IsResourceCheckedEnabled);
            NUnit.Framework.Assert.AreEqual(_target.DeployResourceCheckboxTooltip, Resources.Languages.Core.DeployResourceCheckbox);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestAreVersionsVisibleFalse()
        {
            //arrange
            var isAreVersionsVisibleChanged = false;
            var isChildrenChanged = false;

            _target.PropertyChanged += (sender, e) =>
            {
                isAreVersionsVisibleChanged = isAreVersionsVisibleChanged || e.PropertyName == "AreVersionsVisible";
                isChildrenChanged = isChildrenChanged || e.PropertyName == "Children";
            };
            //act
            _target.AreVersionsVisible = false;
            //assert
            NUnit.Framework.Assert.IsFalse(_target.Children.Any());
            NUnit.Framework.Assert.AreEqual(Resources.Languages.Core.ShowVersionHistoryLabel, _target.VersionHeader);

            NUnit.Framework.Assert.IsTrue(isAreVersionsVisibleChanged);
            NUnit.Framework.Assert.IsTrue(isChildrenChanged);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSetIsExpanderVisible()
        {
            //arrange
            var isExpanderVisibleChanged = false;
            _target.PropertyChanged += (sender, e) => isExpanderVisibleChanged = e.PropertyName == "IsExpanderVisible";
            //act
            _target.IsExpanderVisible = true;
            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsVisible);
            NUnit.Framework.Assert.IsTrue(isExpanderVisibleChanged);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestChecked()
        {
            //arrange
            //act
            _target.Checked = true;
            //assert
            NUnit.Framework.Assert.IsTrue(_target.Checked);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestIsRenaming()
        {
            //arrange
            var isRenamingFired = false;
            _target.PropertyChanged += (sender, e) =>
            {
                isRenamingFired = isRenamingFired || e.PropertyName == "IsRenaming";
            };
            //act
            _target.IsRenaming = true;
            //assert
            NUnit.Framework.Assert.IsTrue(isRenamingFired);
            NUnit.Framework.Assert.IsTrue(_target.IsRenaming);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestResourceNameWithoutSlashes()
        {
            //arrange
            CustomContainer.Register<IAsyncWorker>(new SynchronousAsyncWorker());
            var isResourceNameFired = false;
            _target.PropertyChanged += (sender, e) =>
            {
                isResourceNameFired = e.PropertyName == "ResourceName";
            };
            _explorerTreeItemMock.SetupGet(it => it.Children).Returns(new ObservableCollection<IExplorerItemViewModel>());
            var newName = "SomeNewName";
            _target.IsRenaming = false;
            _target.ResourcePath = "someResPath";
            _target.ResourceName = "someResPath";
            _target.Children.Clear();
            _target.IsRenaming = true;
            _explorerRepositoryMock.Setup(it => it.Rename(_target, It.IsAny<string>())).Returns(true);
            //act
            _target.ResourceName = newName;
            //assert
            NUnit.Framework.Assert.IsTrue(isResourceNameFired);
            NUnit.Framework.Assert.IsFalse(_target.IsRenaming);
            NUnit.Framework.Assert.IsTrue(_target.ResourcePath.Contains(newName));
            _explorerRepositoryMock.Verify(it => it.Rename(_target, newName));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestResourceNameWithSlashes()
        {
            //arrange
            CustomContainer.Register<IAsyncWorker>(new SynchronousAsyncWorker());
            var isResourceNameFired = false;
            _target.PropertyChanged += (sender, e) =>
            {
                isResourceNameFired = e.PropertyName == "ResourceName";
            };
            _explorerTreeItemMock.SetupGet(it => it.Children).Returns(new ObservableCollection<IExplorerItemViewModel>());
            var newName = "SomeNewName";
            _target.IsRenaming = false;
            _target.ResourcePath = "Some\\someResPath";
            _target.ResourceName = "someResPath";
            _target.Children.Clear();
            _target.IsRenaming = true;
            _explorerRepositoryMock.Setup(it => it.Rename(It.IsAny<ExplorerItemViewModel>(), It.IsAny<string>())).Returns(true);
            //act
            _target.ResourceName = newName;
            //assert
            NUnit.Framework.Assert.IsTrue(isResourceNameFired);
            NUnit.Framework.Assert.IsFalse(_target.IsRenaming);
            NUnit.Framework.Assert.AreEqual("Some\\SomeNewName", _target.ResourcePath);
            _explorerRepositoryMock.Verify(it => it.Rename(It.IsAny<ExplorerItemViewModel>(), It.IsAny<string>()));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestResourceNameDuplicate()
        {
            //arrange
            var isResourceNameFired = false;
            _target.PropertyChanged += (sender, e) =>
            {
                isResourceNameFired = e.PropertyName == "ResourceName";
            };
            var newName = "SomeNewName";
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.SetupGet(it => it.ResourceName).Returns(newName);
            _explorerTreeItemMock.SetupGet(it => it.Children)
                .Returns(new ObservableCollection<IExplorerItemViewModel>() { childMock.Object });
            _target.IsRenaming = false;
            _target.ResourcePath = "Some\\someResPath";
            _target.ResourceName = "someResPath";
            _target.Children.Clear();
            _target.IsRenaming = true;
            _explorerRepositoryMock.Setup(it => it.Rename(_target, It.IsAny<string>())).Returns(true);
            //act
            _target.ResourceName = newName;
            //assert
            NUnit.Framework.Assert.IsFalse(isResourceNameFired);
            NUnit.Framework.Assert.IsTrue(_target.IsRenaming);
            NUnit.Framework.Assert.AreEqual("Some\\someResPath", _target.ResourcePath);
            NUnit.Framework.Assert.AreEqual("someResPath", _target.ResourceName);
            _shellViewModelMock.Verify(it => it.ShowPopup(It.IsAny<IPopupMessage>()));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestActivityName()
        {
            //assert
            _target.ResourceType = "Folder";
            NUnit.Framework.Assert.IsTrue(_target.ActivityName.StartsWith("Unlimited.Applications.BusinessDesignStudio.Activities.DsfActivity"));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestChildrenCount()
        {
            //arrange
            var childMockVersion = new Mock<IExplorerItemViewModel>();
            childMockVersion.SetupGet(it => it.ResourceType).Returns("Version");
            childMockVersion.SetupGet(it => it.ResourceName).Returns("ResourceVersion");
            childMockVersion.SetupGet(it => it.IsVisible).Returns(true);
            var childMockMessage = new Mock<IExplorerItemViewModel>();
            childMockMessage.SetupGet(it => it.ResourceType).Returns("Message");
            childMockMessage.SetupGet(it => it.ResourceName).Returns("ResourceMessage");
            childMockMessage.SetupGet(it => it.IsVisible).Returns(true);
            var childMockFolder = new Mock<IExplorerItemViewModel>();
            childMockFolder.SetupGet(it => it.ResourceType).Returns("Folder");
            childMockFolder.SetupGet(it => it.ResourceName).Returns("ResourceFolder");
            childMockFolder.SetupGet(it => it.IsVisible).Returns(true);
            childMockFolder.SetupGet(it => it.IsFolder).Returns(true);
            childMockFolder.SetupGet(it => it.ChildrenCount).Returns(2);
            var childMockServer = new Mock<IExplorerItemViewModel>();
            childMockServer.SetupGet(it => it.ResourceType).Returns("Server");
            childMockServer.SetupGet(it => it.ResourceName).Returns("ResourceServer");
            childMockServer.SetupGet(it => it.IsVisible).Returns(true);

            _target.AddChild(childMockVersion.Object);
            _target.AddChild(childMockMessage.Object);
            _target.AddChild(childMockFolder.Object);
            _target.AddChild(childMockServer.Object);

            _target.UpdateChildrenCount();

            //act
            var childrenCount = _target.ChildrenCount;

            //assert
            NUnit.Framework.Assert.AreEqual(4, childrenCount);
        }

        #endregion Test properties

        #region Test methods

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDeleteClosesWindow()
        {
            //arrange
            var mock = new Mock<IExplorerRepository>();
            mock.Setup(metadata => metadata.TryDelete(It.IsAny<IExplorerItemViewModel>())).Returns(new DeletedFileMetadata() { IsDeleted = false });
            _popupControllerMock.Setup(a => a.Show(It.IsAny<IPopupMessage>())).Returns(MessageBoxResult.Yes);
            _target.Server = new Mock<IServer>().Object;
            var child = new Mock<IExplorerItemViewModel>();
            _target.Children.Add(child.Object);

            var privateObject = new PrivateObject(_target);
            privateObject.SetField("_explorerRepository", mock.Object);
            //act
            _target.Delete();

            //assert
            _shellViewModelMock.Verify(a => a.CloseResource(It.IsAny<Guid>(), It.IsAny<Guid>()));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestDispose()
        {
            //arrange
            var child = new Mock<IExplorerItemViewModel>();
            child.Setup(model => model.IsVisible).Returns(true);
            _target.AddChild(child.Object);

            //act
            _target.Dispose();

            //assert
            child.Verify(a => a.Dispose());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestFilter()
        {
            //arrange
            var isChildrenChanged = false;
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.SetupGet(it => it.IsVisible).Returns(false);
            childMock.SetupGet(it => it.ResourceType).Returns("Folder");
            _target.ResourceName = "someFilter";
            _target.ResourceType = "Message";
            _target.AddChild(childMock.Object);
            _target.PropertyChanged += (sender, e) =>
            {
                isChildrenChanged = isChildrenChanged || e.PropertyName == "Children";
            };
            //act
            _target.Filter("someFilter");
            //assert
            NUnit.Framework.Assert.IsTrue(isChildrenChanged);
            NUnit.Framework.Assert.IsTrue(_target.IsVisible);
            childMock.Verify(it => it.Filter("someFilter"));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestFilterNoText()
        {
            //arrange
            var isChildrenChanged = false;
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.SetupGet(it => it.IsVisible).Returns(false);
            childMock.SetupGet(it => it.ResourceType).Returns("Folder");
            _target.ResourceName = "someFilter";
            _target.ResourceType = "Folder";
            _target.IsFolder = true;
            _target.Children.Add(childMock.Object);
            _target.PropertyChanged += (sender, e) =>
            {
                isChildrenChanged = isChildrenChanged || e.PropertyName == "Children";
            };
            //act
            _target.Filter("");
            //assert
            NUnit.Framework.Assert.IsTrue(isChildrenChanged);
            NUnit.Framework.Assert.IsTrue(_target.IsVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestFilterVersion()
        {
            //arrange
            var isChildrenChanged = false;
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.SetupGet(it => it.IsVisible).Returns(false);
            childMock.SetupGet(it => it.ResourceType).Returns("Version");
            childMock.SetupGet(it => it.IsVersion).Returns(true);
            _target.ResourceName = "someFilter";
            _target.ResourceType = "Version";
            _target.IsVersion = true;

            var parent = new Mock<IExplorerTreeItem>();
            parent.SetupGet(it => it.ResourceName).Returns("parent");

            _target.Parent = parent.Object;
            _target.AddChild(childMock.Object);
            _target.PropertyChanged += (sender, e) =>
            {
                isChildrenChanged = isChildrenChanged || e.PropertyName == "Children";
            };
            //act
            _target.Filter("someFilter");
            //assert
            NUnit.Framework.Assert.IsTrue(isChildrenChanged);
            NUnit.Framework.Assert.IsFalse(_target.IsVisible);
            childMock.Verify(it => it.Filter("someFilter"));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestFilterInvisible()
        {
            //arrange
            var isChildrenChanged = false;
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.SetupGet(it => it.IsVisible).Returns(true);
            childMock.SetupGet(it => it.ResourceType).Returns("Folder");
            _target.ResourceType = "Message";
            _target.ResourceName = "someFilter";
            _target.AddChild(childMock.Object);
            _target.PropertyChanged += (sender, e) =>
            {
                isChildrenChanged = isChildrenChanged || e.PropertyName == "Children";
            };
            //act
            _target.Filter("1");
            //assert
            NUnit.Framework.Assert.IsTrue(isChildrenChanged);
            NUnit.Framework.Assert.IsTrue(_target.IsVisible);
            childMock.Verify(it => it.Filter("1"));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestFilterInvisibleNoChildren()
        {
            //arrange
            var isChildrenChanged = false;
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.SetupGet(it => it.IsVisible).Returns(false);
            childMock.SetupGet(it => it.ResourceType).Returns("Folder");
            _target.ResourceType = "Message";
            _target.ResourceName = "someFilter";
            _target.AddChild(childMock.Object);
            _target.PropertyChanged += (sender, e) =>
            {
                isChildrenChanged = isChildrenChanged || e.PropertyName == "Children";
            };
            //act
            _target.Filter("1");
            //assert
            NUnit.Framework.Assert.IsTrue(isChildrenChanged);
            NUnit.Framework.Assert.IsFalse(_target.IsVisible);
            childMock.Verify(it => it.Filter("1"));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestRemoveChild()
        {
            //arrange
            var child = new Mock<IExplorerItemViewModel>().Object;
            _target.Children.Add(child);
            var wasCalled = false;
            _target.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Children")
                {
                    wasCalled = true;
                }
            };
            //act
            _target.RemoveChild(child);
            //assert
            NUnit.Framework.Assert.IsFalse(_target.Children.Contains(child));
            NUnit.Framework.Assert.IsTrue(wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestAddChild()
        {
            //arrange
            var mockChild = new Mock<IExplorerItemViewModel>();
            mockChild.Setup(model => model.IsVisible).Returns(true);
            var child = mockChild.Object;

            _target.Children.Clear();

            var wasCalled = false;
            _target.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Children")
                {
                    wasCalled = true;
                }
            };

            //act
            _target.AddChild(child);
            //assert
            NUnit.Framework.Assert.IsTrue(_target.Children.Contains(child));
            NUnit.Framework.Assert.IsTrue(wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSelectItem()
        {
            //arrange
            var id = Guid.NewGuid();
            var childSameId = new Mock<IExplorerItemViewModel>();
            childSameId.SetupGet(it => it.ResourceId).Returns(id);
            childSameId.SetupGet(it => it.ResourceName).Returns("ResourceName");
            childSameId.SetupGet(it => it.IsVisible).Returns(true);
            var childDifferentId = new Mock<IExplorerItemViewModel>();
            childDifferentId.SetupGet(it => it.ResourceId).Returns(Guid.NewGuid);
            childDifferentId.SetupGet(it => it.IsVisible).Returns(true);
            _target.AddChild(childSameId.Object);
            var foundActionRun = false;
            _target.AddChild(childDifferentId.Object);
            Action<IExplorerItemViewModel> foundAction = item => foundActionRun = ReferenceEquals(item, childSameId.Object);
            //act
            _target.SelectItem(id, foundAction);
            //assert
            childSameId.VerifySet(it => it.IsExpanded = true);
            childSameId.VerifySet(it => it.IsSelected = true);
            NUnit.Framework.Assert.IsTrue(foundActionRun);
            childDifferentId.Verify(it => it.SelectItem(id, foundAction));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCreateNewFolderResourceTypeDbService()
        {
            //arrange
            _target.ForcedRefresh = false;
            _target.IsExpanded = false;
            _target.ResourceType = "DbService";
            _target.Children.Clear();

            //act
            _target.CreateNewFolder();

            //assert
            NUnit.Framework.Assert.IsFalse(_target.IsExpanded);
            NUnit.Framework.Assert.IsFalse(_target.Children.Any());
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCreateNewFolderResourceTypeFolder()
        {
            //arrange
            _serverMock.Setup(server => server.UserPermissions)
                       .Returns(Permissions.Administrator);
            _serverMock.Setup(server => server.GetPermissions(It.IsAny<Guid>())).Returns(Permissions.Administrator);

            _target.IsExpanded = false;
            _target.ResourceType = "Folder";
            _target.IsFolder = true;
            _target.Children.Clear();
            _target.AllowResourceCheck = true;
            _target.IsResourceChecked = true;
            _target.IsFolderChecked = true;
            _target.CanCreateFolder = true;
            _target.CanCreateSource = true;
            _target.CanShowVersions = true;
            _target.CanRename = true;
            _target.CanDeploy = true;
            _target.CanShowDependencies = true;
            _target.ResourcePath = Guid.NewGuid().ToString();
            _target.CanCreateWorkflowService = true;
            _target.ShowContextMenu = true;

            //act
            _target.CreateNewFolder();

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsExpanded);
            //_explorerRepositoryMock.Verify(it => it.CreateFolder(_target.ResourcePath, "New Folder", It.IsAny<Guid>()));
            var createdFolder = _target.Children.Single();
            NUnit.Framework.Assert.AreEqual("New Folder", createdFolder.ResourceName);
            NUnit.Framework.Assert.AreEqual("Folder", createdFolder.ResourceType);
            NUnit.Framework.Assert.AreEqual(_target.IsFolder, createdFolder.IsFolder);
            NUnit.Framework.Assert.AreEqual(_target.AllowResourceCheck, createdFolder.AllowResourceCheck);
            NUnit.Framework.Assert.AreEqual(_target.IsResourceChecked, createdFolder.IsResourceChecked);
            NUnit.Framework.Assert.AreEqual(_target.IsFolderChecked, createdFolder.IsFolderChecked);
            NUnit.Framework.Assert.AreEqual(_target.CanCreateFolder, createdFolder.CanCreateFolder);
            NUnit.Framework.Assert.AreEqual(_target.CanCreateSource, createdFolder.CanCreateSource);
            NUnit.Framework.Assert.AreEqual(_target.CanShowVersions, createdFolder.CanShowVersions);
            NUnit.Framework.Assert.AreEqual(_target.CanRename, createdFolder.CanRename);
            NUnit.Framework.Assert.AreEqual(_target.CanRollback, createdFolder.CanRollback);
            NUnit.Framework.Assert.AreEqual(_target.CanDeploy, createdFolder.CanDeploy);
            NUnit.Framework.Assert.AreEqual(_target.CanShowDependencies, createdFolder.CanShowDependencies);
            NUnit.Framework.Assert.AreEqual(_target.ResourcePath + "\\" + createdFolder.ResourceName, createdFolder.ResourcePath);
            NUnit.Framework.Assert.AreEqual(_target.CanCreateWorkflowService, createdFolder.CanCreateWorkflowService);
            NUnit.Framework.Assert.AreEqual(_target.ShowContextMenu, createdFolder.ShowContextMenu);
            NUnit.Framework.Assert.IsTrue(createdFolder.IsRenaming);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCreateNewFolderResourceTypeFolderNewFolder1()
        {
            //arrange
            const string newFolderName = "New Folder";
            var childMock = new Mock<IExplorerItemViewModel>();
            childMock.SetupGet(it => it.ResourceName).Returns(newFolderName);
            childMock.SetupGet(it => it.IsVisible).Returns(true);
            _target.IsExpanded = false;
            _target.ResourceType = "Folder";
            _target.IsFolder = true;
            _target.Children.Clear();
            _target.AddChild(childMock.Object);

            //act
            _target.CreateNewFolder();

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsExpanded);
            //_explorerRepositoryMock.Verify(it => it.CreateFolder(_target.ResourcePath, "New Folder 1", It.IsAny<Guid>()));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestApply()
        {
            //arrange
            var child = new Mock<IExplorerItemViewModel>();
            child.Setup(model => model.IsVisible).Returns(true);
            var actionRun = false;
            _target.AddChild(child.Object);
            Action<IExplorerItemViewModel> action = a => actionRun = ReferenceEquals(_target, a);
            //act
            _target.Apply(action);
            //assert
            NUnit.Framework.Assert.IsTrue(actionRun);
            child.Verify(it => it.Apply(It.IsAny<Action<IExplorerItemViewModel>>()));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestFilterChildrenFound()
        {
            //arrange
            var propertyChangedRaised = false;
            var child = new Mock<IExplorerItemViewModel>();
            child.SetupGet(it => it.IsVisible).Returns(true);
            child.SetupGet(it => it.ResourceType).Returns("Folder");
            _target.AddChild(child.Object);
            Func<IExplorerItemViewModel, bool> filter = item => false;
            _target.PropertyChanged += (sender, e) => propertyChangedRaised = e.PropertyName == "Children";
            //act
            _target.Filter(filter);
            //assert
            NUnit.Framework.Assert.IsTrue(propertyChangedRaised);
            child.Verify(it => it.Filter(filter));
            NUnit.Framework.Assert.IsTrue(_target.IsVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestFilterChildrenEmpty()
        {
            //arrange
            var propertyChangedRaised = false;
            _target.ResourceName = Guid.NewGuid().ToString();
            _target.ResourceType = "Folder";
#pragma warning disable 252,253
            Func<IExplorerItemViewModel, bool> filter = item => item == _target;
#pragma warning restore 252,253
            _target.PropertyChanged += (sender, e) => propertyChangedRaised = e.PropertyName == "Children";
            //act
            _target.Filter(filter);
            //assert
            NUnit.Framework.Assert.IsTrue(propertyChangedRaised);
            NUnit.Framework.Assert.IsTrue(_target.IsVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestAsList()
        {
            //arrange
            var child = new Mock<IExplorerItemViewModel>();
            child.Setup(model => model.IsVisible).Returns(true);
            var child2 = new Mock<IExplorerItemViewModel>();
            child2.Setup(model => model.IsVisible).Returns(true);
            child.Setup(it => it.AsList()).Returns(new List<IExplorerItemViewModel> { child2.Object });
            _target.AddChild(child.Object);
            _target.Children.Add(child.Object);
            _target.Children.Add(child2.Object);
            //act
            var result = _target.AsList();
            //assert
            NUnit.Framework.Assert.IsTrue(result.Contains(child2.Object));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestAsListChildrenNull()
        {
            //arrange
            _target.Children = null;
            //act
            var result = _target.AsList();
            //assert
            NUnit.Framework.Assert.IsNotNull(result);
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSetPermissionsSameResource()
        {
            //arrange
            _target.ResourceId = Guid.NewGuid();
            _target.ResourceType = "WorkflowService";
            _target.IsService = true;
            //act
            _target.SetPermissions(Permissions.Administrator);
            //assert
            NUnit.Framework.Assert.IsTrue(_target.CanEdit);
            NUnit.Framework.Assert.IsTrue(_target.CanView);
            NUnit.Framework.Assert.IsTrue(_target.CanRename);
            NUnit.Framework.Assert.IsTrue(_target.CanDuplicate);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateTest);
            NUnit.Framework.Assert.IsTrue(_target.CanDelete);
            NUnit.Framework.Assert.IsTrue(_target.CanMove);
            NUnit.Framework.Assert.IsFalse(_target.CanCreateFolder);
            NUnit.Framework.Assert.IsTrue(_target.CanDeploy);
            NUnit.Framework.Assert.IsTrue(_target.CanShowVersions);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateWorkflowService);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateSource);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestNonrPermissionsSameResource()
        {
            //arrange
            _target.ResourceId = Guid.NewGuid();
            _target.ResourceType = "WorkflowService";
            _target.IsService = true;
            //act
            _target.SetPermissions(Permissions.None);
            //assert
            NUnit.Framework.Assert.IsFalse(_target.CanEdit);
            NUnit.Framework.Assert.IsFalse(_target.CanView);
            NUnit.Framework.Assert.IsFalse(_target.CanRename);
            NUnit.Framework.Assert.IsFalse(_target.CanDuplicate);
            NUnit.Framework.Assert.IsFalse(_target.CanCreateTest);
            NUnit.Framework.Assert.IsFalse(_target.CanDelete);
            NUnit.Framework.Assert.IsFalse(_target.CanMove);
            NUnit.Framework.Assert.IsFalse(_target.CanCreateFolder);
            NUnit.Framework.Assert.IsFalse(_target.CanDeploy);
            NUnit.Framework.Assert.IsFalse(_target.CanShowVersions);
            NUnit.Framework.Assert.IsFalse(_target.CanCreateWorkflowService);
            NUnit.Framework.Assert.IsFalse(_target.CanCreateSource);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSetPermissions_ContributePermission_AllowsMove()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.IsService = true;
            //act
            _target.SetPermissions(Permissions.Contribute);
            //assert
            NUnit.Framework.Assert.IsTrue(_target.CanEdit);
            NUnit.Framework.Assert.IsTrue(_target.CanView);
            NUnit.Framework.Assert.IsTrue(_target.CanRename);
            NUnit.Framework.Assert.IsTrue(_target.CanDuplicate);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateTest);
            NUnit.Framework.Assert.IsTrue(_target.CanDelete);
            NUnit.Framework.Assert.IsTrue(_target.CanMove);
            NUnit.Framework.Assert.IsFalse(_target.CanCreateFolder);
            NUnit.Framework.Assert.IsFalse(_target.CanDeploy);
            NUnit.Framework.Assert.IsTrue(_target.CanShowVersions);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateWorkflowService);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateSource);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSetPermissionsServerPermission()
        {
            //arrange

            _target.ResourceId = Guid.NewGuid();
            _target.ResourceType = "WorkflowService";
            _target.IsService = true;
            //act
            _target.SetPermissions(Permissions.Administrator);
            //assert
            NUnit.Framework.Assert.IsTrue(_target.CanEdit);
            NUnit.Framework.Assert.IsTrue(_target.CanView);
            NUnit.Framework.Assert.IsTrue(_target.CanRename);
            NUnit.Framework.Assert.IsTrue(_target.CanMove);
            NUnit.Framework.Assert.IsTrue(_target.CanDuplicate);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateTest);
            NUnit.Framework.Assert.IsTrue(_target.CanDelete);
            NUnit.Framework.Assert.IsFalse(_target.CanCreateFolder);
            NUnit.Framework.Assert.IsTrue(_target.CanDeploy);
            NUnit.Framework.Assert.IsTrue(_target.CanShowVersions);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateWorkflowService);
            NUnit.Framework.Assert.IsTrue(_target.CanCreateSource);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanViewSwaggerIsVisible()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.CanView = true;
            _target.IsService = true;
            _target.IsSource = false;

            //act

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsService);
            NUnit.Framework.Assert.IsTrue(_target.CanViewSwagger);
            NUnit.Framework.Assert.IsTrue(_target.CanViewApisJson);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanViewSwaggerIsNotVisible()
        {
            //arrange
            _target.ResourceType = "Folder";
            _target.CanView = true;
            _target.IsService = false;
            _target.IsFolder = true;
            //act

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsFolder);
            NUnit.Framework.Assert.IsFalse(_target.CanViewSwagger);
            NUnit.Framework.Assert.IsTrue(_target.CanViewApisJson);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanViewMergeIsVisible()
        {
            //arrange
            _target.ResourceType = "WorkflowService";
            _target.CanView = true;
            _target.IsService = true;
            _target.IsSource = false;
            _target.IsSaveDialog = false;

            var connection = new Mock<IEnvironmentConnection>();
            connection.SetupGet(it => it.ID).Returns(Guid.NewGuid());
            var mock = new Mock<IServer>();
            mock.SetupGet(it => it.Connection).Returns(connection.Object);
            mock.SetupGet(it => it.Connection.WebServerUri).Returns(new Uri("http://localhost:3142"));
            mock.SetupGet(it => it.IsLocalHost).Returns(true);
            _target.Server = mock.Object;

            //act

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsService);
            NUnit.Framework.Assert.IsTrue(_target.CanMerge);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanViewMergeIsNotVisible()
        {
            //arrange
            _target.ResourceType = "Folder";
            _target.CanView = true;
            _target.IsService = false;
            _target.IsFolder = true;
            //act

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsFolder);
            NUnit.Framework.Assert.IsFalse(_target.CanMerge);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestCanViewApisJsonIsVisible()
        {
            //arrange
            _target.ResourceType = "Folder";
            _target.CanView = true;
            _target.IsService = false;
            _target.IsFolder = true;
            //act

            //assert
            NUnit.Framework.Assert.IsTrue(_target.IsFolder);
            NUnit.Framework.Assert.IsFalse(_target.CanViewSwagger);
            NUnit.Framework.Assert.IsTrue(_target.CanViewApisJson);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSetPermissionsServerPermissionFolder()
        {
            //arrange

            _target.ResourceType = "Folder";
            _target.IsFolder = true;
            //act
            _target.SetPermissions(Permissions.Administrator);
            //assert
            NUnit.Framework.Assert.IsFalse(_target.CanEdit);
            NUnit.Framework.Assert.IsFalse(_target.CanExecute);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestSetPermissionsIsDeploy()
        {
            //arrange

            _target.ResourceType = "WorkflowService";

            //act
            _target.SetPermissions(Permissions.DeployFrom, true);
            //assert
            NUnit.Framework.Assert.IsFalse(_target.CanEdit);
            NUnit.Framework.Assert.IsFalse(_target.CanExecute);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public void TestAddSibling()
        {
            //arrange
            var siblingMock = new Mock<IExplorerItemViewModel>();
            //act
            _target.AddSibling(siblingMock.Object);
            //assert
            _explorerTreeItemMock.Verify(it => it.AddChild(siblingMock.Object));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public async System.Threading.Tasks.Task TestMoveAlreadyExist()
        {
            //arrange
            var movedItem = new Mock<IExplorerTreeItem>();
            var childDestItem = new Mock<IExplorerItemViewModel>();
            _target.ResourceName = "someName";
            _target.ResourceType = "EmailSource";
            childDestItem.SetupGet(it => it.ResourceName).Returns(_target.ResourceName);
            movedItem.SetupGet(it => it.Children).Returns(new ObservableCollection<IExplorerItemViewModel>()
            {
                childDestItem.Object
            });
            //act
            var result = await _target.MoveAsync(movedItem.Object);
            //assert
            NUnit.Framework.Assert.IsFalse(result);
            _shellViewModelMock.Verify(it => it.ShowPopup(It.IsAny<IPopupMessage>()));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public async System.Threading.Tasks.Task TestMoveToFolderExists()
        {
            //arrange
            var destinationMock = new Mock<IExplorerTreeItem>();
            destinationMock.Setup(it => it.ResourceType).Returns("Folder");
            var currentChildrenMock = new Mock<IExplorerItemViewModel>();
            currentChildrenMock.SetupGet(it => it.ResourceName).Returns("someResourceName");
            var childDestItem = new Mock<IExplorerItemViewModel>();
            _target.ResourceName = "someName";
            _target.ResourceType = "Folder";
            _target.IsFolder = true;
            _target.Children.Add(currentChildrenMock.Object);
            childDestItem.SetupGet(it => it.ResourceName).Returns(_target.ResourceName);
            childDestItem.SetupGet(it => it.ResourceType).Returns("Folder");
            childDestItem.SetupGet(it => it.ResourcePath).Returns("somePath");
            childDestItem.SetupGet(it => it.Children).Returns(new ObservableCollection<IExplorerItemViewModel>());
            destinationMock.SetupGet(it => it.Children).Returns(new ObservableCollection<IExplorerItemViewModel>()
            {
                childDestItem.Object
            });
            var studioUpdateManagerMock = new Mock<IStudioUpdateManager>();

            _serverMock.SetupGet(it => it.UpdateRepository).Returns(studioUpdateManagerMock.Object);
            //act
            var result = await _target.MoveAsync(destinationMock.Object);
            //assert
            NUnit.Framework.Assert.IsFalse(result);
            _explorerRepositoryMock.Verify(it => it.Move(_target, destinationMock.Object));
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        public async System.Threading.Tasks.Task TestMoveException()
        {
            //arrange
            var destinationMock = new Mock<IExplorerTreeItem>();
            destinationMock.Setup(it => it.ResourceType).Returns("ServerSource");
            destinationMock.SetupGet(it => it.ResourcePath).Returns("someDestPath");
            var childDestItem = new Mock<IExplorerItemViewModel>();
            childDestItem.SetupGet(it => it.ResourceName).Returns("someOtherName");
            childDestItem.SetupGet(it => it.ResourceType).Returns("Folder");
            childDestItem.SetupGet(it => it.ResourcePath).Returns("somePath");
            childDestItem.SetupGet(it => it.Children).Returns(new ObservableCollection<IExplorerItemViewModel>());
            destinationMock.Setup(it => it.AddChild(_target)).Throws(new Exception());
            destinationMock.SetupGet(it => it.Children).Returns(new ObservableCollection<IExplorerItemViewModel>()
            {
                childDestItem.Object
            });
            _target.ResourceName = "someName";
            _target.ResourceType = "WebSource";

            var studioUpdateManagerMock = new Mock<IStudioUpdateManager>();
            _explorerRepositoryMock.Setup(a => a.Move(It.IsAny<IExplorerItemViewModel>(), It.IsAny<IExplorerTreeItem>())).Throws(new Exception());
            _serverMock.SetupGet(it => it.UpdateRepository).Returns(studioUpdateManagerMock.Object);
            //act
            var result = await _target.MoveAsync(destinationMock.Object);
            //assert
            NUnit.Framework.Assert.IsFalse(result);
        }

        #endregion Test methods
    }
}
