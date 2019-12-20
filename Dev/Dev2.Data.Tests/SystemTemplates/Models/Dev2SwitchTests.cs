/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Data.SystemTemplates.Models;
using NUnit.Framework;

namespace Dev2.Data.Tests.SystemTemplates.Models
{
    [TestFixture]
    [SetUpFixture]
    public class Dev2SwitchTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(Dev2Switch))]
        public void Dev2Switch_SetProperties_AreEqual_ExpectTrue()
        {
            //-----------------------Arrange--------------------
            //-----------------------Act------------------------
            var dev2Switch = new Dev2Switch()
            {
                SwitchVariable = "TestSwitchVariable",
                SwitchExpression = "TestSwitchExpression",
                DisplayText = "TestDisplayText"
            };
            //-----------------------Assert---------------------
            NUnit.Framework.Assert.AreEqual(Dev2ModelType.Dev2Switch, dev2Switch.ModelName);
            NUnit.Framework.Assert.AreEqual("TestSwitchVariable", dev2Switch.SwitchVariable);
            NUnit.Framework.Assert.AreEqual("TestSwitchExpression", dev2Switch.SwitchExpression);
            NUnit.Framework.Assert.AreEqual("TestDisplayText", dev2Switch.DisplayText);
        }
    }
}
