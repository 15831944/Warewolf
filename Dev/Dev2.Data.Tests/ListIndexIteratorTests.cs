/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/
using Dev2.Data.Binary_Objects;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dev2.Data.Tests
{
    [TestFixture]
    public class ListIndexIteratorTests
    {
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(ListIndexIterator))]
        public void ListIndexIterator_ShouldHaveConstructor()
        {
            var indexes = new List<int> { 1, 2, 3 };
            var listIndexIterator = new ListIndexIterator(indexes);
            NUnit.Framework.Assert.IsNotNull(listIndexIterator);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(ListIndexIterator))]
        public void ListIndexIterator_MaxIndex_ShouldReturnLastIndex()
        {
            var indexes = new List<int> { 1, 2, 3 };
            var listIndexIterator = new ListIndexIterator(indexes);
            var maxIndex = listIndexIterator.MaxIndex();
            NUnit.Framework.Assert.AreEqual(3, maxIndex);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(ListIndexIterator))]
        public void ListIndexIterator_HasMore_ShouldReturnTrue()
        {
            var indexes = new List<int> { 1, 2, 3 };
            var listIndexIterator = new ListIndexIterator(indexes);
            NUnit.Framework.Assert.IsFalse(listIndexIterator.IsEmpty);
            NUnit.Framework.Assert.IsTrue(listIndexIterator.HasMore());
            var fetchNextIndex = listIndexIterator.FetchNextIndex();
            NUnit.Framework.Assert.AreEqual(1, fetchNextIndex);
            NUnit.Framework.Assert.IsTrue(listIndexIterator.HasMore());
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(ListIndexIterator))]
        public void ListIndexIterator_FetchNextIndex_ShouldReturnNewIndex()
        {
            var indexes = new List<int> { 1, 2, 3 };
            var indexListIndexIterator = new ListIndexIterator(indexes);
            var fetchNextIndex = indexListIndexIterator.FetchNextIndex();
            NUnit.Framework.Assert.AreEqual(1, fetchNextIndex);
            fetchNextIndex = indexListIndexIterator.FetchNextIndex();
            NUnit.Framework.Assert.AreEqual(2, fetchNextIndex);
            fetchNextIndex = indexListIndexIterator.FetchNextIndex();
            NUnit.Framework.Assert.AreEqual(3, fetchNextIndex);
        }
    }
}
