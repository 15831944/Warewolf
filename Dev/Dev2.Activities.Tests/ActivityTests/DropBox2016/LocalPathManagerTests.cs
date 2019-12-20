﻿using System.IO;
using Dev2.Activities.DropBox2016;
using NUnit.Framework;


namespace Dev2.Tests.Activities.ActivityTests.DropBox2016
{
    //These test cannot be run on the build server, best to cemment them out when checking in
    [TestFixture]
    [SetUpFixture]
    public class LocalPathManagerTests
    {
        [SetUp]
        public void MyTestInitialise()
        {
            ValidFileName = Path.GetTempFileName();
            InValidFileName = @"\Home\Hi\hi.file";
        }
        
        [TearDown]
        public void MyTestCleanup()
        {
            if(File.Exists(ValidFileName))
            {
                File.Delete(ValidFileName);
            }

            if (File.Exists(InValidFileName))
            {
                File.Delete(InValidFileName);
            }
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Construct_GivenIsValid_ShouldShouldNotBeNull()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(ValidFileName);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(localPathManager);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Construct_GivenInValid_ShouldShouldNotBeNull()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(InValidFileName);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(localPathManager);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ManageDirectory_GivenInValid_ShouldBeNotNull()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(InValidFileName);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(localPathManager);
            //---------------Execute Test ----------------------
            var manageDirectory = localPathManager.GetDirectoryName();
            //---------------Test Result -----------------------
            Assert.IsFalse(string.IsNullOrEmpty(manageDirectory));
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ManageDirectory_GivenValid_ShouldBeNotNull()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(ValidFileName);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(localPathManager);
            //---------------Execute Test ----------------------
            var manageDirectory = localPathManager.GetDirectoryName();
            //---------------Test Result -----------------------
            Assert.IsFalse(string.IsNullOrEmpty(manageDirectory));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void CreateValidFolder_GivenValid_ShouldNotCreateFolder()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(ValidFileName);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(localPathManager);
            //---------------Execute Test ----------------------
            var validFolder = localPathManager.CreateValidFolder();
            //---------------Test Result -----------------------
            Assert.IsFalse(string.IsNullOrEmpty(validFolder));
            Assert.IsTrue(Directory.Exists(validFolder));
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void CreateValidFolder_GivenInValid_ShouldCreateFolder()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(InValidFileName);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(localPathManager);
            //---------------Execute Test ----------------------
            var validFolder = localPathManager.CreateValidFolder();
            //---------------Test Result -----------------------
            Assert.IsFalse(string.IsNullOrEmpty(validFolder));
            Assert.IsTrue(Directory.Exists(validFolder));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GetFileName_GivenInValid_ShouldReturnFilename()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(InValidFileName);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(localPathManager);
            //---------------Execute Test ----------------------
            var fileName = localPathManager.GetFileName();
            //---------------Test Result -----------------------
            Assert.IsFalse(string.IsNullOrEmpty(fileName));
            Assert.AreEqual("hi.file", fileName);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GetFullFileName_GivenInValid_ShouldReturnFullFilename()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(InValidFileName);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(localPathManager);
            //---------------Execute Test ----------------------
            var fileName = localPathManager.GetFullFileName();
            //---------------Test Result -----------------------
            Assert.IsFalse(string.IsNullOrEmpty(fileName));
            Assert.AreEqual(InValidFileName, fileName);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void FileExist_GivenTemp_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var localPathManager = new LocalPathManager(InValidFileName);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var fileExist = localPathManager.FileExist();
            //---------------Test Result -----------------------
            Assert.IsFalse(fileExist);
        }
        public string ValidFileName
        {
            get;
            set;
        }
        public string InValidFileName
        {
            get;
            set;
        }
    }
}
