using System.Xml.Linq;
using Dev2.Data.ServiceModel;
using NUnit.Framework;

namespace Dev2.Data.Tests.ServiceModel
{
    [TestFixture]
    [SetUpFixture]
    public class OauthSourceTests
    {
        [Test]
        public void OauthSource_ShouldContructorAndDefaultValuiesSet()
        {
            OauthSource oauthSource = new DropBoxSource();
            NUnit.Framework.Assert.IsNotNull(oauthSource);
            NUnit.Framework.Assert.IsTrue(oauthSource.IsSource);
            NUnit.Framework.Assert.IsFalse(oauthSource.IsService);
            NUnit.Framework.Assert.IsFalse(oauthSource.IsFolder);
            NUnit.Framework.Assert.IsFalse(oauthSource.IsReservedService);
            NUnit.Framework.Assert.IsFalse(oauthSource.IsServer);
            NUnit.Framework.Assert.IsFalse(oauthSource.IsResourceVersion);
        }

        [Test]
        public void OauthSource_ToXml_ShouldContructorAndDefaultValuiesSet()
        {
            OauthSource oauthSource = new DropBoxSource();
            NUnit.Framework.Assert.IsNotNull(oauthSource);
            var xElement = oauthSource.ToXml();
            NUnit.Framework.Assert.IsNotNull(xElement);
        }
        [Test]
        public void GivenXelement_DropBox_ShouldHaveContructorAndDefaultValuiesSet()
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

            var element = XElement.Parse(conStr);
            OauthSource oauthSource = new DropBoxSource(element);
            NUnit.Framework.Assert.IsNotNull(oauthSource);
            var xElement = oauthSource.ToXml();
            NUnit.Framework.Assert.IsNotNull(xElement);
        }
    }
}
