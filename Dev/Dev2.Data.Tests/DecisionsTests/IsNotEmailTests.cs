using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    [SetUpFixture]
    public class IsNotEmailTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotEmail_Invoke")]
        public void GivenSomeString_IsNotEmail_Invoke_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isNotEmail = new IsNotEmail();
            var cols = new string[2];
            cols[0] = "something";
            //------------Execute Test---------------------------
            var result = isNotEmail.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotEmail_Invoke")]
        public void IsNotEmail_Invoke_IsNotEmail_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isNotEmail = new IsNotEmail();
            var cols = new string[2];
            cols[0] = "soumething@something.com";
            //------------Execute Test---------------------------
            var result = isNotEmail.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsNotEmail_HandlesType")]
        public void IsNotEmail_HandlesType_ReturnsIsNotEmailType()
        {
            var decisionType = enDecisionType.IsNotEmail;
            //------------Setup for test--------------------------
            var isNotEmail = new IsNotEmail();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isNotEmail.HandlesType());
        }
    }
}
