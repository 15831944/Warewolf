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
    [SetUpFixture]
    public class IsErrorTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsError_Invoke")]
        public void GivenSomeString_IsError_Invoke_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var isError = new IsError();
            var cols = new string[2];
            cols[0] = "Eight";
            //------------Execute Test---------------------------
            var result = isError.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Sanele Mthembu")]
        [Category("IsError_Invoke")]
        public void IsError_Invoke_IsError_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var isError = new IsError();
            var cols = new string[2];
            cols[0] = "";
            //------------Execute Test---------------------------
            var result = isError.Invoke(cols);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Sanele Mthmembu")]
        [Category("IsError_HandlesType")]
        public void IsError_HandlesType_ReturnsIsErrorType()
        {
            var decisionType = enDecisionType.IsError;
            //------------Setup for test--------------------------
            var isError = new IsError();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isError.HandlesType());
        }
    }
}
