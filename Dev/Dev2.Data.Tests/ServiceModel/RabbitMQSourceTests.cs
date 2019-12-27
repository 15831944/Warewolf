/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Xml.Linq;
using Dev2.Data.ServiceModel;
using NUnit.Framework;

namespace Dev2.Data.Tests.ServiceModel
{
    [TestFixture]
    public class RabbitMQSourceTests
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(RabbitMQSource))]
        public void RabbitMQSource_Validate_DefaultValues()
        {
            var rabbitMqSource = new RabbitMQSource();
            NUnit.Framework.Assert.IsTrue(rabbitMqSource.IsSource);
            NUnit.Framework.Assert.IsFalse(rabbitMqSource.IsService);
            NUnit.Framework.Assert.IsFalse(rabbitMqSource.IsFolder);
            NUnit.Framework.Assert.IsFalse(rabbitMqSource.IsReservedService);
            NUnit.Framework.Assert.IsFalse(rabbitMqSource.IsServer);
            NUnit.Framework.Assert.IsFalse(rabbitMqSource.IsResourceVersion);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(RabbitMQSource))]
        public void RabbitMQSource_Validate_ToXml_DefaultValues()
        {
            const string xmlString = @"<Source ID=""1a82a341-b678-4992-a25a-39cdd57198d4"" Name=""Example Rabbit MQ Source"" ResourceType=""RabbitMQSource"" IsValid=""false"" 
                                               ConnectionString=""HostName=localhost;Port=;UserName=warewolf;Password=test123;VirtualHost=hostyhost/"" Type=""RabbitMQSource"" ServerVersion=""1.4.1.27"" ServerID=""693ca20d-fb17-4044-985a-df3051d6bac7"">
                                          <DisplayName>Example Rabbit MQ Source</DisplayName>
                                          <AuthorRoles>
                                          </AuthorRoles>
                                          <ErrorMessages />
                                          <TypeOf>RabbitMQSource</TypeOf>
                                          <VersionInfo DateTimeStamp=""2017-05-26T14:21:24.3247847+02:00"" Reason="""" User=""NT AUTHORITY\SYSTEM"" VersionNumber=""3"" ResourceId=""1a82a341-b678-4992-a25a-39cdd57198d4"" VersionId=""b1a6de00-3cac-41cd-b0ed-9fac9bb61266"" />
                                        </Source>";

            var xElement = XElement.Parse(xmlString);
            var rabbitMqSource = new RabbitMQSource(xElement);
            var result = rabbitMqSource.ToXml();

            var rabbitMqSourceWithXml = new RabbitMQSource(result);
            NUnit.Framework.Assert.AreEqual(nameof(RabbitMQSource), rabbitMqSourceWithXml.ResourceType);
            NUnit.Framework.Assert.AreEqual(5672, rabbitMqSourceWithXml.Port);
            NUnit.Framework.Assert.AreEqual("localhost", rabbitMqSourceWithXml.HostName);
            NUnit.Framework.Assert.AreEqual("warewolf", rabbitMqSourceWithXml.UserName);
            NUnit.Framework.Assert.AreEqual("test123", rabbitMqSourceWithXml.Password);
            NUnit.Framework.Assert.AreEqual("hostyhost/", rabbitMqSourceWithXml.VirtualHost);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(RabbitMQSource))]
        public void RabbitMQSource_Constructor_Validate_DefaultValues()
        {
            var rabbitMqSource = new RabbitMQSource();
            NUnit.Framework.Assert.IsNotNull(rabbitMqSource.ResourceID);
            NUnit.Framework.Assert.AreEqual(Guid.Empty, rabbitMqSource.ResourceID);
            NUnit.Framework.Assert.AreEqual(nameof(RabbitMQSource), rabbitMqSource.ResourceType);
            NUnit.Framework.Assert.AreEqual(5672, rabbitMqSource.Port);
            NUnit.Framework.Assert.AreEqual("/", rabbitMqSource.VirtualHost);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(RabbitMQSource))]
        public void RabbitMQSource_GivenXElement_WithoutValues_Constructor_Validate_DefaultValues()
        {
            const string xmlString = @"<Source ID=""2aa3fdba-e0c3-47dd-8dd5-e6f24aaf5c7a"" Name=""test server"" Type=""Dev2Server"" ConnectionString=""AppServerUri=http://178.63.172.163:3142/dsf;WebServerPort=3142;AuthenticationType=Public;UserName=;Password="" Version=""1.0"" ResourceType=""Server"" ServerID=""51a58300-7e9d-4927-a57b-e5d700b11b55"">
                                      <TypeOf>Dev2Server</TypeOf>
                                      <DisplayName>test server</DisplayName>
                                      <Category>WAREWOLF SERVERS</Category>
                                      <Signature xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                        <SignedInfo>
                                          <CanonicalizationMethod Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315"" />
                                          <SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1"" />
                                          <Reference URI="""">
                                            <Transforms>
                                              <Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
                                            </Transforms>
                                            <DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1"" />
                                            <DigestValue>1ia51dqx+BIMQ4QgLt+DuKtTBUk=</DigestValue>
                                          </Reference>
                                        </SignedInfo>
                                        <SignatureValue>Wqd39EqkFE66XVETuuAqZveoTk3JiWtAk8m1m4QykeqY4/xQmdqRRSaEfYBr7EHsycI3STuILCjsz4OZgYQ2QL41jorbwULO3NxAEhu4nrb2EolpoNSJkahfL/N9X5CvLNwpburD4/bPMG2jYegVublIxE50yF6ZZWG5XiB6SF8=</SignatureValue>
                                      </Signature>
                                    </Source>";

            var xElement = XElement.Parse(xmlString);
            var rabbitMqSource = new RabbitMQSource(xElement);
            NUnit.Framework.Assert.AreEqual(nameof(RabbitMQSource), rabbitMqSource.ResourceType);
            NUnit.Framework.Assert.AreEqual(5672, rabbitMqSource.Port);
            NUnit.Framework.Assert.AreEqual("", rabbitMqSource.HostName);
            NUnit.Framework.Assert.AreEqual("", rabbitMqSource.UserName);
            NUnit.Framework.Assert.AreEqual("", rabbitMqSource.Password);
            NUnit.Framework.Assert.AreEqual("/", rabbitMqSource.VirtualHost);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(RabbitMQSource))]
        public void RabbitMQSource_GivenXElement_WithValues_Constructor_Validate_DefaultValues()
        {
            const string xmlString = @"<Source ID=""1a82a341-b678-4992-a25a-39cdd57198d4"" Name=""Example Rabbit MQ Source"" ResourceType=""RabbitMQSource"" IsValid=""false"" 
                                               ConnectionString=""HostName=localhost;Port=;UserName=warewolf;Password=test123;VirtualHost=hostyhost/"" Type=""RabbitMQSource"" ServerVersion=""1.4.1.27"" ServerID=""693ca20d-fb17-4044-985a-df3051d6bac7"">
                                          <DisplayName>Example Rabbit MQ Source</DisplayName>
                                          <AuthorRoles>
                                          </AuthorRoles>
                                          <ErrorMessages />
                                          <TypeOf>RabbitMQSource</TypeOf>
                                          <VersionInfo DateTimeStamp=""2017-05-26T14:21:24.3247847+02:00"" Reason="""" User=""NT AUTHORITY\SYSTEM"" VersionNumber=""3"" ResourceId=""1a82a341-b678-4992-a25a-39cdd57198d4"" VersionId=""b1a6de00-3cac-41cd-b0ed-9fac9bb61266"" />
                                        </Source>";

            var xElement = XElement.Parse(xmlString);
            var rabbitMqSource = new RabbitMQSource(xElement);
            NUnit.Framework.Assert.AreEqual(nameof(RabbitMQSource), rabbitMqSource.ResourceType);
            NUnit.Framework.Assert.AreEqual(5672, rabbitMqSource.Port);
            NUnit.Framework.Assert.AreEqual("localhost", rabbitMqSource.HostName);
            NUnit.Framework.Assert.AreEqual("warewolf", rabbitMqSource.UserName);
            NUnit.Framework.Assert.AreEqual("test123", rabbitMqSource.Password);
            NUnit.Framework.Assert.AreEqual("hostyhost/", rabbitMqSource.VirtualHost);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(RabbitMQSource))]
        [Depends(Depends.ContainerType.RabbitMQ)]
        public void RabbitMQSource_NewConnection_Success()
        {
            //-------------------------------Arrange-----------------------------
            string xmlString = $@"<Source ID=""1a82a341-b678-4992-a25a-39cdd57198d4"" Name=""Example Rabbit MQ Source"" ResourceType=""RabbitMQSource"" IsValid=""false"" 
                                               ConnectionString=""HostName={Depends.RigOpsIP};Port=;UserName=test;Password=test;VirtualHost=/"" Type=""RabbitMQSource"" ServerVersion=""1.4.1.27"" ServerID=""693ca20d-fb17-4044-985a-df3051d6bac7"">
                                          <DisplayName>Example Rabbit MQ Source</DisplayName>
                                          <AuthorRoles>
                                          </AuthorRoles>
                                          <ErrorMessages />
                                          <TypeOf>RabbitMQSource</TypeOf>
                                          <VersionInfo DateTimeStamp=""2017-05-26T14:21:24.3247847+02:00"" Reason="""" User=""NT AUTHORITY\SYSTEM"" VersionNumber=""1"" ResourceId=""1a82a341-b678-4992-a25a-39cdd57198d4"" VersionId=""b1a6de00-3cac-41cd-b0ed-9fac9bb61266"" />
                                        </Source>";

            var xElement = XElement.Parse(xmlString);
            var rabbitMqSource = new RabbitMQSource(xElement);
            //----------------------Pre-Assert---------------------------------
            NUnit.Framework.Assert.AreEqual(nameof(RabbitMQSource), rabbitMqSource.ResourceType);
            NUnit.Framework.Assert.AreEqual(5672, rabbitMqSource.Port);
            NUnit.Framework.Assert.AreEqual(Depends.RigOpsIP, rabbitMqSource.HostName);
            NUnit.Framework.Assert.AreEqual("test", rabbitMqSource.UserName);
            NUnit.Framework.Assert.AreEqual("test", rabbitMqSource.Password);
            NUnit.Framework.Assert.AreEqual("/", rabbitMqSource.VirtualHost);

            //-------------------------------Act---------------------------------
            using (var rabbitConnection = rabbitMqSource.NewConnection())
            {
                //-------------------------------Assert------------------------------
                NUnit.Framework.Assert.IsTrue(rabbitConnection.IsOpen);
            }
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(RabbitMQSource))]
        [Depends(Depends.ContainerType.RabbitMQ)]
        public void RabbitMQSource_NewConnection_GivenNoArgConstructor_ConnectionSuccess()
        {
            //-------------------------------Arrange-----------------------------
            var port = 5672;
            var hostName = Depends.RigOpsIP;
            var userName = "test";
            var password = "test";
            var virtualHost = "/";

            var rabbitMqSource = new RabbitMQSource
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
                Port = port,
                VirtualHost = virtualHost,
            };
            //----------------------Pre-Assert---------------------------------
            NUnit.Framework.Assert.AreEqual(nameof(RabbitMQSource), rabbitMqSource.ResourceType);
            NUnit.Framework.Assert.AreEqual(5672, rabbitMqSource.Port);
            NUnit.Framework.Assert.AreEqual(Depends.RigOpsIP, rabbitMqSource.HostName);
            NUnit.Framework.Assert.AreEqual("test", rabbitMqSource.UserName);
            NUnit.Framework.Assert.AreEqual("test", rabbitMqSource.Password);
            NUnit.Framework.Assert.AreEqual("/", rabbitMqSource.VirtualHost);

            //-------------------------------Act---------------------------------
            using (var rabbitConnection = rabbitMqSource.NewConnection())
            {
                //-------------------------------Assert------------------------------
                NUnit.Framework.Assert.IsTrue(rabbitConnection.IsOpen);
            }
        }
    }
}
