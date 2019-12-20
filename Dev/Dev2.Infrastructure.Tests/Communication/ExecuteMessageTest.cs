/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Communication;
using NUnit.Framework;

namespace Dev2.Infrastructure.Tests.Communication
{
    [TestFixture]
    [SetUpFixture]
    public class ExecuteMessageTest
    {
        [Test]
        [Author("Travis Frisinger")]
        [Category("ExecuteMessage_Constructor")]
        public void ExecuteMessage_Constructor_WhenSettingMessage_ExpectMessageWithNoErrors()
        {
            //------------Setup for test--------------------------
            var executeMessage = new ExecuteMessage { HasError = false};
            executeMessage.SetMessage("the message");
 
            //------------Assert Results-------------------------

            Assert.AreEqual("the message", executeMessage.Message.ToString());
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("ExecuteMessage_Constructor")]
        public void ExecuteMessage_Constructor_WhenSettingHasErrors_ExpectHasErrorsTrue()
        {
            //------------Setup for test--------------------------
            var executeMessage = new ExecuteMessage { HasError = true};
            executeMessage.SetMessage("the message");

            //------------Assert Results-------------------------

            Assert.IsTrue(executeMessage.HasError);
        }
    }
}
