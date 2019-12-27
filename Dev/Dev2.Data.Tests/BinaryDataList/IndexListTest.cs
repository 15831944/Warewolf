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
using Dev2.Data.Binary_Objects;
using NUnit.Framework;

namespace Dev2.Data.Tests.BinaryDataList
{
    [TestFixture]
    public class IndexListTest
    {
        [Test]
        [Author("Travis")]
        [Description("Ensure the IndexList can init properly")]
        [Category("UnitTest,IndexList")]
        public void IndexList_UnitTest_CanInitNormally()
        {

            var il = new IndexList(null, 5);

            NUnit.Framework.Assert.AreEqual(1, il.MinValue);
            NUnit.Framework.Assert.AreEqual(5, il.MaxValue);
        }

        [Test]
        [Author("Travis")]
        [Description("Ensure the IndexList can init properly")]
        [Category("UnitTest,IndexList")]
        public void IndexList_UnitTest_CanInitWithGaps()
        {

            var gaps = new HashSet<int> { 1, 3 };
            var il = new IndexList(gaps, 5);

            NUnit.Framework.Assert.AreEqual(1, il.MinValue);
            NUnit.Framework.Assert.AreEqual(5, il.MaxValue);
            NUnit.Framework.Assert.AreEqual(3, il.Count());
        }
        
        [Test]
        [Author("Travis")]
        [Description("Ensure the IndexList can count correctly when the min value is not 1")]
        [Category("UnitTest,IndexList")]
        public void IndexList_UnitTest_CanCountCorrectlyWhenMinValueGreaterThan1()
        {

            var gaps = new HashSet<int> { 1, 5 };
            var il = new IndexList(gaps, 4, 3);

            NUnit.Framework.Assert.AreEqual(3, il.MinValue);
            NUnit.Framework.Assert.AreEqual(4, il.MaxValue);
            NUnit.Framework.Assert.AreEqual(1, il.Count());
        }
        
    }
}
