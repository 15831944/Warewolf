using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class NotRegExTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        [Category("NotRegEx_Invoke")]
        public void GivenSomeString_NotRegEx_Invoke_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var notRegEx = new NotRegEx();
            var cols = new string[2];
            cols[0] = "Number 5 should";
            cols[1] = "d";
            //------------Execute Test---------------------------
            var result = notRegEx.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("NotRegEx_Invoke")]
        public void NotRegEx_Invoke_NotRegEx_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new NotRegEx();
            var cols = new string[2];
            cols[0] = "324";
            cols[1] = "d";
            //------------Execute Test---------------------------
            var result = notStartsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("NotRegEx_HandlesType")]
        public void NotRegEx_HandlesType_ReturnsNotRegExType()
        {
            var decisionType = enDecisionType.NotRegEx;
            //------------Setup for test--------------------------
            var notRegEx = new NotRegEx();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, notRegEx.HandlesType());
        }
    }
}
