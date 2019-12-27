using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class IsNotErrorTests
    {

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotError_Invoke")]
        public void GivenSomeString_IsNotError_Invoke_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isNotError = new IsNotError();
            var cols = new string[2];
            cols[0] = "Eight";
            //------------Execute Test---------------------------
            var result = isNotError.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotError_Invoke")]
        public void IsNotError_Invoke_IsNotError_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isNotError = new IsNotError();
            var cols = new string[2];
            cols[0] = "";
            //------------Execute Test---------------------------
            var result = isNotError.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsNotError_HandlesType")]
        public void IsNotError_HandlesType_ReturnsIsNotErrorType()
        {
            var decisionType = enDecisionType.IsNotError;
            //------------Setup for test--------------------------
            var isNotError = new IsNotError();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isNotError.HandlesType());
        }
    }
}
