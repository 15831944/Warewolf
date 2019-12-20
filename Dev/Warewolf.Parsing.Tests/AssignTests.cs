using System;
using Dev2.Common.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Warewolf.Storage;
using WarewolfParserInterop;


namespace WarewolfParsingTest
{
    [TestFixture]
    [SetUpFixture]
    public class AssignTests
    {

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignARecset")]
        [NUnit.Framework.ExpectedException(typeof(NullValueInVariableException))]
        public void AssignEvaluationRecsets_AssignARecset_Last_WithFraming()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            EvaluationFunctions.evalScalar("a", data);

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignARecset")]
        public void GetIntFromAtomTest()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.getIntFromAtom(DataStorage.WarewolfAtom.NewInt(1));

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x, 1);
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignARecset")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void GetIntFromAtomTestLessThan0()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.getIntFromAtom(DataStorage.WarewolfAtom.NewInt(-11));

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x, 1);
        }



        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignARecset")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void GetIntFromAtomTestLessNotAnInt()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.getIntFromAtom(DataStorage.WarewolfAtom.NewDataString("a"));

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x, 1);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignARecset")]
        public void ParseLanguage_IndexExpression()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpression("[[rec([[a]]).a]]", 1);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIndexExpression);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionStrict")]
        public void ParseLanguageExpressionStrict_IndexExpression()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionStrict("[[rec([[a]]).a]]", 1);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIndexExpression);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignARecset")]
        public void ParseLanguage_IndexExpression_PassAnUpdate()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpression("[[rec(1).a]]", 3);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIntIndex);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionStrict")]
        public void ParseLanguageExpressionStrict_IndexExpression_PassAnUpdate()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionStrict("[[rec(1).a]]", 3);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIntIndex);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignARecset")]
        public void ParseLanguage_RecsetExpression_PassAnUpdate()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpression("[[rec(1)]]", 3);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetNameExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetNameExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIntIndex);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionStrict")]
        public void ParseLanguageExpression_RecsetExpression_PassAnUpdate()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionStrict("[[rec(1)]]", 3);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetNameExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetNameExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIntIndex);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignARecset")]
        public void ParseLanguage_RecsetIndexExpression()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpression("[[rec([[a]])]]", 1);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetNameExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetNameExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIndexExpression);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionStrict")]
        public void ParseLanguageExpression_RecsetIndexExpression()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionStrict("[[rec([[a]])]]", 1);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetNameExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetNameExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIndexExpression);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionWithoutUpdateStrict")]
        public void ParseLanguageExpressionWithoutUpdateStrict_RecsetIndexExpression()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionWithoutUpdateStrict("[[rec([[a]])]]");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsRecordSetNameExpression, true);
            var rec = x as LanguageAST.LanguageExpression.RecordSetNameExpression;
            
            NUnit.Framework.Assert.IsTrue(rec.Item.Index.IsIndexExpression);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionStrict")]
        public void ParseLanguageExpression_InvalidScalar()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionStrict("[[rec", 1);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsWarewolfAtomExpression, true);           
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionWithoutUpdateStrict")]
        public void ParseLanguageExpressionWithoutUpdateStrict_InvalidScalar()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionWithoutUpdateStrict("[[rec");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsWarewolfAtomExpression, true);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionStrict")]
        public void ParseLanguageExpression_InvalidRecordSet()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionStrict("[[rec()", 1);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsWarewolfAtomExpression, true);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionWithoutUpdateStrict")]
        public void ParseLanguageExpressionWithoutUpdateStrict_InvalidRecordSet()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionWithoutUpdateStrict("[[rec()");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsWarewolfAtomExpression, true);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionStrict")]
        public void ParseLanguageExpression_InvalidnamedRecordSet()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionStrict("[[rec([[a]])", 1);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsWarewolfAtomExpression, true);
        }


        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionWithoutUpdateStrict")]
        public void ParseLanguageExpressionWithoutUpdateStrict_InvalidnamedRecordSet()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionWithoutUpdateStrict("[[rec([[a]])");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsWarewolfAtomExpression, true);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionStrict")]
        public void ParseLanguageExpression_InvalidIndexRecordSet()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionStrict("[[rec(1)", 1);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsWarewolfAtomExpression, true);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("EvaluationFunctions_parseLanguageExpressionWithoutUpdateStrict")]
        public void ParseExpressionWithoutUpdateStrict_InvalidIndexRecordSetUpdate()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = EvaluationFunctions.parseLanguageExpressionWithoutUpdateStrict("[[rec(1)");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(x.IsWarewolfAtomExpression, true);
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignAShape")]
        public void Assign_Shape()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = AssignEvaluation.evalDataShape("[[b]]", 1, data);

            //------------Assert Results-------------------------

            
            NUnit.Framework.Assert.IsTrue(x.Scalar.ContainsKey("b"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("Assign")]
        public void Assign_Given_Value_StartsWithOpeningLanguageBracktes_Should_Assign_Value_Correclty_Scalar()
        {
            //------------Setup for test--------------------------
            var emptyenv = CreateEmptyEnvironment();
            var value = "[[nathi";
            var exp = "[[myValue]]";

            //------------Execute Test---------------------------
            var envTemp = PublicFunctions.EvalAssignWithFrameStrict(new AssignValue(exp, value), 1, emptyenv);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(envTemp.Scalar);
            NUnit.Framework.Assert.AreEqual(1, envTemp.Scalar.Count);
            NUnit.Framework.Assert.AreEqual(value, envTemp.Scalar["myValue"].ToString());
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("Assign")]
        public void Assign_Given_Value_ContainsOpeningLanguageBracktes_Should_Assign_Value_Correclty_Scalar()
        {
            //------------Setup for test--------------------------
            var emptyenv = CreateEmptyEnvironment();
            var value = "na[[thi";
            var exp = "[[myValue]]";

            //------------Execute Test---------------------------
            var envTemp = PublicFunctions.EvalAssignWithFrameStrict(new AssignValue(exp, value), 1, emptyenv);
            //PublicFunctions.AssignWithFrame(new AssignValue(exp, value), 1, emptyenv);
            //------------Assert Results-------------------------
           
            NUnit.Framework.Assert.IsNotNull(envTemp.Scalar);
            NUnit.Framework.Assert.AreEqual(1, envTemp.Scalar.Count);
            var a = PublicFunctions.EvalEnvExpression(exp, 0, false, envTemp);
            var valueFromEnv = ExecutionEnvironment.WarewolfEvalResultToString(a);
            NUnit.Framework.Assert.AreEqual(value, envTemp.Scalar["myValue"].ToString());
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("Assign")]
        public void Assign_Given_Value_ContainsOpeningLanguageBracktes_Should_Assign_Value_Correclty_RecordSets()
        {
            //------------Setup for test--------------------------
            var emptyenv = CreateEmptyEnvironment();
            var value = "na[[thi";
            var exp = "[[myValue().name]]";

            //------------Execute Test---------------------------
            var envTemp = PublicFunctions.EvalAssignWithFrameStrict(new AssignValue(exp, value), 1, emptyenv);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(envTemp.RecordSets);
            NUnit.Framework.Assert.AreEqual(1, envTemp.RecordSets.Count);
            var a = PublicFunctions.EvalEnvExpression(exp, 0, false, envTemp);
            var valueFromEnv = ExecutionEnvironment.WarewolfEvalResultToString(a);
            NUnit.Framework.Assert.AreEqual(value, valueFromEnv);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("Assign")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void Assign_Given_Value_ContainsOpeningLanguageBracktes_Should_Assign_Value_Correclty_JsonObjects()
        {
            //------------Setup for test--------------------------
            var emptyenv = CreateEmptyEnvironment();
            var value = "na[[thi";
            var exp = "[[@myValue().name]]";

            //------------Execute Test---------------------------
            var envTemp = PublicFunctions.EvalAssignWithFrameStrict(new AssignValue(exp, value), 1, emptyenv);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(envTemp.RecordSets);
            NUnit.Framework.Assert.AreEqual(1, envTemp.RecordSets.Count);
            var a = PublicFunctions.EvalEnvExpression(exp, 0, false, envTemp);
            var valueFromEnv = ExecutionEnvironment.WarewolfEvalResultToString(a);
            NUnit.Framework.Assert.AreEqual(value, valueFromEnv);
        }        

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignAShape")]
        public void Assign_Shape_Recset()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = AssignEvaluation.evalDataShape("[[bx().d]]", 1, data);

            //------------Assert Results-------------------------

            
            NUnit.Framework.Assert.IsTrue(x.RecordSets.ContainsKey("bx"));
            NUnit.Framework.Assert.IsTrue(x.RecordSets["bx"].Data.ContainsKey("d"));
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignAShape")]
        public void Assign_Shape_Recset_ExistsGetsReplaced()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = AssignEvaluation.evalDataShape("[[Rec().d]]", 1, data);

            //------------Assert Results-------------------------

            
            NUnit.Framework.Assert.IsTrue(x.RecordSets.ContainsKey("Rec"));
            NUnit.Framework.Assert.IsTrue(x.RecordSets["Rec"].Data.ContainsKey("d"));
            NUnit.Framework.Assert.IsTrue(x.RecordSets["Rec"].Data.ContainsKey("a"));
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluationRecsets_AssignAShape")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void Assign_Shape_Recset_JsonThrows()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            var x = AssignEvaluation.evalDataShape("1", 1, data);
            x = AssignEvaluation.evalDataShape("[[Rec().d.x]]", 1, x);

            //------------Assert Results-------------------------

            
            NUnit.Framework.Assert.IsTrue(x.RecordSets.ContainsKey("Rec"));
            NUnit.Framework.Assert.IsTrue(x.RecordSets["Rec"].Data.ContainsKey("d"));
            NUnit.Framework.Assert.IsTrue(x.RecordSets["Rec"].Data.ContainsKey("a"));
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("Assign_JsonProperty")]
        public void Assign_ValueToJsonProperty()
        {
            //------------Setup for test--------------------------
            var data = CreateEnvironmentWithData();

            //------------Execute Test---------------------------
            AssignEvaluation.evalAssignWithFrame(new AssignValue("[[@Person.Name]]", "dora"), 0, data);

            //------------Assert Results-------------------------
            var jsonObject = data.JsonObjects["Person"];
            NUnit.Framework.Assert.IsNotNull(jsonObject);
            var value = jsonObject.First;
            var token = ((Newtonsoft.Json.Linq.JProperty)value).Value;
            NUnit.Framework.Assert.AreEqual("dora", token);
        }

        static DataStorage.WarewolfEnvironment CreateEmptyEnvironment()
        {
            var env = new ExecutionEnvironment();
            var p = new PrivateObject(env);
            return (DataStorage.WarewolfEnvironment)p.GetFieldOrProperty("_env");
        }

        static DataStorage.WarewolfEnvironment CreateEnvironmentWithData()
        {
            var env = new ExecutionEnvironment();
            env.Assign("[[Rec(1).a]]", "1", 0);
            env.Assign("[[Rec(2).a]]", "2", 0);
            env.Assign("[[Rec(3).a]]", "3", 0);
            env.Assign("[[Rec(1).b]]", "a", 0);
            env.Assign("[[Rec(2).b]]", "b", 0);
            env.Assign("[[Rec(3).b]]", "c", 0);
            env.Assign("[[x]]", "1", 0);
            env.Assign("[[y]]", "y", 0);
            env.AssignJson(new AssignValue("[[@Person.Name]]", "bob"), 0);
            env.AssignJson(new AssignValue("[[@Person.Age]]", "22"), 0);
            env.AssignJson(new AssignValue("[[@Person.Spouse.Name]]", "dora"), 0);
            env.AssignJson(new AssignValue("[[@Person.Children(1).Name]]", "Mary"), 0);
            env.AssignJson(new AssignValue("[[@Person.Children(2).Name]]", "Jane"), 0);
            env.AssignJson(new AssignValue("[[@Person.Score(1)]]", "2"), 0);
            env.AssignJson(new AssignValue("[[@Person.Score(2)]]", "3"), 0);
            env.AssignJson(new AssignValue("[[@array(1)]]", "bob"), 0);
            env.AssignJson(new AssignValue("[[@arrayObj(1).Name]]", "bob"), 0);
            env.AssignJson(new AssignValue("[[@arrayObj(2).Name]]", "bobe"), 0);
            var p = new PrivateObject(env);
            return (DataStorage.WarewolfEnvironment)p.GetFieldOrProperty("_env");
        }
    }
}
