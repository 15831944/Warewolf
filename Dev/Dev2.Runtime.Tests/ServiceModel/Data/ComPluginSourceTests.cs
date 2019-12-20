using System;
using System.Xml.Linq;
using Dev2.Runtime.ServiceModel.Data;
using NUnit.Framework;


namespace Dev2.Tests.Runtime.ServiceModel
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class ComPluginSourceTests
    {
  
        #region CTOR

        [Test]
        public void ComPluginSourceContructorWithDefaultExpectedInitializesProperties()
        {
            var source = new ComPluginSource();
            NUnit.Framework.Assert.AreEqual(Guid.Empty, source.ResourceID);
            NUnit.Framework.Assert.AreEqual("ComPluginSource", source.ResourceType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComPluginSourceContructorWithNullXmlExpectedThrowsArgumentNullException()
        {
            var source = new ComPluginSource(null);
        }

        [Test]
        public void ComPluginSourceContructorWithInvalidXmlExpectedDoesNotThrowExceptionAndInitializesProperties()
        {
            var xml = new XElement("root");
            var source = new ComPluginSource(xml);
            NUnit.Framework.Assert.AreNotEqual(Guid.Empty, source.ResourceID);
            NUnit.Framework.Assert.IsTrue(source.IsUpgraded);
            NUnit.Framework.Assert.AreEqual("ComPluginSource", source.ResourceType);
        }

        #endregion

        #region ToXml

        [Test]
        public void ComPluginSourceToXmlExpectedSerializesProperties()
        {
            var expected = new ComPluginSource
            {
                ClsId = "Plugins\\someDllIMadeUpToTest.dll",
                Is32Bit = false,
            };

            var xml = expected.ToXml();

            var actual = new ComPluginSource(xml);

            NUnit.Framework.Assert.AreEqual(expected.ResourceType, actual.ResourceType);
            NUnit.Framework.Assert.AreEqual(expected.ClsId, actual.ClsId);
            NUnit.Framework.Assert.AreEqual(expected.Is32Bit, actual.Is32Bit);
        }

        [Test]
        public void ComPluginSourceToXmlWithNullPropertiesExpectedSerializesPropertiesAsEmpty()
        {
            var expected = new ComPluginSource
            {
                ClsId = null,
                Is32Bit = false,
            };

            var xml = expected.ToXml();

            var actual = new ComPluginSource(xml);

            NUnit.Framework.Assert.AreEqual(expected.ResourceType, actual.ResourceType);
            NUnit.Framework.Assert.AreEqual("", actual.ClsId);
            NUnit.Framework.Assert.AreEqual(false, actual.Is32Bit);
        }

        #endregion
    }
}