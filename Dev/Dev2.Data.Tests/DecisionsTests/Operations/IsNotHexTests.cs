/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
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
    /// <summary>
    /// Is Not Hex Decision
    /// </summary>
    [TestFixture]
    [SetUpFixture]
    public class IsNotHexTests
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsNotHex))]
        public void IsNotHex_Invoke_ItemsEqual_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var endsWith = new IsNotHex();
            var cols = new string[1];
            cols[0] = "01";
            //------------Execute Test---------------------------
            var result = endsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsNotHex))]
        public void IsNotHex_Invoke_ItemsEqual_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var endsWith = new IsNotHex();
            var cols = new string[1];
            cols[0] = "BBB";
            //------------Execute Test---------------------------
            var result = endsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsNotHex))]
        public void IsNotHex_Invoke_ItemWithxEqual_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var endsWith = new IsNotHex();
            var cols = new string[1];
            cols[0] = "0x01";
            //------------Execute Test---------------------------
            var result = endsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsNotHex))]
        public void IsNotHex_Invoke_NotEqualItems_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var endsWith = new IsNotHex();
            var cols = new string[2];
            cols[0] = "TestData";
            //------------Execute Test---------------------------
            var result = endsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsNotHex))]
        public void IsNotHex_Invoke_EmptyColumns_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var endsWith = new IsNotHex();
            var cols = new string[1];
            cols[0] = null;
            //------------Execute Test---------------------------
            var result = endsWith.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(IsNotHex))]
        public void IsNotHex_HandlesType_ReturnsIsEndsWithType()
        {
            var expected = enDecisionType.IsNotHex;
            //------------Setup for test--------------------------
            var isEndsWith = new IsNotHex();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(expected, isEndsWith.HandlesType());
        }
    }
}
