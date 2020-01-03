/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using NUnit.Framework;

namespace Dev2.Data.Tests
{
    [TestFixture]
    public class WarewolfAtomComparerTests
    {
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(WarewolfAtomComparer))]
        public void WarewolfAtomComparer_Equals_BothNull_ReturnTrue()
        {
            var comparer = new WarewolfAtomComparer();
            Assert.IsTrue(comparer.Equals(null, null));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(WarewolfAtomComparer))]
        public void WarewolfAtomComparer_Equals_XisNull_ReturnFalse()
        {
            var a = DataStorage.WarewolfAtom.NewDataString("a");
            var comparer = new WarewolfAtomComparer();
            Assert.IsFalse(comparer.Equals(a, null));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(WarewolfAtomComparer))]
        public void WarewolfAtomComparer_Equals_YisNull_ReturnFalse()
        {
            var a = DataStorage.WarewolfAtom.NewDataString("a");
            var comparer = new WarewolfAtomComparer();
            Assert.IsFalse(comparer.Equals(null, a));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(WarewolfAtomComparer))]
        public void WarewolfAtomComparer_Equals_ReturnFalse()
        {
            var a = DataStorage.WarewolfAtom.NewDataString("a");
            var b = DataStorage.WarewolfAtom.NewDataString("b");
            var comparer = new WarewolfAtomComparer();
            Assert.IsFalse(comparer.Equals(a, b));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(WarewolfAtomComparer))]
        public void WarewolfAtomComparer_Equals_ReturnTrue()
        {
            var a = DataStorage.WarewolfAtom.NewDataString("a");
            var b = DataStorage.WarewolfAtom.NewDataString("a");
            var comparer = new WarewolfAtomComparer();
            Assert.IsTrue(comparer.Equals(a, b));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(WarewolfAtomComparer))]
        public void WarewolfAtomComparer_GetHashCode()
        {
            var comparer = new WarewolfAtomComparer();
            var hashCode = comparer.GetHashCode(DataStorage.WarewolfAtom.NewDataString("a"));
            Assert.AreNotEqual(0, hashCode);
        }
    }
}
