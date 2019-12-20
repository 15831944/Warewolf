using System.Xml.Linq;
using Dev2.Data.ServiceModel;
using NUnit.Framework;
using Warewolf.Security.Encryption;


namespace Dev2.Tests.Runtime.ServiceModel
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class OauthSourceTest
    {

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("OauthSource_Ctor")]
        public void OauthSource_Ctor_Construct_ExpectSource()
        {
            //------------Setup for test--------------------------
            var oauthSource = new DropBoxSource();
            
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(null,oauthSource.AccessToken);
            Assert.AreEqual(null,oauthSource.AppKey);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("OauthSource_Ctor")]
        public void OauthSource_Ctor_FromXML_Construct_ExpectSource()
        {
            //------------Setup for test--------------------------
            var oauthSource = new DropBoxSource(XElement.Parse(@"<Source ID=""00000000-0000-0000-0000-000000000000"" Name="""" ResourceType=""OauthSource"" IsValid=""false"" ConnectionString=""AccessToken=secret;AppKey=key"" Type=""OauthSource"">
  <DisplayName></DisplayName>
  <Category></Category>
  <AuthorRoles></AuthorRoles>
  <ErrorMessages />
  <TypeOf>OauthSource</TypeOf>
</Source>"));

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(oauthSource.AccessToken, "secret");
            Assert.AreEqual(oauthSource.AppKey, "key");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("OauthSource_ToXML")]
        public void OauthSource_ToXML_Construct_ExpectPropertiesSet()
        {
            //------------Setup for test--------------------------
            var oauthSource = new DropBoxSource(){AppKey = "key",AccessToken = "secret"};

            //------------Execute Test---------------------------
            var outxml = oauthSource.ToXml();
            //------------Assert Results-------------------------
            var readOauthSource = new DropBoxSource(outxml)
            {
                AppKey = "key",
                AccessToken = "secret"
            };

            var conStringAttr = outxml.Attribute("ConnectionString");
            Assert.IsNotNull(conStringAttr);
            Assert.IsTrue(conStringAttr.Value.IsBase64());            
        }

    }
}
