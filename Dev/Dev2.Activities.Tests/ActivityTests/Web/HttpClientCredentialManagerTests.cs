using System.Net;
using System.Net.Http;
using Dev2.Activities;
using NUnit.Framework;

namespace Dev2.Tests.Activities.ActivityTests.Web
{
    [TestFixture]
    public class HttpClientCredentialManagerTests
    {

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void SetCredentialOnHandler_GivenHandlerIsNull_ShouldReturnNull()
        {
            //---------------Set up test pack-------------------
            var webSource = TestUtils.CreateWebSourceWithCredentials();
            var httpClientHandler = HttpClientCredentialManager.SetCredentialOnHandler(webSource, null);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNull(httpClientHandler);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void SetCredentialOnHandler_GivenHandlerIsNotNull_ShouldReturnHttpHandler()
        {
            //---------------Set up test pack-------------------
            var webSource = TestUtils.CreateWebSourceWithCredentials();
            var clientHandler = new HttpClientHandler();
            var httpClientHandler = HttpClientCredentialManager.SetCredentialOnHandler(webSource, clientHandler);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(clientHandler);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(httpClientHandler);
        } 
        
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void SetCredentialOnHandler_GivenSourceIsAnonymousAuth_ShouldReturnHttpHandlerWithNoCredentials()
        {
            //---------------Set up test pack-------------------
            var webSource = TestUtils.CreateWebSourceWithCredentials();
            var clientHandler = new HttpClientHandler();
            var httpClientHandler = HttpClientCredentialManager.SetCredentialOnHandler(webSource, clientHandler);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(clientHandler);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(httpClientHandler);
        } 
        
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void SetCredentialOnHandler_GivenSourceIsUserAuth_ShouldReturnHttpHandlerWithCredentials()
        {
            //---------------Set up test pack-------------------
            var webSource = TestUtils.CreateWebSourceWithCredentials();
            var clientHandler = new HttpClientHandler();
            var httpClientHandler = HttpClientCredentialManager.SetCredentialOnHandler(webSource, clientHandler);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(clientHandler);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(httpClientHandler);
            var credential = (NetworkCredential)httpClientHandler.Credentials;
            Assert.AreEqual("Passwr1", credential.Password);
            Assert.AreEqual("User1", credential.UserName);
        }
    }
}