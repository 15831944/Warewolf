﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Infrastructure;
using Dev2.Studio.Interfaces;
using NUnit.Framework;
using Moq;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class ManageWcfServiceModelTests
    {
        #region Fields

        ManageWcfServiceModel _target;

        Guid _localhostServerEnvironmentId;
        Mock<IShellViewModel> _shellViewModelMock;
        Mock<IServer> _localhostServerMock;
        Mock<IWindowsGroupPermission> _windowsGroupPermissionMock;

        Mock<IStudioUpdateManager> _mockStudioUpdateManager;
        Mock<IQueryManager> _mockQueryManager;

        #endregion Fields

        #region Test initialize

        [SetUp]
        public void TestInitialize()
        {
            _shellViewModelMock = new Mock<IShellViewModel>();
            _localhostServerEnvironmentId = Guid.NewGuid();
            _localhostServerMock = new Mock<IServer>();
            _localhostServerMock.Setup(it => it.EnvironmentID).Returns(_localhostServerEnvironmentId);
            _windowsGroupPermissionMock = new Mock<IWindowsGroupPermission>();
            _localhostServerMock.Setup(it => it.Permissions).Returns(new List<IWindowsGroupPermission>()
            {
                _windowsGroupPermissionMock.Object
            });
            //_localhostServerMock.Setup(it => it.GetServerConnections()).Returns(new List<IServer>());
            _localhostServerMock.SetupGet(it => it.DisplayName).Returns("localhostServerResourceName");
            _shellViewModelMock.SetupGet(it => it.LocalhostServer).Returns(_localhostServerMock.Object);

            _mockStudioUpdateManager = new Mock<IStudioUpdateManager>();
            _mockQueryManager = new Mock<IQueryManager>();

            _target = new ManageWcfServiceModel(_mockStudioUpdateManager.Object, _mockQueryManager.Object, _shellViewModelMock.Object, _localhostServerMock.Object);
        }

        #endregion Test initialize

        [Test]
        [Timeout(60000)]
        public void TestRetrieveSources()
        {
            var expectedResult = new ObservableCollection<IWcfServerSource>();
            _mockQueryManager.Setup(it => it.FetchWcfSources()).Returns(expectedResult);

            var result = _target.RetrieveSources();

            Assert.AreEqual(expectedResult.Count, result.Count);
            _mockQueryManager.Verify(it => it.FetchWcfSources());
        }

        [Test]
        [Timeout(60000)]
        public void TestGetActions()
        {
            var expectedResult = new ObservableCollection<IWcfAction>();
            var mockWcfServerSource = new Mock<IWcfServerSource>();
            _mockQueryManager.Setup(it => it.WcfActions(mockWcfServerSource.Object)).Returns(expectedResult);

            var result = _target.GetActions(mockWcfServerSource.Object);

            Assert.AreEqual(expectedResult.Count, result.Count);
            _mockQueryManager.Verify(it => it.WcfActions(mockWcfServerSource.Object));
        }

        [Test]
        [Timeout(60000)]
        public void TestCreateNewSource()
        {
            try
            {
                _target.CreateNewSource();
            }
            catch (Exception ex)
            {
                Assert.Fail("Create new web source failed. Exception: " + ex.Message);
            }
        }

        [Test]
        [Timeout(60000)]
        public void TestEditSqlSource()
        {
            var mockWcfServiceSource = new Mock<IWcfServerSource>();
            mockWcfServiceSource.Setup(source => source.Name).Returns("WcfSource");
            mockWcfServiceSource.Setup(source => source.ResourceName).Returns("WcfSourceName");
            mockWcfServiceSource.Setup(source => source.EndpointUrl).Returns("SomeUrl");

            _target.EditSource(mockWcfServiceSource.Object);
        }

        [Test]
        [Timeout(60000)]
        public void TestService()
        {
            var mockWcfServiceValues = new Mock<IWcfService>();
            var expectedResult = string.Empty;
            _mockStudioUpdateManager.Setup(it => it.TestWcfService(mockWcfServiceValues.Object)).Returns(expectedResult);

            var result = _target.TestService(mockWcfServiceValues.Object);

            Assert.AreSame(expectedResult, result);
            _mockStudioUpdateManager.Verify(it => it.TestWcfService(mockWcfServiceValues.Object));
        }
    }
}
