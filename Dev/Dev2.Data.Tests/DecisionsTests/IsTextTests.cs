using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class IsTextTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsText_Invoke")]
        public void GivenNumber_IsText_Invoke_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isText = new IsText();
            var cols = new string[2];
            cols[0] = "9";
            //------------Execute Test---------------------------
            var result = isText.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
            //------------Execute Test---------------------------
            var emptyString = new[] { string.Empty };
            result = isText.Invoke(emptyString);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsText_Invoke")]
        public void IsText_Invoke_IsText_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new IsText();
            var cols = new string[2];
            cols[0] = "Text";
            //------------Execute Test---------------------------
            var result = notStartsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsText_HandlesType")]
        public void IsText_HandlesType_ReturnsIsTextType()
        {
            var decisionType = enDecisionType.IsText;
            //------------Setup for test--------------------------
            var isText = new IsText();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isText.HandlesType());
        }
    }
}
