using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class IsAlphanumericTests
    {
        [Test]
        [Author("Massimo Guerrera")]
        [Category("IsAlphanumeric_Invoke")]
        public void IsAlphanumeric_Invoke_DoesEndWith_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isAlphanumeric = new IsAlphanumeric();
            var cols = new string[2];
            cols[0] = "'";
            //------------Execute Test---------------------------
            var result = isAlphanumeric.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("IsAlphanumeric_Invoke")]
        public void IsAlphanumeric_Invoke_DoesntEndWith_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isAlphanumeric = new IsAlphanumeric();
            var cols = new string[2];
            cols[0] = "TestData";
            //------------Execute Test---------------------------
            var result = isAlphanumeric.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
            result = isAlphanumeric.Invoke(new[] { string.Empty});
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsAlphanumeric_HandlesType")]
        public void IsAlphanumeric_HandlesType_ReturnsIsAlphanumericType()
        {
            var expected = enDecisionType.IsAlphanumeric;
            //------------Setup for test--------------------------
            var isAlphanumeric = new IsAlphanumeric();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(expected, isAlphanumeric.HandlesType());
        }
    }
}
