using Dev2.Activities.DropBox2016.DownloadActivity;
using Dev2.Common;
using NUnit.Framework;


namespace Dev2.Tests.Activities.ActivityTests.DropBox2016.ExceptionTests
{
    [TestFixture]
    public class DropboxFileNotFoundExceptionTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Construct_GivenMessege_ShouldNotBeNull()
        {
            //---------------Set up test pack-------------------
            var dropboxFileNotFoundException = new DropboxFileNotFoundException();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(dropboxFileNotFoundException);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Construct_GivenMessege_ShouldHaveMessegeSet()
        {
            //---------------Set up test pack-------------------
            var dropboxFileNotFoundException = new DropboxFileNotFoundException();
            //---------------Assert Precondition----------------
            Assert.IsNotNull(dropboxFileNotFoundException);
            //---------------Execute Test ----------------------
            var message = dropboxFileNotFoundException.Message;
            //---------------Test Result -----------------------
            Assert.AreEqual(GlobalConstants.DropboxPathNotFoundException, message);
            
        }
    } 
    
    [TestFixture]
    public class DropboxPathNotFileFoundExceptionTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Construct_GivenMessege_ShouldNotBeNull()
        {
            //---------------Set up test pack-------------------
            var dropboxPathNotFileFoundException = new DropboxPathNotFileFoundException();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(dropboxPathNotFileFoundException);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Construct_GivenMessege_ShouldHaveMessegeSet()
        {
            //---------------Set up test pack-------------------
            var dropboxPathNotFileFoundException = new DropboxPathNotFileFoundException();
            //---------------Assert Precondition----------------
            Assert.IsNotNull(dropboxPathNotFileFoundException);
            //---------------Execute Test ----------------------
            var message = dropboxPathNotFileFoundException.Message;
            //---------------Test Result -----------------------
            Assert.AreEqual(GlobalConstants.DropboxPathNotFileException, message);
            
        }
    }
    
    [TestFixture]
    public class DropboxFileMalformdedExceptionTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Construct_GivenMessege_ShouldNotBeNull()
        {
            //---------------Set up test pack-------------------
            var dropboxFileMalformdedException = new DropboxFileMalformdedException();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(dropboxFileMalformdedException);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Construct_GivenMessege_ShouldHaveMessegeSet()
        {
            //---------------Set up test pack-------------------
            var value = new DropboxFileMalformdedException();
            //---------------Assert Precondition----------------
            Assert.IsNotNull(value);
            //---------------Execute Test ----------------------
            var message = value.Message;
            //---------------Test Result -----------------------
            Assert.AreEqual(GlobalConstants.DropboxPathMalformdedException, message);
            
        }
    }
}
