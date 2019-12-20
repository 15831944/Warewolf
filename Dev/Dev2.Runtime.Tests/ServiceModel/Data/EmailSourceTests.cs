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
using System.Net.Mail;
using System.Xml.Linq;
using Dev2.Runtime.ServiceModel.Data;
using Dev2.Tests.Runtime.XML;
using NUnit.Framework;

namespace Dev2.Tests.Runtime.ServiceModel.Data
{
    // PBI 953 - 2013.05.16 - TWR - Created
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class EmailSourceTests
    {
        #region CTOR

        [Test]
        public void EmailSourceContructorWithDefaultExpectedInitializesProperties()
        {
            var source = new EmailSource();
            NUnit.Framework.Assert.AreEqual(Guid.Empty, source.ResourceID);
            NUnit.Framework.Assert.AreEqual(nameof(EmailSource), source.ResourceType);
            NUnit.Framework.Assert.AreEqual(EmailSource.DefaultTimeout, source.Timeout);
            NUnit.Framework.Assert.AreEqual(EmailSource.DefaultPort, source.Port);
            NUnit.Framework.Assert.IsNull(source.DataList);
            NUnit.Framework.Assert.IsTrue(source.IsSource);
            NUnit.Framework.Assert.IsFalse(source.IsService);
            NUnit.Framework.Assert.IsFalse(source.IsFolder);
            NUnit.Framework.Assert.IsFalse(source.IsReservedService);
            NUnit.Framework.Assert.IsFalse(source.IsServer);
            NUnit.Framework.Assert.IsFalse(source.IsResourceVersion);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmailSource_Contructor_WithNullXmlExpected_ThrowsArgumentNullException()
        {
            var source = new EmailSource(null);
        }

        [Test]
        public void EmailSourceContructorWithInvalidXmlExpectedDoesNotThrowExceptionAndInitializesProperties()
        {
            var xml = new XElement("root");
            var source = new EmailSource(xml);
            NUnit.Framework.Assert.AreNotEqual(Guid.Empty, source.ResourceID);
            NUnit.Framework.Assert.IsTrue(source.IsUpgraded);
            NUnit.Framework.Assert.AreEqual(nameof(EmailSource), source.ResourceType);
            NUnit.Framework.Assert.AreEqual(EmailSource.DefaultTimeout, source.Timeout);
            NUnit.Framework.Assert.AreEqual(EmailSource.DefaultPort, source.Port);
        }


        [Test]
        public void EmailSourceContructorWithValidXmlExpectedInitializesProperties()
        {
            var xml = XmlResource.Fetch(nameof(EmailSource));

            var source = new EmailSource(xml);
            NUnit.Framework.Assert.AreEqual(Guid.Parse("bf810e43-3633-4638-9d0a-56473ef54151"), source.ResourceID);
            NUnit.Framework.Assert.AreEqual(nameof(EmailSource), source.ResourceType);
            NUnit.Framework.Assert.AreEqual("smtp.gmail.com", source.Host);
            NUnit.Framework.Assert.AreEqual(465, source.Port);
            NUnit.Framework.Assert.AreEqual(true, source.EnableSsl);
            NUnit.Framework.Assert.AreEqual(30000, source.Timeout);
            NUnit.Framework.Assert.AreEqual("user@gmail.com", source.UserName);
            NUnit.Framework.Assert.AreEqual("1234", source.Password);
        }

        [Test]
        public void EmailSourceContructorWithCorruptXmlExpectedInitializesProperties()
        {
            var xml = XmlResource.Fetch("EmailSourceCorrupt");

            var source = new EmailSource(xml);
            NUnit.Framework.Assert.AreEqual(Guid.Parse("bf810e43-3633-4638-9d0a-56473ef54151"), source.ResourceID);
            NUnit.Framework.Assert.AreEqual(nameof(EmailSource), source.ResourceType);
            NUnit.Framework.Assert.AreEqual("smtp.gmail.com", source.Host);
            NUnit.Framework.Assert.AreEqual(EmailSource.DefaultPort, source.Port);
            NUnit.Framework.Assert.AreEqual(false, source.EnableSsl);
            NUnit.Framework.Assert.AreEqual(EmailSource.DefaultTimeout, source.Timeout);
            NUnit.Framework.Assert.AreEqual("user@gmail.com", source.UserName);
            NUnit.Framework.Assert.AreEqual("1234", source.Password);
        }

        #endregion

        #region ToXml

        [Test]
        public void EmailSourceToXmlExpectedSerializesProperties()
        {
            var expected = new EmailSource
            {
                Host = "smtp.mydomain.com",
                Port = 25,
                EnableSsl = false,
                UserName = "user@mydomain.com",
                Password = "mypassword",
                Timeout = 1000,
                TestFromAddress = "user@mydomain.com",
                TestToAddress = "user2@mydomain2.com"
            };

            var xml = expected.ToXml();

            var actual = new EmailSource(xml);

            NUnit.Framework.Assert.AreEqual(expected.ResourceType, actual.ResourceType);
            NUnit.Framework.Assert.AreEqual(expected.Host, actual.Host);
            NUnit.Framework.Assert.AreEqual(expected.Port, actual.Port);
            NUnit.Framework.Assert.AreEqual(expected.EnableSsl, actual.EnableSsl);
            NUnit.Framework.Assert.AreEqual(expected.UserName, actual.UserName);
            NUnit.Framework.Assert.AreEqual(expected.Password, actual.Password);
            NUnit.Framework.Assert.AreEqual(expected.Timeout, actual.Timeout);
            NUnit.Framework.Assert.IsNull(actual.TestFromAddress);
            NUnit.Framework.Assert.IsNull(actual.TestToAddress);
        }

        [Test]
        [DeploymentItem("EnableDocker.txt")]
        [Author("Pieter Terblanche")]
        [Category(nameof(EmailSource))]
        public void EmailSource_Validate_DataList()
        {
            const string expectedDataList = "data list";

            var source = new EmailSource
            {
                DataList = expectedDataList
            };

            NUnit.Framework.Assert.AreEqual(expectedDataList, source.DataList);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(EmailSource))]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmailSource_Send_ExpectedException()
        {
            var expected = new EmailSource
            {
                Host = "smtp.mydomain.com",
                Port = 25,
                EnableSsl = false,
                UserName = "user@mydomain.com",
                Password = "mypassword",
                Timeout = 1000,
                TestFromAddress = "user@mydomain.com",
                TestToAddress = "user2@mydomain2.com"
            };

            var xml = expected.ToXml();

            var mailMessage = new MailMessage();
            var emailSource = new EmailSource(xml);
            emailSource.Send(mailMessage);
        }

        #endregion
    }
}
