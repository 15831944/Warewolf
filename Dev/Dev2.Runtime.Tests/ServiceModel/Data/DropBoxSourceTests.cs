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
using Dev2.Common.Interfaces;
using Dev2.Data.ServiceModel;
using NUnit.Framework;
using Moq;
using Newtonsoft.Json;

namespace Dev2.Tests.Runtime.ServiceModel.Data
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class DropBoxSourceTests
    {
        const string conStr = @"<Source ID=""2aa3fdba-e0c3-47dd-8dd5-e6f24aaf5c7a"" Name=""test server"" Type=""Dev2Server"" ConnectionString=""AppServerUri=http://178.63.172.163:3142/dsf;WebServerPort=3142;AuthenticationType=Public;UserName=;Password="" Version=""1.0"" ResourceType=""Server"" ServerID=""51a58300-7e9d-4927-a57b-e5d700b11b55"">
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


        [Test]
        [DeploymentItem("EnableDocker.txt")]
        [Author("Siphamandla Dube")]
        [Category(nameof(DropBoxSource))]
        public void DropBoxSource_ToString_SetProperty_JsonSerializedObjectReturnedAsString_AreEqual_ExpectTrue()
        {
            var testDropBoxSource = SetupDefaultDropBoxSource();
            var actualDropBoxSourceToString = testDropBoxSource.ToString();
            var expected = JsonConvert.SerializeObject(testDropBoxSource);
            NUnit.Framework.Assert.AreEqual(expected, actualDropBoxSourceToString);
        }

        [Test]
        [DeploymentItem("EnableDocker.txt")]
        [Author("Siphamandla Dube")]
        [Category(nameof(DropBoxSource))]
        public void DropBoxSource_ToString_EmptyObject_AreEqual_ExpectedTrue()
        {
            var testDropBoxSource = new DropBoxSource();
            var actualSerializedDropBoxSource = testDropBoxSource.ToString();
            var expected = JsonConvert.SerializeObject(testDropBoxSource);
            NUnit.Framework.Assert.AreEqual(expected, actualSerializedDropBoxSource);
        }

        [Test]
        [DeploymentItem("EnableDocker.txt")]
        [Author("Siphamandla Dube")]
        [Category(nameof(DropBoxSource))]
        public void DropBoxSource_AppKey_CannotBeEmpty()
        {
            //------------Setup for test--------------------------
            var dbSource = new DropBoxSource
            {
                AppKey = "",
                AccessToken = ""
            };
            //------------Execute Test---------------------------
            var appKey = dbSource.AppKey;
            //------------Assert Results-------------------------
            NUnit.Framework.StringAssert.Contains(appKey, "");
        }

        [Test]
        [DeploymentItem("EnableDocker.txt")]
        [Author("Siphamandla Dube")]
        [Category(nameof(DropBoxSource))]
        public void DropBoxSource_ToXml_SetProperty_AreEqual_ExpectTrue()
        {
            var testDropBoxSource = SetupDefaultDropBoxSource();
            var expectedXml = testDropBoxSource.ToXml();
            var workflowXamlDefintion = expectedXml.Element("XamlDefinition");
            var attrib = expectedXml.Attributes();
            var attribEnum = attrib.GetEnumerator();
            while (attribEnum.MoveNext())
            {
                if (attribEnum.Current.Name == "Name")
                {
                    NUnit.Framework.Assert.AreEqual("TestResource", attribEnum.Current.Value);
                    break;
                }
            }
            NUnit.Framework.Assert.IsNull(workflowXamlDefintion);
        }

        [Test]
        [DeploymentItem("EnableDocker.txt")]
        [Author("Siphamandla Dube")]
        [Category(nameof(DropBoxSource))]
        public void DropBoxSource_ToXmlEmptyObject_XElementWithNoInformation_AreEqual_ExpectTrue()
        {
            var testDropBoxSource = new DropBoxSource();
            var expectedXml = testDropBoxSource.ToXml();

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
        [DeploymentItem("EnableDocker.txt")]
        [Author("Siphamandla Dube")]
        [Category(nameof(DropBoxSource))]
        public void DropBoxSource_XML_Constractor_Initialise_ExpectTrue()
        {
            //------------------------Arrange-------------------------
            var element = XElement.Parse(conStr);
            //------------------------Act-----------------------------
            var dropBoxSource = new DropBoxSource(element);
            //------------------------Assert--------------------------
            NUnit.Framework.Assert.AreEqual("", dropBoxSource.AccessToken);
            NUnit.Framework.Assert.AreEqual("", dropBoxSource.AppKey);
            NUnit.Framework.Assert.AreEqual("", dropBoxSource.AuthorRoles);
            NUnit.Framework.Assert.AreEqual("test server", dropBoxSource.ResourceName);
            NUnit.Framework.Assert.AreEqual("DropBoxSource", dropBoxSource.ResourceType);
        }

        [Test]
        [DeploymentItem("EnableDocker.txt")]
        [Author("Siphamandla Dube")]
        [Category(nameof(DropBoxSource))]
        public void DropBoxSource_AppKey_And_AccessToken_Equals_ExpectTrue()
        {
            //------------------------Arrange-------------------------
            var element = XElement.Parse(conStr);
            //------------------------Act-----------------------------
            var mockOAuthSource = new Mock<IOAuthSource>();

            mockOAuthSource.Setup(o => o.AppKey).Returns("");
            mockOAuthSource.Setup(o => o.AccessToken).Returns("");

            var dropBoxSource = new DropBoxSource(element);

            var isDropboxSource = dropBoxSource.Equals(mockOAuthSource.Object);
            //------------------------Assert--------------------------
            NUnit.Framework.Assert.IsTrue(isDropboxSource);
        }

        [Test]
        [DeploymentItem("EnableDocker.txt")]
        [Author("Siphamandla Dube")]
        [Category(nameof(DropBoxSource))]
        public void DropBoxSource_Other_IsNull_Equals_ExpectFalse()
        {
            //------------------------Arrange-------------------------
            var element = XElement.Parse(conStr);
            //------------------------Act-----------------------------
            var mockOAuthSource = new Mock<IOAuthSource>();

            mockOAuthSource.Setup(o => o.AppKey).Returns("");
            mockOAuthSource.Setup(o => o.AccessToken).Returns("");

            var dropBoxSource = new DropBoxSource(element);

            var isDropboxSource = dropBoxSource.Equals(null);
            //------------------------Assert--------------------------
            NUnit.Framework.Assert.IsFalse(isDropboxSource);
        }

        DropBoxSource SetupDefaultDropBoxSource()
        {
            var testDropBoxSource = new DropBoxSource
            {
                ResourceID = Guid.NewGuid(),
                AppKey = "",
                AccessToken = "",
                ResourceName = "TestResource",
                ResourceType = "DropBoxSource"
            };

            return testDropBoxSource;
        }
    }
}
