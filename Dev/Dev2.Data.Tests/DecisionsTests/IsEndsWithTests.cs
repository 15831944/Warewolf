using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    /// <summary>
    /// Summary description for IsEndsWithTests
    /// </summary>
    [TestFixture]
    public class IsEndsWithTests
    {
        [Test]
        [Author("Massimo Guerrera")]
        [Category("IsEndsWith_Invoke")]
        public void IsEndsWith_Invoke_DoesEndWith_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var endsWith = new IsEndsWith();
            var cols = new string[2];
            cols[0] = "TestData";
            cols[1] = "Data";
            //------------Execute Test---------------------------
            var result = endsWith.Invoke(cols);
            //------------Assert Results-------------------------
            Assert.IsTrue(result);
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("IsEndsWith_Invoke")]
        public void IsEndsWith_Invoke_DoesntEndWith_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var endsWith = new IsEndsWith();
            var cols = new string[2];
            cols[0] = "TestData";
            cols[1] = "No";
            //------------Execute Test---------------------------
            var result = endsWith.Invoke(cols);
            //------------Assert Results-------------------------
            Assert.IsFalse(result);
        }
        
        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsEndsWith_HandlesType")]
        public void IsEndsWith_HandlesType_ReturnsIsEndsWithType()
        {
            var expected = enDecisionType.IsEndsWith;
            //------------Setup for test--------------------------
            var isEndsWith = new IsEndsWith();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            Assert.AreEqual(expected, isEndsWith.HandlesType());
        }
    }
}
