using NUnit.Framework;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    public class ExplorerTooltipsTests
    {
        ExplorerTooltips _target;

        [SetUp]
        public void TestInitialize()
        {
            _target = new ExplorerTooltips();
        }

        [Test]
        [Timeout(60000)]
        public void TestNewServiceTooltip()
        {
            _target.NewServiceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewServiceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewServerSourceTooltip()
        {
            _target.NewServerSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewServerSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewSqlServerSourceTooltip()
        {
            _target.NewSqlServerSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewSqlServerSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewMySqlSourceTooltip()
        {
            _target.NewMySqlSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewMySqlSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewPostgreSqlSourceTooltip()
        {
            _target.NewPostgreSqlSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewPostgreSqlSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewOracleSourceTooltip()
        {
            _target.NewOracleSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewOracleSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewOdbcSourceTooltip()
        {
            _target.NewOdbcSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewOdbcSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewWebSourceTooltip()
        {
            _target.NewWebSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewWebSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewRedisSourceTooltip()
        {
            _target.NewRedisSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewRedisSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewPluginSourceTooltip()
        {
            _target.NewPluginSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewPluginSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewComPluginSourceTooltip()
        {
            _target.NewComPluginSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewComPluginSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewEmailSourceTooltip()
        {
            _target.NewEmailSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewEmailSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewExchangeSourceTooltip()
        {
            _target.NewExchangeSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewExchangeSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewRabbitMqSourceTooltip()
        {
            _target.NewRabbitMqSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewRabbitMqSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewDropboxSourceTooltip()
        {
            _target.NewDropboxSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewDropboxSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewSharepointSourceTooltip()
        {
            _target.NewSharepointSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewSharepointSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewWcfSourceTooltip()
        {
            _target.NewWcfSourceTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewWcfSourceTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestNewFolderTooltip()
        {
            _target.NewFolderTooltip = Resources.Languages.Tooltips.NoPermissionsToolTip;
            Assert.AreEqual(Resources.Languages.Tooltips.NoPermissionsToolTip, _target.NewFolderTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestViewApisJsonTooltip()
        {
            _target.ViewApisJsonTooltip = Resources.Languages.Tooltips.ViewApisJsonTooltip;
            Assert.AreEqual(Resources.Languages.Tooltips.ViewApisJsonTooltip, _target.ViewApisJsonTooltip);
        }

        [Test]
        [Timeout(60000)]
        public void TestServerVersionTooltip()
        {
            _target.ServerVersionTooltip = Resources.Languages.Tooltips.ServerVersionTooltip;
            Assert.AreEqual(Resources.Languages.Tooltips.ServerVersionTooltip, _target.ServerVersionTooltip);
        }

    }
}
