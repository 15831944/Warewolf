/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Collections.Generic;
using Dev2.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using WarewolfParserInterop;

namespace Dev2.Data.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class WarewolfIteratorTests
    {
        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_Construct_GivenList()
        {
            var item1 = DataStorage.WarewolfAtom.NewDataString("some string");
            var item2 = DataStorage.WarewolfAtom.NewFloat(3.2);
            var item3 = DataStorage.WarewolfAtom.NewInt(1234);
            var item4 = DataStorage.WarewolfAtom.NewPositionedValue(new Tuple<int, DataStorage.WarewolfAtom>(3, DataStorage.WarewolfAtom.NewDataString("positioned value")));

            var values = new List<DataStorage.WarewolfAtom>
            {
                item1,
                item2,
                item3,
                item4,
            };

            var warewolfAtomList = new WarewolfAtomList<DataStorage.WarewolfAtom>(DataStorage.WarewolfAtom.NewDataString("some default value"), values);
            var warewolfAtomListResult = CommonFunctions.WarewolfEvalResult.NewWarewolfAtomListresult(warewolfAtomList);

            var iterator = new WarewolfIterator(warewolfAtomListResult);

            NUnit.Framework.Assert.AreEqual("\r\n", iterator.NewLineFormat);

            NUnit.Framework.Assert.AreEqual(4, iterator.GetLength());

            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            NUnit.Framework.Assert.AreEqual("some string", iterator.GetNextValue());
            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            NUnit.Framework.Assert.AreEqual("3.2", iterator.GetNextValue());
            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            NUnit.Framework.Assert.AreEqual("1234", iterator.GetNextValue());
            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            NUnit.Framework.Assert.AreEqual("positioned value", iterator.GetNextValue());
            NUnit.Framework.Assert.IsFalse(iterator.HasMoreData());
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_Construct_GivenScalar()
        {
            var item1 = DataStorage.WarewolfAtom.NewDataString("some string");

            var warewolfAtomResult = CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(item1);

            var iterator = new WarewolfIterator(warewolfAtomResult);

            NUnit.Framework.Assert.AreEqual("\r\n", iterator.NewLineFormat);

            NUnit.Framework.Assert.AreEqual(1, iterator.GetLength());

            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            NUnit.Framework.Assert.AreEqual("some string", iterator.GetNextValue());
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_Construct_GivenScalarWithUnixNewLine()
        {
            var item1 = DataStorage.WarewolfAtom.NewDataString("some \nstring");

            var warewolfAtomResult = CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(item1);

            var iterator = new WarewolfIterator(warewolfAtomResult);

            NUnit.Framework.Assert.AreEqual("\n", iterator.NewLineFormat);

            NUnit.Framework.Assert.AreEqual(1, iterator.GetLength());

            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            NUnit.Framework.Assert.AreEqual("some \nstring", iterator.GetNextValue());
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_Construct_GivenRecordset()
        {
            var expectedResult = "some string0,3.2,1234,positioned value0,some string1,4.2,1235,positioned value1,some string2,5.2,1236,positioned value2,some string3,6.2,1237,positioned value3";
            IEnumerable<Tuple<string, WarewolfAtomList<DataStorage.WarewolfAtom>>> generateElements()
            {
                for (int i = 0; i < 4; i++)
                {
                    var warewolfAtomList = new WarewolfAtomList<DataStorage.WarewolfAtom>(DataStorage.WarewolfAtom.NewDataString("some default value"), new List<DataStorage.WarewolfAtom>
                    {
                        DataStorage.WarewolfAtom.NewDataString("some string"+ i),
                        DataStorage.WarewolfAtom.NewFloat(3.2 + i),
                        DataStorage.WarewolfAtom.NewInt(1234 + i),
                        DataStorage.WarewolfAtom.NewPositionedValue(new Tuple<int, DataStorage.WarewolfAtom>(3+ i, DataStorage.WarewolfAtom.NewDataString("positioned value"+ i))),
                    });
                    yield return new Tuple<string, WarewolfAtomList<DataStorage.WarewolfAtom>>("col"+ i, warewolfAtomList);
                }
            }

            var fSharpMap = new Microsoft.FSharp.Collections.FSharpMap<string, WarewolfAtomList<DataStorage.WarewolfAtom>>(generateElements());

            var warewolfRecordset = new DataStorage.WarewolfRecordset(fSharpMap, DataStorage.WarewolfAttribute.Sorted, 0, 0);
            var warewolfRecordsetResult = CommonFunctions.WarewolfEvalResult.NewWarewolfRecordSetResult(warewolfRecordset);

            var iterator = new WarewolfIterator(warewolfRecordsetResult);

            NUnit.Framework.Assert.AreEqual("\r\n", iterator.NewLineFormat);

            NUnit.Framework.Assert.AreEqual(1, iterator.GetLength());

            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            NUnit.Framework.Assert.AreEqual(expectedResult, iterator.GetNextValue());
            NUnit.Framework.Assert.IsFalse(iterator.HasMoreData());
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_Should()
        {
            var listResult = CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(DataStorage.WarewolfAtom.Nothing);
            var atomIterator = new WarewolfIterator(listResult);
            NUnit.Framework.Assert.IsNotNull(atomIterator);
            var privateObj = new PrivateObject(atomIterator);
            var maxVal = (int) privateObj.GetField("_maxValue");
            NUnit.Framework.Assert.IsNotNull(maxVal);
            var length = atomIterator.GetLength();
            NUnit.Framework.Assert.AreEqual(maxVal, length);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_SetupForWarewolfRecordSetResult_Should()
        {
            var listResult = CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(DataStorage.WarewolfAtom.Nothing);
            var atomIterator = new WarewolfIterator(listResult);
            NUnit.Framework.Assert.IsNotNull(atomIterator);
            var privateObj = new PrivateObject(atomIterator);
            var assigns = new List<IAssignValue>
             {
                 new AssignValue("[[rec(25).a]]", "25"),
                 new AssignValue("[[rec(27).b]]", "33"),
                 new AssignValue("[[rec(29).b]]", "26")
             };
            var testEnv = WarewolfTestData.CreateTestEnvEmpty("");
            var testEnv3 = PublicFunctions.EvalMultiAssign(assigns, 0, testEnv);
            var res = PublicFunctions.EvalEnvExpression("[[rec(27)]]", 0, false, testEnv3);
            NUnit.Framework.Assert.IsTrue(res.IsWarewolfRecordSetResult);
            object[] arg = { res };
            privateObj.Invoke("SetupForWarewolfRecordSetResult", arg);
            var scalarRes = privateObj.GetField("_scalarResult") as CommonFunctions.WarewolfEvalResult.WarewolfAtomResult;
            NUnit.Framework.Assert.IsNotNull(scalarRes);
        }


        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_Construct_GivenScalarWithCalculate()
        {
            var item1 = DataStorage.WarewolfAtom.NewDataString("!~calculation~!\"now()\"!~~calculation~!");

            var warewolfAtomResult = CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(item1);

            var iterator = new WarewolfIterator(warewolfAtomResult, Common.Interfaces.Diagnostics.Debug.FunctionEvaluatorOption.DotNetDateTimeFormat);

            NUnit.Framework.Assert.AreEqual("\r\n", iterator.NewLineFormat);

            NUnit.Framework.Assert.AreEqual(1, iterator.GetLength());

            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            var realNow = DateTime.Now;
            var nowString = iterator.GetNextValue();
            var now = DateTime.Parse(nowString, System.Globalization.CultureInfo.InvariantCulture);
            NUnit.Framework.Assert.IsTrue(now > realNow);
        }


        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_Construct_GivenScalarWithInvalidCalculate()
        {
            var item1 = DataStorage.WarewolfAtom.NewDataString("!~calculation~!\"2- nos2w()-200\"!~~calculation~!");

            var warewolfAtomResult = CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(item1);

            var iterator = new WarewolfIterator(warewolfAtomResult, Common.Interfaces.Diagnostics.Debug.FunctionEvaluatorOption.DotNetDateTimeFormat);

            NUnit.Framework.Assert.AreEqual("\r\n", iterator.NewLineFormat);

            NUnit.Framework.Assert.AreEqual(1, iterator.GetLength());

            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            var nowString = iterator.GetNextValue();
            NUnit.Framework.Assert.AreEqual("2- nos2w()-200", nowString);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(WarewolfIterator))]
        public void WarewolfIterator_Construct_GivenEmptyCalculate()
        {
            var item1 = DataStorage.WarewolfAtom.NewDataString("!~calculation~!!~~calculation~!");

            var warewolfAtomResult = CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(item1);

            var iterator = new WarewolfIterator(warewolfAtomResult, Common.Interfaces.Diagnostics.Debug.FunctionEvaluatorOption.DotNetDateTimeFormat);

            NUnit.Framework.Assert.AreEqual("\r\n", iterator.NewLineFormat);

            NUnit.Framework.Assert.AreEqual(1, iterator.GetLength());

            NUnit.Framework.Assert.IsTrue(iterator.HasMoreData());
            try
            {
                var nowString = iterator.GetNextValue();
                NUnit.Framework.Assert.Fail("expected exception Nothing to Evaluate");
            }
            catch (Exception e)
            {
                NUnit.Framework.Assert.AreEqual("Nothing to Evaluate", e.Message);
            }
        }
    }
}
