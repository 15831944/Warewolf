/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.DecisionsTests.Operations
{
    [TestFixture]
    [SetUpFixture]
    public class IsBase64Tests
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsBase64))]
        public void IsBase64_Invoke_ItemsEqual_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isBase64 = new IsBase64();
            var cols = new string[2];
            cols[0] = "aGVsbG8=";
            //------------Execute Test---------------------------
            var result = isBase64.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsBase64))]
        public void IsBase64_Invoke_NotEqualItems_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isBase64 = new IsBase64();
            var cols = new string[2];
            cols[0] = "aGVsbG8ASS@";
            //------------Execute Test---------------------------
            var result = isBase64.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsBase64))]
        public void IsBase64_Invoke_EmptyColumns_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isBase64 = new IsBase64();
            var cols = new string[1];
            cols[0] = null;
            //------------Execute Test---------------------------
            var result = isBase64.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsBase64))]
        public void IsBase64_HandlesType_ReturnsIsBase64Type()
        {
            var expected = enDecisionType.IsBase64;
            //------------Setup for test--------------------------
            var isBase64 = new IsBase64();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(expected, isBase64.HandlesType());
        }
    }
}
