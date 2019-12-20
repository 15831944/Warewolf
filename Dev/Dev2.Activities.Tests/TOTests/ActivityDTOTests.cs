/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Collections.Generic;
using Dev2.Common.Interfaces.Infrastructure.Providers.Errors;
using Dev2.Common.Interfaces.Infrastructure.Providers.Validation;
using Dev2.Providers.Validation.Rules;
using Dev2.Validation;
using NUnit.Framework;
using Unlimited.Applications.BusinessDesignStudio.Activities;



namespace Dev2.Tests.Activities.TOTests
{
    [TestFixture]
    [SetUpFixture]
    public class ActivityDtoTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_Constructor")]
        public void ActivityDTO_Constructor_EmptyConstructor_SetsFieldNameFieldValueIndexNumber()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var activityDTO = new ActivityDTO();
            //------------Assert Results-------------------------
            Assert.AreEqual("[[Variable]]", activityDTO.FieldName);
            Assert.AreEqual("Expression", activityDTO.FieldValue);
            Assert.AreEqual(0, activityDTO.IndexNumber);
            Assert.AreEqual(false, activityDTO.Inserted);
            Assert.IsNotNull(activityDTO.Errors);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_Constructor")]
        public void ActivityDTO_Constructor_ConstructorWithParameters_SetsProperties()
        {
            //------------Setup for test--------------------------
            const string fieldName = "MyField";
            const string fieldValue = "MyFieldValue";
            const int indexNumber = 2;
            const bool inserted = true;
            //------------Execute Test---------------------------
            var activityDTO = new ActivityDTO(fieldName, fieldValue, indexNumber, inserted);
            //------------Assert Results-------------------------
            Assert.AreEqual(fieldName, activityDTO.FieldName);
            Assert.AreEqual(fieldValue, activityDTO.FieldValue);
            Assert.AreEqual(indexNumber, activityDTO.IndexNumber);
            Assert.AreEqual(inserted, activityDTO.Inserted);
            Assert.IsNotNull(activityDTO.Errors);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_Constructor")]
        public void ActivityDTO_Constructor_Construct_IsInstanceOfTypeIPerformsValidation()
        {
            //------------Setup for test--------------------------
            const string fieldName = "MyField";
            const string fieldValue = "MyFieldValue";
            const int indexNumber = 2;
            const bool inserted = true;
            //------------Execute Test---------------------------
            var activityDTO = new ActivityDTO(fieldName, fieldValue, indexNumber, inserted);
            //------------Assert Results-------------------------
            Assert.IsInstanceOf(activityDTO.GetType(), typeof(IPerformsValidation));
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_OnPropertyChanged")]
        public void ActivityDTO_OnPropertyChanged_FieldNameChanged_FiresPropertyChanged()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            const string value = "value";
            //------------Execute Test---------------------------

            var propertyChangedFired = TestUtils.PropertyChangedTester(activityDTO, () => activityDTO.FieldName, () => activityDTO.FieldName = value);
            //------------Assert Results-------------------------
            Assert.IsTrue(propertyChangedFired);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_OnPropertyChanged")]
        public void ActivityDTO_OnPropertyChanged_FieldValueChanged_FiresPropertyChanged()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            const string value = "value";
            //------------Execute Test---------------------------
            var propertyChangedFired = TestUtils.PropertyChangedTester(activityDTO, () => activityDTO.FieldValue, () => activityDTO.FieldValue = value);
            //------------Assert Results-------------------------
            Assert.IsTrue(propertyChangedFired);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_OnPropertyChanged")]
        public void ActivityDTO_OnPropertyChanged_IndexNumberChanged_FiresPropertyChanged()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            const int value = 2;
            //------------Execute Test---------------------------
            var propertyChangedFired = TestUtils.PropertyChangedTester(activityDTO, () => activityDTO.IndexNumber, () => activityDTO.IndexNumber = value);
            //------------Assert Results-------------------------
            Assert.IsTrue(propertyChangedFired);
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_CanRemove")]
        public void ActivityDTO_CanRemove_FieldNameAndFieldValueEmpty_True()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = "", FieldValue = "" };
            //------------Execute Test---------------------------
            var canRemove = activityDTO.CanRemove();
            //------------Assert Results-------------------------
            Assert.IsTrue(canRemove);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_CanRemove")]
        public void ActivityDTO_CanRemove_FieldNameAndFieldValueNull_True()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = null, FieldValue = null };
            //------------Execute Test---------------------------
            var canRemove = activityDTO.CanRemove();
            //------------Assert Results-------------------------
            Assert.IsTrue(canRemove);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_CanRemove")]
        public void ActivityDTO_CanRemove_FieldNameHasValueAndFieldValueNull_False()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = "FieldName", FieldValue = null };
            //------------Execute Test---------------------------
            var canRemove = activityDTO.CanRemove();
            //------------Assert Results-------------------------
            Assert.IsFalse(canRemove);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_CanRemove")]
        public void ActivityDTO_CanRemove_FieldNameNullAndFieldValueHasValue_False()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = null, FieldValue = "FieldValue" };
            //------------Execute Test---------------------------
            var canRemove = activityDTO.CanRemove();
            //------------Assert Results-------------------------
            Assert.IsFalse(canRemove);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_CanAdd")]
        public void ActivityDTO_CanAdd_FieldNameAndFieldValueEmpty_False()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = "", FieldValue = "" };
            //------------Execute Test---------------------------
            var canRemove = activityDTO.CanAdd();
            //------------Assert Results-------------------------
            Assert.IsFalse(canRemove);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_CanAdd")]
        public void ActivityDTO_CanAdd_FieldNameAndFieldValueNull_False()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = null, FieldValue = null };
            //------------Execute Test---------------------------
            var canRemove = activityDTO.CanAdd();
            //------------Assert Results-------------------------
            Assert.IsFalse(canRemove);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_CanAdd")]
        public void ActivityDTO_CanAdd_FieldNameHasValueAndFieldValueNull_True()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = "FieldName", FieldValue = null };
            //------------Execute Test---------------------------
            var canRemove = activityDTO.CanAdd();
            //------------Assert Results-------------------------
            Assert.IsTrue(canRemove);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_CanAdd")]
        public void ActivityDTO_CanAdd_FieldNameNullAndFieldValueHasValue_True()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = null, FieldValue = "FieldValue" };
            //------------Execute Test---------------------------
            var canRemove = activityDTO.CanAdd();
            //------------Assert Results-------------------------
            Assert.IsTrue(canRemove);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_ClearRow")]
        public void ActivityDTO_ClearRow_Executed_SetsFieldNameFieldValueToEmptyString()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            //------------Precondition----------------------------
            Assert.IsFalse(string.IsNullOrEmpty(activityDTO.FieldName));
            Assert.IsFalse(string.IsNullOrEmpty(activityDTO.FieldValue));
            //------------Execute Test---------------------------
            activityDTO.ClearRow();
            //------------Assert Results-------------------------
            Assert.AreEqual(string.Empty, activityDTO.FieldName);
            Assert.AreEqual(string.Empty, activityDTO.FieldValue);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_ConvertToOutputTO")]
        public void ActivityDTO_ConvertToOutputTO_Executed_ReturnsOutputTO()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();

            //------------Execute Test---------------------------
            var convertToOutputTO = activityDTO.ConvertToOutputTo();
            //------------Assert Results-------------------------
            Assert.IsNotNull(convertToOutputTO);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_OutList")]
        public void ActivityDTO_OutList_Property_ListOfString()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            var expectedOutList = new List<string> { "TestValue" };
            activityDTO.OutList = expectedOutList;
            //------------Execute Test---------------------------
            var outList = activityDTO.OutList;
            //------------Assert Results-------------------------
            CollectionAssert.AreEqual(expectedOutList, outList);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_ValidateRules")]
        public void ActivityDTO_ValidateRules_NullRuleSet_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            //------------Execute Test---------------------------
            var validate = activityDTO.Validate("FieldName", (IRuleSet)null);
            //------------Assert Results-------------------------
            Assert.IsTrue(validate);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_Validate")]
        public void ActivityDTO_Validate_GivenNoRules_ReturnTrue()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            //------------Execute Test---------------------------
            var isValid = activityDTO.Validate("FieldName", new RuleSet());
            //------------Assert Results-------------------------
            Assert.IsTrue(isValid);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_Validate")]
        public void ActivityDTO_Validate_GivenRules_HasFailingRuleReturnFalse()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = null };
            var ruleSet = new RuleSet();
            ruleSet.Add(new IsNullRule(() => activityDTO.FieldName));
            //------------Execute Test---------------------------
            var isValid = activityDTO.Validate("FieldName", ruleSet);
            //------------Assert Results-------------------------
            Assert.IsFalse(isValid);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_Validate")]
        public void ActivityDTO_Validate_GivenRules_HasPassingRuleReturnTrue()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = "FeildName" };
            var ruleSet = new RuleSet();
            ruleSet.Add(new IsNullRule(() => activityDTO.FieldName));
            //------------Execute Test---------------------------
            var isValid = activityDTO.Validate("FieldName", ruleSet);
            //------------Assert Results-------------------------
            Assert.IsTrue(isValid);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_Validate")]
        public void ActivityDTO_Validate_Executed_SetErrorsProperty()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO { FieldName = null };
            var ruleSet = new RuleSet();
            ruleSet.Add(new IsNullRule(() => activityDTO.FieldName));
            //------------Execute Test---------------------------
            activityDTO.Validate("FieldName", ruleSet);
            //------------Assert Results-------------------------
            Assert.AreEqual(1, activityDTO.Errors.Count);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("ActivityDTO_GetRuleSet")]
        public void ActivityDTO_GetRuleSet_OnFieldName_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var activityDto = new ActivityDTO { FieldName = "[[a]]" , FieldValue = "anything"};
            //------------Execute Test---------------------------
            var rulesSet = activityDto.GetRuleSet("FieldName", "");
            //------------Assert Results-------------------------
            Assert.IsNotNull(rulesSet);
            Assert.AreEqual(2, rulesSet.Rules.Count);
            Assert.IsInstanceOf(rulesSet.Rules[0].GetType(), typeof(IsStringEmptyRule));
            Assert.IsInstanceOf(rulesSet.Rules[1].GetType(), typeof(IsValidExpressionRule));
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("ActivityDTO_GetRuleSet")]
        public void ActivityDTO_GetRuleSet_OnFieldValue_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var activityDto = new ActivityDTO { FieldName = "[[a]]", FieldValue = "[[b]]" };
            //------------Execute Test---------------------------
            var rulesSet = activityDto.GetRuleSet("FieldValue", "");
            //------------Assert Results-------------------------
            Assert.IsNotNull(rulesSet);
            Assert.AreEqual(1, rulesSet.Rules.Count);
            Assert.IsInstanceOf(rulesSet.Rules[0].GetType(), typeof(IsValidExpressionRule));
        }



        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_OnPropertyChanged")]
        public void ActivityDTO_OnPropertyChanged_ErrorsChanged_FiresPropertyChanged()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            var value = new Dictionary<string, List<IActionableErrorInfo>>();
            //------------Execute Test---------------------------
            var propertyChangedFired = TestUtils.PropertyChangedTester(activityDTO, () => activityDTO.Errors, () => activityDTO.Errors = value);
            //------------Assert Results-------------------------
            Assert.IsTrue(propertyChangedFired);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ActivityDTO_FieldName")]
        public void ActivityDTO_FieldName_ValidatesForErrors_ReturnsError()
        {
            //------------Setup for test--------------------------
            var activityDTO = new ActivityDTO();
            activityDTO.FieldName = "1";
            //------------Execute Test---------------------------
            activityDTO.Validate(() => activityDTO.FieldName, "");
            //------------Assert Results-------------------------
            Assert.AreEqual(1, activityDTO.Errors.Count);
        }
    }
}
