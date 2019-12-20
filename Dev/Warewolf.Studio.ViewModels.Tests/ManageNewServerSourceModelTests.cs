﻿using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Warewolf.Studio.ViewModels.Tests
{
    using Dev2.Common.Interfaces;

    using Moq;

    [TestFixture]
    [SetUpFixture]
    public class ManageNewServerSourceModelTests
    {
        #region Fields

        Mock<IStudioUpdateManager> _updateRepositoryMock;
        Mock<IQueryManager> _queryProxyMock;

        string _serverName;

        ManageNewServerSourceModel _target;

        #endregion Fields

        #region Test initialize

        [SetUp]
        public void TestInitialize()
        {
            _updateRepositoryMock = new Mock<IStudioUpdateManager>();
            _queryProxyMock = new Mock<IQueryManager>();
            _serverName = Guid.NewGuid().ToString();
            _target = new ManageNewServerSourceModel(_updateRepositoryMock.Object, _queryProxyMock.Object, _serverName);
        }

        #endregion Test initialize

        #region Test methods

        [Test]
        [Timeout(60000)]
        public void TestGetComputerNames()
        {
            //arrange
            var expectedResult = new List<string>();
            _queryProxyMock.Setup(it => it.GetComputerNames()).Returns(expectedResult);

            //act
            var result = _target.GetComputerNames();

            //assert
            Assert.AreSame(expectedResult, result);
            _queryProxyMock.Verify(it => it.GetComputerNames());
        }

        [Test]
        [Timeout(60000)]
        public void TestTestConnection()
        {
            //arrange
            var resourceMock = new Mock<IServerSource>();

            //act
            _target.TestConnection(resourceMock.Object);

            //assert
            _updateRepositoryMock.Verify(it => it.TestConnection(resourceMock.Object));
        }

        [Test]
        [Timeout(60000)]
        public void TestSave()
        {
            //arrange
            var resourceMock = new Mock<IServerSource>();

            //act
            _target.Save(resourceMock.Object);

            //assert
            _updateRepositoryMock.Verify(it => it.Save(resourceMock.Object));
        }

        #endregion Test methods

        #region Test properties

        [Test]
        [Timeout(60000)]
        public void TestServerName()
        {
            //act
            var value = _target.ServerName;

            //assert
            Assert.AreEqual(_serverName, value);
        }

        [Test]
        [Timeout(60000)]
        public void TestServerSourceServerNameBrackets()
        {
            //arrange  
            _target = new ManageNewServerSourceModel(_updateRepositoryMock.Object, _queryProxyMock.Object, _serverName + "(sthInBrackets)");

            //act
            var value = _target.ServerName;

            //assert
            Assert.AreEqual(_serverName, value);
        }

        #endregion Test properties
    }
}