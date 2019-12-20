/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Dev2.Common;
using Dev2.Common.ExtMethods;
using Dev2.Common.Interfaces.StringTokenizer.Interfaces;
using Dev2.Data.TO;
using Dev2.Data.Util;
using NUnit.Framework;
using Moq;
using ServiceStack.Text;
using Warewolf.Storage;
using Warewolf.Storage.Interfaces;
using Dev2.Data.Interfaces.Enums;

namespace Dev2.Data.Tests.BinaryDataList
{
    [TestFixture]
    [SetUpFixture]

    public class DataListUtilTest
    {

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsArray_GivenGivenNotArray_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var scalr = "[[a]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isArray = DataListUtil.IsArray(ref scalr);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(isArray);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsArray_GivenGivenArray_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var scalr = "[[a()]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isArray = DataListUtil.IsArray(ref scalr);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(isArray);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GetAllPossibleExpressionsForFunctionOperations_GivenValidArgs_ShouldReturnsCorrectly()
        {
            //---------------Set up test pack-------------------
            var env = new Mock<IExecutionEnvironment>();
            env.Setup(environment => environment.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<string>());
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            try
            {
                var operations = DataListUtil.GetAllPossibleExpressionsForFunctionOperations("[[a]]", env.Object, out ErrorResultTO errorResultTO, 1);
                NUnit.Framework.Assert.AreEqual(0, operations.Count);
                env.Setup(environment => environment.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception("error"));
                DataListUtil.GetAllPossibleExpressionsForFunctionOperations("[[a]]", env.Object, out errorResultTO, 1);
            }
            catch (Exception ex)
            {
                NUnit.Framework.Assert.AreEqual("error", ex.Message);

            }
            //---------------Test Result -----------------------
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GenerateDefsFromDataList_GivenDataList_ShouldReurnList()
        {
            //---------------Set up test pack-------------------
            const string trueString = "True";
            const string noneString = "None";
            var datalist = string.Format("<DataList><var Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><a Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><rec Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" ><set Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /></rec></DataList>", trueString, noneString);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            try
            {
                var generateDefsFromDataList = DataListUtil.GenerateDefsFromDataList(datalist, enDev2ColumnArgumentDirection.Input);
                NUnit.Framework.Assert.IsNotNull(generateDefsFromDataList);
            }
            catch (Exception ex)
            {
                NUnit.Framework.Assert.Fail(ex.Message);

            }

            //---------------Test Result -----------------------


        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GenerateDefsFromDataList_GivenDataList_ShouldReurnListWithEntries()
        {
            //---------------Set up test pack-------------------
            const string trueString = "True";
            const string noneString = "Input";
            var datalist = string.Format("<DataList><var Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><a Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><rec Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" ><set Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /></rec></DataList>", trueString, noneString);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            try
            {
                var generateDefsFromDataList = DataListUtil.GenerateDefsFromDataList(datalist, enDev2ColumnArgumentDirection.Input);
                NUnit.Framework.Assert.IsNotNull(generateDefsFromDataList);
                NUnit.Framework.Assert.AreNotEqual(0, generateDefsFromDataList.Count);
            }
            catch (Exception ex)
            {
                NUnit.Framework.Assert.Fail(ex.Message);

            }

            //---------------Test Result -----------------------


        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataListUtil_IsJson")]
        public void DataListUtil_IsJson_WhenValidJsonString_ExpectTrue()
        {//------------Setup for test--------------------------
            const string startingData = "{ \"message\" : \"Howzit, Samantha\"}";
            //------------Execute Test---------------------------
            var result = DataListUtil.IsJson(startingData);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result, "Else Valid JSON not detected as such");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataListUtil_IsJson")]
        public void DataListUtil_IsJson_WhenValidJsonStringWithExtraWhitespace_ExpectTrue()
        {//------------Setup for test--------------------------
            const string startingData = " { \"message\" : \"Howzit, Samantha\"} ";
            //------------Execute Test---------------------------
            var result = DataListUtil.IsJson(startingData);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result, "Else Valid JSON not detected as such");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataListUtil_IsJson")]
        public void DataListUtil_IsJson_WhenInvalidJsonString_ExpectFalse()
        {//------------Setup for test--------------------------
            const string startingData = "<\"message\" : \"Howzit, Samantha\">";
            //------------Execute Test---------------------------
            var result = DataListUtil.IsJson(startingData);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result, "Invalid JSON not detected as such");
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("DataListUtil_AdjustForEncodingIssues")]
        public void DataListUtil_AdjustForEncodingIssues_WithStringLengthOf3_SameDataAsPassedIn()
        {
            //------------Setup for test--------------------------
            const string startingData = "A A";
            //------------Execute Test---------------------------
            var result = DataListUtil.AdjustForEncodingIssues(startingData);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(startingData, result, "The data has changed when there was no encoding issues.");
        }

        [Test]
        [Author("eon Rajindrapersadh")]
        [Category("DataListUtil_AdjustForEncodingIssues")]
        public void DataListUtil_AdjustForEncodingIssues_BOMRemoved()
        {
            //------------Setup for test--------------------------
            const char c = (char)65279;
            var startingData = c + "<A></A>";
            NUnit.Framework.Assert.IsFalse(startingData.StartsWith("<", StringComparison.OrdinalIgnoreCase));
            //------------Execute Test---------------------------
            var result = DataListUtil.AdjustForEncodingIssues(startingData);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result.StartsWith("<", StringComparison.Ordinal));
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataListUtil_IsSystemTag")]
        public void DataListUtil_IsSystemTag_WhenNoPrefix_ExpectSystemTagDetected()
        {
            //------------Setup for test--------------------------
            const string tag = "ManagmentServicePayload";

            //------------Execute Test---------------------------
            var result = DataListUtil.IsSystemTag(tag);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataListUtil_IsSystemTag")]
        public void DataListUtil_IsSystemTag_WhenDev2SystemPrefix_ExpectSystemTagDetected()
        {
            //------------Setup for test--------------------------
            var tag = GlobalConstants.SystemTagNamespaceSearch + "ManagmentServicePayload";

            //------------Execute Test---------------------------
            var result = DataListUtil.IsSystemTag(tag);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Travis")]
        [Description("Ensure star is replaced with an index")]
        [Category("DataListUtil,UnitTest")]
        public void DataListUtil_UnitTest_ReplaceStarWithFixedIndex()
        {
            const string exp = "[[rs(*).val]]";
            var result = DataListUtil.ReplaceStarWithFixedIndex(exp, 1);

            NUnit.Framework.Assert.AreEqual("[[rs(1).val]]", result, "Did not replace index in recordset");
        }

        [Test]
        [Author("Travis")]
        [Description("Ensure star is not replaced with an invalid index")]
        [Category("DataListUtil,UnitTest")]
        public void DataListUtil_UnitTest_NotReplaceStarWithInvalidIndex()
        {
            const string exp = "[[rs(*).val]]";
            var result = DataListUtil.ReplaceStarWithFixedIndex(exp, -1);

            NUnit.Framework.Assert.AreEqual("[[rs(*).val]]", result, "Replaced with invalid index in recordset");
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("DataListUtil,UnitTest")]
        public void DataListUtil_UnitTest_NotReplaceObjectBlankWithIndex()
        {
            const string exp = "[[@rs().val]]";
            var result = DataListUtil.ReplaceObjectBlankWithIndex(exp, 1);

            NUnit.Framework.Assert.AreEqual("[[@rs(1).val]]", result, "Replaced with invalid index in recordset");
        }


        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataListUtil_UpsertTokens_NullTarget_ThrowsArgumentNullException()
        {
            //------------Setup for test--------------------------

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(null, null);

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_NullTokenizer_ClearsTarget()
        {
            //------------Setup for test--------------------------
            var target = new Collection<ObservablePair<string, string>>
            {
                new ObservablePair<string, string>("key1", "value1"),
                new ObservablePair<string, string>("key2", "value2")
            };

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, null);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(0, target.Count);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_ValidTokenizer_AddsToTarget()
        {
            //------------Setup for test--------------------------
            const int TokenCount = 5;
            var tokenNumber = 0;

            var tokenizer = new Mock<IDev2Tokenizer>();
            tokenizer.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber < TokenCount);
            tokenizer.Setup(t => t.NextToken()).Returns(() => $"[[Var{tokenNumber++}]]");

            var target = new Collection<ObservablePair<string, string>>();

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, tokenizer.Object);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(TokenCount, target.Count);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_ValidTokenizer_AddsDuplicatesToTarget()
        {
            //------------Setup for test--------------------------
            const int DuplicateCount = 2;
            const int TokenCount = 3;
            var tokenNumber = 0;
            var iterCount = 0;
            var tokenizer = new Mock<IDev2Tokenizer>();
            tokenizer.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber < TokenCount);
            tokenizer.Setup(t => t.NextToken()).Returns(() =>
            {
                var result = $"[[Var{tokenNumber}]]";
                if (++iterCount % DuplicateCount == 0)
                {
                    tokenNumber++;
                }
                return result;
            });

            var target = new Collection<ObservablePair<string, string>>();

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, tokenizer.Object);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(TokenCount * DuplicateCount, target.Count);
            var groups = target.GroupBy(g => g.Key).ToList();
            NUnit.Framework.Assert.AreEqual(3, groups.Count);
            foreach (var grp in groups)
            {
                var enumerator = grp.GetEnumerator();
                var duplicateCount = 0;
                while (enumerator.MoveNext())
                {
                    duplicateCount++;
                }
                NUnit.Framework.Assert.AreEqual(DuplicateCount, duplicateCount);
            }
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_ValidTokenizer_ClearsTargetFirst()
        {
            //------------Setup for test--------------------------
            const int TokenCount1 = 5;
            var tokenNumber1 = 0;

            var tokenizer = new Mock<IDev2Tokenizer>();
            tokenizer.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber1 < TokenCount1);
            tokenizer.Setup(t => t.NextToken()).Returns(() => $"[[Var{tokenNumber1++}]]");

            var target = new Collection<ObservablePair<string, string>>();

            DataListUtil.UpsertTokens(target, tokenizer.Object);

            // Create a second tokenizer that will return 2 vars with the same name as the first tokenizer and 1 new one
            const int ExpectedCount = 3;
            const int TokenCount2 = 6;
            var tokenNumber2 = 3;
            var tokenizer2 = new Mock<IDev2Tokenizer>();
            tokenizer2.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber2 < TokenCount2);
            tokenizer2.Setup(t => t.NextToken()).Returns(() => $"[[Var{tokenNumber2++}]]");


            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, tokenizer2.Object);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(ExpectedCount, target.Count);

            // Expect only vars from second tokenizer
            var keys = target.Select(p => p.Key);
            var i = 3;
            foreach (var key in keys)
            {
                var expected = $"[[Var{i++}]]";
                NUnit.Framework.Assert.AreEqual(expected, key);
            }
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_ValidTokenizer_StripsWhiteSpaces()
        {
            //------------Setup for test--------------------------
            const int TokenCount1 = 5;
            var tokenNumber1 = 0;

            var tokenizer = new Mock<IDev2Tokenizer>();
            tokenizer.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber1 < TokenCount1);
            tokenizer.Setup(t => t.NextToken()).Returns(() => $"[[Var {tokenNumber1++}]]");

            var target = new Collection<ObservablePair<string, string>>();

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, tokenizer.Object);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(5, target.Count);

            // Expect only vars from second tokenizer
            var keys = target.Select(p => p.Key);
            var i = 0;
            foreach (var key in keys)
            {
                var expected = $"[[Var{i++}]]";
                NUnit.Framework.Assert.AreEqual(expected, key);
            }
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_ValidTokenizer_AddsPrefixToKey()
        {
            //------------Setup for test--------------------------
            const int TokenCount = 5;
            var tokenNumber = 0;

            var tokenizer = new Mock<IDev2Tokenizer>();
            tokenizer.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber < TokenCount);
            tokenizer.Setup(t => t.NextToken()).Returns(() => $"[[Var{tokenNumber++}]]");

            var target = new Collection<ObservablePair<string, string>>();

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, tokenizer.Object, "prefix", null, true);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(TokenCount, target.Count);

            var keys = target.Select(p => p.Key);
            var i = 0;
            foreach (var key in keys)
            {
                var expected = $"[[prefixVar{i++}]]";
                NUnit.Framework.Assert.AreEqual(expected, key);
            }
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_ValidTokenizer_AddsSuffixToKey()
        {
            //------------Setup for test--------------------------
            const int TokenCount = 5;
            var tokenNumber = 0;

            var tokenizer = new Mock<IDev2Tokenizer>();
            tokenizer.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber < TokenCount);
            tokenizer.Setup(t => t.NextToken()).Returns(() => $"[[Var{tokenNumber++}]]");

            var target = new Collection<ObservablePair<string, string>>();

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, tokenizer.Object, "prefix", "suffix", true);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(TokenCount, target.Count);

            var keys = target.Select(p => p.Key);
            var i = 0;
            foreach (var key in keys)
            {
                var expected = $"[[prefixVar{i++}suffix]]";
                NUnit.Framework.Assert.AreEqual(expected, key);
            }
        }


        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_ValidTokenizer_RemoveEmptyEntriesIsFalse_AddsEmptyEntriesToTarget()
        {
            //------------Setup for test--------------------------
            const int TokenCount = 5;
            var tokenNumber = 0;

            var tokens = new List<string> { "f1", "", "f3", "f4", "" };

            var tokenizer = new Mock<IDev2Tokenizer>();

            tokenizer.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber < TokenCount);

