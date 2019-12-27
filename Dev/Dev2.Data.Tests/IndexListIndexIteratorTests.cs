using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Dev2.Data
{
    [TestFixture]
    public class IndexListIndexIteratorTests
    {        
        [Test]
        public void IndexListIndexIterator_ShouldHaveConstructor()
        {
            var indexes = new List<int> {1, 2, 3};
            var indexListIndexIterator = new IndexListIndexIterator(indexes);
            NUnit.Framework.Assert.IsNotNull(indexListIndexIterator);
        }
           
        [Test]
        public void IndexListIndexIterator_MaxIndex_ShouldReturnLastIndex()
        {
            var indexes = new List<int> {1, 2, 3};
            var indexListIndexIterator = new IndexListIndexIterator(indexes);
            NUnit.Framework.Assert.IsNotNull(indexListIndexIterator);
            var maxIndex = indexListIndexIterator.MaxIndex();
            NUnit.Framework.Assert.AreEqual(3, maxIndex);
        }

        [Test]
        public void IndexListIndexIterator_HasMore_ShouldReturnTrue()
        {
            var indexes = new List<int> {1, 2, 3};
            var indexListIndexIterator = new IndexListIndexIterator(indexes);
            NUnit.Framework.Assert.IsNotNull(indexListIndexIterator);
            var prObj = new PrivateObject(indexListIndexIterator);
            NUnit.Framework.Assert.IsFalse(indexListIndexIterator.IsEmpty);
            var current = (int) prObj.GetField("_current");
            NUnit.Framework.Assert.IsNotNull(current);
            NUnit.Framework.Assert.AreEqual(0, current);
            NUnit.Framework.Assert.IsTrue(indexListIndexIterator.HasMore());
            var fetchNextIndex = indexListIndexIterator.FetchNextIndex();
            NUnit.Framework.Assert.AreEqual(1, fetchNextIndex);
        }
    }
}
