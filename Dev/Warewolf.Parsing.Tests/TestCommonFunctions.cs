using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Warewolf.Storage;
using WarewolfParserInterop;


namespace WarewolfParsingTest
{
    [TestFixture]
    public class TestCommonFunctions
    {
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_MethodName")]
        public void CommonFunctions_MethodName_AtomToString_ExpectCorrectString()
        {
            //------------Setup for test--------------------------
           NUnit.Framework.Assert.AreEqual(CommonFunctions.atomtoString(DataStorage.WarewolfAtom.Nothing),null);
           NUnit.Framework.Assert.AreEqual(CommonFunctions.atomtoString(DataStorage.WarewolfAtom.NewDataString("!")),"!");
           NUnit.Framework.Assert.AreEqual(CommonFunctions.atomtoString(DataStorage.WarewolfAtom.NewInt(1)), "1");
           NUnit.Framework.Assert.AreEqual(CommonFunctions.atomtoString(DataStorage.WarewolfAtom.NewFloat(1.2345)), "1.2345");
           NUnit.Framework.Assert.AreEqual(CommonFunctions.atomtoString(DataStorage.WarewolfAtom.NewPositionedValue(new Tuple<int,DataStorage.WarewolfAtom>(1,DataStorage.WarewolfAtom.NewDataString("a")))), "a");
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_MethodName")]
        public void CommonFunctions_MethodName_AtomRecordToString_ExpectCorrectString()
        {
            //------------Setup for test--------------------------
            NUnit.Framework.Assert.AreEqual(CommonFunctions.warewolfAtomRecordtoString(DataStorage.WarewolfAtom.Nothing), "");
            NUnit.Framework.Assert.AreEqual(CommonFunctions.warewolfAtomRecordtoString(DataStorage.WarewolfAtom.NewDataString("!")), "!");
            NUnit.Framework.Assert.AreEqual(CommonFunctions.warewolfAtomRecordtoString(DataStorage.WarewolfAtom.NewInt(1)), "1");
            NUnit.Framework.Assert.AreEqual(CommonFunctions.warewolfAtomRecordtoString(DataStorage.WarewolfAtom.NewFloat(1.2345)), "1.2345");
            NUnit.Framework.Assert.AreEqual(CommonFunctions.warewolfAtomRecordtoString(DataStorage.WarewolfAtom.NewPositionedValue(new Tuple<int, DataStorage.WarewolfAtom>(1, DataStorage.WarewolfAtom.NewDataString("a")))), "a");
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToJsonCompatable")]
        public void CommonFunctions_AtomToJsonCompatable()
        {
            NUnit.Framework.Assert.IsNull(CommonFunctions.atomToJsonCompatibleObject(DataStorage.WarewolfAtom.Nothing));
            NUnit.Framework.Assert.AreEqual(CommonFunctions.atomToJsonCompatibleObject(DataStorage.WarewolfAtom.NewFloat(1.2)),1.2);
            NUnit.Framework.Assert.AreEqual(CommonFunctions.atomToJsonCompatibleObject(DataStorage.WarewolfAtom.NewInt(1)), 1);
            NUnit.Framework.Assert.AreEqual(CommonFunctions.atomToJsonCompatibleObject(DataStorage.WarewolfAtom.NewDataString("true")), true);
            NUnit.Framework.Assert.AreEqual(CommonFunctions.atomToJsonCompatibleObject(DataStorage.WarewolfAtom.NewDataString("false")), false);
            NUnit.Framework.Assert.AreEqual(CommonFunctions.atomToJsonCompatibleObject(DataStorage.WarewolfAtom.NewDataString("trues")), "trues");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToInt")]
        [Warewolf.UnitTestAttributes.ExpectedException(typeof(Exception))]
        public void CommonFunctions_AtomToInt()
        {
            CommonFunctions.atomToInt(DataStorage.WarewolfAtom.Nothing);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToInt")]
        [Warewolf.UnitTestAttributes.ExpectedException(typeof(Exception))]
        public void CommonFunctions_AtomToInt_neg()
        {
            CommonFunctions.atomToInt(DataStorage.WarewolfAtom.NewInt(-1));
        }
        
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToInt")]
        public void CommonFunctions_AtomToInt_Parsed()
        {
          NUnit.Framework.Assert.AreEqual(1,  CommonFunctions.atomToInt(DataStorage.WarewolfAtom.NewDataString("1")));
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToInt")]
        public void CommonFunctions_AtomToInt_ParsedInt()
        {
            NUnit.Framework.Assert.AreEqual(1, CommonFunctions.atomToInt(DataStorage.WarewolfAtom.NewInt(1)));
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToJsonCompatable")]
        public void CommonFunctions_EvalResultToJsonCompatable()
        {
            var env = CreateEnvironmentWithData();
            var a = CommonFunctions.evalResultToString( EvaluationFunctions.eval(env, 0, false, "[[x]]"));
            NUnit.Framework.Assert.AreEqual(a,"1");       
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToJsonCompatable")]
        public void CommonFunctions_EvalResultToJsonCompatableRecset()
        {
            var env = CreateEnvironmentWithData();
            var a = CommonFunctions.evalResultToJsonCompatibleObject( EvaluationFunctions.eval(env, 0, false, "[[Rec(*).a]]"));

        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_GetRecsetPosition")]
        public void CommonFunctions_GetRecsetPosition()
        {
            var env = CreateEnvironmentWithData();
            var rec = env.RecordSets["bec"];
            var a = CommonFunctions.getRecordSetIndex(rec,3);
            NUnit.Framework.Assert.AreEqual(((CommonFunctions.PositionValue.IndexFoundPosition)a).Item,0);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_GetRecsetPosition")]
        public void CommonFunctions_GetRecsetPositionNonExistent()
        {
            var env = CreateEnvironmentWithData();
            var rec = env.RecordSets["bec"];
            var a = CommonFunctions.getRecordSetIndex(rec, 5);
            NUnit.Framework.Assert.IsTrue(a.IsIndexDoesNotExist);
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_GetRecsetPosition")]
        public void CommonFunctions_IsNothing()
        {
            var env = CreateEnvironmentWithData();

            NUnit.Framework.Assert.IsTrue(CommonFunctions.isNothing(CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(DataStorage.WarewolfAtom.Nothing)));
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_GetRecsetPosition")]
        public void CommonFunctions_IsNothingNot()
        {
            var env = CreateEnvironmentWithData();

            NUnit.Framework.Assert.IsFalse(CommonFunctions.isNothing(CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(DataStorage.WarewolfAtom.NewDataString("A"))));
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_GetRecsetPosition")]
        public void CommonFunctions_IsNothingLsit()
        {
            var env = CreateEnvironmentWithData();

            NUnit.Framework.Assert.IsFalse(CommonFunctions.isNothing(CommonFunctions.WarewolfEvalResult.NewWarewolfAtomListresult(new WarewolfAtomList<DataStorage.WarewolfAtom>(DataStorage.WarewolfAtom.Nothing))));
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToJsonCompatable")]
        [Warewolf.UnitTestAttributes.ExpectedException(typeof(Exception))]
        public void CommonFunctions_EvalResultToJsonCompatableJson()
        {
            var env = CreateEnvironmentWithData();
            var a = CommonFunctions.evalResultToJsonCompatibleObject(EvaluationFunctions.eval(env, 0, false, "[[Rec(*)]]"));

        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToJsonCompatable")]
        [Warewolf.UnitTestAttributes.ExpectedException(typeof(Exception))]
        public void CommonFunctions_getLastIndexFromRecordSet()
        {
            var env = CreateEnvironmentWithData();
            var a = CommonFunctions.getLastIndexFromRecordSet("a",env);

        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("CommonFunctions_AtomToJsonCompatable")]
        [Warewolf.UnitTestAttributes.ExpectedException(typeof(Exception))]
        public void CommonFunctions_deleteValues()
        {
            var env = CreateEnvironmentWithData();
            var a = Delete.deleteValues("Resc",env);
         

        }

        public static DataStorage.WarewolfEnvironment CreateEnvironmentWithData()
        {
            var env = new ExecutionEnvironment();
            env.Assign("[[Rec(1).a]]", "1", 0);
            env.Assign("[[Rec(2).a]]", "2", 0);
            env.Assign("[[Rec(3).a]]", "3", 0);
            env.Assign("[[Rec(4).a]]", "2", 0);
            env.Assign("[[Rec(1).b]]", "a", 0);
            env.Assign("[[Rec(2).b]]", "b", 0);
            env.Assign("[[Rec(3).b]]", "c", 0);
            env.Assign("[[Rec(4).b]]", "c", 0);
            env.Assign("[[bec(3).b]]", "c", 0);
            env.Assign("[[bec(2).b]]", "c", 0);
            env.Assign("[[x]]", "1", 0);
            env.Assign("[[y]]", "y", 0);
            env.AssignJson(new AssignValue("[[@Person.Name]]", "bob"), 0);
            env.AssignJson(new AssignValue("[[@Person.Age]]", "22"), 0);
            env.AssignJson(new AssignValue("[[@Person.Spouse.Name]]", "dora"), 0);
            env.AssignJson(new AssignValue("[[@Person.Children(1).Name]]", "Mary"), 0);
            env.AssignJson(new AssignValue("[[@Person.Children(2).Name]]", "Jane"), 0);
            env.AssignJson(new AssignValue("[[@Person.Score(1)]]", "2"), 0);
            env.AssignJson(new AssignValue("[[@Person.Score(2)]]", "3"), 0);
            env.AssignJson(new AssignValue("[[array(1)]]", "bob"), 0);
            env.AssignJson(new AssignValue("[[arrayObj(1).Name]]", "bob"), 0);
            env.AssignJson(new AssignValue("[[arrayObj(2).Name]]", "bobe"), 0);
            var p = new PrivateObject(env);
            return (DataStorage.WarewolfEnvironment)p.GetFieldOrProperty("_env");
        }

    }
}
