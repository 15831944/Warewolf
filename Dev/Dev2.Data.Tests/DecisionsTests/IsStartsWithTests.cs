using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    /// <summary>
    /// Summary description for IsStartsWithTests
    /// </summary>
    [TestFixture]
    [SetUpFixture]
    public class IsStartsWithTests
    {
        [Test]
        [Author("Sanele Mthembu")]        
        public void IsStartsWith_Invoke_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var startsWith = new IsStartsWith();
            var cols = new string[2];
            cols[0] = "TestData";
            cols[1] = "Test";
            //------------Execute Test---------------------------
            var result = startsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        public void IsStartsWith_Invoke_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var startsWith = new IsStartsWith();
            var cols = new string[2];
            cols[0] = "TestData";
            cols[1] = "No";
            //------------Execute Test---------------------------
            var result = startsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsStartsWith_HandlesType")]
        public void IsStartsWith_HandlesType_ReturnsNotStartWithType()
        {
            var startsWith = enDecisionType.IsStartsWith;
            //------------Setup for test--------------------------
            var isStartsWith = new IsStartsWith();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(startsWith, isStartsWith.HandlesType());
        }
    }
}
