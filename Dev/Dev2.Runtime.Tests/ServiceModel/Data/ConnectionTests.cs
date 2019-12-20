/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Data.ServiceModel;
using Dev2.Runtime.ServiceModel.Data;
using Dev2.Tests.Runtime.XML;
using NUnit.Framework;
using Newtonsoft.Json;
using System;
using System.Xml.Linq;
namespace Dev2.Tests.Runtime.ServiceModel.Data
{
    [TestFixture]
    [SetUpFixture]
    public class ConnectionTests
    {

        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_GetTestConnectionAddress_WhenDsfPresent_ExpectSameAddress()
        {
            //------------Setup for test--------------------------
            const string address = "http://localhost:3142/dsf";
            var conn = new Connection
            {
                ResourceType = "Server",
                Address = address
            };


            //------------Execute Test---------------------------
            var result = conn.FetchTestConnectionAddress();

            //------------Assert Results-------------------------
            NUnit.Framework.StringAssert.Contains(result, address);

        }

        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_GetTestConnectionAddress_WhenOnlySlashPresent_ExpectDsfAdded()
        {

            //------------Setup for test--------------------------
            const string address = "http://localhost:3142/";
            const string expected = address + "dsf";
            var conn = new Connection
            {
                ResourceType = "Server",
                Address = address
            };


            //------------Execute Test---------------------------
            var result = conn.FetchTestConnectionAddress();

            //------------Assert Results-------------------------
            NUnit.Framework.StringAssert.Contains(result, expected);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_GetTestConnectionAddress_WhenNoSlashPresent_ExpectSlashAndDsfAdded()
        {
            //------------Setup for test--------------------------
            const string address = "http://localhost:3142";
            const string expected = address + "/dsf";
            var conn = new Connection
            {
                ResourceType = "Server",
                Address = address
            };


            //------------Execute Test---------------------------
            var result = conn.FetchTestConnectionAddress();

            //------------Assert Results-------------------------
            NUnit.Framework.StringAssert.Contains(result, expected);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_ToString_FullySetupObject_Expected_JSONSerializedObjectReturnedAsString()
        {
            var testConnection = SetupDefaultConnection();
            var actualConnectionToString = testConnection.ToString();
            var expected = JsonConvert.SerializeObject(testConnection);
            NUnit.Framework.Assert.AreEqual(expected, actualConnectionToString);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_ToString_EmptyObject_Expected_()
        {
            var testConnection = new Connection();
            var actualSerializedConnection = testConnection.ToString();
            var expected = JsonConvert.SerializeObject(testConnection);
            NUnit.Framework.Assert.AreEqual(expected, actualSerializedConnection);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_ToXml_AllPropertiesSetup_Expected_XElementContainingAllObjectInformation()
        {

            var testConnection = SetupDefaultConnection();
            testConnection.AuthenticationType = AuthenticationType.User;
            var expectedXml = testConnection.ToXml();

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
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_ToXml_EmptyObject_Expected_XElementContainingNoInformationRegardingSource()
        {
            var testConnection = new Connection();
            var expectedXml = testConnection.ToXml();

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
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_Equals_Connection_Return_False()
        {
            var testConnection = new Connection()
            {
                Address = "http://127.0.0.1:1234",
                ResourceName = "TheConnection",
                ResourceID = Guid.NewGuid(),
                AuthenticationType = AuthenticationType.User,
                UserName = "Hagashen",
                Password = "password",
                WebServerPort = 1234
            };
            var testConnectionOther = new Connection()
            {
                Address = "http://127.0.0.1:1234",
                ResourceName = "TheConnection",
                ResourceID = Guid.NewGuid(),
                AuthenticationType = AuthenticationType.Public,
                UserName = "Hagashen2",
                Password = "password2",
                WebServerPort = 12343
            };
            NUnit.Framework.Assert.IsFalse(testConnection.Equals(testConnectionOther));
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_Equals_Object_Return_False()
        {
            var testConnection = SetupDefaultConnection();
            object other = new object();
            NUnit.Framework.Assert.IsFalse(testConnection.Equals(other));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_Equals_Object_Connection_Return_False()
        {
            var testConnection = SetupDefaultConnection();
            object other = new object();
            other = testConnection;
            NUnit.Framework.Assert.IsTrue(testConnection.Equals(other));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_Equals_Object_Null_Return_False()
        {
            var testConnection = SetupDefaultConnection();
            object other = new object();
            other = null;
            NUnit.Framework.Assert.IsFalse(testConnection.Equals(other));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_Ctor_XElement()
        {
            XElement _connectionXml;
            Connection _connection;

            _connectionXml = XmlResource.Fetch("ServerConnection2");
            _connection = new Connection(_connectionXml);
            NUnit.Framework.Assert.AreEqual(1235, _connection.WebServerPort);
            NUnit.Framework.Assert.AreEqual("http://127.0.0.2:1235/", _connection.Address);
            NUnit.Framework.Assert.AreEqual(AuthenticationType.User, _connection.AuthenticationType);
            NUnit.Framework.Assert.AreEqual("Dev2", _connection.Password);
            NUnit.Framework.Assert.AreEqual("Dev2Server", _connection.ResourceType);
            NUnit.Framework.Assert.AreEqual("http://127.0.0.2:1235/", _connection.WebAddress);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_GetHashCode()
        {
            var connection = SetupDefaultConnection();
            connection.GetHashCode();
            NUnit.Framework.Assert.AreNotEqual(0, connection.GetHashCode());
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("Connection")]
        public void Connection_GetHashCode_NullValues()
        {
            var testConnection = new Connection
            {
                Address = null,
                AuthenticationType = AuthenticationType.Windows,
                Password = null,
                ResourceID = Guid.NewGuid(),
                ResourceName = "TestResourceIMadeUp",
                ResourceType = "Server",
                UserName = null,
                WebServerPort = 8080
            };
            testConnection.GetHashCode();
            NUnit.Framework.Assert.AreNotEqual(0, testConnection.GetHashCode());
        }


        Connection SetupDefaultConnection()
        {
            var testConnection = new Connection
            {
                Address = "http://someAddressIMadeUpToTest:7654/Server",
                AuthenticationType = AuthenticationType.Windows,
                Password = "secret",
                ResourceID = Guid.NewGuid(),
                ResourceName = "TestResourceIMadeUp",
                ResourceType = "Server",
                UserName = @"Domain\User",
                WebServerPort = 8080
            };

            return testConnection;
        }
    }
}
