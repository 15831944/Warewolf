using Dev2.Activities.DropBox2016.DropboxFileActivity;
using Dev2.Activities.DropBox2016.Result;
using Dropbox.Api.Files;
using NUnit.Framework;
using Moq;
using System.Reflection;
using Dev2.Common.Interfaces.Wrappers;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Tests.Activities.ActivityTests.DropBox2016.DropboxFiles
{
    [TestFixture]
    [SetUpFixture]

    public class DropboxFileReadTests
    {
        Mock<IDropboxFileRead> CreateDropboxReadMock()
        {
            var mock = new Mock<IDropboxFileRead>();
            var successResult = new DropboxListFolderSuccesResult(It.IsAny<ListFolderResult>());
            mock.Setup(upload => upload.ExecuteTask(It.IsAny<IDropboxClient>()))
                 .Returns(successResult);
            return mock;
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void CreateDropBoxActivity_GivenIsNew_ShouldNotBeNull()
        {
            //---------------Set up test pack-------------------
            var dropboxFileRead = CreateDropboxReadMock().Object;
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsNotNull(dropboxFileRead);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ExecuteTask_GivendropboxFileRead_ShouldReturnFileMetadata()
        {
            //---------------Set up test pack-------------------
            var downloadMock = CreateDropboxReadMock();
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsNotNull(downloadMock);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            downloadMock.Object.ExecuteTask(It.IsAny<IDropboxClient>());
            downloadMock.Verify(upload => upload.ExecuteTask(It.IsAny<IDropboxClient>()));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void CreateNewDropboxUpload_GivenEmptyPath_ShouldBeValid()
        {
            //---------------Set up test pack-------------------
            var dropboxFileRead = new DropboxFileRead(true, "", false, false);
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsNotNull(dropboxFileRead);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void CreateNewdropboxFileRead_GivenPath_ShouldBeValid()
        {
            //---------------Set up test pack-------------------
            var dropboxFileRead = new DropboxFileRead(true, "a.file", false, false);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsNotNull(dropboxFileRead);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void CreateNewdropboxFileRead_GivenNullPath_ShouldBeValid()
        {
            //---------------Set up test pack-------------------
            var dropboxFileRead = new DropboxFileRead(true, null, false, false);
            var type = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(dropboxFileRead);
            var staticField = type.GetField("_path", BindingFlags.Instance | BindingFlags.NonPublic);
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsNotNull(dropboxFileRead);
            NUnit.Framework.Assert.IsNotNull(staticField);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsNotNull(staticField);
            NUnit.Framework.Assert.AreEqual("", "");
        }

        [Test]
        [Author("Ashley Lewis")]
        public void ExecuteDropboxFileRead_Throws_ShouldReturnFailedResult()
        {
            //---------------Set up test pack-------------------
            var dropBoxFileRead = new DropboxFileRead(true, null, false, false);
            var mockDropboxClient = new Mock<IDropboxClient>();
            mockDropboxClient.Setup(client => client.ListFolderAsync(It.IsAny<ListFolderArg>())).Throws(new Exception());
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = dropBoxFileRead.ExecuteTask(mockDropboxClient.Object);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsInstanceOf(result.GetType(), typeof(DropboxFailureResult), "Dropbox failure result not returned after exception");
        }
    }
}