using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests
{    
    [TestFixture]
    public class IsBinaryTests
    {
        [Test]
        [Author("Massimo Guerrera")]
        [Category("IsBinary_Invoke")]
        public void IsBinary_Invoke_DoesEndWith_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isBinary = new IsBinary();
            var cols = new string[2];
            cols[0] = "2";
            //------------Execute Test---------------------------
            var result = isBinary.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
            result = isBinary.Invoke(new []{string.Empty});
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("IsBinary_Invoke")]
        public void IsBinary_Invoke_DoesntEndWith_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isBinary = new IsBinary();
            var cols = new string[2];
            cols[0] = "1";
            //------------Execute Test---------------------------
            var result = isBinary.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsBinary_HandlesType")]
        public void IsBinary_HandlesType_ReturnsIsBinaryType()
        {
            var expected = enDecisionType.IsBinary;
            //------------Setup for test--------------------------
            var isBinary = new IsBinary();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(expected, isBinary.HandlesType());
        }
    }
}
