using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class IsNotNumericTests
    {

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotNumeric_Invoke")]
        public void GivenSomeString_IsNotNumeric_Invoke_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isNotNumeric = new IsNotNumeric();
            var cols = new string[2];
            cols[0] = "Eight";
            //------------Execute Test---------------------------
            var result = isNotNumeric.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotNumeric_Invoke")]
        public void IsNotNumeric_Invoke_IsNotNumeric_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new IsNotNumeric();
            var cols = new string[2];
            cols[0] = "324";
            //------------Execute Test---------------------------
            var result = notStartsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsNotNumeric_HandlesType")]
        public void IsNotNumeric_HandlesType_ReturnsIsNotNumericType()
        {
            var decisionType = enDecisionType.IsNotNumeric;
            //------------Setup for test--------------------------
            var isNotNumeric = new IsNotNumeric();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isNotNumeric.HandlesType());
        }
    }
}
