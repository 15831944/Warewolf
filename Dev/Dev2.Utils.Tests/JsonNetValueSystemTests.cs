/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common.Utils;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System;

namespace Dev2.Utils.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class JsonNetValueSystemTests
    {

        const string _jsonObject = "{\"results\":[" +
               "{\"employeename\":\"name1\",\"employeesupervisor\":\"supervisor1\"}," +
               "{\"employeename\":\"name2\",\"employeesupervisor\":\"supervisor1\"}," +
               "{\"employeename\":\"name3\",\"employeesupervisor\":[\"supervisor1\",\"supervisor2\"]}" +
               "]}";
        const string _jsonArray = @"[  'Small',  'Medium',  'Large']";

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_HasMember_JObject_ReturnTrue()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JObject.Parse(_jsonObject);
            ValueSystem = new JsonNetValueSystem();
            Assert.IsTrue(ValueSystem.HasMember(results, "results"));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_HasMember_JObject_ReturnFalse()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JObject.Parse(_jsonObject);
            ValueSystem = new JsonNetValueSystem();
            Assert.IsFalse(ValueSystem.HasMember(results, "teststring"));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_HasMember_JArray_ReturnTrue()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JArray.Parse(_jsonArray);
            ValueSystem = new JsonNetValueSystem();
            Assert.IsTrue(ValueSystem.HasMember(results, "2"));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_HasMember_JArray_ReturnFalse()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JArray.Parse(_jsonArray);
            ValueSystem = new JsonNetValueSystem();
            Assert.IsFalse(ValueSystem.HasMember(results, "6"));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_HasMember_JArray_ReturnFalse_DefaultValue()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JArray.Parse(_jsonArray);
            ValueSystem = new JsonNetValueSystem();
            Assert.IsFalse(ValueSystem.HasMember(results, "-1"));
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_HasMember_Object_ReturnFalse()
        {
            IJsonPathValueSystem ValueSystem;
            ValueSystem = new JsonNetValueSystem();
            Assert.IsFalse(ValueSystem.HasMember(_jsonArray, "6"));
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_GetMembers()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JObject.Parse(_jsonObject);
            var result = "";
            ValueSystem = new JsonNetValueSystem();
            foreach (string key in ValueSystem.GetMembers(results))
            {
                result = key;
            }
            Assert.AreEqual("results", result);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_JObject_ReturnTrue()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JObject.Parse(_jsonObject);
            ValueSystem = new JsonNetValueSystem();
            Assert.IsTrue(ValueSystem.IsObject(results));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_JObject_ReturnFalse()
        {
            IJsonPathValueSystem ValueSystem;
            ValueSystem = new JsonNetValueSystem();
            Assert.IsFalse(ValueSystem.IsObject(new object()));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_IsArray_ReturnTrue()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JArray.Parse(_jsonArray);
            ValueSystem = new JsonNetValueSystem();
            Assert.IsTrue(ValueSystem.IsArray(results));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_IsArray_ReturnFalse()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JObject.Parse(_jsonObject);
            ValueSystem = new JsonNetValueSystem();
            Assert.IsFalse(ValueSystem.IsArray(results));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_IsPrimitive_ReturnTrue()
        {
            IJsonPathValueSystem ValueSystem;
            ValueSystem = new JsonNetValueSystem();
            Assert.IsTrue(ValueSystem.IsPrimitive(new object()));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_IsPrimitive_ReturnFalse()
        {
            IJsonPathValueSystem ValueSystem;
            ValueSystem = new JsonNetValueSystem();
            Assert.IsFalse(ValueSystem.IsArray(new JObject()));
            Assert.IsFalse(ValueSystem.IsPrimitive(new JArray()));
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void JsonNetValueSystem_IsPrimitive_ReturnException()
        {
            IJsonPathValueSystem ValueSystem;
            ValueSystem = new JsonNetValueSystem();
            ValueSystem.IsPrimitive(null);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_GetMemberValue_JObject()
        {
            IJsonPathValueSystem ValueSystem;
            var myJsonString = "{report: {Id: \"aaakkj98898983\"}}";
            ValueSystem = new JsonNetValueSystem();
            var obj = ValueSystem.GetMemberValue(JObject.Parse(myJsonString), "report");
            Assert.AreEqual("{\r\n  \"Id\": \"aaakkj98898983\"\r\n}".ToString(), obj.ToString());
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_GetMemberValue_JArray()
        {
            IJsonPathValueSystem ValueSystem;
            var results = JArray.Parse(_jsonArray);
            ValueSystem = new JsonNetValueSystem();
            var obj = ValueSystem.GetMemberValue(results, "1");
            Assert.AreEqual("Medium", obj.ToString());
        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(JsonNetValueSystem))]
        public void JsonNetValueSystem_GetMemberValue_ReturnNull()
        {
            IJsonPathValueSystem ValueSystem;
            ValueSystem = new JsonNetValueSystem();
            var obj = ValueSystem.GetMemberValue(new object(), "results");
            Assert.IsNull(obj);
        }
    }
}
