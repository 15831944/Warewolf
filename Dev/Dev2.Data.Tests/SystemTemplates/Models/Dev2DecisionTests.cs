/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Data.Decisions.Operations;
using Dev2.Data.SystemTemplates.Models;
using Dev2.Data.TO;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Warewolf.Storage.Interfaces;

namespace Dev2.Data.Tests.SystemTemplates.Models
{
    [TestFixture]
    [SetUpFixture]
    public class Dev2DecisionTests
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_PopulatedColumnCount_Default()
        {
            var dev2Decision = new Dev2Decision();

            NUnit.Framework.Assert.AreEqual(0, dev2Decision.PopulatedColumnCount);
            NUnit.Framework.Assert.IsNull(dev2Decision.Col1);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_PopulatedColumnCount_Col1()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[a]]"
            };

            NUnit.Framework.Assert.AreEqual(1, dev2Decision.PopulatedColumnCount);
            NUnit.Framework.Assert.AreEqual("[[a]]", dev2Decision.Col1);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_PopulatedColumnCount_Col2()
        {
            var dev2Decision = new Dev2Decision
            {
                Col2 = "="
            };

            NUnit.Framework.Assert.AreEqual(1, dev2Decision.PopulatedColumnCount);
            NUnit.Framework.Assert.AreEqual("=", dev2Decision.Col2);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_PopulatedColumnCount_Col3()
        {
            var dev2Decision = new Dev2Decision
            {
                Col3 = "bob"
            };

            NUnit.Framework.Assert.AreEqual(1, dev2Decision.PopulatedColumnCount);
            NUnit.Framework.Assert.AreEqual("bob", dev2Decision.Col3);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_PopulatedColumnCount_AllColumns()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[a]]",
                Col2 = "=",
                Col3 = "bob"
            };

            NUnit.Framework.Assert.AreEqual(3, dev2Decision.PopulatedColumnCount);
            NUnit.Framework.Assert.AreEqual("[[a]]", dev2Decision.Col1);
            NUnit.Framework.Assert.AreEqual("=", dev2Decision.Col2);
            NUnit.Framework.Assert.AreEqual("bob", dev2Decision.Col3);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Zero()
        {
            var dev2Decision = new Dev2Decision
            {
                EvaluationFn = enDecisionType.IsEqual
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();

            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If = ", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_One_Scalar()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[field]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();

            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If [[field]] Is Between ", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_One_Recordset()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[recset(*).field]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();
            mockExecutionEnvironment.Setup(env => env.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<string> { "[[a]]" });
            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If [[a]] Is Between", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_One_Recordset_ExpectedError()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[recset(*).field]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var result = dev2Decision.GenerateToolLabel(null, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If ", result);
            NUnit.Framework.Assert.AreEqual(1, error.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("Object reference not set to an instance of an object.", error.FetchErrors()[0]);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_One_Recordset_Multiple_Values()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[recset(*).field]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();
            mockExecutionEnvironment.Setup(env => env.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<string> { "[[a]]", "[[b]]" });
            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If [[a]] Is Between AND [[b]] Is Between", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_Col2_Recordset_ExpectedError()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[field]]",
                Col2 = "[[recset(*).field]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var result = dev2Decision.GenerateToolLabel(null, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If ", result);
            NUnit.Framework.Assert.AreEqual(1, error.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("Object reference not set to an instance of an object.", error.FetchErrors()[0]);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_Col2_IsOnly_Recordset()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[field]]",
                Col2 = "[[recset(*).field]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();
            mockExecutionEnvironment.Setup(env => env.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<string> { "[[a]]", "[[b]]" });
            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If [[field]] Is Between [[a]] AND [[field]] Is Between [[b]]", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_Col1_Recordset_ExpectedError()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[recset(*).field]]",
                Col2 = "[[field]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var result = dev2Decision.GenerateToolLabel(null, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If ", result);
            NUnit.Framework.Assert.AreEqual(1, error.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("Object reference not set to an instance of an object.", error.FetchErrors()[0]);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_Col1_IsOnly_Recordset()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[recset(*).field]]",
                Col2 = "[[field]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();
            mockExecutionEnvironment.Setup(env => env.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<string> { "[[a]]", "[[b]]" });
            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If [[a]] Is Between [[field]] AND [[b]] Is Between [[field]]", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_Col1_And_Col2_Recordset_ExpectedError()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[recset(*).field]]",
                Col2 = "[[recset(*).field1]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var result = dev2Decision.GenerateToolLabel(null, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If ", result);
            NUnit.Framework.Assert.AreEqual(1, error.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("Object reference not set to an instance of an object.", error.FetchErrors()[0]);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_Col1_And_Col2_Recordset()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[recset(*).field]]",
                Col2 = "[[recset(*).field1]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();
            mockExecutionEnvironment.Setup(env => env.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<string> { "[[a]]", "[[b]]" });
            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If [[a]] Is Between [[a]]", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_Col1_And_Col2_IsNotRecordset_ExpectedError()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[field]]",
                Col2 = "[[field1]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var result = dev2Decision.GenerateToolLabel(null, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If ", result);
            NUnit.Framework.Assert.AreEqual(1, error.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("Object reference not set to an instance of an object.", error.FetchErrors()[0]);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_Col1_And_Col2_IsNotRecordset()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[field]]",
                Col2 = "[[field1]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();
            mockExecutionEnvironment.Setup(env => env.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<string> { "[[a]]", "[[b]]" });
            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If [[a]] Is Between [[a]]", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(Dev2Decision))]
        public void Dev2Decision_GenerateUserFriendlyModel_PopulatedColumnCount_Two_NoMatching_Options()
        {
            var dev2Decision = new Dev2Decision
            {
                Col1 = "[[recset(*).field]]",
                Col2 = "[[recset(*).field1]]",
                EvaluationFn = enDecisionType.IsBetween
            };

            var mockExecutionEnvironment = new Mock<IExecutionEnvironment>();
            mockExecutionEnvironment.Setup(env => env.EvalAsListOfStrings(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<string> { "[[a]]", "[[b]]", "[[c]]" });
            var result = dev2Decision.GenerateToolLabel(mockExecutionEnvironment.Object, Dev2DecisionMode.AND, out var error);

            NUnit.Framework.Assert.AreEqual("If [[a]] Is Between [[a]] AND [[c]] Is Between [[c]]", result);
            NUnit.Framework.Assert.AreEqual(0, error.FetchErrors().Count);
        }
    }
}
