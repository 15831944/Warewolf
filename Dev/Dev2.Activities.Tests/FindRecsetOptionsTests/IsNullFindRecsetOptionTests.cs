using System.Collections.Generic;
using Dev2.DataList;
using NUnit.Framework;


namespace Dev2.Tests.Activities.FindRecsetOptionsTests
{
    [TestFixture]
    [SetUpFixture]
    public class IsNullFindRecsetOptionTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("RsOpIsNull_CreateFunc")]
        public void RsOpIsNull_CreateFunc_WhenNull_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var rsOpIsNull = new RsOpIsNull();
            
            //------------Execute Test---------------------------
            var func = rsOpIsNull.CreateFunc(new List<DataStorage.WarewolfAtom>{DataStorage.WarewolfAtom.Nothing}, null,null, false);
            var isNull = func.Invoke(DataStorage.WarewolfAtom.Nothing);
            //------------Assert Results-------------------------
            Assert.IsTrue(isNull);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("RsOpIsNull_CreateFunc")]
        public void RsOpIsNull_CreateFunc_WhenNotNull_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var rsOpIsNull = new RsOpIsNull();

            //------------Execute Test---------------------------
            var func = rsOpIsNull.CreateFunc(new List<DataStorage.WarewolfAtom> { DataStorage.WarewolfAtom.Nothing }, null, null, false);
            var isNull = func.Invoke(DataStorage.WarewolfAtom.NewDataString("bob"));
            //------------Assert Results-------------------------
            Assert.IsFalse(isNull);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("RsOpIsNull_HandlesType")]
        public void RsOpIsNull_HandlesType_ReturnsIsNULL()
        {
            //------------Setup for test--------------------------
            var rsOpIsNull = new RsOpIsNull();
            
            //------------Execute Test---------------------------
            var handlesType = rsOpIsNull.HandlesType();
            //------------Assert Results-------------------------
            Assert.AreEqual("Is NULL",handlesType);
        }
    }
}