using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    [SetUpFixture]
    public class IsNullTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNull_Invoke")]
        public void GivenSomeString_IsNull_Invoke_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isNull = new IsNull();
            var cols = new string[2];
            cols[0] = "Eight";
            //------------Execute Test---------------------------
            var result = isNull.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
            //------------Execute Test---------------------------
            result = isNull.Invoke(null);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNull_Invoke")]
        public void IsNull_Invoke_IsNull_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new IsNull();
            var cols = new string[2];
            cols[0] = null;
            //------------Execute Test---------------------------
            var result = notStartsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsNull_HandlesType")]
        public void IsNull_HandlesType_ReturnsIsNullType()
        {
            var decisionType = enDecisionType.IsNull;
            //------------Setup for test--------------------------
            var isNull = new IsNull();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isNull.HandlesType());
        }
    }
}