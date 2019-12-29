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
using System.Linq;
using System.Xml.Linq;
using Dev2.Runtime.Configuration.Settings;
using Dev2.Runtime.Configuration.Tests.XML;
using NUnit.Framework;
using Warewolf.UnitTestAttributes;

namespace Dev2.Runtime.Configuration.Tests.Settings
{
    [TestFixture]
    public class ConfigurationTests
    {
        #region CTOR

        [Test]
        public void ConstructorWithDefaultExpectedInitializesAllProperties()
        {
            var config = new Configuration.Settings.Configuration("localhost");
            ValidateInitializesAllProperties(config);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorWithNullXmlExpectedThrowsArgumentNullException()
        {
            
            var config = new Configuration.Settings.Configuration((XElement)null);
            
        } 
        
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorWithNullWebServerUriExpectedThrowsArgumentNullException()
        {
            
            var config = new Configuration.Settings.Configuration((string)null);
            
        }
        

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithInvalidXmlVersionExpectedThrowsArgumentException()
        {
            
            var config = new Configuration.Settings.Configuration(new XElement("x", new XElement("y"), new XElement("z")));
            
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorWithInvalidXmlExpectedThrowsArgumentNullException()
        {
              
            var config = new Configuration.Settings.Configuration(new XElement("x", new XAttribute("Version", "1.0"), new XElement("y"), new XElement("z")));
            
        }

        [Test]
        public void ConstructorWithValidXmlArgumentExpectedInitializesAllProperties()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            ValidateInitializesAllProperties(config);
        }       
        
        [Test]
        public void ConstructorWithValidXmlNullWebServerUriArgumentExpectedThrowsArgumentNullException()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            ValidateInitializesAllProperties(config);
        }

        #endregion

        #region ToXmlExpectedReturnsXml

        [Test]
        public void ToXmlExpectedReturnsXml()
        {
            var config = new Configuration.Settings.Configuration("localhost");
            var result = config.ToXml();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(XElement), result);
        }

        [Test]
        public void ToXmlExpectedInvokesToXmlForEachProperty()
        {
            var config = new Configuration.Settings.Configuration("localhost");
            var result = config.ToXml();

            var properties = config.GetType().GetProperties();
            foreach(var property in properties)
            {
                var value = property.GetValue(config);

                Version version;
                SettingsBase settings;

                if((settings = value as SettingsBase) != null)
                {
                    var expected = settings.ToXml().ToString(SaveOptions.DisableFormatting);
                    
                    var actual = result.Element(settings.SettingName).ToString(SaveOptions.DisableFormatting);
                    
                    Assert.AreEqual(expected, actual);
                }
                else if((version = value as Version) != null)
                {
                    var actual = result.AttributeSafe(property.Name);
                    var expected = version.ToString(2);
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [Test]
        public void HasErrorReturnsFalse()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            Assert.IsFalse(config.HasError);
        }

        [Test]
        public void HasErrorReturnsTrueWhenLoggingError()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            config.Logging.Error = "Error";
            Assert.IsTrue(config.HasError);
        }

        [Test]
        public void HasErrorReturnsTrueWhenSecurityError()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            config.Security.Error = "Error";
            Assert.IsTrue(config.HasError);
        }

        [Test]
        public void HasErrorReturnsTrueWhenBackupError()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            config.Backup.Error = "Error";
            Assert.IsTrue(config.HasError);
        }

        [Test]
        public void LoggingSettingChangedExpectsHasChangesTrueWhenNotInitializating()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            config.Logging.IsDataAndTimeLogged = true;
            Assert.IsTrue(config.HasChanges);
        }

        [Test]
        public void LoggingSettingChangedExpectsHasChangesFalseWhenInitializating()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            Assert.IsFalse(config.HasChanges);
        }

        #endregion

        //
        // Static helpers
        //
       

        #region ValidateInitializesAllProperties

        static void ValidateInitializesAllProperties(Configuration.Settings.Configuration config)
        {
            var properties = config.GetType().GetProperties();

            foreach(var value in properties.Select(property => property.GetValue(config)))
            {
                Assert.IsNotNull(value);
            }
        }

        #endregion
    }
}
