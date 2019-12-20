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
using Dev2.Common.Utils;
using NUnit.Framework;

namespace Dev2.Utils.Tests
{

    [TestFixture]
    [SetUpFixture]
    public class JsonPathNodeTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(JsonPathNode))]
        public void JsonPathNode_Constractor_PathLength_IsNull_AreEqual_ExpectArgumentNullException()
        {
            //--------------------------Arrange---------------------------
            //--------------------------Act-------------------------------
            //--------------------------Assert----------------------------
            Assert.Throws<ArgumentNullException>(() => new JsonPathNode(new object(), null));
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(JsonPathNode))]
        public void JsonPathNode_Constractor_PathLength_IsZero_AreEqual_ExpectArgumentException()
        {
            //--------------------------Arrange---------------------------
            //--------------------------Act-------------------------------
            //--------------------------Assert----------------------------
            Assert.Throws<ArgumentException>(() => new JsonPathNode(new object(), string.Empty));
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(JsonPathNode))]
        public void JsonPathNode_Constractor_IsNotNull_AreEqual_ExpectTrue()
        {
            //--------------------------Arrange---------------------------
            var obj = new object();
            obj = "testObject";

            var testString = "testString";

            var jsonPathNode = new JsonPathNode(obj, testString);
            //--------------------------Act-------------------------------
            //--------------------------Assert----------------------------
            Assert.AreEqual(testString + " = " + obj, jsonPathNode.ToString());
        }
    }
}
