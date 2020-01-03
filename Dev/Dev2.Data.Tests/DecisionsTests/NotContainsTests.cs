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

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    //
    public class NotContainsTests
    {
        [Test]
        [Author("Massimo Guerrera")]
        [Category("NotContains_Invoke")]
        public void NotContains_Invoke_DoesContain_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new NotContains();
            var cols = new string[2];
            cols[0] = "TestData";
            cols[1] = "Test";

            //------------Execute Test---------------------------

            var result = notStartsWith.Invoke(cols);

            //------------Assert Results-------------------------
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("NotContains_Invoke")]
        public void NotContains_Invoke_DoesntContain_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new NotContains();
            var cols = new string[2];
            cols[0] = "TestData";
            cols[1] = "No";

            //------------Execute Test---------------------------

            var result = notStartsWith.Invoke(cols);

            //------------Assert Results-------------------------
            Assert.IsTrue(result);

        }


        [Test]
        [Author("Sanele Mthmembu")]
        [Category("NotContains_HandlesType")]
        public void NotContains_HandlesType_ReturnsNotContainsType()
        {
            var decisionType = enDecisionType.NotContain;
            //------------Setup for test--------------------------
            var notContains = new NotContains();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            Assert.AreEqual(decisionType, notContains.HandlesType());
        }
    }
}
