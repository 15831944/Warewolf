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
    /// <summary>
    /// Summary description for NotStartsWithTests
    /// </summary>
    [TestFixture]
    [SetUpFixture]
    public class NotStartsWithTests
    {
        [Test]
        [Author("Massimo Guerrera")]
        [Category("NotStartsWith_Invoke")]
        public void NotStartsWith_Invoke_DoesStartWith_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new NotStartsWith();
            var cols = new string[2];
            cols[0] = "TestData";
            cols[1] = "Test";
            
            //------------Execute Test---------------------------

            var result = notStartsWith.Invoke(cols);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("NotStartsWith_Invoke")]
        public void NotStartsWith_Invoke_DoesntStartWith_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var notStartsWith = new NotStartsWith();
            var cols = new string[2];
            cols[0] = "TestData";
            cols[1] = "No";
            //------------Execute Test---------------------------

            var result = notStartsWith.Invoke(cols);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("NotStartsWith_HandlesType")]
        public void NotStartsWith_HandlesType_ReturnsNotStartWithType()
        {
            var startsWith = enDecisionType.NotStartsWith;
            //------------Setup for test--------------------------
            var notStartsWith = new NotStartsWith();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(startsWith, notStartsWith.HandlesType());
        }
    }
}
