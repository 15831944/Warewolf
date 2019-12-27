/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Providers.Validation.Rules;
using Dev2.Validation;
using NUnit.Framework;
using Unlimited.Applications.BusinessDesignStudio.Activities;


namespace Dev2.Tests.Activities.TOTests
{
    [TestFixture]
    public class FindRecordsTOTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("FindRecordsTO_Constructor")]
        public void FindRecordsTO_Constructor_Default_SetsProperties()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var findRecordsTO = new FindRecordsTO();
            //------------Assert Results-------------------------
            Assert.IsNotNull(findRecordsTO);
            Assert.AreEqual("Match On", findRecordsTO.SearchCriteria);
            Assert.AreEqual("Equal", findRecordsTO.SearchType);
            Assert.AreEqual(0, findRecordsTO.IndexNumber);
            Assert.IsFalse(findRecordsTO.Inserted);
            Assert.IsFalse(findRecordsTO.IsSearchCriteriaEnabled);

        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("FindRecordsTO_Constructor")]
        public void FindRecordsTO_ParameterConstructor_SetsProperties()
        {
            //------------Setup for test--------------------------
            const string searchCriteria = "Bob";
            const string searchType = ">";
            const int indexNum = 3;
            //------------Execute Test---------------------------
            var findRecordsTO = new FindRecordsTO(searchCriteria, searchType, indexNum);
            //------------Assert Results-------------------------
            Assert.IsNotNull(findRecordsTO);
            Assert.AreEqual(searchCriteria, findRecordsTO.SearchCriteria);
            Assert.AreEqual(searchType, findRecordsTO.SearchType);
            Assert.AreEqual(indexNum, findRecordsTO.IndexNumber);
            Assert.IsFalse(findRecordsTO.Inserted);
            Assert.IsFalse(findRecordsTO.IsSearchCriteriaEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("FindRecordsTO_SearchType")]
        public void FindRecordsTO_SearchType_SetValue_FiresNotifyPropertyChanged()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO();
            const string searchType = "MyValue";
            //------------Execute Test---------------------------
            
            var notifyPropertyChanged = TestUtils.PropertyChangedTester(findRecordsTO, () => findRecordsTO.SearchType, () => findRecordsTO.SearchType = searchType);
            
            //------------Assert Results-------------------------
            Assert.AreEqual(searchType, findRecordsTO.SearchType);
            Assert.IsTrue(notifyPropertyChanged);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("FindRecordsTO_SearchType")]
        public void FindRecordsTO_SearchCriteria_SetValue_FiresNotifyPropertyChanged()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO();
            const string searchCriteria = "MyValue";
            //------------Execute Test---------------------------
            
            var notifyPropertyChanged = TestUtils.PropertyChangedTester(findRecordsTO, () => findRecordsTO.SearchCriteria, () => findRecordsTO.SearchCriteria = searchCriteria);
            
            //------------Assert Results-------------------------
            Assert.AreEqual(searchCriteria, findRecordsTO.SearchCriteria);
            Assert.IsTrue(notifyPropertyChanged);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("FindRecordsTO_IndexNum")]
        public void FindRecordsTO_IndexNum_SetValue_FiresNotifyPropertyChanged()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO();
            const int indexNum = 5;
            //------------Execute Test---------------------------
            var notifyPropertyChanged = TestUtils.PropertyChangedTester(findRecordsTO, () => findRecordsTO.IndexNumber, () => findRecordsTO.IndexNumber = indexNum);
            //------------Assert Results-------------------------
            Assert.AreEqual(indexNum, findRecordsTO.IndexNumber);
            Assert.IsTrue(notifyPropertyChanged);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("FindRecordsTO_IsSearchCriteriaEnabled")]
        public void FindRecordsTO_IsSearchCriteriaEnabled_SetValue_FiresNotifyPropertyChanged()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO();
            const bool isSearchCriteriaEnabled = true;
            //------------Execute Test---------------------------
            var notifyPropertyChanged = TestUtils.PropertyChangedTester(findRecordsTO, () => findRecordsTO.IsSearchCriteriaEnabled, () => findRecordsTO.IsSearchCriteriaEnabled = isSearchCriteriaEnabled);
            //------------Assert Results-------------------------
            Assert.AreEqual(isSearchCriteriaEnabled, findRecordsTO.IsSearchCriteriaEnabled);
            Assert.IsTrue(notifyPropertyChanged);
        }

        #region CanAdd Tests

        [Test]
        [Author("Massimo Guerrera")]
        [Category("FindRecordsTO_CanAdd")]
        public void FindRecordsTO_CanAdd_SearchTypeEmpty_ReturnFalse()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = string.Empty };
            //------------Execute Test---------------------------
            Assert.IsFalse(findRecordsTO.CanAdd());
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("FindRecordsTO_CanAdd")]
        public void FindRecordsTO_CanAdd_SearchTypeWithData_ReturnTrue()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Contains" };
            //------------Execute Test---------------------------
            Assert.IsTrue(findRecordsTO.CanAdd());
            //------------Assert Results-------------------------
        }

        #endregion

        #region CanRemove Tests

        [Test]
        [Author("Massimo Guerrera")]
        [Category("FindRecordsTO_CanRemove")]
        public void FindRecordsTO_CanRemove_SearchTypeEmptyAndSearchTypeEmpty_ReturnTrue()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = string.Empty, SearchCriteria = string.Empty };
            //------------Execute Test---------------------------
            Assert.IsTrue(findRecordsTO.CanRemove());
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("FindRecordsTO_CanRemove")]
        public void FindRecordsTO_CanRemove_SearchTypeWithDataAndSearchTypeEmpty_ReturnFalse()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Contains", SearchCriteria = string.Empty };
            //------------Execute Test---------------------------
            Assert.IsFalse(findRecordsTO.CanRemove());
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("FindRecordsTO_CanRemove")]
        public void FindRecordsTO_CanRemove_SearchTypeEmptyAndSearchTypeWithData_ReturnFalse()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = string.Empty, SearchCriteria = "Data" };
            //------------Execute Test---------------------------
            Assert.IsFalse(findRecordsTO.CanRemove());
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("FindRecordsTO_GetRuleSet")]
        public void FindRecordsTO_GetRuleSet_OnSearchCriteriaSearchTypeAsStartsWith_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Starts With", SearchCriteria = string.Empty };
            VerifyCorrectRulesForEachFieldSearch(findRecordsTO, "SearchCriteria");
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("FindRecordsTO_GetRuleSet")]
        public void FindRecordsTO_GetRuleSet_OnSearchCriteriaSearchTypeAsEndsWith_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Ends With", SearchCriteria = string.Empty };
            VerifyCorrectRulesForEachFieldSearch(findRecordsTO, "SearchCriteria");
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("FindRecordsTO_GetRuleSet")]
        public void FindRecordsTO_GetRuleSet_OnSearchCriteriaSearchTypeAsDoesntEndWith_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Doesn't End With", SearchCriteria = string.Empty };
            VerifyCorrectRulesForEachFieldSearch(findRecordsTO, "SearchCriteria");
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("FindRecordsTO_GetRuleSet")]
        public void FindRecordsTO_GetRuleSet_OnSearchCriteriaSearchTypeAsDoesntStartWith_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Doesn't Start With", SearchCriteria = string.Empty };
            VerifyCorrectRulesForEachFieldSearch(findRecordsTO, "SearchCriteria");
        }
        
        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("FindRecordsTO_GetRuleSet")]
        public void FindRecordsTO_GetRuleSet_OnFromSearchTypeAsIsBetween_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Is Between", SearchCriteria = string.Empty };
            VerifyCorrectRulesForEachField(findRecordsTO, "From");
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("FindRecordsTO_GetRuleSet")]
        public void FindRecordsTO_GetRuleSet_OnFromSearchTypeAsIsNotBetween_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Is Not Between", SearchCriteria = string.Empty };
            VerifyCorrectRulesForEachField(findRecordsTO, "From");
        }
        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("FindRecordsTO_GetRuleSet")]
        public void FindRecordsTO_GetRuleSet_OnToSearchTypeAsIsBetween_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Is Between", SearchCriteria = string.Empty };
            VerifyCorrectRulesForEachField(findRecordsTO, "To");
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("FindRecordsTO_GetRuleSet")]
        public void FindRecordsTO_GetRuleSet_OnToSearchTypeAsIsNotBetween_ReturnTwoRules()
        {
            //------------Setup for test--------------------------
            var findRecordsTO = new FindRecordsTO { SearchType = "Is Not Between", SearchCriteria = string.Empty };
            VerifyCorrectRulesForEachField(findRecordsTO, "To");
        }


        static void VerifyCorrectRulesForEachField(FindRecordsTO findRecordsTO, string fieldName)
        {
            //------------Execute Test---------------------------
            var rulesSet = findRecordsTO.GetRuleSet(fieldName, "");
            //------------Assert Results-------------------------
            Assert.IsNotNull(rulesSet);
            Assert.IsInstanceOf(typeof(IsStringEmptyRule), rulesSet.Rules[0]);
            Assert.IsInstanceOf(typeof(IsValidExpressionRule), rulesSet.Rules[1]);
        }
        static void VerifyCorrectRulesForEachFieldSearch(FindRecordsTO findRecordsTO, string fieldName)
        {
            //------------Execute Test---------------------------
            var rulesSet = findRecordsTO.GetRuleSet(fieldName, "");
            //------------Assert Results-------------------------
            Assert.IsNotNull(rulesSet);
    
            Assert.IsInstanceOf(typeof(IsValidExpressionRule), rulesSet.Rules[0]);
        }
        #endregion
    }
}
