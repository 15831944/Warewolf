/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;

namespace Dev2.Common.Tests
{
    [TestFixture]
    public class NameValueTests
    {
        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Constructor()
        {
            var constructor = new NameValue
            {
                Name = "testName",
                Value = "testValue"
            };
            Assert.IsNotNull(constructor);
            Assert.AreEqual("testName", constructor.Name);
            Assert.AreEqual("testValue", constructor.Value);
            Assert.AreEqual("testName", constructor.ToString());
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Constructor_Values()
        {
            var constructor = new NameValue("testName", "testValue");
            Assert.IsNotNull(constructor);
            Assert.AreEqual("testName", constructor.Name);
            Assert.AreEqual("testValue", constructor.Value);
            Assert.AreEqual("testName", constructor.ToString());
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_GetHashCode()
        {
            var constructor = new NameValue("testName", "testValue");
            Assert.IsNotNull(constructor);
            Assert.AreNotEqual("0", constructor.GetHashCode());
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_GetHashCode_NameEmpty()
        {
            var constructor = new NameValue(null, "testValue");
            Assert.IsNotNull(constructor);
            Assert.AreNotEqual(0, constructor.GetHashCode());
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_GetHashCode_ValueEmpty()
        {
            var constructor = new NameValue("Name", null);
            Assert.IsNotNull(constructor);
            Assert.AreNotEqual(0, constructor.GetHashCode());
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Equals_NameValue_ExpectTrue()
        {
            var constructor = new NameValue("testName", "testValue");
            var other = new NameValue("testName", "testValue");
            var result = constructor.Equals(other);
            Assert.IsTrue(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Equals_NameValue_ExpectFalse()
        {
            var constructor = new NameValue("testName", "testValue");
            var other = new NameValue("testNameOther", "testValueOther");
            var result = constructor.Equals(other);
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Equals_NameValue_OtherisNullExpectFalse()
        {
            var constructor = new NameValue("testName", "testValue");
            var other = new NameValue();
            other = null;
            var result = constructor.Equals(other);
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Equals_NameValue_ConstructorEqualsOtherExpectTrue()
        {
            var constructor = new NameValue("testName", "testValue");
            var other = constructor;
            var result = constructor.Equals(other);
            Assert.IsTrue(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Equals_Object_ExpectTrue()
        {
            var constructor = new NameValue("testName", "testValue");
            var other = new object();
            other = constructor;
            var result = constructor.Equals(other);
            Assert.IsTrue(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Equals_Object_isNullExpectFalse()
        {
            var constructor = new NameValue("testName", "testValue");
            var other = new object();
            other = null;
            var result = constructor.Equals(other);
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_Equals_Object_ObjectDifferentType()
        {
            var constructor = new NameValue("testName", "testValue");
            var other = new object();
            var result = constructor.Equals(other);
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_bool_operator_NotEqual()
        {
            var result = (new NameValue("testName", "testValue") != new NameValue("testName3", "testValue3"));
            Assert.IsTrue(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_bool_operator_Equal()
        {
            var result = (new NameValue("testName", "testValue") == new NameValue("testName", "testValue3"));
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [NUnit.Framework.Category(nameof(NameValue))]
        public void NameValue_OnPropertyChanged()
        {
            var constructor = new NameValue();
            var receivedEvents = new List<string>();

            constructor.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };
            constructor.Name = "testNameChanged";
            constructor.Value = "testNameChanged";
            Assert.AreEqual(2, receivedEvents.Count);
            Assert.AreEqual("Name", receivedEvents[0]);
            Assert.AreEqual("Value", receivedEvents[1]);
        }
    }
}
