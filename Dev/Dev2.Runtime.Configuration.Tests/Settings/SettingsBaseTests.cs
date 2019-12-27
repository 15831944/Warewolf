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
using System.Data;
using System.Xml.Linq;
using NUnit.Framework;

namespace Dev2.Runtime.Configuration.Tests.Settings
{
    [TestFixture]
    public class SettingsBaseTests
    {
        #region CTOR

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorWithNullSettingsNameExpectedThrowsArgumentNullException()
        {
            var settings = new SettingsBaseMock(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorWithNullDisplayNameExpectedThrowsArgumentNullException()
        {
            var settings = new SettingsBaseMock("xx", null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorWithNullWebServerUriExpectedThrowsArgumentNullException()
        {
            var settings = new SettingsBaseMock("xx", "xxx", null);
        }

        [Test]
        public void ConstructorWithValidNameArgumentsExpectedSetsProperties()
        {
            var settings = new SettingsBaseMock("x", "y", "localhost");
            Assert.AreEqual("x", settings.SettingName);
            Assert.AreEqual("y", settings.DisplayName);
            Assert.AreEqual("localhost", settings.WebServerUri);
        }

        [Test]
        [ExpectedException(typeof(NoNullAllowedException))]
        public void ConstructorWithInvalidXmlArgumentExpectedThrowsNoNullAllowedException()
        {
            var settings = new SettingsBaseMock(new XElement("x", new XElement("y"), new XElement("z")), "webserverUri");
        }

        [Test]
        public void ConstructorWithValidXmlArgumentExpectedInitializesAllProperties()
        {
            var xml = new XElement("Settings", new XAttribute("DisplayName", "hello"));
            var settings = new SettingsBaseMock(xml, "localhost");

            Assert.AreEqual("hello", settings.DisplayName);
            Assert.AreEqual(xml.Name, settings.SettingName);
            Assert.AreEqual("localhost", settings.WebServerUri);
        }

        [Test]
        [ExpectedException(typeof(NoNullAllowedException))]
        public void ConstructorWithValidXmlArgumentNullWebserverExpectedException()
        {
            var xml = new XElement("Settings", new XAttribute("DisplayName", "hello"));
            var settings = new SettingsBaseMock(xml, null);

            Assert.AreEqual("hello", settings.DisplayName);
            Assert.AreEqual(xml.Name, settings.SettingName);
        }

        #endregion

        #region ToXmlExpectedReturnsXml

        [Test]
        public void ToXmlExpectedReturnsXml()
        {
            var settings = new SettingsBaseMock("x", "y", "localhost");
            var result = settings.ToXml();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(XElement), result);
        }

        [Test]
        public void ToXmlExpectedSerializesEachProperty()
        {
            var settings = new SettingsBaseMock("x", "y", "localhost");

            var result = settings.ToXml();
            Assert.AreEqual(settings.SettingName, result.Name);
            Assert.AreEqual(settings.DisplayName, result.AttributeSafe("DisplayName"));
        }

        #endregion
    }
}
