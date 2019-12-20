using Dev2.Runtime.ServiceModel.Data;
using NUnit.Framework;

namespace Dev2.Data.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class MethodOutputTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void MethodOutput_GivenIsNew_ShouldPassThrough()
        {
            //---------------Set up test pack-------------------
            var dev2Definition = new MethodOutput();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsNotNull(dev2Definition);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void MethodOutput_GivenIsNewParameters_ShouldPassThrough()
        {
            //---------------Set up test pack-------------------
            var dev2Definition = new MethodOutput("a", "b", "c", false, "", false, "", false, "", false);
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsNotNull(dev2Definition);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("a", dev2Definition.Name);
            NUnit.Framework.Assert.AreEqual("b", dev2Definition.MapsTo);
            NUnit.Framework.Assert.AreEqual("c", dev2Definition.Value);
        }
    }
}