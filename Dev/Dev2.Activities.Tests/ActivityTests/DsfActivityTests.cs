/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using ActivityUnitTests;
using Dev2.Common.Interfaces.DB;
using Dev2.Common.Interfaces.Enums;
using Dev2.Common.State;
using Dev2.Communication;
using Dev2.Diagnostics;
using Dev2.DynamicServices;
using Dev2.Services.Security;
using NUnit.Framework;
using Moq;
using Unlimited.Applications.BusinessDesignStudio.Activities;
using Warewolf.Core;
using Warewolf.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Tests.Activities.ActivityTests
{
    [TestFixture]
    public class DsfActivityTests : BaseActivityUnitTest
    { 
        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_BeforeExecutionStart")]
        public void DsfActivity_BeforeExecutionStart_NullResourceID_DoesNothing()
        {
            //------------Setup for test--------------------------
            var act = new DsfActivity { InputMapping = ActivityStrings.DsfActivityInputMapping, OutputMapping = ActivityStrings.DsfActivityOutputMapping, ResourceID = null };
            //------------Execute Test---------------------------
            CheckPathOperationActivityDebugInputOutput(act, @"<ADL><scalar></scalar><Numeric><num></num></Numeric><CompanyName></CompanyName><Customer><FirstName></FirstName></Customer></ADL>",
                                                               "<ADL><scalar>scalarData</scalar><Numeric><num>1</num></Numeric><Numeric><num>2</num></Numeric><Numeric><num>3</num></Numeric><Numeric><num>4</num></Numeric><CompanyName>Dev2</CompanyName><Customer><FirstName>Wallis</FirstName></Customer></ADL>", out List<DebugItem> inRes, out List<DebugItem> outRes);

            // remove test datalist ;)

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(5, inRes.Count);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_GetForEachInputs")]
        [NUnit.Framework.ExpectedException(typeof(NotImplementedException))]
        public void DsfActivity_GetForEachInputs_WhenExecuted_ThrowsException()
        {
            //------------Setup for test--------------------------
            var dsfActivity = new DsfActivity();

            //------------Execute Test---------------------------
            dsfActivity.GetForEachInputs();
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_GetForEachOutputs")]
        [NUnit.Framework.ExpectedException(typeof(NotImplementedException))]
        public void DsfActivity_GetForEachOutputs_WhenExecuted_ThrowsException()
        {
            //------------Setup for test--------------------------
            var dsfActivity = new DsfActivity();

            //------------Execute Test---------------------------
            dsfActivity.GetForEachOutputs();
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_UpdateForEachInputs")]
        [NUnit.Framework.ExpectedException(typeof(NotImplementedException))]
        public void DsfActivity_UpdateForEachInputs_WhenExecuted_ThrowsException()
        {
            //------------Setup for test--------------------------
            var dsfActivity = new DsfActivity();

            //------------Execute Test---------------------------
            dsfActivity.UpdateForEachInputs(new List<Tuple<string, string>>());
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_UpdateForEachOutputs")]
        [NUnit.Framework.ExpectedException(typeof(NotImplementedException))]
        public void DsfActivity_UpdateForEachOutputs_WhenExecuted_ThrowsException()
        {
            //------------Setup for test--------------------------
            var dsfActivity = new DsfActivity();

            //------------Execute Test---------------------------
            dsfActivity.UpdateForEachOutputs(new List<Tuple<string, string>>());
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_GetFindMissingType")]
        public void DsfActivity_GetFindMissingType_Executed_ReturnsDsfActivity()
        {
            //------------Setup for test--------------------------
            var dsfActivity = new DsfActivity();

            //------------Execute Test---------------------------
            var findMissingType = dsfActivity.GetFindMissingType();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(enFindMissingType.DsfActivity, findMissingType);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_GetOutputs")]
        public void DsfActivity_GetOutputs_Called_ShouldReturnListWithResultValueInIt()
        {
            //------------Setup for test--------------------------
            var act = new DsfActivity
            {
                Outputs = new List<IServiceOutputMapping>
            {
                new ServiceOutputMapping {MappedTo = "[[res1]]"},
                new ServiceOutputMapping {MappedTo = "[[res2]]"}
            }
            };
            //------------Execute Test---------------------------
            var outputs = act.GetOutputs();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(2, outputs.Count);
            NUnit.Framework.Assert.AreEqual("[[res1]]", outputs[0]);
            NUnit.Framework.Assert.AreEqual("[[res1]]", outputs[0]);
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_BeforeExecutionStart")]
        public void DsfActivity_BeforeExecutionStart_EmptyResourceID_DoesNothing()
        {
            //------------Setup for test--------------------------
            var act = new DsfActivity { InputMapping = ActivityStrings.DsfActivityInputMapping, OutputMapping = ActivityStrings.DsfActivityOutputMapping, ResourceID = new InArgument<Guid>(Guid.Empty) };
            //------------Execute Test---------------------------
            CheckPathOperationActivityDebugInputOutput(act, @"<ADL><scalar></scalar><Numeric><num></num></Numeric><CompanyName></CompanyName><Customer><FirstName></FirstName></Customer></ADL>",
                                                               "<ADL><scalar>scalarData</scalar><Numeric><num>1</num></Numeric><Numeric><num>2</num></Numeric><Numeric><num>3</num></Numeric><Numeric><num>4</num></Numeric><CompanyName>Dev2</CompanyName><Customer><FirstName>Wallis</FirstName></Customer></ADL>", out List<DebugItem> inRes, out List<DebugItem> outRes);

            // remove test datalist ;)

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(5, inRes.Count);
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_BeforeExecutionStart")]
        public void DsfActivity_BeforeExecutionStart_ResourceIDAutorised_Executes()
        {
            //------------Setup for test--------------------------
            var resourceID = Guid.NewGuid();
            var act = new DsfActivity { InputMapping = ActivityStrings.DsfActivityInputMapping, OutputMapping = ActivityStrings.DsfActivityOutputMapping, ResourceID = new InArgument<Guid>(resourceID) };
            var mockAutorizationService = new Mock<IAuthorizationService>();
            mockAutorizationService.Setup(service => service.IsAuthorized(It.IsAny<IPrincipal>(), AuthorizationContext.Execute, resourceID.ToString())).Returns(true);

            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(act);
            p.SetProperty("AuthorizationService", mockAutorizationService.Object);
            //------------Execute Test---------------------------
            CheckPathOperationActivityDebugInputOutput(act, @"<ADL><scalar></scalar><Numeric><num></num></Numeric><CompanyName></CompanyName><Customer><FirstName></FirstName></Customer></ADL>",
                                                               "<ADL><scalar>scalarData</scalar><Numeric><num>1</num></Numeric><Numeric><num>2</num></Numeric><Numeric><num>3</num></Numeric><Numeric><num>4</num></Numeric><CompanyName>Dev2</CompanyName><Customer><FirstName>Wallis</FirstName></Customer></ADL>", out List<DebugItem> inRes, out List<DebugItem> outRes, new Mock<IPrincipal>().Object);

            // remove test datalist ;)
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(5, inRes.Count);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DsfActivity_OnExecute")]
        public void DsfActivity_OnExecute_WhenLocalExecutionInLocalContext_ExpectEnviromentIDRemainsLocalAndOverrideSetToFalse()
        {
            //------------Setup for test--------------------------
            var resourceID = Guid.NewGuid();
            var environmentID = Guid.Empty;
            var act = new DsfActivity
            {
                ResourceID = new InArgument<Guid>(resourceID),
                EnvironmentID = Guid.Empty
            };
            var mockAutorizationService = new Mock<IAuthorizationService>();
            mockAutorizationService.Setup(service => service.IsAuthorized(It.IsAny<IPrincipal>(), AuthorizationContext.Execute, resourceID.ToString())).Returns(true);

            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(act);
            p.SetProperty("AuthorizationService", mockAutorizationService.Object);

            //------------Execute Test---------------------------
            TestStartNode = new FlowStep
            {
                Action = act
            };

            TestData = "<DataList></DataList>";
            CurrentDl = "<DataList></DataList>";
            User = new Mock<IPrincipal>().Object;
            var result = ExecuteProcess(null, true, null, false, true, false, environmentID);


            var resultEnvironmentID = result.EnvironmentID;
            var isRemoteOverridden = result.IsRemoteInvokeOverridden;


            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(environmentID, resultEnvironmentID);
            NUnit.Framework.Assert.IsFalse(isRemoteOverridden);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("DsfActivity_UpdateDebugParentID")]
        public void DsfActivity_UpdateDebugParentID_UniqueIdSameIfNestingLevelNotChanged()

        {
            var dataObject = new DsfDataObject(CurrentDl, Guid.NewGuid())
            {
                // NOTE: WorkflowApplicationFactory.InvokeWorkflowImpl() will use HostSecurityProvider.Instance.ServerID 
                //       if this is NOT provided which will cause the tests to fail!
                ServerID = Guid.NewGuid(),
                IsDebug = true,
            };

            var act = new DsfActivity();
            var originalGuid = Guid.NewGuid();
            act.UniqueID = originalGuid.ToString();
            act.UpdateDebugParentID(dataObject);
            NUnit.Framework.Assert.AreEqual(originalGuid.ToString(), act.UniqueID);
            NUnit.Framework.Assert.AreEqual(act.GetWorkSurfaceMappingId(), originalGuid);


        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("DsfActivity_UpdateDebugParentID")]

        public void DsfActivity_UpdateDebugParentID_UniqueIdNotSameIfNestingLevelIncreased()

        {
            var dataObject = new DsfDataObject(CurrentDl, Guid.NewGuid())
            {
                ServerID = Guid.NewGuid(),
                IsDebug = true,
                ForEachNestingLevel = 1
            };

            var act = new DsfActivity();
            var originalGuid = Guid.NewGuid();
            act.UniqueID = originalGuid.ToString();
            act.UpdateDebugParentID(dataObject);
            NUnit.Framework.Assert.AreNotEqual(originalGuid.ToString(), act.UniqueID);
            NUnit.Framework.Assert.AreEqual(act.GetWorkSurfaceMappingId(), originalGuid);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_Constructor")]
        public void DsfActivity_Constructor_WithParameters_ShouldSetValues()
        {
            //------------Setup for test--------------------------
            const string toolboxFriendlyName = "toolBoxName";
            const string iconPath = "iconPath";
            const string serviceName = "serviceName";
            const string dataTags = "tags";
            const string resultValidationRequiredTags = "resValidationTags";
            const string resultValidationExpression = "resValidationExp";
            //------------Execute Test---------------------------

            var dsfActivity = new DsfActivity(toolboxFriendlyName, iconPath, serviceName, dataTags, resultValidationRequiredTags, resultValidationExpression);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(toolboxFriendlyName, dsfActivity.ToolboxFriendlyName);
            NUnit.Framework.Assert.AreEqual(serviceName, dsfActivity.ServiceName);
            NUnit.Framework.Assert.AreEqual(dataTags, dsfActivity.DataTags);
            NUnit.Framework.Assert.AreEqual(resultValidationRequiredTags, dsfActivity.ResultValidationRequiredTags);
            NUnit.Framework.Assert.AreEqual(resultValidationExpression, dsfActivity.ResultValidationExpression);
            NUnit.Framework.Assert.IsFalse(dsfActivity.IsService);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_Inputs")]
        public void DsfActivity_Inputs_GivenValue_ShouldSetProperty()
        {
            //------------Setup for test--------------------------
            var dsfActivity = new DsfActivity();

            //------------Execute Test---------------------------
            dsfActivity.Inputs = new List<IServiceInput>();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(dsfActivity.Inputs);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_Inputs")]
        public void DsfActivity_Inputs_GivenNull_ShouldNotSetProperty()
        {
            //------------Setup for test--------------------------
            var dsfActivity = new DsfActivity();
            dsfActivity.Inputs = new List<IServiceInput>();
            //------------Assert Precondition--------------------
            NUnit.Framework.Assert.IsNotNull(dsfActivity.Inputs);
            //------------Execute Test---------------------------
            dsfActivity.Inputs = null;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(dsfActivity.Inputs);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_ExecuteTool")]

        public void DsfActivity_ExecuteTool_DataObjectWithNullChannel_ShouldError()

        {
            var dataObject = new DsfDataObject(CurrentDl, Guid.NewGuid())
            {
                ServerID = Guid.NewGuid(),
                IsDebug = true,
                ForEachNestingLevel = 1
            };

            var act = new DsfActivity();
            var originalGuid = Guid.NewGuid();
            act.UniqueID = originalGuid.ToString();
            act.Execute(dataObject, 0);
            NUnit.Framework.Assert.IsTrue(dataObject.Environment.HasErrors());
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_BeforeExecutionStart")]
        public void DsfActivity_GetOutputs_NotObjectAndOutputMapping_GetsOutputs()
        {
            //------------Setup for test--------------------------
            var act = new DsfActivity { InputMapping = ActivityStrings.DsfActivityInputMapping, OutputMapping = ActivityStrings.DsfActivityOutputMapping, ResourceID = null };
            //------------Execute Test---------------------------
            var outputs = act.GetOutputs();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(outputs);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_BeforeExecutionStart")]
        public void DsfActivity_GetOutptus_ObjectAndOutputMapping_GetsObjectName()
        {
            //------------Setup for test--------------------------
            var act = new DsfActivity
            {
                InputMapping = ActivityStrings.DsfActivityInputMapping,
                OutputMapping = ActivityStrings.DsfActivityOutputMapping,
                ResourceID = null,
                IsObject = true,
                ObjectName = "Obj",
                ObjectResult = "{Name:BOb}"
            };
            //------------Execute Test---------------------------
            var outputs = act.GetOutputs();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(outputs);
            NUnit.Framework.Assert.AreEqual("Obj", outputs[0]);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category("DsfActivity_BeforeExecutionStart")]
        public void DsfActivity_GetDebugInputs_ServiceInputIsEmpty()
        {
            //------------Setup for test--------------------------
            var inputs = new List<IServiceInput>();
            inputs.Add(new Mock<IServiceInput>().Object);
            var act = new DsfActivity
            {
                InputMapping = ActivityStrings.DsfActivityInputMapping,
                Inputs = inputs,
                OutputMapping = ActivityStrings.DsfActivityOutputMapping,
                ResourceID = null,
                IsObject = true,
                ObjectName = "Obj",
                ObjectResult = "{Name:BOb}"
            };

            var env = new ExecutionEnvironment();
            //------------Execute Test---------------------------
            var outputs = act.GetDebugInputs(env, 0);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(0, outputs.Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category("DsfActivity_BeforeExecutionStart")]
        public void DsfActivity_GetDebugInputs_ServiceInputIsAtomListResult()
        {
            //------------Setup for test--------------------------
            var inputs = new List<IServiceInput>();
            var input = new Mock<IServiceInput>();
            input.SetupGet(o => o.Value).Returns("[[theList(*).Name]]");
            inputs.Add(input.Object);
            var act = new DsfActivity
            {
                InputMapping = ActivityStrings.DsfActivityInputMapping,
                Inputs = inputs,
                OutputMapping = ActivityStrings.DsfActivityOutputMapping,
                ResourceID = null,
                IsObject = true,
                ObjectName = "Obj",
                ObjectResult = "{Name:BOb}"
            };

            var env = new ExecutionEnvironment();
            env.AssignStrict("[[theList().Name]]", "Albert", 0);
            env.AssignStrict("[[theList().Name]]", "Bob", 0);
            //------------Execute Test---------------------------
            var outputs = act.GetDebugInputs(env, 0);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(1, outputs.Count);
            NUnit.Framework.Assert.AreEqual("Albert", outputs[0].ResultsList[0].Value);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].ResultsList[0].HasError);
            NUnit.Framework.Assert.AreEqual("Bob", outputs[0].ResultsList[1].Value);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].ResultsList[1].HasError);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DsfActivity_OnExecute")]
        public void DsfActivity_OnExecute_WhenIsTestExecutionShouldUpdateDataObject_SetBackToTrue()
        {
            //------------Setup for test--------------------------
            var environmentID = Guid.Empty;
            var dataObject = new DsfDataObject(CurrentDl, ExecutionId)
            {

                ServerID = Guid.NewGuid(),
                ExecutingUser = User,
                IsDebug = true,
                EnvironmentID = environmentID,
                IsRemoteInvokeOverridden = false,
                DataList = new StringBuilder(CurrentDl),
                IsServiceTestExecution = true
            };
            var resourceID = Guid.NewGuid();

            var act = new DsfActivity
            {
                ResourceID = new InArgument<Guid>(resourceID),
                EnvironmentID = Guid.Empty
            };
            var mockAutorizationService = new Mock<IAuthorizationService>();
            mockAutorizationService.Setup(service => service.IsAuthorized(It.IsAny<IPrincipal>(), AuthorizationContext.Execute, resourceID.ToString())).Returns(true);

            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(act);
            p.SetProperty("AuthorizationService", mockAutorizationService.Object);

            //------------Execute Test---------------------------
            TestStartNode = new FlowStep
            {
                Action = act
            };

            TestData = "<DataList></DataList>";
            CurrentDl = "<DataList></DataList>";
            User = new Mock<IPrincipal>().Object;
            ExecuteProcess(dataObject, true, null, false, true, false, environmentID);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(dataObject.IsServiceTestExecution);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category("DsfActivity_OnExecute")]
        public void DsfActivity_GetDebugInputs_WhenListInput_DebugInputExists()
        {
            //------------Setup for test--------------------------
            var environmentID = Guid.Empty;
            var env = new ExecutionEnvironment();
            var dataObject = new DsfDataObject(CurrentDl, ExecutionId)
            {

                ServerID = Guid.NewGuid(),
                ExecutingUser = User,
                IsDebug = true,
                EnvironmentID = environmentID,
                Environment = env,
                IsRemoteInvokeOverridden = false,
                DataList = new StringBuilder(CurrentDl),
                IsServiceTestExecution = true
            };
            env.Assign("[[list().Name]]", "bob", 0);
            //------------Execute Test---------------------------
            var act = new DsfActivity
            {
                Inputs = new List<IServiceInput>
                    {
                        new ServiceInput("Input1", "[[list(*).Name]]")
                    }
            };
            var inputs = act.GetDebugInputs(env, 0);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(1, inputs.Count);
            NUnit.Framework.Assert.AreEqual("bob", inputs[0].ResultsList[0].Value);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category("DsfActivity_OnExecute")]
        public void DsfActivity_GetDebugOutputsFromEnv_ObjectNotNull()
        {
            //------------Setup for test--------------------------
            var environmentID = Guid.Empty;
            var env = new ExecutionEnvironment();
            var dataObject = new DsfDataObject(CurrentDl, ExecutionId)
            {

                ServerID = Guid.NewGuid(),
                ExecutingUser = User,
                IsDebug = true,
                EnvironmentID = environmentID,
                Environment = env,
                IsRemoteInvokeOverridden = false,
                DataList = new StringBuilder(CurrentDl),
                IsServiceTestExecution = true
            };
            env.Assign("[[list().Name]]", "bob", 0);
            //------------Execute Test---------------------------
            var act = new DsfActivity
            {
                ObjectName = "objectname",
                IsObject = true,
                Outputs = new List<IServiceOutputMapping>
                    {
                        new ServiceOutputMapping("bob", "[[out]]", "list")
                    }
            };
            var outputs = act.GetDebugOutputs(env, 0);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(1, outputs[0].ResultsList.Count);
            NUnit.Framework.Assert.AreEqual("objectname", outputs[0].ResultsList[0].Value);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("DsfActivity_GetState")]
        public void DsfActivity_GetState()
        {
            //------------Setup for test--------------------------
            var remoteResourceID = Guid.Empty;
            var environmentID = Guid.Empty;
            var env = new ExecutionEnvironment();
            env.Assign("[[list().Name]]", "bob", 0);
            //------------Execute Test---------------------------
            var Inputs = new List<IServiceInput>
                    {
                        new ServiceInput("Input1", "[[list(*).Name]]")
                    };
            var Outputs = new List<IServiceOutputMapping>
                    {
                        new ServiceOutputMapping("bob", "[[out]]", "list")
                    };

            var serializer = new Dev2JsonSerializer();
            var inputs = serializer.Serialize(Inputs);
            var outputs = serializer.Serialize(Outputs);
            var remoteServerId = Guid.NewGuid();
            var ServiceServer = remoteServerId;
            var EnvironmentID = remoteServerId;
            var ServiceUri = "remoteEnvironment.Connection.AppServerUri";
            var ServiceName = "remote category";
            var ResourceID = remoteResourceID;
            var ParentServiceName = "ParentServiceName";
            var act = new DsfWorkflowActivity
            {
                ObjectName = "objectname",
                IsObject = true,
                Inputs = Inputs,
                Outputs = Outputs,
                IsWorkflow = false,
                ResourceID = new InArgument<Guid>(ResourceID),
                ServiceName = ServiceName,
                ServiceUri = ServiceUri,
                EnvironmentID = EnvironmentID,
                ServiceServer = ServiceServer,
                ParentServiceName = ParentServiceName

            };
            //------------Execute Test---------------------------
            var stateItems = act.GetState();
            NUnit.Framework.Assert.AreEqual(9, stateItems.Count());
            var expectedResults = new[]
            {
                new StateVariable
                {
                    Name="Inputs",
                    Type = StateVariable.StateType.Input,
                    Value = inputs
                },
                 new StateVariable
                {
                    Name="Outputs",
                    Type = StateVariable.StateType.Output,
                    Value = outputs
                 },
                 new StateVariable
                {
                    Name="ServiceServer",
                    Type = StateVariable.StateType.Input,
                    Value = ServiceServer.ToString()
                 },
                 new StateVariable
                {
                    Name="EnvironmentID",
                    Type = StateVariable.StateType.Input,
                    Value = EnvironmentID.ToString()
                 },
                 new StateVariable
                {
                    Name="IsWorkflow",
                    Type = StateVariable.StateType.Input,
                    Value = "False"
                 },
                 new StateVariable
                {
                    Name="ServiceUri",
                    Type = StateVariable.StateType.Input,
                    Value = ServiceUri
                 },
                 new StateVariable
                {
                    Name="ResourceID",
                    Type = StateVariable.StateType.Input,
                    Value = ResourceID.ToString()
                 },
                 new StateVariable
                {
                    Name="ServiceName",
                    Type = StateVariable.StateType.Input,
                    Value = ServiceName
                 },
                 new StateVariable
                {
                    Name="ParentServiceName",
                    Type = StateVariable.StateType.Input,
                    Value = ParentServiceName
                 }
            };
            var iter = act.GetState().Select(
                (item, index) => new
                {
                    value = item,
                    expectValue = expectedResults[index]
                }
                );

            //------------Assert Results-------------------------
            foreach (var entry in iter)
            {
                NUnit.Framework.Assert.AreEqual(entry.expectValue.Name, entry.value.Name);
                NUnit.Framework.Assert.AreEqual(entry.expectValue.Type, entry.value.Type);
                NUnit.Framework.Assert.AreEqual(entry.expectValue.Value, entry.value.Value);
            }
        }
    }
}
