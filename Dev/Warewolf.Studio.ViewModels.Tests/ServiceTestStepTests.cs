using System;
using System.Collections.ObjectModel;
using System.Linq;
using Dev2.Activities;
using Dev2.Common.Interfaces;
using NUnit.Framework;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class ServiceTestStepTests
    {
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_ActivityType_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ActivityType")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.ActivityType = typeof(DsfSwitch).Name;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(typeof(DsfSwitch).Name, testModel.ActivityType);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_AssertSelected_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "AssertSelected")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.AssertSelected = true;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(true, testModel.AssertSelected);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_Children_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Children")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.Children = new ObservableCollection<IServiceTestStep>();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_StepDescription_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "StepDescription")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.StepDescription = "Desc";
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual("Desc", testModel.StepDescription);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_MockSelected_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "MockSelected")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.MockSelected = true;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(true, testModel.MockSelected);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_TestPending_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "TestPending")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.TestPending = true;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(true, testModel.TestPending);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]

        public void ServiceTestStep_TestFailing_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "TestFailing")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.TestFailing = true;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(true, testModel.TestFailing);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_TestInvalid_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "TestInvalid")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.TestInvalid = true;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(true, testModel.TestInvalid);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_TestPassed_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "TestPassed")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.TestPassed = true;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(true, testModel.TestPassed);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_Result_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Result")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            var testRunResult = new TestRunResult();
            testModel.Result = testRunResult;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_Type_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Type")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.Type = StepType.Mock;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(StepType.Mock, testModel.Type);
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_Parent_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Parent")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.Parent = new Mock<IServiceTestStep>().Object;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_StepOutputs_WhenSet_ShouldFirePropertyChanged()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var _wasCalled = false;
            testModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "StepOutputs")
                {
                    _wasCalled = true;
                }
            };
            //------------Execute Test---------------------------
            testModel.StepOutputs = new ObservableCollection<IServiceTestOutput>();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(_wasCalled);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_AddNewOutput_WhenEmptyVariable_ShouldNotAddStepOutPut()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var beforeCount = testModel.StepOutputs.Count;
            //------------Execute Test---------------------------
            testModel.AddNewOutput("");
            var afterCount = testModel.StepOutputs.Count;
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(beforeCount, afterCount);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_AddNewOutput_WhenVariableIsRecordSet_ShouldAddStepOutPutWithAddAction()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var beforeCount = testModel.StepOutputs.Count;
            //------------Execute Test---------------------------
            testModel.AddNewOutput("rec().a");
            var serviceTestOutput = (ServiceTestOutput)testModel.StepOutputs.Last();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(testModel.StepOutputs.Count, beforeCount + 1);
            NUnit.Framework.Assert.AreEqual("rec().a", testModel.StepOutputs.Single().Variable);
            NUnit.Framework.Assert.AreEqual("", testModel.StepOutputs.Single().From);
            NUnit.Framework.Assert.AreEqual("", testModel.StepOutputs.Single().To);
            NUnit.Framework.Assert.AreEqual("", testModel.StepOutputs.Single().Value);
            NUnit.Framework.Assert.IsNotNull(serviceTestOutput.AddNewAction);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void ServiceTestStep_AddNewOutput_WhenVariableIsNumericRecordSet_ShouldAddStepOutPutWithAddAction()
        {
            //------------Setup for test--------------------------
            var testModel = CreateDecisionMock();
            var beforeCount = testModel.StepOutputs.Count;
            //------------Execute Test---------------------------
            testModel.AddNewOutput("rec(1).a");
            var serviceTestOutput = (ServiceTestOutput)testModel.StepOutputs.Last();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(testModel.StepOutputs.Count, beforeCount + 1);
            NUnit.Framework.Assert.AreEqual("rec(2).a", testModel.StepOutputs.Single().Variable);
            NUnit.Framework.Assert.AreEqual("", testModel.StepOutputs.Single().From);
            NUnit.Framework.Assert.AreEqual("", testModel.StepOutputs.Single().To);
            NUnit.Framework.Assert.AreEqual("", testModel.StepOutputs.Single().Value);
            NUnit.Framework.Assert.IsNotNull(serviceTestOutput.AddNewAction);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void MockSelected_GivenIsTrue_ShouldSetUpCorrectly()
        {
            //---------------Set up test pack-------------------
            var serviceTestOutput = new ServiceTestOutput("a", "a", "", "");
            var testModel = new ServiceTestStep(Guid.NewGuid(), typeof(DsfDecision).Name, new ObservableCollection<IServiceTestOutput>()
            { serviceTestOutput }, StepType.Mock);
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsTrue(testModel.MockSelected);
            //---------------Execute Test ----------------------
            testModel.MockSelected = true;
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(false, serviceTestOutput.IsBetweenCriteriaVisible);
            NUnit.Framework.Assert.AreEqual(true, serviceTestOutput.IsSinglematchCriteriaVisible);
            NUnit.Framework.Assert.AreEqual(true, serviceTestOutput.IsSearchCriteriaVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Nkosinathi Sangweni")]
        public void MockSelected_Given_IsSearchCriteriaEnabled()
        {
            //---------------Set up test pack-------------------
            var serviceTestOutput = new ServiceTestOutput("a", "a", "", "")
            {
                IsSearchCriteriaEnabled = false,
                IsSinglematchCriteriaVisible = false
            };
            var testModel = new ServiceTestStep(Guid.NewGuid(), typeof(DsfDecision).Name, new ObservableCollection<IServiceTestOutput>()
            { serviceTestOutput
            }, StepType.Mock);
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsTrue(testModel.MockSelected);
            //---------------Execute Test ----------------------
            testModel.MockSelected = true;
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.IsTrue(serviceTestOutput.IsSearchCriteriaEnabled);
            NUnit.Framework.Assert.IsTrue(serviceTestOutput.IsSinglematchCriteriaVisible);
            NUnit.Framework.Assert.IsFalse(serviceTestOutput.IsBetweenCriteriaVisible);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Pieter Terblanche")]
        public void ServiceTestStep_EmptyStepOutputs_ShouldNotSetTestInvalid()
        {
            var serviceTestStep = new ServiceTestStep(Guid.NewGuid(), "", new ObservableCollection<IServiceTestOutput>(), StepType.Assert)
            {
                StepOutputs = new ObservableCollection<IServiceTestOutput>()
            };
            NUnit.Framework.Assert.IsFalse(serviceTestStep.TestInvalid);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Pieter Terblanche")]
        public void AddNewRecordsetOutput_Given_Recordset_Adds_New_Test_Step()
        {
            //---------------Set up test pack-------------------            
            var serviceTestOutput = new ServiceTestOutput("[[person().name]]", "bob", "", "")
            {
                IsSearchCriteriaEnabled = false,
                IsSinglematchCriteriaVisible = false
            };
            var testModel = new ServiceTestStep(Guid.NewGuid(), typeof(DsfDecision).Name, new ObservableCollection<IServiceTestOutput>()
            { serviceTestOutput
            }, StepType.Mock);
            var testModelObject = new PrivateObject(testModel);
            //---------------Assert Precondition----------------
            var ExpectedCount = testModel.StepOutputs.Count + 1;
            NUnit.Framework.Assert.IsTrue(testModel.MockSelected);
            NUnit.Framework.Assert.AreEqual(1, testModel.StepOutputs.Count);
            //---------------Execute Test ----------------------           
            testModelObject.Invoke("AddNewRecordsetOutput", "[[person().name]]");
            var countAfter = testModel.StepOutputs.Count;
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(ExpectedCount, countAfter);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Pieter Terblanche")]
        public void AddNewRecordsetOutput_Sets_VariableName_Given_TestStep_Has_Empty_Variable()
        {
            //---------------Set up test pack-------------------            
            var serviceTestOutput = new ServiceTestOutput("", "", "", "")
            {
                IsSearchCriteriaEnabled = false,
                IsSinglematchCriteriaVisible = false
            };
            var testModel = new ServiceTestStep(Guid.NewGuid(), typeof(DsfDecision).Name, new ObservableCollection<IServiceTestOutput>()
            { serviceTestOutput
            }, StepType.Mock);
            var testModelObject = new PrivateObject(testModel);
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsTrue(testModel.MockSelected);
            //---------------Execute Test ----------------------           
            testModelObject.Invoke("AddNewRecordsetOutput", "[[Name]]");
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("[[Name]]", testModel.StepOutputs[0].Variable);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Pieter Terblanche")]
        public void AddNewRecordsetOutput_LastInput_IsNull_ShouldSet_Variable()
        {
            //---------------Set up test pack-------------------            
            var serviceTestOutput = new ServiceTestOutput("", "", "", "")
            {
                IsSearchCriteriaEnabled = false,
                IsSinglematchCriteriaVisible = false
            };
            var testModel = new ServiceTestStep(Guid.NewGuid(), typeof(DsfDecision).Name, new ObservableCollection<IServiceTestOutput>()
            { serviceTestOutput
            }, StepType.Mock);
            var testModelObject = new PrivateObject(testModel);
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsTrue(testModel.MockSelected);
            //---------------Execute Test ----------------------           
            testModelObject.Invoke("AddNewRecordsetOutput", "[[Person(1).Name]]");
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("[[Person(2).Name]]", testModel.StepOutputs[0].Variable);
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Pieter Terblanche")]
        public void AddNewRecordsetOutput_LastInput_IsNotNull_ShouldSet_Variable()
        {
            //---------------Set up test pack-------------------            
            var serviceTestOutput = new ServiceTestOutput("[[Person().Name]]", "bob", "", "")
            {
                IsSearchCriteriaEnabled = false,
                IsSinglematchCriteriaVisible = false
            };
            var testModel = new ServiceTestStep(Guid.NewGuid(), typeof(DsfDecision).Name, new ObservableCollection<IServiceTestOutput>()
            { serviceTestOutput
            }, StepType.Mock);
            var testModelObject = new PrivateObject(testModel);
            //---------------Assert Precondition----------------
            var ExpectedCount = testModel.StepOutputs.Count + 1;
            NUnit.Framework.Assert.IsTrue(testModel.MockSelected);
            NUnit.Framework.Assert.AreEqual(1, testModel.StepOutputs.Count);
            //---------------Execute Test ----------------------           
            testModelObject.Invoke("AddNewRecordsetOutput", "[[Person(1).Name]]");
            //---------------Test Result -----------------------
            var countAfter = testModel.StepOutputs.Count;
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(ExpectedCount, countAfter);
        }

        static ServiceTestStep CreateDecisionMock()
        {
            return new ServiceTestStep(Guid.NewGuid(), typeof(DsfDecision).Name, new ObservableCollection<IServiceTestOutput>(), StepType.Mock);
        }
    }


}
