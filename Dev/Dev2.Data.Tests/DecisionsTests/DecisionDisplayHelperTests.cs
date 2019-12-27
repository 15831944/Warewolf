using Dev2.Data.Decisions.Operations;
using Dev2.DataList;
using NUnit.Framework;
using Warewolf.Resource.Messages;

namespace Dev2.Data.Tests.DecisionsTests
{
    [TestFixture]
    public class DecisionDisplayHelperTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        public void DecisionDisplayHelper_GetValue_ShouldMatchToRsOpHandleTypes()
        {
            //------------Setup for test--------------------------
            var allOptions = FindRecsetOptions.FindAllDecision();
            var allMatched = true;
            //------------Execute Test---------------------------
            foreach(var findRecsetOptionse in allOptions)
            {
                var decisionType = DecisionDisplayHelper.GetValue(findRecsetOptionse.HandlesType());
                if (decisionType == enDecisionType.Choose)
                {
                    allMatched = false;
                    NUnit.Framework.Assert.Fail($"{findRecsetOptionse.HandlesType()} not found");
                }
            }
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(allMatched);
        }

        [Test]
        [Author("Ashley Lewis")]
        public void DecisionDisplayHelper_GetErrorMessage_EnumShouldMatchToErrorMessage()
        {
            var errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.Choose);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_Choose, "Decision Choose Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsError);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsError, "Decision IsError Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotError);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotError, "Decision IsNotError Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNull);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNull, "Decision IsNull Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotNull);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotNull, "Decision IsNotNull Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNumeric);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNumeric, "Decision IsNumeric Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotNumeric);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotNumeric, "Decision IsNotNumeric Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsText);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsText, "Decision IsText Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotText);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotText, "Decision IsNotText Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsAlphanumeric);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsAlphanumeric, "Decision IsAlphanumeric Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotAlphanumeric);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotAlphanumeric, "Decision IsNotAlphanumeric Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsXML);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsXML, "Decision IsXML Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotXML);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotXML, "Decision IsNotXML Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsDate);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsDate, "Decision IsDate Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotDate);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotDate, "Decision IsNotDate Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsEmail);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsEmail, "Decision IsEmail Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotEmail);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotEmail, "Decision IsNotEmail Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsRegEx);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsRegEx, "Decision IsRegEx Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.NotRegEx);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_NotRegEx, "Decision NotRegEx Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsEqual);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_Equals, "Decision IsEqual Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotEqual);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotEqual, "Decision IsNotEqual Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsLessThan);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsLessThan, "Decision IsLessThan Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsLessThanOrEqual);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsLessThanOrEqual, "Decision IsLessThanOrEqual Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsGreaterThan);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsGreaterThan, "Decision IsGreaterThan Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsGreaterThanOrEqual);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsGreaterThanOrEqual, "Decision IsGreaterThanOrEqual Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsContains);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsContains, "Decision IsContains Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.NotContain);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_NotContain, "Decision NotContain Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsEndsWith);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsEndsWith, "Decision IsEndsWith Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.NotEndsWith);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_NotEndsWith, "Decision NotEndsWith Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsStartsWith);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsStartsWith, "Decision IsStartsWith Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.NotStartsWith);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_NotStartsWith, "Decision NotStartsWith Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsBetween);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsBetween, "Decision IsBetween Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.NotBetween);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_NotBetween, "Decision NotBetween Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsBinary);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsBinary, "Decision IsBinary Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotBinary);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotBinary, "Decision IsNotBinary Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsHex);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsHex, "Decision IsHex Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsNotHex);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsNotHex, "Decision IsNotHex Failure Error Message Wrong.");
            errorMessage = DecisionDisplayHelper.GetFailureMessage(enDecisionType.IsBase64);
            NUnit.Framework.Assert.AreEqual(errorMessage, Messages.Test_FailureMessage_IsBase64, "Decision IsBase64 Failure Error Message Wrong.");
        }
    }
}