using System;
using Dev2.Common.Interfaces;
using NUnit.Framework;
using Moq;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class SharepointServerSourceModelTests
    {
        #region Fields

        Mock<IStudioUpdateManager> _updateRepositoryMock;

        string _serverName;

        SharepointServerSourceModel _target;
        Mock<IQueryManager> _queryManager;

        #endregion Fields

        #region Test initialize

        [SetUp]
        public void TestInitialize()
        {
            _updateRepositoryMock = new Mock<IStudioUpdateManager>();
            _queryManager = new Mock<IQueryManager>();
            _serverName = Guid.NewGuid().ToString();
            _target = new SharepointServerSourceModel(_updateRepositoryMock.Object,_queryManager.Object, _serverName);
        }

        #endregion Test initialize

        #region Test methods

        [Test]
        [Timeout(60000)]
        public void TestTestConnection()
        {
            //arrange
            var resourceMock = new Mock<ISharepointServerSource>();

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
            var resourceMock = new Mock<ISharepointServerSource>();

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
            //arrange
            var expectedValue = "someValue";

            //act
            _target.ServerName = expectedValue;
            var value = _target.ServerName;

            //assert
            Assert.AreEqual(expectedValue, value);
        }

        #endregion Test properties
    }
}