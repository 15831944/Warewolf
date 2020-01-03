using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class IsNotNullTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotNull_Invoke")]
        public void GivenSomeString_IsNotNull_Invoke_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isNotNull = new IsNotNull();
            var cols = new string[2];
            cols[0] = "Eight";
            //------------Execute Test---------------------------
            var result = isNotNull.Invoke(cols);
            //------------Assert Results-------------------------
            Assert.IsTrue(result);
            //------------Execute Test---------------------------
            result = isNotNull.Invoke(null);
            //------------Assert Results-------------------------
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotNull_Invoke")]
        public void IsNotNull_Invoke_IsNotNull_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new IsNotNull();
            var cols = new string[2];
            cols[0] = null;
            //------------Execute Test---------------------------
            var result = notStartsWith.Invoke(cols);
            //------------Assert Results-------------------------
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsNotNull_HandlesType")]
        public void IsNotNull_HandlesType_ReturnsIsNotNullType()
        {
            var decisionType = enDecisionType.IsNotNull;
            //------------Setup for test--------------------------
            var isNotNull = new IsNotNull();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            Assert.AreEqual(decisionType, isNotNull.HandlesType());
        }
    }
}