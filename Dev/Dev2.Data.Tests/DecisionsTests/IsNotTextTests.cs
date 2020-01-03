using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class IsNotTextTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotText_Invoke")]
        public void GivenNumber_IsNotText_Invoke_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isNotText = new IsNotText();
            var cols = new string[2];
            cols[0] = "9";
            //------------Execute Test---------------------------
            var result = isNotText.Invoke(cols);
            //------------Assert Results-------------------------
            Assert.IsTrue(result);
            //------------Execute Test---------------------------
            var emptyString = new[] { string.Empty };
            result = isNotText.Invoke(emptyString);
            //------------Assert Results-------------------------
            Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsNotText_Invoke")]
        public void IsNotText_Invoke_IsNotText_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new IsNotText();
            var cols = new string[2];
            cols[0] = "Text";
            //------------Execute Test---------------------------
            var result = notStartsWith.Invoke(cols);
            //------------Assert Results-------------------------
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsNotText_HandlesType")]
        public void IsNotText_HandlesType_ReturnsIsNotTextType()
        {
            var decisionType = enDecisionType.IsNotText;
            //------------Setup for test--------------------------
            var isNotText = new IsNotText();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            Assert.AreEqual(decisionType, isNotText.HandlesType());
        }
    }
}
