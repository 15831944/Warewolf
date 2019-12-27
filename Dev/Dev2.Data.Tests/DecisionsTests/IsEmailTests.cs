using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class IsEmailTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsEmail_Invoke")]
        public void GivenSomeString_IsEmail_Invoke_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isEmail = new IsEmail();
            var cols = new string[2];
            cols[0] = "something";
            //------------Execute Test---------------------------
            var result = isEmail.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsEmail_Invoke")]
        public void IsEmail_Invoke_IsEmail_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isEmail = new IsEmail();
            var cols = new string[2];
            cols[0] = "soumething@something.com";
            //------------Execute Test---------------------------
            var result = isEmail.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsEmail_HandlesType")]
        public void IsEmail_HandlesType_ReturnsIsEmailType()
        {
            var decisionType = enDecisionType.IsEmail;
            //------------Setup for test--------------------------
            var isEmail = new IsEmail();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isEmail.HandlesType());
        }
    }
}
