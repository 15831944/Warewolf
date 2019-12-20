using Dev2.Activities.DropBox2016.Result;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using NUnit.Framework;
using Moq;

namespace Dev2.Tests.Activities.ActivityTests.DropBox2016
{
    [TestFixture]
    [SetUpFixture]

    public class DropboxDownloadSuccessResultShould
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ConstructDropBoxSuccessResult_GivenFileMetadata_ShouldRetunNewSuccessResult()
        {
            //---------------Set up test pack-------------------
            var successResult = new DropboxDownloadSuccessResult(It.IsAny<IDownloadResponse<FileMetadata>>());
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(successResult);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void failureResult_GivenException_ShouldRetunNewFailureResult()
        {
            //---------------Set up test pack-------------------
            var fileMetadata = new FileMetadata();
            var mock = new Mock<IDownloadResponse<FileMetadata>>();

            var failureResult = new DropboxDownloadSuccessResult(mock.Object);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var expected = failureResult.GetDownloadResponse();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, mock.Object);
        }
    }
}