            tokenizer.Setup(t => t.NextToken()).Returns(() => tokens[tokenNumber++]);

            var target = new Collection<ObservablePair<string, string>>();

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, tokenizer.Object, "rs(*).", "a", false);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(TokenCount, target.Count);

            for (var i = 0; i < TokenCount; i++)
            {
                if (i == 1 || i == 4)
                {
                    NUnit.Framework.Assert.AreEqual(string.Empty, target[i].Key);
                }
                else
                {
                    NUnit.Framework.Assert.AreEqual($"[[rs(*).{tokens[i]}a]]", target[i].Key);
                }
            }
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataListUtil_UpsertTokens")]
        public void DataListUtil_UpsertTokens_ValidTokenizer_RemoveEmptyEntriesIsTrue_RemovesEmptyEntriesFromTarget()
        {
            //------------Setup for test--------------------------
            const int TokenCount = 5;
            var tokenNumber = 0;

            var tokens = new List<string> { "f1", "", "f2", "f3", "" };

            var tokenizer = new Mock<IDev2Tokenizer>();

            tokenizer.Setup(t => t.HasMoreOps()).Returns(() => tokenNumber < TokenCount);

            tokenizer.Setup(t => t.NextToken()).Returns(() => tokens[tokenNumber++]);

            var target = new Collection<ObservablePair<string, string>>();

            //------------Execute Test---------------------------
            DataListUtil.UpsertTokens(target, tokenizer.Object, "rs(*).", "a", true);

            //------------Assert Results-------------------------
            const int ExpectedCount = TokenCount - 2;

            NUnit.Framework.Assert.AreEqual(ExpectedCount, target.Count);

            for (var i = 0; i < ExpectedCount; i++)
            {
                NUnit.Framework.Assert.AreEqual($"[[rs(*).f{i + 1}a]]", target[i].Key);
            }
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ExtractFieldNameOnlyFromValue_GivenHasClosingBrace_ShouldExctractFieldName()
        {
            //---------------Set up test pack-------------------
            const string recSetFiedWithNoClosingBrace = "[[rec().Name]]";
            const string expectedFielName = "Name";
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = DataListUtil.ExtractFieldNameOnlyFromValue(recSetFiedWithNoClosingBrace);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(expectedFielName, result);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ExtractFieldNameOnlyFromValue_GivenHasNoClosingBrace_ShouldExctractFieldName()
        {
            //---------------Set up test pack-------------------
            const string recSetFiedWithNoClosingBrace = "rec().Name";
            const string expectedFielName = "Name";
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = DataListUtil.ExtractFieldNameOnlyFromValue(recSetFiedWithNoClosingBrace);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(expectedFielName, result);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordsetIndexWithBlank_GivenRecSetWithNoIndex_ShouldReturnOriginalExp()
        {
            //---------------Set up test pack-------------------
            const string recName = "rec().name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var indexWithBlank = DataListUtil.ReplaceRecordsetIndexWithBlank(recName);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(recName, indexWithBlank);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordsetIndexWithBlank_GivenRecSetWithIndex_ShouldRemoveExp()
        {
            //---------------Set up test pack-------------------
            const string recName = "rec(1).name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var indexWithBlank = DataListUtil.ReplaceRecordsetIndexWithBlank(recName);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec().name", indexWithBlank);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordsetIndexWithStar_GivenRecSetWithIndex1_ShouldReplaceWithStar()
        {
            //---------------Set up test pack-------------------
            const string recName = "rec(1).name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var indexWithStar = DataListUtil.ReplaceRecordsetIndexWithStar(recName);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(*).name", indexWithStar);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordsetIndexWithStar_GivenRecSetWithStar_ShouldReturnSame()
        {
            //---------------Set up test pack-------------------
            const string recName = "rec(*).name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var indexWithStar = DataListUtil.ReplaceRecordsetIndexWithStar(recName);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(*).name", indexWithStar);
        }
        [Test]
        public void VariableNameToMapTo_GivenVariable_ShouldReturnVariableName()
        {
            //---------------Set up test pack-------------------
            const string recName = "[[name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var variableName = DataListUtil.GetVariableNameToMapOutputTo(recName);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("name", variableName);
        }


        [Test]
        public void FieldNameToMapTo_GivenRecordSet_ShouldReturnFieldName()
        {
            //---------------Set up test pack-------------------
            const string recName = "[[rec(*).name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var fieldName = DataListUtil.GetVariableNameToMapOutputTo(recName);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("name", fieldName);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsCalcEvaluation_GivenNonCalcExp_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string exp = "rec(*).name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isCalcEvaluation = DataListUtil.IsCalcEvaluation(exp, out string newExp);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(isCalcEvaluation);
            NUnit.Framework.Assert.AreEqual(string.Empty, newExp);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsCalcEvaluation_GivenCalcTxtExp_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var exp = GlobalConstants.CalculateTextConvertPrefix + "rec(*).name" + GlobalConstants.CalculateTextConvertSuffix;
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isCalcEvaluation = DataListUtil.IsCalcEvaluation(exp, out string newExp);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(isCalcEvaluation);
            NUnit.Framework.Assert.AreEqual("rec(*).name", newExp);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsCalcEvaluation_GivenCalcAggExp_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var exp = GlobalConstants.AggregateCalculateTextConvertPrefix + "rec(*).name" + GlobalConstants.AggregateCalculateTextConvertSuffix;
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isCalcEvaluation = DataListUtil.IsCalcEvaluation(exp, out string newExp);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(isCalcEvaluation);
            NUnit.Framework.Assert.AreEqual("rec(*).name", newExp);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsCalcEvaluation_GivenStartWithAggCalcAggExp_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var exp = GlobalConstants.AggregateCalculateTextConvertPrefix + "rec(*).name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isCalcEvaluation = DataListUtil.IsCalcEvaluation(exp, out string newExp);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(isCalcEvaluation);
            NUnit.Framework.Assert.AreEqual(string.Empty, newExp);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsCalcEvaluation_GivenEndsWithAggCalcAggExp_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var exp = "rec(*).name" + GlobalConstants.AggregateCalculateTextConvertSuffix;
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isCalcEvaluation = DataListUtil.IsCalcEvaluation(exp, out string newExp);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(isCalcEvaluation);
            NUnit.Framework.Assert.AreEqual(string.Empty, newExp);
        }


        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsFullyEvaluated_GivenValidVariable_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            const string v = "[[a]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var encrypted = DataListUtil.IsFullyEvaluated(v);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(encrypted);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsFullyEvaluated_GivenINotClosedVariable_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string v = "[[a";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var encrypted = DataListUtil.IsFullyEvaluated(v);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(encrypted);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsFullyEvaluated_GivenInValidVariable_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string v = "";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var encrypted = DataListUtil.IsFullyEvaluated(v);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(encrypted);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void NotEncrypted_GivenValidVariable_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            const string v = "[[a]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var encrypted = DataListUtil.NotEncrypted(v);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(encrypted);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void NotEncrypted_GivenEmptyString_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string v = "";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var encrypted = DataListUtil.NotEncrypted(v);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(encrypted);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordsetBlankWithIndex_GivenIndex1_ShouldAppendIndex()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec().Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var fixedRexSet = DataListUtil.ReplaceRecordsetBlankWithIndex(reSet, 1);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(1).Name", fixedRexSet);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordsetBlankWithIndex_GivenHasIndex_ShouldNotAppendIndex()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec(1).Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var fixedRexSet = DataListUtil.ReplaceRecordsetBlankWithIndex(reSet, 2);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(1).Name", fixedRexSet);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordsetBlankWithStar_GivenRecordSetWithIndex_ShouldAppendStar()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec(1).Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var recordsetBlankWithStar = DataListUtil.ReplaceRecordsetBlankWithStar(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(1).Name", recordsetBlankWithStar);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordsetBlankWithStar_GivenRecordSetWithBlank_ShouldAppendStar()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec().Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var recordsetBlankWithStar = DataListUtil.ReplaceRecordsetBlankWithStar(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(*).Name", recordsetBlankWithStar);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordBlankWithStar_GivenHasIndex_ShouldNotAppendStar()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec(1).Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var recordsetBlankWithStar = DataListUtil.ReplaceRecordBlankWithStar(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(reSet, recordsetBlankWithStar);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ReplaceRecordBlankWithStar_GivenHasBlank_ShouldAppendStar()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec().Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var recordsetBlankWithStar = DataListUtil.ReplaceRecordBlankWithStar(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(*).Name", recordsetBlankWithStar);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void HasNegativeIndex_GivenRecordSetWithNoIndexAndNoBraces_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec().Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var hasNegativeIndex = DataListUtil.HasNegativeIndex(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(hasNegativeIndex);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void HasNegativeIndex_GivenRecordSetWithBracketsAndIndex_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec(1).Name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var hasNegativeIndex = DataListUtil.HasNegativeIndex(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(hasNegativeIndex);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void HasNegativeIndex_GivenRecordSetWithBlankIndex_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec().Name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var hasNegativeIndex = DataListUtil.HasNegativeIndex(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(hasNegativeIndex);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void HasNegativeIndex_GivenRecordSetNegetiveIndex_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec(-1).Name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var hasNegativeIndex = DataListUtil.HasNegativeIndex(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(hasNegativeIndex);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ExtractFieldNameFromValue_GivenRecSet_ShouldExtractField()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec(-1).Name]]";
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var field = DataListUtil.ExtractFieldNameFromValue(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("Name", field);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ExtractFieldNameFromValue_GivenRecSetWithNoBrackets_ShouldExtractField()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec(-1).Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var field = DataListUtil.ExtractFieldNameFromValue(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("Name", field);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ExtractFieldNameFromValue_GivenRecSetWithNoIndex_ShouldExtractField()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec().Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var field = DataListUtil.ExtractFieldNameFromValue(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("Name", field);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ExtractRecordsetNameFromValue_GivenRecsetNoBrackets_ShouldExtractRecname()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec().Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var recName = DataListUtil.ExtractRecordsetNameFromValue(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec", recName);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ExtractRecordsetNameFromValue_GivenValidRecset_ShouldExtractRecname()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec().Name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var recName = DataListUtil.ExtractRecordsetNameFromValue(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec", recName);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void StripBracketsFromValue_GivenValueWithBrackets_ShouldRemoveBrackets()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec().Name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var noBrackets = DataListUtil.StripBracketsFromValue(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec().Name", noBrackets);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void StripBracketsFromValue_GivenValueWithNoBrackets_ShouldReturnValue()
        {
            //---------------Set up test pack-------------------
            const string reSet = "rec().Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var noBrackets = DataListUtil.StripBracketsFromValue(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec().Name", noBrackets);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void MakeValueIntoHighLevelRecordset_GivenRecsetWithBlankAndAddStarNotation_ShouldAppendStar()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var highLevelRecordset = DataListUtil.MakeValueIntoHighLevelRecordset(reSet, true);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(*)", highLevelRecordset);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void MakeValueIntoHighLevelRecordset_GivenRecsetWithBlank_ShouldAppendBraces()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var highLevelRecordset = DataListUtil.MakeValueIntoHighLevelRecordset(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec()", highLevelRecordset);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void MakeValueIntoHighLevelRecordset_GivenRecsetEndsWithOpenBracket_ShouldAppendBraces()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec(";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var highLevelRecordset = DataListUtil.MakeValueIntoHighLevelRecordset(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec()", highLevelRecordset);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void MakeValueIntoHighLevelRecordset_GivenRecsetEndsCloseWithBrackets_ShouldAppendBraces()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec)";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var highLevelRecordset = DataListUtil.MakeValueIntoHighLevelRecordset(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec()", highLevelRecordset);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsStarIndex_GivenHasStar_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec(*)";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isStarIndex = DataListUtil.IsStarIndex(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(isStarIndex);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsStarIndex_GivenDoesNotContainStar_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string reSet = "[[rec()";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isStarIndex = DataListUtil.IsStarIndex(reSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(isStarIndex);
            NUnit.Framework.Assert.IsFalse(DataListUtil.IsStarIndex(string.Empty));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ShouldEncrypt_GivenEmptyValue_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string value = "";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var shouldEncrypt = DataListUtil.ShouldEncrypt(value);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(shouldEncrypt);
            NUnit.Framework.Assert.IsFalse(shouldEncrypt);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ShouldEncrypt_GivenInvalidRecset_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            const string value = "rec";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var shouldEncrypt = DataListUtil.ShouldEncrypt(value);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(shouldEncrypt);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ShouldEncrypt_GivenValidRecset_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string value = "[[rec(1).name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var shouldEncrypt = DataListUtil.ShouldEncrypt(value);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(shouldEncrypt);
        }
        public class Car
        {
            public string Name { get; set; }
            public string SurName { get; set; }
            public List<Car> Cars { get; set; }
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ConvertFromJsonToModel_GivenvalidModel_ShouldConvertBack()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            const string cars = "{ \"Name\":\"Mick\",\"SurName\":\"Mouse\",\"Cars\":[]}";
            var model = DataListUtil.ConvertFromJsonToModel<Car>(new StringBuilder(cars));
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsNotNull(model);
            NUnit.Framework.Assert.AreEqual(typeof(Car), model.GetType());
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ConvertModelToJson_GivenValidJson_ShouldCreateModel()
        {
            //---------------Set up test pack-------------------
            var car = new Car
            {
                Cars = new List<Car>()
               ,
                Name = "Mick"
               ,
                SurName = "Mouse"
            };
            const string cars = "{ \"Name\":\"Mick\",\"SurName\":\"Mouse\",\"Cars\":[]}";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var builder = DataListUtil.ConvertModelToJson(car);
            //---------------Test Result -----------------------
            var expected = cars.RemoveWhiteSpace().ToJson();
            var actual = builder.ToString().RemoveWhiteSpace().ToJson();
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GenerateDefsFromDataListForDebug_GivenEmpty_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var generateDefsFromDataListForDebug = DataListUtil.GenerateDefsFromDataListForDebug("", enDev2ColumnArgumentDirection.Output);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(0, generateDefsFromDataListForDebug.Count);
        }
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GenerateDefsFromDataListForDebug_GivenEmptyDataList_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var generateDefsFromDataListForDebug = DataListUtil.GenerateDefsFromDataListForDebug("<Datalist></Datalist>", enDev2ColumnArgumentDirection.Output);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(0, generateDefsFromDataListForDebug.Count);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GenerateDefsFromDataListForDebug_GivenLoadedDataList_ShouldReturnDebugList()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            const string datalist = @"<DataList><Car Description=""A recordset of information about a car"" IsEditable=""True"" ColumnIODirection=""Both"" ><Make Description=""Make of vehicle"" IsEditable=""True"" ColumnIODirection=""None"" /><Model Description=""Model of vehicle"" IsEditable=""True"" ColumnIODirection=""None"" /></Car><Country Description=""name of Country"" IsEditable=""True"" ColumnIODirection=""Both"" /><Person Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ><Age Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ></Age><Name Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ></Name><School Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ><Name Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ></Name><Location Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ></Location></School></Person></DataList>";
            var generateDefsFromDataListForDebug = DataListUtil.GenerateDefsFromDataListForDebug(datalist, enDev2ColumnArgumentDirection.Output);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreNotEqual(0, generateDefsFromDataListForDebug.Count);
            NUnit.Framework.Assert.AreEqual(2, generateDefsFromDataListForDebug.Count);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void CreateRecordsetDisplayValue_GivenValues_ShouldCreateDisplayValue()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var displayValue = DataListUtil.CreateRecordsetDisplayValue("rec", "Name", "1");
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec(1).Name", displayValue);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GenerateSerializableDefsFromDataList_GivenInput_ShouldCreateInputDefination()
        {
            //---------------Set up test pack-------------------
            const enDev2ColumnArgumentDirection enDev2ColumnArgumentDirection = enDev2ColumnArgumentDirection.Input;
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var list = DataListUtil.GenerateSerializableDefsFromDataList("<DataList></DataList>", enDev2ColumnArgumentDirection);
            //---------------Test Result -----------------------
            var containsInput = list.Contains("Input");
            NUnit.Framework.Assert.IsTrue(containsInput);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void GenerateSerializableDefsFromDataList_GivenOutput_ShouldCreateOutPutDefination()
        {
            //---------------Set up test pack-------------------
            const enDev2ColumnArgumentDirection enDev2ColumnArgumentDirection = enDev2ColumnArgumentDirection.Output;
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var list = DataListUtil.GenerateSerializableDefsFromDataList("<DataList></DataList>", enDev2ColumnArgumentDirection);
            //---------------Test Result -----------------------
            var containsInput = list.Contains("Output");
            NUnit.Framework.Assert.IsTrue(containsInput);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void RemoveLanguageBrackets_GivenStringWithBrackets_ShouldReplaceWithNothing()
        {
            //---------------Set up test pack-------------------
            var s1 = "[[V1]]";
            var s2 = "V1]]";
            var s3 = "V1]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            s1 = DataListUtil.RemoveLanguageBrackets(s1);
            s2 = DataListUtil.RemoveLanguageBrackets(s2);
            s3 = DataListUtil.RemoveLanguageBrackets(s3);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("V1", s1);
            NUnit.Framework.Assert.AreEqual("V1", s2);
            NUnit.Framework.Assert.AreEqual("V1", s3);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsValueRecordsetWithFields_GivenValusHasRecsetNotation_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var rec = "[[rec().Name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isValueRecordsetWithFields = DataListUtil.IsValueRecordsetWithFields(rec);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(isValueRecordsetWithFields);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsValueRecordsetWithFields_GivenValueIsScalr_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            const string rec = "[[Name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isValueRecordsetWithFields = DataListUtil.IsValueRecordsetWithFields(rec);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(isValueRecordsetWithFields);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsXml_GivenNotXml_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var noXml = "kkk";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isXml = DataListUtil.IsXml(noXml, out bool isFragment);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(isXml);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsXml_GivenNotXml_ShouldReturnFalse_Overload()
        {
            //---------------Set up test pack-------------------
            var noXml = "kkk";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isXml = DataListUtil.IsXml(noXml);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsFalse(isXml);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsXml_GivenXml_ShouldReturntrue()
        {
            //---------------Set up test pack-------------------
            const string noXml = "<Person></Person>";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isXml = DataListUtil.IsXml(noXml, out bool isFragment);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(isXml);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsXml_GivenXml_ShouldReturntrue_Overload()
        {
            //---------------Set up test pack-------------------
            const string noXml = "<Person></Person>";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var isXml = DataListUtil.IsXml(noXml);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(isXml);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void RemoveRecordsetBracketsFromValue_GivenRecSet_ShouldRemoveBrackets()
        {
            //---------------Set up test pack-------------------
            const string recSet = "rec().name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var removeRecordsetBracketsFromValue = DataListUtil.RemoveRecordsetBracketsFromValue(recSet);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("rec.name", removeRecordsetBracketsFromValue);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void IsValueScalar_GivenNotScalr_ShouldreturnFalse()
        {
            //---------------Set up test pack-------------------
            const string cmlObj = "[[person.name]]";
            const string recSet = "[[person().name]]";
            const string sclar = "[[name]]";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var cmlpIsScalr = DataListUtil.IsValueScalar(cmlObj);
            var recSetIsScalr = DataListUtil.IsValueScalar(recSet);
            var sclarIsScalr = DataListUtil.IsValueScalar(sclar);
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(sclarIsScalr);
            NUnit.Framework.Assert.IsFalse(cmlpIsScalr);
            NUnit.Framework.Assert.IsFalse(recSetIsScalr);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void InputsToEnvironment_GivenGivenInputs_ShouldNeverThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            try
            {

                var inputs = "<Inputs> <Input Name = \"CityName\" Source = \"CityName\" EmptyToNull = \"false\" DefaultValue = \"Paris-Aeroport Charles De Gaulle\" /> <Input Name = \"CountryName\" Source = \"CountryName\" EmptyToNull = \"false\" DefaultValue = \"France\" /> </Inputs >";
                var executionEnvironment = new ExecutionEnvironment();
                var inputsToEnvironment = DataListUtil.InputsToEnvironment(executionEnvironment, inputs, 0);

            }
            catch (Exception ex)
            {

                NUnit.Framework.Assert.Fail(ex.Message);
            }

            //---------------Test Result -----------------------
        }
    }
}
