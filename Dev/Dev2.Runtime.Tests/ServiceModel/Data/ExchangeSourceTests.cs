using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Core.DynamicServices;
using Dev2.Runtime.ServiceModel.Data;
using Microsoft.Exchange.WebServices.Data;
using NUnit.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;



namespace Dev2.Tests.Runtime.ServiceModel
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class ExchangeSourceTests
    {
        public const string TestOwner = "Bernardt Joubert";
        public const string Category = "Exchange Email";

        ExchangeSource SetupDefaultSource()
        {
            return new ExchangeSource(new FakeEmailSender())
            {
                AutoDiscoverUrl = "http/test.com",
                UserName = "testuser",
                Password = "testPassword",
                Type = enSourceType.ExchangeSource,
                ResourceID = Guid.NewGuid(),
                ResourceName = "TestResourceIMadeUp"
            };
        }

        [Test]
        [Author(TestOwner)]
        [Category(Category)]
        public void ToStringFullySetupObjectExpectedJsonSerializedObjectReturnedAsString()
        {
            var testDbSource = SetupDefaultSource();
            var actualDbSourceToString = testDbSource.ToString();
            var expected = JsonConvert.SerializeObject(testDbSource);
            NUnit.Framework.Assert.AreEqual(expected, actualDbSourceToString);
        }

        [Test]
        [Author(TestOwner)]
        [Category(Category)]
        public void ToStringEmptyObjectExpected()
        {
            var testDbSource = new ExchangeSource();
            var actualSerializedDbSource = testDbSource.ToString();
            var expected = JsonConvert.SerializeObject(testDbSource);
            NUnit.Framework.Assert.AreEqual(expected, actualSerializedDbSource);
        }

        [Test]
        [Author(TestOwner)]
        [Category(Category)]
        public void ExchangeSource_SetUpProerties_ReturnsNoErrors()
        {
            var testDbSource = new ExchangeSource
            {
                DataList = "This is test string",
                EmailFrom = "test@email.com",
                EmailTo = "test@email.com",
                ResourceName = "testName",
                Path = "Test Path",
                TestFromAddress = "test@email.com",
                Type = enSourceType.ExchangeSource,
                Timeout = 1000,
                TestToAddress = "test@email.com",
                UserName = "test",
                Password = "test"
            };

            NUnit.Framework.Assert.IsNotNull(testDbSource.DataList);
            NUnit.Framework.Assert.IsNotNull(testDbSource.EmailFrom);
            NUnit.Framework.Assert.IsNotNull(testDbSource.EmailTo);
            NUnit.Framework.Assert.IsNotNull(testDbSource.Path);
            NUnit.Framework.Assert.IsNotNull(testDbSource.TestFromAddress);
            NUnit.Framework.Assert.IsNotNull(testDbSource.Type);
            NUnit.Framework.Assert.IsNotNull(testDbSource.Timeout);
            NUnit.Framework.Assert.IsNotNull(testDbSource.TestToAddress);
            NUnit.Framework.Assert.IsNotNull(testDbSource.UserName);
            NUnit.Framework.Assert.IsNotNull(testDbSource.Password);
        }

        [Test]
        [Author(TestOwner)]
        [Category(Category)]
        public void ExchangeSource_Equals()
        {
            var testDbSource = new ExchangeSource();
            var result = testDbSource.Equals(testDbSource);

            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author(TestOwner)]
        [Category(Category)]
        public void ToXmlAllPropertiesSetupExpectedXElementContainingAllObjectInformation()
        {
            var testDbSource = SetupDefaultSource();
            var expectedXml = testDbSource.ToXml();
            var workflowXamlDefintion = expectedXml.Element("XamlDefinition");
            var attrib = expectedXml.Attributes();
            var attribEnum = attrib.GetEnumerator();
            while (attribEnum.MoveNext())
            {
                if (attribEnum.Current.Name == "Name")
                {
                    NUnit.Framework.Assert.AreEqual("TestResourceIMadeUp", attribEnum.Current.Value);
                    break;
                }
            }
            NUnit.Framework.Assert.IsNull(workflowXamlDefintion);
        }

        [Test]
        [Author(TestOwner)]
        [Category(Category)]
        public void ToXmlEmptyObjectExpectedXElementContainingNoInformationRegardingSource()
        {
            var testDbSource = new ExchangeSource();
            var expectedXml = testDbSource.ToXml();

            var attrib = expectedXml.Attributes();
            var attribEnum = attrib.GetEnumerator();
            while (attribEnum.MoveNext())
            {
                if (attribEnum.Current.Name == "Name")
                {
                    NUnit.Framework.Assert.AreEqual(string.Empty, attribEnum.Current.Value);
                    break;
                }
            }
        }

        [Test]
        [Author(TestOwner)]
        [Category(Category)]
        public void ToXmlRegardingSource()
        {
            var testDbSource = SetupDefaultSource();
            var expectedXml = testDbSource.ToXml();

            testDbSource = new ExchangeSource(expectedXml);
            NUnit.Framework.Assert.IsNotNull(testDbSource);
        }

        [Test]
        [Author(TestOwner)]
        [Category(Category)]
        public void ExchangeSource_Send_Success()
        {
            var testDbSource = SetupDefaultSource();
            var message = new ExchangeTestMessage()
            {
                Attachments = new List<string> { "testpath" },
                BcCs = new List<string> { "testmail" },
                CCs = new List<string> { "testcc" },
                Body = "this is a test maii",
                Subject = "this is a test",
                Tos = new List<string> { "testemails" }
            };

            testDbSource.Send(new FakeEmailSender(), message);

            NUnit.Framework.Assert.IsNotNull(message.Body);
            NUnit.Framework.Assert.IsNotNull(message.Attachments);
            NUnit.Framework.Assert.IsNotNull(message.BcCs);
            NUnit.Framework.Assert.IsNotNull(message.CCs);
            NUnit.Framework.Assert.IsNotNull(message.Subject);
            NUnit.Framework.Assert.IsNotNull(message.Tos);
        }
    }

    public class FakeEmailSender : IExchangeEmailSender
    {
        public void Send(Microsoft.Exchange.WebServices.Data.ExchangeService service, EmailMessage message)
        {
        }
    }
}