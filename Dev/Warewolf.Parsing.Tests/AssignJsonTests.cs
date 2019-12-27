using Dev2.Common.Interfaces;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Warewolf.Storage;
using WarewolfParserInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WarewolfParsingTest
{
    [TestFixture]
    public class AssignJsonTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignSingleProperty_ValueProperty_Assign_A_PropertyWithAtNotation()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var values = new List<IAssignValue>() { new AssignValue("[[@Person.Name]]", "John") };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------

            var data = GetFromEnv(environment);
            NUnit.Framework.Assert.IsTrue(data.JsonObjects.ContainsKey("Person"));
            if (data.JsonObjects["Person"] is JObject obj)
            {
                NUnit.Framework.Assert.AreEqual(obj.ToString(), "{\r\n  \"Name\": \"John\"\r\n}");
            }
            else
            {
                NUnit.Framework.Assert.Fail("bob");
            }
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignJArray_MultipleJValues_EvalCorreclty()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var jObject = "{\"PolicyNo\":\"A0003\",\"DateId\":32,\"SomeVal\":\"Bob\"}";
            var values = new List<IAssignValue>()
            {
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),
            };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------

            var data = GetFromEnv(environment);
            var warewolfEvalResult = environment.Eval("[[@Person()]]", 0);
            var isWarewolfAtomListresult = warewolfEvalResult.IsWarewolfAtomListresult;
            var isWarewolfAtomresult = warewolfEvalResult.IsWarewolfAtomResult;
            NUnit.Framework.Assert.IsFalse(isWarewolfAtomListresult);
            NUnit.Framework.Assert.IsTrue(isWarewolfAtomresult);
            var container = data.JsonObjects["Person"];
            var obj = container as JArray;
            NUnit.Framework.Assert.IsNotNull(obj);

        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignJArray_MultipleJValues_EvalStarCorreclty()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var jObject = "{\"PolicyNo\":\"A0003\",\"DateId\":32,\"SomeVal\":\"Bob\"}";
            var values = new List<IAssignValue>()
            {
                new AssignValue("[[@Person(*)]]", jObject),
                new AssignValue("[[@Person(*)]]", jObject),
                new AssignValue("[[@Person(*)]]", jObject),
                new AssignValue("[[@Person(*)]]", jObject),
            };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------

            var data = GetFromEnv(environment);
            var warewolfEvalResult = environment.Eval("[[@Person(*)]]", 0);
            var isWarewolfAtomListresult = warewolfEvalResult.IsWarewolfAtomListresult;
            var isWarewolfAtomresult = warewolfEvalResult.IsWarewolfAtomResult;
            NUnit.Framework.Assert.IsFalse(isWarewolfAtomListresult);
            NUnit.Framework.Assert.IsTrue(isWarewolfAtomresult);
            var container = data.JsonObjects["Person"];
            var obj = container as JArray;
            NUnit.Framework.Assert.IsNotNull(obj);

        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignJArrayAndJobject_MultipleJValues_EvalCorreclty()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var jObject = "{\"PolicyNo\":\"A0003\",\"DateId\":32,\"SomeVal\":\"Bob\"}";
            var values = new List<IAssignValue>()
            {
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),

            };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            environment.AssignJson(new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"NNNN\",\"DateId\":32,\"SomeVal\":\"Bob\"}"), 0);
            //------------Assert Results-------------------------

            var data = GetFromEnv(environment);
            var warewolfEvalResult = environment.Eval("[[@Person()]]", 0);
            var isWarewolfAtomListresult = warewolfEvalResult.IsWarewolfAtomListresult;
            var isWarewolfAtomresult = warewolfEvalResult.IsWarewolfAtomResult;
            NUnit.Framework.Assert.IsFalse(isWarewolfAtomListresult);
            NUnit.Framework.Assert.IsTrue(isWarewolfAtomresult);
            var container = data.JsonObjects["Person"];
            var obj = container as JArray;
            NUnit.Framework.Assert.IsNotNull(obj);

        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignJobjectAndJArray_MultipleJValues_EvalCorreclty()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var jObject = "{\"PolicyNo\":\"A0003\",\"DateId\":32,\"SomeVal\":\"Bob\"}";
            var values = new List<IAssignValue>()
            {
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),
                new AssignValue("[[@Person()]]", jObject),

            };


            //------------Execute Test---------------------------

            environment.AssignJson(new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"NNNN\",\"DateId\":32,\"SomeVal\":\"Bob\"}"), 0);
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------

            var data = GetFromEnv(environment);
            var warewolfEvalResult = environment.Eval("[[@Person()]]", 0);
            var isWarewolfAtomListresult = warewolfEvalResult.IsWarewolfAtomListresult;
            var isWarewolfAtomresult = warewolfEvalResult.IsWarewolfAtomResult;
            NUnit.Framework.Assert.IsFalse(isWarewolfAtomListresult);
            NUnit.Framework.Assert.IsTrue(isWarewolfAtomresult);
            var container = data.JsonObjects["Person"];
            var obj = container as JArray;
            NUnit.Framework.Assert.IsNotNull(obj);

        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignJobjectToJObject_DiffTypes_EvalCorreclty()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            environment.AssignJson(new AssignValue("[[@Person]]", "{\"Name\":\"NNNN\",\"Age\":32}"), 0);

            //------------Execute Test---------------------------
            environment.AssignJson(new AssignValue("[[@Person2.Name]]", "[[@Person.Name]]"), 0);
            environment.AssignJson(new AssignValue("[[@Person2.Age]]", "[[@Person.Age]]"), 0);
            //------------Assert Results-------------------------

            var data = GetFromEnv(environment);
            var warewolfEvalResult = environment.Eval("[[@Person2]]", 0);            


        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignJArray_MultipleJValues_EvalIndexOneCorreclty()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var values = new List<IAssignValue>()
            {
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0001\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0002\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0003\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0004\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
            };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------

            var warewolfEvalResult = environment.Eval("[[@Person(1)]]", 0);
            var isWarewolfAtomListresult = warewolfEvalResult.IsWarewolfAtomListresult;
            var isWarewolfAtomresult = warewolfEvalResult.IsWarewolfAtomResult;
            NUnit.Framework.Assert.IsFalse(isWarewolfAtomListresult);
            NUnit.Framework.Assert.IsTrue(isWarewolfAtomresult);
            var evalResultToString = ExecutionEnvironment.WarewolfEvalResultToString(warewolfEvalResult);
            var contains = evalResultToString.Contains("A0001");
            NUnit.Framework.Assert.IsTrue(contains);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignJArray_MultipleJValues_EvalIndexTwoCorreclty()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var values = new List<IAssignValue>()
            {
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0001\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0002\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0003\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0004\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
            };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------

            var warewolfEvalResult = environment.Eval("[[@Person(2)]]", 0);
            var isWarewolfAtomListresult = warewolfEvalResult.IsWarewolfAtomListresult;
            var isWarewolfAtomresult = warewolfEvalResult.IsWarewolfAtomResult;
            NUnit.Framework.Assert.IsFalse(isWarewolfAtomListresult);
            NUnit.Framework.Assert.IsTrue(isWarewolfAtomresult);
            var evalResultToString = ExecutionEnvironment.WarewolfEvalResultToString(warewolfEvalResult);
            var contains = evalResultToString.Contains("A0002");
            NUnit.Framework.Assert.IsTrue(contains);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignJArray_MultipleJValues_EvalIndexThreeCorreclty()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var values = new List<IAssignValue>()
            {
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0001\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0002\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0003\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
                new AssignValue("[[@Person()]]", "{\"PolicyNo\":\"A0004\",\"DateId\":32,\"SomeVal\":\"Bob\"}"),
            };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------

            var warewolfEvalResult = environment.Eval("[[@Person(3)]]", 0);
            var isWarewolfAtomListresult = warewolfEvalResult.IsWarewolfAtomListresult;
            var isWarewolfAtomresult = warewolfEvalResult.IsWarewolfAtomResult;
            NUnit.Framework.Assert.IsFalse(isWarewolfAtomListresult);
            NUnit.Framework.Assert.IsTrue(isWarewolfAtomresult);
            var evalResultToString = ExecutionEnvironment.WarewolfEvalResultToString(warewolfEvalResult);
            var contains = evalResultToString.Contains("A0003");
            NUnit.Framework.Assert.IsTrue(contains);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignSingleProperty_AssignAChildArrayValue_WithAtNotation()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var values = new List<IAssignValue>() { new AssignValue("[[@Person.Name]]", "John"), new AssignValue("[[@Person.Children(1).Name]]", "Mary") };
            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------
            var data = GetFromEnv(environment);
            NUnit.Framework.Assert.IsTrue(data.JsonObjects.ContainsKey("Person"));
            if (data.JsonObjects["Person"] is JObject obj)
            {
                NUnit.Framework.Assert.AreEqual(obj.ToString(), "{\r\n  \"Name\": \"John\",\r\n  \"Children\": [\r\n    {\r\n      \"Name\": \"Mary\"\r\n    }\r\n  ]\r\n}");
            }
            else
            {
                NUnit.Framework.Assert.Fail("bob");
            }
        }


        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignSingleProperty_AssignASecondValueChildNameWithAtNotation()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var values = new List<IAssignValue>() { new AssignValue("[[@Person.Name]]", "John"), new AssignValue("[[@Person.Children(1).Name]]", "Mary"), new AssignValue("[[@Person.Children(2).Name]]", "Joe") };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------
            var data = GetFromEnv(environment);
            NUnit.Framework.Assert.IsTrue(data.JsonObjects.ContainsKey("Person"));
            if (data.JsonObjects["Person"] is JObject obj)
            {
                NUnit.Framework.Assert.AreEqual(obj.ToString(), "{\r\n  \"Name\": \"John\",\r\n  \"Children\": [\r\n    {\r\n      \"Name\": \"Mary\"\r\n    },\r\n    {\r\n      \"Name\": \"Joe\"\r\n    }\r\n  ]\r\n}");
            }
            else
            {
                NUnit.Framework.Assert.Fail("bob");
            }
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignSingleProperty_AssignALastValueChildNameWithAtNotation()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var values = new List<IAssignValue>() { new AssignValue("[[@Person.Name]]", "John"), new AssignValue("[[@Person.Children(1).Name]]", "Mary"), new AssignValue("[[@Person.Children(2).Name]]", "Joe"), new AssignValue("[[@Person.Children(2).Name]]", "Moe") };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------
            var data = GetFromEnv(environment);
            NUnit.Framework.Assert.IsTrue(data.JsonObjects.ContainsKey("Person"));
            if (data.JsonObjects["Person"] is JObject obj)
            {
                NUnit.Framework.Assert.AreEqual(obj.ToString(), "{\r\n  \"Name\": \"John\",\r\n  \"Children\": [\r\n    {\r\n      \"Name\": \"Mary\"\r\n    },\r\n    {\r\n      \"Name\": \"Moe\"\r\n    }\r\n  ]\r\n}");
            }
            else
            {
                NUnit.Framework.Assert.Fail("bob");
            }
        }


        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("AssignSingleProperty_ValueProperty")]
        public void AssignSingleProperty_AssignAllValueChildNameWithAtNotation()
        {
            //------------Setup for test--------------------------

            var environment = new ExecutionEnvironment();
            var values = new List<IAssignValue>() { new AssignValue("[[@Person.Name]]", "John"), new AssignValue("[[@Person.Children(1).Name]]", "Mary"), new AssignValue("[[@Person.Children(2).Name]]", "Joe"), new AssignValue("[[@Person.Children(*).Name]]", "Moe") };

            //------------Execute Test---------------------------
            environment.AssignJson(values, 0);
            //------------Assert Results-------------------------
            var data = GetFromEnv(environment);
            NUnit.Framework.Assert.IsTrue(data.JsonObjects.ContainsKey("Person"));
            if (data.JsonObjects["Person"] is JObject obj)
            {
                NUnit.Framework.Assert.AreEqual(obj.ToString(), "{\r\n  \"Name\": \"John\",\r\n  \"Children\": [\r\n    {\r\n      \"Name\": \"Moe\"\r\n    },\r\n    {\r\n      \"Name\": \"Moe\"\r\n    }\r\n  ]\r\n}");
            }
            else
            {
                NUnit.Framework.Assert.Fail("bob");
            }
        }

        DataStorage.WarewolfEnvironment GetFromEnv(ExecutionEnvironment env)
        {
            var p = new PrivateObject(env);
            return (DataStorage.WarewolfEnvironment)p.GetField("_env");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_AddProperty")]
        public void AssignEvaluation_AddProperty_AddAtom_ExpectSuccess()
        {
            //------------Setup for test--------------------------
            var j = new JObject();

            //------------Execute Test---------------------------
            var obj = WarewolfDataEvaluationCommon.addAtomicPropertyToJson(j, "Name", DataStorage.WarewolfAtom.NewDataString("a"));
            var result = obj.ToString();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(@"{
  ""Name"": ""a""
}", result);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_AddProperty")]
        public void AssignEvaluation_AddProperty_AddAtom_AlreadyExist_ExpectSuccess()
        {
            //------------Setup for test--------------------------
            var j = new JObject();

            //------------Execute Test---------------------------

            var obj = WarewolfDataEvaluationCommon.addAtomicPropertyToJson(j, "Name", DataStorage.WarewolfAtom.NewDataString("a"));
            obj = WarewolfDataEvaluationCommon.addAtomicPropertyToJson(j, "Name", DataStorage.WarewolfAtom.NewDataString("x"));
            var result = obj.ToString();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(@"{
  ""Name"": ""x""
}", result);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_AddProperty")]
        public void AssignEvaluation_AddProperty_AddNothing_ExpectSuccess()
        {
            //------------Setup for test--------------------------
            var j = new JObject();

            //------------Execute Test---------------------------

            var obj = WarewolfDataEvaluationCommon.addAtomicPropertyToJson(j, "Name", DataStorage.WarewolfAtom.Nothing);
            var result = obj.ToString();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(@"{
  ""Name"": null
}", result);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_AddProperty")]
        public void AssignEvaluation_AddProperty_AddArray_ExpectSuccess()
        {
            //------------Setup for test--------------------------
            var j = new JObject();

            //------------Execute Test---------------------------
            var obj = WarewolfDataEvaluationCommon.addArrayPropertyToJson(j, "Name", new List<DataStorage.WarewolfAtom> { DataStorage.WarewolfAtom.NewDataString("a"), DataStorage.WarewolfAtom.NewDataString("b") });
            var result = obj.ToString();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual("{\r\n  \"Name\": [\r\n    \"a\",\r\n    \"b\"\r\n  ]\r\n}", result);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_AddProperty")]
        public void AssignEvaluation_AddProperty_AddArray_Exists_ExpectSuccess()
        {
            //------------Setup for test--------------------------
            var j = new JObject();

            //------------Execute Test---------------------------

            var obj = WarewolfDataEvaluationCommon.addArrayPropertyToJson(j, "Name", new List<DataStorage.WarewolfAtom> { DataStorage.WarewolfAtom.NewDataString("a"), DataStorage.WarewolfAtom.NewDataString("b") });
            obj = WarewolfDataEvaluationCommon.addArrayPropertyToJson(j, "Name", new List<DataStorage.WarewolfAtom> { DataStorage.WarewolfAtom.NewDataString("x"), DataStorage.WarewolfAtom.NewDataString("y") });
            var result = obj.ToString();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual("{\r\n  \"Name\": [\r\n    \"x\",\r\n    \"y\"\r\n  ]\r\n}", result);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_AddProperty")]
        public void AssignEvaluation_AddProperty_AddArray_withNulls_ExpectSuccess()
        {
            //------------Setup for test--------------------------
            var j = new JObject();

            //------------Execute Test---------------------------
            var obj = WarewolfDataEvaluationCommon.addArrayPropertyToJson(j, "Name", new List<DataStorage.WarewolfAtom> { DataStorage.WarewolfAtom.Nothing, DataStorage.WarewolfAtom.NewDataString("b") });
            var result = obj.ToString();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual("{\r\n  \"Name\": [\r\n    null,\r\n    \"b\"\r\n  ]\r\n}", result);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAValue_addsObjectIfItDoesNotExist()
        {
            var env = CreateTestEnvWithData();

            var result = PublicFunctions.EvalEnvExpression("[[a]]", 0, false, env);

            var env2 = AssignEvaluation.assignGivenAValue(env, result, LanguageAST.JsonIdentifierExpression.NewNestedNameExpression(new LanguageAST.JsonPropertyIdentifier("Person", LanguageAST.JsonIdentifierExpression.NewNameExpression(new LanguageAST.JsonIdentifier("Person")))));
            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAValueCreatesValidJson_addsObjectIfItDoesNotExist()
        {
            var env = CreateTestEnvWithData();

            var result = PublicFunctions.EvalEnvExpression("[[a]]", 0, false, env);

            var env2 = AssignEvaluation.assignGivenAValue(env, result, LanguageAST.JsonIdentifierExpression.NewNestedNameExpression(new LanguageAST.JsonPropertyIdentifier("Bob", LanguageAST.JsonIdentifierExpression.NewNameExpression(new LanguageAST.JsonIdentifier("Age")))));

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Bob"));
            NUnit.Framework.Assert.AreEqual("{\r\n  \"Age\": 5\r\n}", env2.JsonObjects["Bob"].ToString());
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_ToJObj")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void AssignEvaluation_ToJObj_ErrorIfWrongType()
        {
            //------------Setup for test--------------------------
            AssignEvaluation.toJObject(new JArray());

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_ToJObj")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void AssignEvaluation_ToJArray_ErrorIfWrongType()
        {
            //------------Setup for test--------------------------
            AssignEvaluation.toJOArray(new JObject());

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignObject_ArrayJson_Last()
        {
            var env = CreateTestEnvWithData();
            var env2 = AssignEvaluation.evalJsonAssign(new AssignValue("[[Person()]]", "{\"Name\":\"a\"}"), 0, env);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual("[\r\n  {\r\n    \"Name\": \"a\"\r\n  }\r\n]", env2.JsonObjects["Person"].ToString());
            var nameValue = PublicFunctions.EvalEnvExpression("[[@Person().Name]]", 0, false, env2);
            NUnit.Framework.Assert.IsNotNull(nameValue);
            var warewolfAtomResult = nameValue as CommonFunctions.WarewolfEvalResult.WarewolfAtomResult;
            NUnit.Framework.Assert.IsNotNull(warewolfAtomResult);
            NUnit.Framework.Assert.AreEqual("a", warewolfAtomResult.Item.ToString());
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignObject_Json()
        {
            var env = CreateTestEnvWithData();

            var env2 = AssignEvaluation.evalJsonAssign(new AssignValue("[[@Person]]", "{\"Name\":\"a\"}"), 0, env);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            var nameValue = PublicFunctions.EvalEnvExpression("[[@Person.Name]]", 0, false, env2);
            NUnit.Framework.Assert.IsNotNull(nameValue);
            var warewolfAtomResult = nameValue as CommonFunctions.WarewolfEvalResult.WarewolfAtomResult;
            NUnit.Framework.Assert.IsNotNull(warewolfAtomResult);
            NUnit.Framework.Assert.AreEqual("a", warewolfAtomResult.Item.ToString());
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignObject_ArrayJson_Last_TwoObjects()
        {
            var env = CreateTestEnvWithData();
            var env2 = AssignEvaluation.evalJsonAssign(new AssignValue("[[Person()]]", "{\"Name\":\"a\"}"), 0, env);
            var env3 = AssignEvaluation.evalJsonAssign(new AssignValue("[[Person()]]", "{\"Name\":\"h\"}"), 0, env2);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            var nameValue = PublicFunctions.EvalEnvExpression("[[@Person().Name]]", 0, false, env3);
            NUnit.Framework.Assert.IsNotNull(nameValue);
            var warewolfAtomResult = nameValue as CommonFunctions.WarewolfEvalResult.WarewolfAtomResult;
            NUnit.Framework.Assert.IsNotNull(warewolfAtomResult);
            NUnit.Framework.Assert.AreEqual("h", warewolfAtomResult.Item.ToString());
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAValue_ArrayJson_Last()
        {
            var env = CreateTestEnvWithData();

            var env2 = AssignEvaluation.evalJsonAssign(new AssignValue("[[Person().Name]]", "a"), 0, env);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "[\r\n  {\r\n    \"Name\": \"a\"\r\n  }\r\n]");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAValue_ArrayJson_index()
        {
            var env = CreateTestEnvWithData();

            var env2 = AssignEvaluation.evalJsonAssign(new AssignValue("[[Person(1).Name]]", "a"), 0, env);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "[\r\n  {\r\n    \"Name\": \"a\"\r\n  }\r\n]");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAValue_ArrayJson_Star()
        {
            var env = CreateTestEnvWithData();

            var env2 = AssignEvaluation.evalJsonAssign(new AssignValue("[[Person(1).Name]]", "a"), 0, env);
            env2 = AssignEvaluation.evalJsonAssign(new AssignValue("[[Person(2).Name]]", "a"), 0, env2);
            env2 = AssignEvaluation.evalJsonAssign(new AssignValue("[[Person(*).Name]]", "x"), 0, env2);
            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "[\r\n  {\r\n    \"Name\": \"x\"\r\n  },\r\n  {\r\n    \"Name\": \"x\"\r\n  }\r\n]");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void AssignEvaluation_assignGivenAValue_ArrayJson_InvalidIndex()
        {
            var env = CreateTestEnvWithData();

            AssignEvaluation.evalJsonAssign(new AssignValue("[[Person(abc).Name]]", "a"), 0, env);

            NUnit.Framework.Assert.Fail("Failed");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void AssignEvaluation_assignGivenAValue_ArrayJson_InvalidNamesExpresion()
        {
            var exp = LanguageAST.JsonIdentifierExpression.Terminal;
            var res = new JObject();
            AssignEvaluation.objectFromExpression(exp, CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(DataStorage.WarewolfAtom.Nothing), res);
            NUnit.Framework.Assert.Fail("Failed");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAnObjectValueCreatesValidJson_addsObjectIfItDoesNotExistIntLastAndAddsProperty()
        {
            var env = CreateTestEnvWithData();
            var x = new JArray();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child.Name]]");
            var val = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed;
            var env2 = AssignEvaluation.assignGivenAValue(env, result, val.Item);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "{\r\n  \"Child\": {\r\n    \"Name\": \"2\"\r\n  }\r\n}");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_ExpressionToObject")]
        public void AssignEvaluation_ExpressionToObject_Terminal_Returns_Object()
        {
            //------------Setup for test--------------------------
            var obj = new JObject();

            //------------Execute Test---------------------------
            var jobj = AssignEvaluation.expressionToObject(obj, LanguageAST.JsonIdentifierExpression.Terminal, CommonFunctions.WarewolfEvalResult.NewWarewolfAtomResult(DataStorage.WarewolfAtom.Nothing));
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(ReferenceEquals(obj, jobj));
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_IndexToInt")]
        public void AssignEvaluation_IndexToInt_LastReturnsCountPlusOne()
        {
            //------------Setup for test--------------------------
            var arr = new JArray { new JValue("bob") };

            //------------Execute Test---------------------------
            var res = AssignEvaluation.indexToInt(LanguageAST.Index.Last, arr);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(res.Length, 1);
            NUnit.Framework.Assert.AreEqual(2, res.Head);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_IndexToInt")]
        public void AssignEvaluation_IndexToInt_IntIndexReturnsInt()
        {
            //------------Setup for test--------------------------
            var arr = new JArray { new JValue("bob") };

            //------------Execute Test---------------------------
            var res = AssignEvaluation.indexToInt(LanguageAST.Index.NewIntIndex(1), arr);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(res.Length, 1);
            NUnit.Framework.Assert.AreEqual(1, res.Head);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_IndexToInt")]
        public void AssignEvaluation_IndexToInt_StarIndexReturnsAllIndexes()
        {
            //------------Setup for test--------------------------
            var arr = new JArray { new JValue("bob"), new JValue("bob") };

            //------------Execute Test---------------------------
            var res = AssignEvaluation.indexToInt(LanguageAST.Index.Star, arr);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(res.Length, 2);
            NUnit.Framework.Assert.AreEqual(1, res.Head);
            NUnit.Framework.Assert.AreEqual(2, res[1]);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_IndexToInt")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void AssignEvaluation_IndexToInt_ErrorsForAnExpression()
        {
            //------------Setup for test--------------------------
            var arr = new JArray();
            //------------Execute Test---------------------------

            var res = AssignEvaluation.indexToInt(LanguageAST.Index.IndexExpression.NewIndexExpression(LanguageAST.LanguageExpression.NewWarewolfAtomExpression(DataStorage.WarewolfAtom.Nothing)), arr);
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_AddPropertyToJsonValue")]
        public void AssignEvaluation_AddPropertyToJsonValue_ReturnsPropertyIfItExists()
        {
            //------------Setup for test--------------------------

            var a = new JObject();
            var x = new JValue("a");
            a.Add("Bob", x);
            //------------Execute Test---------------------------
            var res = AssignEvaluation.addPropertyToJsonNoValue(a, "Bob");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(ReferenceEquals(x, res));
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void AssignEvaluation_FailsIfExpressionIsNotOfCorrectType()
        {
            var env = CreateTestEnvWithData();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var val = LanguageAST.JsonIdentifierExpression.Terminal;
            AssignEvaluation.assignGivenAValue(env, result, val);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAnArrayValueCreatesValidJson_addsArrayIfItDoesNotExistIntLastAndAddsProperty()
        {
            var env = CreateTestEnvWithData();
            var x = new JArray();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child(1).Name]]");
            var val = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed;
            var env2 = AssignEvaluation.assignGivenAValue(env, result, val.Item);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "{\r\n  \"Child\": [\r\n    {\r\n      \"Name\": \"2\"\r\n    }\r\n  ]\r\n}");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAnArrayValueCreatesValidJson_LastIndex_addsArrayIfItDoesNotExistIntLastAndAddsProperty()
        {
            var env = CreateTestEnvWithData();
            var x = new JArray();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child().Name]]");
            var val = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed;
            var env2 = AssignEvaluation.assignGivenAValue(env, result, val.Item);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "{\r\n  \"Child\": [\r\n    {\r\n      \"Name\": \"2\"\r\n    }\r\n  ]\r\n}");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAnArrayValueCreatesValidJson_LastIndex_MutateArray()
        {
            var env = CreateTestEnvWithData();
            var x = new JArray();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var secondResult = PublicFunctions.EvalEnvExpression("[[rec(2).a]]", 0, false, env);
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child().Name]]");

            var val = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed;

            var env2 = AssignEvaluation.assignGivenAValue(env, result, val.Item);
            env2 = AssignEvaluation.assignGivenAValue(env2, secondResult, val.Item);
            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "{\r\n  \"Child\": [\r\n    {\r\n      \"Name\": \"2\"\r\n    },\r\n    {\r\n      \"Name\": \"4\"\r\n    }\r\n  ]\r\n}");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAnArrayValueCreatesValidJson_LastIndex_MutateArray_differentProperties()
        {
            var env = CreateTestEnvWithData();
            var x = new JArray();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var secondResult = PublicFunctions.EvalEnvExpression("[[rec(2).a]]", 0, false, env);
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child().Name]]");
            var parsed2 = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child().Age]]");
            var val = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed;
            var val2 = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed2;

            var env2 = AssignEvaluation.assignGivenAValue(env, result, val.Item);
            env2 = AssignEvaluation.assignGivenAValue(env2, secondResult, val2.Item);
            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual("{\r\n  \"Child\": [\r\n    {\r\n      \"Name\": \"2\",\r\n      \"Age\": \"4\"\r\n    }\r\n  ]\r\n}", env2.JsonObjects["Person"].ToString());
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAnArrayValueCreatesValidJson_StarIndex_MutateArray()
        {
            var env = CreateTestEnvWithData();
            var x = new JArray();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var secondResult = PublicFunctions.EvalEnvExpression("[[rec(2).a]]", 0, false, env);
            var thirdResult = PublicFunctions.EvalEnvExpression("[[rec(3).a]]", 0, false, env);
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child().Name]]");
            var parsed2 = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child(*).Name]]");
            var val = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed;
            var val2 = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed2;

            var env2 = AssignEvaluation.assignGivenAValue(env, result, val.Item);
            env2 = AssignEvaluation.assignGivenAValue(env2, secondResult, val.Item);
            env2 = AssignEvaluation.assignGivenAValue(env2, thirdResult, val2.Item);
            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "{\r\n  \"Child\": [\r\n    {\r\n      \"Name\": \"Bob\"\r\n    },\r\n    {\r\n      \"Name\": \"Bob\"\r\n    }\r\n  ]\r\n}");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAnArrayValueCreatesValidJson_StarIndex_MutateArray_AndAddAProperty()
        {
            var env = CreateTestEnvWithData();
            var x = new JArray();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var secondResult = PublicFunctions.EvalEnvExpression("[[rec(2).a]]", 0, false, env);
            var thirdResult = PublicFunctions.EvalEnvExpression("[[rec(3).a]]", 0, false, env);
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child().Name]]");
            var parsed2 = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child(*).Name]]");
            var parsed3 = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Age]]");
            var val = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed;
            var val2 = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed2;
            var val3 = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed3;

            var env2 = AssignEvaluation.assignGivenAValue(env, result, val.Item);
            env2 = AssignEvaluation.assignGivenAValue(env2, secondResult, val.Item);
            env2 = AssignEvaluation.assignGivenAValue(env2, thirdResult, val2.Item);
            env2 = AssignEvaluation.assignGivenAValue(env2, result, val3.Item);
            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            var obj = env2.JsonObjects["Person"];
            NUnit.Framework.Assert.AreEqual(obj.ToString(), "{\r\n  \"Child\": [\r\n    {\r\n      \"Name\": \"Bob\"\r\n    },\r\n    {\r\n      \"Name\": \"Bob\"\r\n    }\r\n  ],\r\n  \"Age\": \"2\"\r\n}");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_assignGivenAnArrayValueCreatesValidJson_Invalid()
        {
            var env = CreateTestEnvWithData();
            var x = new JArray();

            var result = PublicFunctions.EvalEnvExpression("[[rec(1).a]]", 0, false, env);
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Person.Child(1).Name]]");
            var val = (LanguageAST.LanguageExpression.JsonIdentifierExpression)parsed;
            var env2 = AssignEvaluation.assignGivenAValue(env, result, val.Item);

            NUnit.Framework.Assert.IsTrue(env2.JsonObjects.ContainsKey("Person"));
            NUnit.Framework.Assert.AreEqual(env2.JsonObjects["Person"].ToString(), "{\r\n  \"Child\": [\r\n    {\r\n      \"Name\": \"2\"\r\n    }\r\n  ]\r\n}");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_LanguageExpressionToJsonExpression()
        {
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[Child(1).Name]]");
            var exp = AssignEvaluation.languageExpressionToJsonIdentifier(parsed);
            NUnit.Framework.Assert.IsTrue(exp.IsIndexNestedNameExpression);
            var exp2 = (exp as LanguageAST.JsonIdentifierExpression.IndexNestedNameExpression).Item;
            var index = exp2.Index;
            NUnit.Framework.Assert.IsTrue(index.IsIntIndex);
            var bob = (index as LanguageAST.Index.IntIndex).Item;
            NUnit.Framework.Assert.AreEqual(1, bob);
            NUnit.Framework.Assert.AreEqual("Child", exp2.ObjectName);
            NUnit.Framework.Assert.IsTrue(exp2.Next.IsNameExpression);
            var x2 = (exp2.Next as LanguageAST.JsonIdentifierExpression.NameExpression).Item;
            NUnit.Framework.Assert.AreEqual(x2.Name, "Name");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_LanguageExpressionToJsonExpression_Scalar()
        {
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[@Child]]");
            var exp = AssignEvaluation.languageExpressionToJsonIdentifier(parsed);
            NUnit.Framework.Assert.IsNotNull(exp);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        public void AssignEvaluation_LanguageExpressionToJsonExpression_CompleteRecset()
        {
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[Child()]]");
            var exp = AssignEvaluation.languageExpressionToJsonIdentifier(parsed);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void AssignEvaluation_LanguageExpressionToJsonExpression_Atom()
        {
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("bob");
            var exp = AssignEvaluation.languageExpressionToJsonIdentifier(parsed);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("AssignEvaluation_assignGivenAValue")]
        [NUnit.Framework.ExpectedException(typeof(Exception))]
        public void AssignEvaluation_LanguageExpressionToJsonExpression_Complex()
        {
            var parsed = EvaluationFunctions.parseLanguageExpressionWithoutUpdate("[[[[bob]]]]");
            var exp = AssignEvaluation.languageExpressionToJsonIdentifier(parsed);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("AssignValue")]
        public void AssignValue_ObjectToObject_KeepsInt_DataType()
        {
            //------------Setup for test--------------------------
            var env = new ExecutionEnvironment();
            var jObject = "{\"Date\":\"2013-10-21T13:28:06.419Z\",\"Number\":32,\"Alpha\":\"Bob\"}";
            env.AssignJson(new AssignValue("[[@Person]]", jObject), 0);

            env.AssignJson(new AssignValue("[[@Person2.Date]]", "[[@Person.Date]]"), 0);
            env.AssignJson(new AssignValue("[[@Person2.Alpha]]", "[[@Person.Alpha]]"), 0);
            env.AssignJson(new AssignValue("[[@Person2.Number]]", "[[@Person.Number]]"), 0);

            //------------Assert Results-------------------------
            var jContainer = env.EvalJContainer("[[@Person2()]]");
            var value = jContainer.Last;
            var token = ((JProperty)value).Value;
            NUnit.Framework.Assert.AreEqual(32, token);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("AssignValue")]
        public void AssignValue_ObjectToObject_NestedObject_1Level_KeepsInt_DataType()
        {
            //------------Setup for test--------------------------
            var env = new ExecutionEnvironment();
            var jObject = "{\"Number\":32,\"Nested\":{\"Number2\":10,\"Alpha2\":\"Jack\"}}";
            env.AssignJson(new AssignValue("[[@Person]]", jObject), 0);

            env.AssignJson(new AssignValue("[[@Person2.Nested]]", "[[@Person.Nested]]"), 0);
            env.AssignJson(new AssignValue("[[@Person2.Number]]", "[[@Person.Number]]"), 0);

            //------------Assert Results-------------------------
            var jContainer = env.EvalJContainer("[[@Person2()]]");
            var value = jContainer.Last;
            var token = ((JProperty)value).Value;
            NUnit.Framework.Assert.AreEqual(32, token);

            var value2 = jContainer.First;
            var token2 = ((JProperty)value2).Value;
            NUnit.Framework.Assert.AreEqual("{\r\n  \"Number2\": 10,\r\n  \"Alpha2\": \"Jack\"\r\n}", token2);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("AssignValue")]
        public void AssignValue_ObjectToObject_NestedObject_2Levels_KeepsInt_DataType()
        {
            //------------Setup for test--------------------------
            var env = new ExecutionEnvironment();
            var jObject = "{\"Number\":32,\"Nested\":{\"Number2\":10,\"Alpha2\":{\"Number2\":10,\"Alpha2\":\"Jack\"}}}";
            env.AssignJson(new AssignValue("[[@Person]]", jObject), 0);

            env.AssignJson(new AssignValue("[[@Person2.Nested]]", "[[@Person.Nested]]"), 0);
            env.AssignJson(new AssignValue("[[@Person2.Number]]", "[[@Person.Number]]"), 0);

            //------------Assert Results-------------------------
            var jContainer = env.EvalJContainer("[[@Person2()]]");
            var value = jContainer.Last;
            var token = ((JProperty)value).Value;
            NUnit.Framework.Assert.AreEqual(32, token);

            var value2 = jContainer.First;
            var token2 = ((JProperty)value2).Value;
            NUnit.Framework.Assert.AreEqual("{\r\n  \"Number2\": 10,\r\n  \"Alpha2\": {\r\n    \"Number2\": 10,\r\n    \"Alpha2\": \"Jack\"\r\n  }\r\n}", token2);

        }
        DataStorage.WarewolfEnvironment CreateTestEnvWithData()
        {
            IEnumerable<IAssignValue> assigns = new List<IAssignValue>
             {
                 new AssignValue("[[rec().a]]", "2"),
                 new AssignValue("[[rec().a]]", "4"),
                 new AssignValue("[[rec().a]]", "Bob"),
                 new AssignValue("[[a]]", "5"),
                 new AssignValue("[[b]]", "2344"),
                 new AssignValue("[[c]]", "a"),
                 new AssignValue("[[d]]", "1")
             };
            var env = WarewolfTestData.CreateTestEnvEmpty("");

            var env2 = PublicFunctions.EvalMultiAssign(assigns, 0, env);
            return env2;
        }
    }
}
