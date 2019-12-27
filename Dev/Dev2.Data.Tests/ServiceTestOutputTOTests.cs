/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using NUnit.Framework;

namespace Dev2.Data.Tests
{
    [TestFixture]
    public class ServiceTestOutputTOTests
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(ServiceTestOutputTO))]
        public void ServiceTestOutputTO_Validate_Defaults()
        {
            var testRunResult = new Common.Interfaces.TestRunResult
            {
                Message = "Message",
                TestName = "TestName",
                RunTestResult = Common.Interfaces.RunResult.None
            };
            var optionsForValueList = new System.Collections.Generic.List<string>
            {
                ">"
            };

            var serviceTestOutputTO = new ServiceTestOutputTO
            {
                Variable = "[[variable]]",
                Value = "hello",
                From = "from",
                To = "to",
                AssertOp = "=",
                HasOptionsForValue = false,
                OptionsForValue = optionsForValueList,
                Result = testRunResult
            };

            serviceTestOutputTO.OnSearchTypeChanged();

            NUnit.Framework.Assert.AreEqual("[[variable]]", serviceTestOutputTO.Variable);
            NUnit.Framework.Assert.AreEqual("hello", serviceTestOutputTO.Value);
            NUnit.Framework.Assert.AreEqual("from", serviceTestOutputTO.From);
            NUnit.Framework.Assert.AreEqual("to", serviceTestOutputTO.To);
            NUnit.Framework.Assert.AreEqual("=", serviceTestOutputTO.AssertOp);
            NUnit.Framework.Assert.IsFalse(serviceTestOutputTO.HasOptionsForValue);
            NUnit.Framework.Assert.AreEqual(1, serviceTestOutputTO.OptionsForValue.Count);
            NUnit.Framework.Assert.AreEqual(">", serviceTestOutputTO.OptionsForValue[0]);
            NUnit.Framework.Assert.AreEqual(testRunResult, serviceTestOutputTO.Result);
        }
    }
}
