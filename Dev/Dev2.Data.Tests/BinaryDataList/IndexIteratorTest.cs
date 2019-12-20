/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Collections.Generic;
using NUnit.Framework;


namespace Dev2.Data.Tests.BinaryDataList
{
    [TestFixture]
    [SetUpFixture]
    public class IndexIteratorTest
    {
        [Test]
        public void CanIteratorNormally()
        {
            var gaps = new HashSet<int>();
            var ii = new IndexIterator(gaps, 100);
            var cnt = 0;
            while (ii.HasMore())
            {
                ii.FetchNextIndex();
                cnt++;
            }

            NUnit.Framework.Assert.AreEqual(cnt, 100);
        }

        [Test]
        public void CanIteratorWithGapAt1()
        {
            var gaps = new HashSet<int>(new List<int>{1});
            const int maxValue = 100;
            var ii = new IndexIterator(gaps, 100);
            var cnt = 0;
            var firstIdx = -1;
            while (ii.HasMore())
            {
                var val = ii.FetchNextIndex();
                if (cnt == 0)
                {
                    firstIdx = val;
                }
                cnt++;
            }

            NUnit.Framework.Assert.AreEqual(cnt, maxValue - gaps.Count);
            NUnit.Framework.Assert.AreEqual(2, firstIdx);
        }

        [Test]
        public void CanIteratorWithGapAt1_PlusGapsEvery10()
        {
            var gaps = new HashSet<int>(new List<int> { 1, 11, 21, 31, 41, 51, 61, 71, 81, 91 });
            const int maxValue = 100;
            var ii = new IndexIterator(gaps, 100);
            var cnt = 0;
            var firstIdx = -1;
            while (ii.HasMore())
            {
                var val = ii.FetchNextIndex();
                if (cnt == 0)
                {
                    firstIdx = val;
                }
                cnt++;
            }

            NUnit.Framework.Assert.AreEqual(cnt, maxValue - gaps.Count);
            NUnit.Framework.Assert.AreEqual(2, firstIdx);
        }

        [Test]
        public void MaxValueIsCorrectWhenCurrentValueInGaps()
        {
            var gaps = new HashSet<int>(new List<int> { 2 });
            var ii = new IndexIterator(gaps, 2);


            NUnit.Framework.Assert.AreEqual(1,ii.MaxIndex());
        }


        [Test]
        [Category("IndexIterator,UnitTest")]
        [Author("Travis")]
        [Description("A test to ensure we quite visiting this 1 based indexing issue ;)")]
        public void IsEmptyIsCorrectWhenTwoElementAndIndex2Removed()
        {
            var gaps = new HashSet<int>(new List<int> { 2 });
            var ii = new IndexIterator(gaps, 2);

            NUnit.Framework.Assert.AreEqual(1, ii.MaxIndex());
            NUnit.Framework.Assert.IsFalse(ii.IsEmpty);
        }


        [Test]
        [Category("IndexIterator,UnitTest")]
        [Author("Travis")]
        [Description("A test to ensure we quite visiting this 1 based indexing issue ;)")]
        public void IsEmptyIsCorrectWhenThreeElementAndIndex2And3Removed()
        {
            var gaps = new HashSet<int>(new List<int> { 2,3 });
            var ii = new IndexIterator(gaps, 3);

            NUnit.Framework.Assert.AreEqual(1, ii.MaxIndex());
            NUnit.Framework.Assert.IsFalse(ii.IsEmpty);
        }
    }
}
