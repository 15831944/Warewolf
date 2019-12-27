/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using NUnit.Framework;
using WarewolfParserInterop;

namespace Warewolf.Storage.Tests
{
    [TestFixture]
    public class BuildJObjectArrayIndexMapHelperTests
    {
        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(BuildJObjectArrayIndexMapHelper))]
        public void BuildJObjectArrayIndexMapHelper_Build_GivenNullExpression_DoesNotThrow()
        {
            var env = new ExecutionEnvironment();
            env.AssignJson(new AssignValue("[[@ob]]", "{\"addresses\":[],\"users\":[{\"name\":\"bob\"},{\"name\":\"joe\"},{\"name\":\"jef\"}],\"tags\":[]}"), 0);

            var test = new BuildJObjectArrayIndexMapHelper(env);

            const string exp = "[[@ob.users(*).name]]";

            var indexMap = test.Build(null, exp);

            Assert.AreEqual(0, indexMap.Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(BuildJObjectArrayIndexMapHelper))]
        public void BuildJObjectArrayIndexMapHelper_Build_GivenObjectWithListOfStrings_ExpectIndexForEachArrayItem()
        {
            var env = new ExecutionEnvironment();
            env.AssignJson(new AssignValue("[[@ob]]", "{\"addresses\":[],\"users\":[{\"name\":\"bob\"},{\"name\":\"joe\"},{\"name\":\"jef\"}],\"tags\":[]}"), 0);

            var test = new BuildJObjectArrayIndexMapHelper(env);
            const string exp = "[[@ob.users(*).name]]";

            var parsedExpression = EvaluationFunctions.parseLanguageExpressionWithoutUpdate(exp) as LanguageAST.LanguageExpression.JsonIdentifierExpression;

            var indexMap = test.Build(parsedExpression.Item, exp);

            Assert.AreEqual(3, indexMap.Count);
            Assert.AreEqual("[[@ob.users(1).name]]", indexMap[0]);
            Assert.AreEqual("[[@ob.users(2).name]]", indexMap[1]);
            Assert.AreEqual("[[@ob.users(3).name]]", indexMap[2]);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(BuildJObjectArrayIndexMapHelper))]
        public void BuildJObjectArrayIndexMapHelper_Build_GivenListOfStrings_ExpectIndexForEachArrayItem()
        {
            var env = new ExecutionEnvironment();
            env.AssignJson(new AssignValue("[[@users()]]", "{\"name\":\"bob\"}"), 0);
            env.AssignJson(new AssignValue("[[@users()]]", "{\"name\":\"joe\"}"), 0);
            env.AssignJson(new AssignValue("[[@users()]]", "{\"name\":\"jef\"}"), 0);

            var test = new BuildJObjectArrayIndexMapHelper(env);

            const string exp = "[[@users(*).name]]";

            var parsedExpression = EvaluationFunctions.parseLanguageExpressionWithoutUpdate(exp) as LanguageAST.LanguageExpression.JsonIdentifierExpression;

            var indexMap = test.Build(parsedExpression.Item, exp);

            Assert.AreEqual(3, indexMap.Count);
            Assert.AreEqual("[[@users(1).name]]", indexMap[0]);
            Assert.AreEqual("[[@users(2).name]]", indexMap[1]);
            Assert.AreEqual("[[@users(3).name]]", indexMap[2]);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(BuildJObjectArrayIndexMapHelper))]
        public void BuildJObjectArrayIndexMapHelper_Build_GivenObjectWithChildThatHasListOfStrings_ExpectIndexForEachArrayItem()
        {
            var env = new ExecutionEnvironment();
            env.AssignJson(new AssignValue("[[@ob]]", "{\"addresses\":[],\"user\":{\"name\":\"bob\", \"friends\":[{\"name\":\"joe\"},{\"name\":\"jef\"}]}}"), 0);

            var test = new BuildJObjectArrayIndexMapHelper(env);

            const string exp = "[[@ob.user.friends(*).name]]";

            var parsedExpression = EvaluationFunctions.parseLanguageExpressionWithoutUpdate(exp) as LanguageAST.LanguageExpression.JsonIdentifierExpression;

            var indexMap = test.Build(parsedExpression.Item, exp);

            Assert.AreEqual(2, indexMap.Count);
            Assert.AreEqual("[[@ob.user.friends(1).name]]", indexMap[0]);
            Assert.AreEqual("[[@ob.user.friends(2).name]]", indexMap[1]);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(BuildJObjectArrayIndexMapHelper))]
        public void BuildJObjectArrayIndexMapHelper_Build_GivenObjectWithChildThatHasListOfStrings_SelectNonExistentItem_ExpectNoIndexMap()
        {
            var env = new ExecutionEnvironment();
            env.AssignJson(new AssignValue("[[@ob]]", "{\"addresses\":[],\"user\":{\"name\":\"bob\", \"friends\":[{\"name\":\"joe\"},{\"name\":\"jef\"}]}}"), 0);

            var test = new BuildJObjectArrayIndexMapHelper(env);

            const string exp = "[[@ob.user.addresses(*).name]]";

            var parsedExpression = EvaluationFunctions.parseLanguageExpressionWithoutUpdate(exp) as LanguageAST.LanguageExpression.JsonIdentifierExpression;

            var indexMap = test.Build(parsedExpression.Item, exp);

            Assert.AreEqual(0, indexMap.Count);
        }
    }
}
