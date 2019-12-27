using System;
using Dev2.Activities.DropBox2016.Result;
using NUnit.Framework;

namespace Dev2.Tests.Activities.ActivityTests.DropBox2016
{
    [TestFixture]

    public class DropBoxFailureResultShould
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ConstructDropBoxFailureResult_GivenException_ShouldRetunNewFailureResult()
        {
            //---------------Set up test pack-------------------
            var failureResult = new DropboxFailureResult(new Exception("Message"));
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(failureResult);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void failureResult_GivenException_ShouldRetunNewFailureResult()
        {
            //---------------Set up test pack-------------------
            var dpExc = new Exception("Message");
            var failureResult = new DropboxFailureResult(dpExc);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(failureResult);
            //---------------Execute Test ----------------------
            var exception = failureResult.GetException();
            //---------------Test Result -----------------------
            Assert.AreEqual(exception, dpExc);
        }
    }
}
