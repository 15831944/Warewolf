/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using Dev2.Communication;
using NUnit.Framework;

namespace Dev2.Infrastructure.Tests.Communication
{
    [TestFixture]
    public class MemoTests
    {
        [Test]
        [Description("Constructor must initialize Date to now.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]
        
        public void MemoConstructor_UnitTest_Date_Now()

        {
            var memo = new Memo();
            var diff = DateTime.Now - memo.Date;
            var expected = diff < new TimeSpan(0, 0, 0, 5);
            Assert.IsTrue(expected,string.Format("The date should be close to date time now as it is set in the constructor to DateTime.Now. But got{0}", diff));
        }

        [Test]
        [Description("DateString must return the Date property formatted as yyyy-MM-dd.HH.mm.ss.ffff.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]

        public void MemoDateString_UnitTest_Format_Correct()

        {
            var memo = new Memo();
            Assert.AreEqual(memo.Date.ToString("yyyy-MM-dd.HH.mm.ss.ffff"), memo.DateString);
        }

        [Test]
        [Description("Equals to null returns false.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]

        public void MemoEquals_UnitTest_Null_False()

        {
            var memo = new Memo();
            var result = memo.Equals(null);
            Assert.IsFalse(result);
        }

        [Test]
        [Description("Equals to another type returns false.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]

        public void MemoEquals_UnitTest_OtherType_False()

        {
            var memo = new Memo();
            var result = memo.Equals(new object());
            Assert.IsFalse(result);
        }

        [Test]
        [Description("Equals to same type but different ID returns false.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]

        public void MemoEquals_UnitTest_SameTypeAndDifferentID_False()

        {
            var memo1 = new Memo { InstanceID = Guid.NewGuid() };
            var memo2 = new Memo { InstanceID = Guid.NewGuid() };
            var result = memo1.Equals(memo2);
            Assert.IsFalse(result);
        }

        [Test]
        [Description("Equals to same type and same ID returns true.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]

        public void MemoEquals_UnitTest_SameTypeAndSameID_True()

        {
            var memo1 = new Memo { InstanceID = Guid.NewGuid() };
            var memo2 = new Memo { InstanceID = memo1.InstanceID };
            var result = memo1.Equals(memo2);
            Assert.IsTrue(result);
        }

        [Test]
        [Description("GetHashCode returns hash code of ID property.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]

        public void MemoGetHashCode_UnitTest_Result_HashCodeOfID()

        {
            var memo = new Memo { InstanceID = Guid.NewGuid() };
            var expected = memo.InstanceID.GetHashCode();
            var actual = memo.GetHashCode();
            Assert.AreEqual(expected, actual);
        }
        
        
    }
}
