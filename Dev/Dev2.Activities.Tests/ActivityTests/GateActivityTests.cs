﻿/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/
using Dev2.Activities;
using Dev2.Activities.Gates;
using Dev2.Data.SystemTemplates.Models;
using Dev2.DynamicServices;
using Dev2.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Warewolf.Data.Options.Enums;
using Warewolf.Storage;
using Warewolf.Storage.Interfaces;

namespace Dev2.Tests.Activities.ActivityTests
{
    [TestClass]
    public class GateActivityTests : BaseActivityTests
    {
        static IExecutionEnvironment CreateExecutionEnvironment()
        {
            return new ExecutionEnvironment();
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Construct_Default_Execute_Returns_GateActivity()
        {
            var env = CreateExecutionEnvironment();
            var dataObject = new Mock<IDSFDataObject>();
            dataObject.Setup(o => o.IsDebugMode()).Returns(true);
            dataObject.Setup(o => o.Environment).Returns(env);
            dataObject.Setup(o => o.Settings.EnableDetailedLogging).Returns(true);

            var activity = new GateActivity();
            activity.Execute(dataObject.Object, 0);
            Assert.AreEqual("Gate", activity.DisplayName);
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Construct_Set_GateRetryStrategy_GateFailure_Returns_GateRetryStrategy_GateFailure()
        {
            var env = CreateExecutionEnvironment();
            var dataObject = new Mock<IDSFDataObject>();
            dataObject.Setup(o => o.IsDebugMode()).Returns(true);
            dataObject.Setup(o => o.Environment).Returns(env);
            dataObject.Setup(o => o.Settings.EnableDetailedLogging).Returns(true);

            var activity = new GateActivity();
            activity.GateRetryStrategy = RetryAlgorithm.LinearBackoff.ToString();
            activity.GateFailure = GateFailureAction.StopOnError.ToString(); ;

            Assert.AreEqual(RetryAlgorithm.LinearBackoff.ToString(), activity.GateRetryStrategy);
            Assert.AreEqual(GateFailureAction.StopOnError.ToString(), activity.GateFailure);
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Equals_Set_OtherIsNull_Returns_IsFalse()
        {
            var gateActivity = new GateActivity();
            var gateActivityEqual = gateActivity.Equals(null);
            Assert.IsFalse(gateActivityEqual);
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Equals_Set_OtherisEqual_Returns_IsTrue()
        {
            var gateActivity = new GateActivity();
            var gateActivityOther = gateActivity;
            var gateActivityEqual = gateActivity.Equals(gateActivityOther);
            Assert.IsTrue(gateActivityEqual);
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Equals_Set_BothAreObjects_Returns_IsFalse()
        {
            object gateActivity = new GateActivity();
            var other = new object();
            var gateActivityEqual = gateActivity.Equals(other);
            Assert.IsFalse(gateActivityEqual);
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Equals_Set_OtherisObjectofGateActivityEqual_Returns_IsFalse()
        {
            var gateActivity = new GateActivity();
            object other = new GateActivity();
            var gateActivityEqual = gateActivity.Equals(other);
            Assert.IsFalse(gateActivityEqual);
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_GetHashCode()
        {
            var gateActivityActivity = new GateActivity { };
            {
                var hashCode = gateActivityActivity.GetHashCode();
                Assert.IsNotNull(hashCode);
            }
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        public void Gate_Environment_Construtor_Returns_Environment_IsNotNull()
        {
            var env = CreateExecutionEnvironment();
            var dataObject = new Mock<IDSFDataObject>();
            dataObject.Setup(o => o.IsDebugMode()).Returns(true);
            dataObject.Setup(o => o.Environment).Returns(env);
            dataObject.Setup(o => o.Settings.EnableDetailedLogging).Returns(true);

            var gate = new Gate(env);
            Assert.IsNotNull(gate.Environment);
        }
        [TestMethod]
        [Owner("Candice Daniel")]
        public void Gate_Construtor_SetEnvironmentProperty_Returns__Environment_IsNotNull()
        {
            var env = CreateExecutionEnvironment();
            var dataObject = new Mock<IDSFDataObject>();
            dataObject.Setup(o => o.IsDebugMode()).Returns(true);
            dataObject.Setup(o => o.Environment).Returns(env);
            dataObject.Setup(o => o.Settings.EnableDetailedLogging).Returns(true);

            var gate = new Gate
            {
                Environment = env
            };
            Assert.IsNotNull(gate.Environment);
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Execute_GivenNoConditions_ExpectDetailedLog()
        {
            var expectedNextActivity = new Mock<IDev2Activity>();

            //---------------Set up test pack-------------------
            var conditions = new Dev2DecisionStack
            {
                TheStack = new List<Dev2Decision>()
            };

            //------------Setup for test--------------------------
            var act = new GateActivity
            {
                Conditions = conditions,
                NextNodes = new List<IDev2Activity> { expectedNextActivity.Object },
            };


            var dataObject = new DsfDataObject("", Guid.NewGuid());

            var result = act.Execute(dataObject, 0);

            Assert.AreEqual(expectedNextActivity.Object, result);
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Execute_GivenFailingCondition_ExpectDetailedLog()
        {
            var expectedNextActivity = new Mock<IDev2Activity>();

            //---------------Set up test pack-------------------
            var conditions = new Dev2DecisionStack
            {
                TheStack = new List<Dev2Decision>()
            };
            conditions.AddModelItem(new Dev2Decision
            {
                Col1 = "[[a]]",
                EvaluationFn = Data.Decisions.Operations.enDecisionType.IsEqual,
                Col2 = "bob"
            });

            //------------Setup for test--------------------------
            var act = new GateActivity
            {
                Conditions = conditions,
                NextNodes = new List<IDev2Activity> { expectedNextActivity.Object },
            };

            var dataObject = new DsfDataObject("", Guid.NewGuid());

            var result = act.Execute(dataObject, 0);

            Assert.IsNull(result);
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Execute_GivenFailingConditionVarNotExistsWithRetry_ExpectRetryGate()
        {
            var expectedNextActivity = new Mock<IDev2Activity>();
            var expectedRetryActivity = new GateActivity();
            var expectedRetryActivityId = Guid.NewGuid();

            //---------------Set up test pack-------------------
            var conditions = new Dev2DecisionStack
            {
                TheStack = new List<Dev2Decision>()
            };
            conditions.AddModelItem(new Dev2Decision
            {
                Col1 = "[[a]]",
                EvaluationFn = Data.Decisions.Operations.enDecisionType.IsEqual,
                Col2 = "bob"
            });

            //------------Setup for test--------------------------
            var act = new GateActivity
            {
                GateFailure = GateFailureAction.Retry.ToString(),
                RetryEntryPoint = expectedRetryActivity,
                RetryEntryPointId = expectedRetryActivityId,
                Conditions = conditions,
                NextNodes = new List<IDev2Activity> { expectedNextActivity.Object },
            };

            var dataObject = new DsfDataObject("", Guid.NewGuid());
            dataObject.Environment.Assign("[[nota]]", "bob", 0);

            var result = act.Execute(dataObject, 0);

            Assert.AreNotEqual(expectedNextActivity.Object, result, "execution should not proceed as normal if gate fails and Retry is set");
            var numberOfRetries = expectedRetryActivity.GetState().First(o => o.Name == "NumberOfRetries").Value;

            Assert.AreEqual("1", numberOfRetries);

            Assert.AreNotEqual(expectedNextActivity.Object, result, "execution should not proceed as normal if gate fails and Retry is set");
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Execute_GivenFailingConditionWithStopOnError_ExpectRetryGate()
        {
            var expectedNextActivity = new Mock<IDev2Activity>();

            //---------------Set up test pack-------------------
            var conditions = new Dev2DecisionStack
            {
                TheStack = new List<Dev2Decision>()
            };
            conditions.AddModelItem(new Dev2Decision
            {
                Col1 = "[[a]]",
                EvaluationFn = Data.Decisions.Operations.enDecisionType.IsEqual,
                Col2 = "bob"
            });

            //------------Setup for test--------------------------
            var act = new GateActivity
            {
                Conditions = conditions,
                NextNodes = new List<IDev2Activity> { expectedNextActivity.Object },
            };

            var dataObject = new DsfDataObject("", Guid.NewGuid());
            dataObject.Environment.Assign("[[a]]", "notbob", 0);

            var result = act.Execute(dataObject, 0);

            Assert.IsNull(result, "execution should stop if gate fails and StopOnError is set");
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Execute_GivenFailingConditionWithRetry_ExpectRetryGate()
        {
            var expectedNextActivity = new Mock<IDev2Activity>();
            var expectedRetryActivity = new GateActivity();
            var expectedRetryActivityId = Guid.NewGuid();

            //---------------Set up test pack-------------------
            var conditions = new Dev2DecisionStack
            {
                TheStack = new List<Dev2Decision>()
            };
            conditions.AddModelItem(new Dev2Decision
            {
                Col1 = "[[a]]",
                EvaluationFn = Data.Decisions.Operations.enDecisionType.IsEqual,
                Col2 = "bob"
            });

            //------------Setup for test--------------------------
            var act = new GateActivity
            {
                GateFailure = GateFailureAction.Retry.ToString(),
                RetryEntryPoint = expectedRetryActivity,
                RetryEntryPointId = expectedRetryActivityId,
                Conditions = conditions,
                NextNodes = new List<IDev2Activity> { expectedNextActivity.Object },
            };

            var dataObject = new DsfDataObject("", Guid.NewGuid());
            dataObject.Environment.Assign("[[a]]", "notbob", 0);

            var result = act.Execute(dataObject, 0);

            Assert.AreNotEqual(expectedNextActivity.Object, result, "execution should not proceed as normal if gate fails and StopOnError is set");
            var numberOfRetries = expectedRetryActivity.GetState().First(o => o.Name == "NumberOfRetries").Value;

            Assert.AreEqual("1", numberOfRetries);
            Assert.AreEqual(expectedRetryActivity, result);
        }

        [TestMethod]
        [Owner("Rory McGuire")]
        [TestCategory(nameof(GateActivity))]
        public void GateActivity_Execute_GivenPassingConditions_ExpectDetailedLog()
        {
            var expectedNextActivity = new Mock<IDev2Activity>();
            var expectedRetryActivity = new GateActivity();
            var expectedRetryActivityId = Guid.NewGuid();
            //---------------Set up test pack-------------------
            var conditions = new Dev2DecisionStack();
            conditions.TheStack = new List<Dev2Decision>();
            conditions.AddModelItem(new Dev2Decision
            {
                Col1 = "[[a]]",
                EvaluationFn = Data.Decisions.Operations.enDecisionType.IsEqual,
                Col2 = "bob"
            });

            //------------Setup for test--------------------------
            var act = new GateActivity
            {
                GateFailure = GateFailureAction.Retry.ToString(),
                Conditions = conditions,
                RetryEntryPoint = expectedRetryActivity,
                RetryEntryPointId = expectedRetryActivityId,
                NextNodes = new List<IDev2Activity> { expectedNextActivity.Object },
            };


            var dataObject = new DsfDataObject("", Guid.NewGuid());
            dataObject.Environment.Assign("[[a]]", "bob", 0);

            var result = act.Execute(dataObject, 0);

            Assert.AreNotEqual(expectedRetryActivity, result, "execution should proceed as normal if gate passes and Retry is set");
            var numberOfRetries = expectedRetryActivity.GetState().First(o => o.Name == "NumberOfRetries").Value;

            Assert.AreEqual("0", numberOfRetries, "number of retries should not change if gate passes");
            Assert.AreEqual(expectedNextActivity.Object, result);
        }
    }
}
