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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Data;
using Dev2.Data.Interfaces.Enums;
using Dev2.DynamicServices;
using Dev2.Interfaces;
using Dev2.Web;
using NUnit.Framework;
using Moq;
using Warewolf.Storage;
using Warewolf.Storage.Interfaces;
using System.Security.Principal;
using Dev2.Runtime.ESB.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Tests
{
    [TestFixture]
    public class DataObjectTest
    {

        [Test]
        [Author("Travis Frisinger")]
        [Category("DsfDataObject_IsRemoteWorkflow")]
        public void DsfDataOBject_IsRemoteWorkflow_WhenOverrideNotSet_ExpectTrue()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid());
            dataObject.EnvironmentID = Guid.NewGuid();

            //------------Execute Test---------------------------
            var result = dataObject.IsRemoteWorkflow();

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void PopEnvironment_GivenHasNoEnvironments_ShouldNotSetEnvironment()
        {
            //---------------Set up test pack-------------------
            var mock = new Mock<IExecutionEnvironment>();
            mock.SetupAllProperties();
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid());
            dataObject.Environment = mock.Object;
            var privateObject = new PrivateObject(dataObject);
            var field = privateObject.GetField("_environments", BindingFlags.Instance | BindingFlags.NonPublic) as ConcurrentStack<IExecutionEnvironment>;
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsNotNull(dataObject.Environment);
            NUnit.Framework.Assert.IsNotNull(field);
            NUnit.Framework.Assert.AreEqual(0, field.Count);
            //---------------Execute Test ----------------------
            dataObject.PopEnvironment();
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(dataObject.Environment, mock.Object);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void PopEnvironment_GivenNoEnvironments_ShouldSetEnvironment()
        {
            //---------------Set up test pack-------------------
            var mock = new Mock<IExecutionEnvironment>();
            mock.SetupAllProperties();
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid());
            dataObject.Environment = mock.Object;
            var privateObject = new PrivateObject(dataObject);
            var field = privateObject.GetField("_environments", BindingFlags.Instance | BindingFlags.NonPublic) as ConcurrentStack<IExecutionEnvironment>;
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsNotNull(dataObject.Environment);
            NUnit.Framework.Assert.IsNotNull(field);
            NUnit.Framework.Assert.AreEqual(0, field.Count);
            //---------------Execute Test ----------------------
            dataObject.PushEnvironment(new ExecutionEnvironment());
            dataObject.PopEnvironment();
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual(dataObject.Environment, mock.Object);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DsfDataObject_IsRemoteWorkflow")]
        public void DsfDataOBject_IsRemoteWorkflow_WhenOverrideSet_ExpectFalse()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid());
            dataObject.EnvironmentID = Guid.NewGuid();
            dataObject.IsRemoteInvokeOverridden = true;

            //------------Execute Test---------------------------
            var result = dataObject.IsRemoteWorkflow();

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DsfDataObject_IsRemoteWorkflow")]
        public void DsfDataOBject_IsRemoteWorkflow_WhenOverrideSetAndEmptyGuid_ExpectFalse()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid());
            dataObject.EnvironmentID = Guid.Empty;
            dataObject.IsRemoteInvokeOverridden = true;

            //------------Execute Test---------------------------
            var result = dataObject.IsRemoteWorkflow();

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DsfDataObject_IsRemoteWorkflow")]
        public void DsfDataOBject_IsRemoteWorkflow_WhenOverrideNotSetAndEmptyGuid_ExpectFalse()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid());
            dataObject.EnvironmentID = Guid.Empty;

            //------------Execute Test---------------------------
            var result = dataObject.IsRemoteWorkflow();

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }


        [Test]
        [Author("Travis Frisinger")]
        [Category("DsfDataObject_RawPayload")]
        public void DsfDataObject_RawPayload_WhenNull_ExpectEmptyString()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid());

            //------------Execute Test---------------------------
            var result = dataObject.RawPayload;

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(string.Empty, result.ToString(), "RawPayload did not return and empty string");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("DsfDataObject_NestingLevel")]
        public void DsfDataObject_NestingLevel_Get_Set_ExpectCorrectGetSet()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid());
            dataObject.ForEachNestingLevel = 3;
            NUnit.Framework.Assert.AreEqual(dataObject.ForEachNestingLevel, 3);

        }
        [Test]
        [Author("Travis Frisinger")]
        [Category("DsfDataObjectz_RawPayload")]
        public void DsfDataObjectz_RawPayload_WhenNotNull_ExpectRawPayload()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid(), "foo");

            //------------Execute Test---------------------------
            var result = dataObject.RawPayload.ToString();

            //------------Assert Results-------------------------
            NUnit.Framework.StringAssert.Contains(result, "foo");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DsfDataObject_Clone")]
        public void DsfDataObject_Clone_NormalClone_FullDuplicationForProperties()
        {
            var executingUser = new Mock<IPrincipal>().Object;
            //------------Setup for test--------------------------
            var wfInstanceID = Guid.NewGuid();
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid(), "<x>1</x>");
            dataObject.BookmarkExecutionCallbackID = Guid.NewGuid();
            dataObject.CurrentBookmarkName = "def";
            dataObject.DataList = new StringBuilder("<x/>");
            dataObject.DataListID = Guid.NewGuid();
            dataObject.DatalistInMergeDepth = enTranslationDepth.Data;
            dataObject.DatalistInMergeID = Guid.NewGuid();
            dataObject.DatalistInMergeType = enDataListMergeTypes.Union;
            dataObject.DatalistOutMergeDepth = enTranslationDepth.Data;
            dataObject.DatalistOutMergeFrequency = DataListMergeFrequency.Always;
            dataObject.DatalistOutMergeID = Guid.NewGuid();
            dataObject.DatalistOutMergeType = enDataListMergeTypes.Union;
            dataObject.DebugSessionID = Guid.NewGuid();
            dataObject.EnvironmentID = Guid.NewGuid();
            dataObject.VersionNumber = 1;
            dataObject.ExecutionCallbackID = Guid.NewGuid();
            dataObject.ExecutionOrigin = ExecutionOrigin.Debug;
            dataObject.ExecutingUser = executingUser;
            dataObject.ExecutionOriginDescription = "xxx";
            dataObject.ForceDeleteAtNextNativeActivityCleanup = true;
            dataObject.IsDataListScoped = false;
            dataObject.IsDebug = true;
            dataObject.IsFromWebServer = true;
            dataObject.IsOnDemandSimulation = true;
            dataObject.NumberOfSteps = 2;
            dataObject.OriginalInstanceID = Guid.NewGuid();
            dataObject.ParentInstanceID = "1211";
            dataObject.ParentServiceName = "xxx";
            dataObject.ParentThreadID = 2;
            dataObject.ParentWorkflowInstanceId = "1233";
            dataObject.RawPayload = new StringBuilder("<raw>a</raw>");
            dataObject.RemoteDebugItems = new List<IDebugState>();
            dataObject.RemoteInvoke = false;
            dataObject.RemoteInvokeResultShape = new StringBuilder("<x/>");
            dataObject.RemoteInvokerID = "999";
            dataObject.RemoteServiceType = "NA";
            dataObject.ResourceID = Guid.NewGuid();
            dataObject.ReturnType = EmitionTypes.XML;
            dataObject.ServerID = Guid.NewGuid();
            dataObject.ServiceName = "xxx";
            dataObject.WorkflowInstanceId = wfInstanceID;
            dataObject.WorkflowResumeable = false;
            dataObject.ParentID = Guid.NewGuid();
            dataObject.WorkspaceID = Guid.NewGuid();
            dataObject.ClientID = Guid.NewGuid();
            dataObject.RunWorkflowAsync = true;
            dataObject.IsDebugNested = true;
            dataObject.ForEachNestingLevel = 3;
            dataObject.StopExecution = false;
            dataObject.IsServiceTestExecution = true;
            dataObject.TestName = "Test 1";
            dataObject.IsDebugFromWeb = true;
            dataObject.SourceResourceID = Guid.NewGuid();
            dataObject.IsSubExecution = true;
            dataObject.ServiceTest = new ServiceTestModelTO { TestName = "Test Mock" };
            dataObject.StateNotifier = new Mock<IStateNotifier>().Object;
            dataObject.Settings = new Dev2WorkflowSettingsTO { KeepLogsForDays = 999 };
            var threadsToDispose = new Dictionary<int, List<Guid>>();
            var guidList = new List<Guid> { Guid.NewGuid() };
            threadsToDispose.Add(3, guidList);
            dataObject.ThreadsToDispose = threadsToDispose;
            dataObject.AuthCache = new ConcurrentDictionary<(IPrincipal, Dev2.Common.Interfaces.Enums.AuthorizationContext, string), bool>();

            //------------Execute Test---------------------------
            var clonedObject = dataObject.Clone();

            //------------Assert Results-------------------------

            // check counts, then check values
            var properties = typeof(IDSFDataObject).GetProperties();
            NUnit.Framework.Assert.AreEqual(72, properties.Length);

            // now check each value to ensure it transfered
            NUnit.Framework.Assert.AreEqual(dataObject.BookmarkExecutionCallbackID, clonedObject.BookmarkExecutionCallbackID);
            NUnit.Framework.Assert.AreEqual(dataObject.CurrentBookmarkName, clonedObject.CurrentBookmarkName);
            NUnit.Framework.Assert.AreEqual(dataObject.DataList, clonedObject.DataList);
            NUnit.Framework.Assert.AreEqual(dataObject.DataListID, clonedObject.DataListID);
            NUnit.Framework.Assert.AreEqual(dataObject.DatalistInMergeDepth, clonedObject.DatalistInMergeDepth);
            NUnit.Framework.Assert.AreEqual(dataObject.DatalistInMergeID, clonedObject.DatalistInMergeID);
            NUnit.Framework.Assert.AreEqual(dataObject.DatalistInMergeType, clonedObject.DatalistInMergeType);
            NUnit.Framework.Assert.AreEqual(dataObject.DatalistOutMergeDepth, clonedObject.DatalistOutMergeDepth);
            NUnit.Framework.Assert.AreEqual(dataObject.DatalistOutMergeFrequency, clonedObject.DatalistOutMergeFrequency);
            NUnit.Framework.Assert.AreEqual(dataObject.DatalistOutMergeID, clonedObject.DatalistOutMergeID);
            NUnit.Framework.Assert.AreEqual(dataObject.DatalistOutMergeType, clonedObject.DatalistOutMergeType);
            NUnit.Framework.Assert.AreEqual(dataObject.DebugSessionID, clonedObject.DebugSessionID);
            NUnit.Framework.Assert.AreEqual(dataObject.EnvironmentID, clonedObject.EnvironmentID);
            NUnit.Framework.Assert.AreEqual(dataObject.VersionNumber, clonedObject.VersionNumber);
            NUnit.Framework.Assert.AreEqual(dataObject.ExecutingUser, clonedObject.ExecutingUser);
            NUnit.Framework.Assert.AreEqual(dataObject.ExecutionCallbackID, clonedObject.ExecutionCallbackID);
            NUnit.Framework.Assert.AreEqual(dataObject.ExecutionOrigin, clonedObject.ExecutionOrigin);
            NUnit.Framework.Assert.AreEqual(dataObject.ExecutionOriginDescription, clonedObject.ExecutionOriginDescription);
            NUnit.Framework.Assert.AreEqual(dataObject.ForceDeleteAtNextNativeActivityCleanup, clonedObject.ForceDeleteAtNextNativeActivityCleanup);
            NUnit.Framework.Assert.AreEqual(dataObject.IsDataListScoped, clonedObject.IsDataListScoped);
            NUnit.Framework.Assert.AreEqual(dataObject.IsDebug, clonedObject.IsDebug);
            NUnit.Framework.Assert.AreEqual(dataObject.IsFromWebServer, clonedObject.IsFromWebServer);
            NUnit.Framework.Assert.AreEqual(dataObject.IsOnDemandSimulation, clonedObject.IsOnDemandSimulation);
            NUnit.Framework.Assert.AreEqual(dataObject.IsRemoteInvoke, clonedObject.IsRemoteInvoke);
            NUnit.Framework.Assert.AreEqual(dataObject.IsRemoteInvokeOverridden, clonedObject.IsRemoteInvokeOverridden);
            NUnit.Framework.Assert.AreEqual(dataObject.NumberOfSteps, clonedObject.NumberOfSteps);
            NUnit.Framework.Assert.AreEqual(dataObject.OriginalInstanceID, clonedObject.OriginalInstanceID);
            NUnit.Framework.Assert.AreEqual(dataObject.ParentInstanceID, clonedObject.ParentInstanceID);
            NUnit.Framework.Assert.AreEqual(dataObject.ParentServiceName, clonedObject.ParentServiceName);
            NUnit.Framework.Assert.AreEqual(dataObject.ParentThreadID, clonedObject.ParentThreadID);
            NUnit.Framework.Assert.AreEqual(dataObject.ParentWorkflowInstanceId, clonedObject.ParentWorkflowInstanceId);
            NUnit.Framework.Assert.AreEqual(dataObject.RawPayload, clonedObject.RawPayload);
            NUnit.Framework.Assert.AreEqual(dataObject.RemoteDebugItems, clonedObject.RemoteDebugItems);
            NUnit.Framework.Assert.AreEqual(dataObject.RemoteInvoke, clonedObject.RemoteInvoke);
            NUnit.Framework.Assert.AreEqual(dataObject.RemoteNonDebugInvoke, clonedObject.RemoteNonDebugInvoke);
            NUnit.Framework.Assert.AreEqual(dataObject.RemoteInvokeResultShape, clonedObject.RemoteInvokeResultShape);
            NUnit.Framework.Assert.AreEqual(dataObject.RemoteInvokerID, clonedObject.RemoteInvokerID);
            NUnit.Framework.Assert.AreEqual(dataObject.RemoteServiceType, clonedObject.RemoteServiceType);
            NUnit.Framework.Assert.AreEqual(dataObject.ResourceID, clonedObject.ResourceID);
            NUnit.Framework.Assert.AreEqual(dataObject.ReturnType, clonedObject.ReturnType);
            NUnit.Framework.Assert.AreEqual(dataObject.ServerID, clonedObject.ServerID);
            NUnit.Framework.Assert.AreEqual(dataObject.ClientID, clonedObject.ClientID);
            NUnit.Framework.Assert.AreEqual(dataObject.ServiceName, clonedObject.ServiceName);
            NUnit.Framework.Assert.AreEqual(dataObject.WorkflowInstanceId, clonedObject.WorkflowInstanceId);
            NUnit.Framework.Assert.AreEqual(dataObject.WorkflowResumeable, clonedObject.WorkflowResumeable);
            NUnit.Framework.Assert.AreEqual(dataObject.WorkspaceID, clonedObject.WorkspaceID);
            NUnit.Framework.Assert.AreEqual(dataObject.ThreadsToDispose, clonedObject.ThreadsToDispose);
            NUnit.Framework.Assert.AreEqual(dataObject.ParentID, clonedObject.ParentID);
            NUnit.Framework.Assert.AreEqual(dataObject.RunWorkflowAsync, clonedObject.RunWorkflowAsync);
            NUnit.Framework.Assert.AreEqual(dataObject.IsDebugNested, clonedObject.IsDebugNested);
            NUnit.Framework.Assert.AreEqual(dataObject.ForEachNestingLevel, clonedObject.ForEachNestingLevel);
            NUnit.Framework.Assert.AreEqual(dataObject.StopExecution, clonedObject.StopExecution);
            NUnit.Framework.Assert.AreEqual(dataObject.SourceResourceID, clonedObject.SourceResourceID);
            NUnit.Framework.Assert.AreEqual(dataObject.TestName, clonedObject.TestName);
            NUnit.Framework.Assert.AreEqual(dataObject.IsServiceTestExecution, clonedObject.IsServiceTestExecution);
            NUnit.Framework.Assert.AreEqual(dataObject.IsDebugFromWeb, clonedObject.IsDebugFromWeb);
            NUnit.Framework.Assert.AreNotEqual(dataObject.ServiceTest, clonedObject.ServiceTest);
            NUnit.Framework.Assert.AreEqual(dataObject.ServiceTest.TestName, clonedObject.ServiceTest.TestName);
            NUnit.Framework.Assert.AreEqual(dataObject.IsSubExecution, clonedObject.IsSubExecution);
            NUnit.Framework.Assert.AreEqual(dataObject.WebUrl, clonedObject.WebUrl);
            NUnit.Framework.Assert.AreEqual(dataObject.QueryString, clonedObject.QueryString);
            NUnit.Framework.Assert.AreEqual(dataObject.ExecutingUser, clonedObject.ExecutingUser);
            NUnit.Framework.Assert.AreEqual(dataObject.StateNotifier, clonedObject.StateNotifier);
            NUnit.Framework.Assert.AreNotEqual(dataObject.Settings, clonedObject.Settings);
            NUnit.Framework.Assert.AreEqual(dataObject.Settings.KeepLogsForDays, clonedObject.Settings.KeepLogsForDays);
            NUnit.Framework.Assert.AreNotEqual(dataObject.AuthCache, clonedObject.AuthCache);
            NUnit.Framework.Assert.AreEqual(dataObject.ExecutionException, clonedObject.ExecutionException);
        }

        #region Debug Mode Test

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataObject_IsDebugMode")]
        public void DataObject_IsDebugMode_IsDebugIsTrueAndRunWorkflowAsyncIsTrue_IsDebugModeIsFalse()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid(), "<x>1</x>");
            dataObject.RunWorkflowAsync = true;
            dataObject.IsDebug = true;
            //------------Execute Test---------------------------
            var isDebug = dataObject.IsDebugMode();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(isDebug);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("DataObject_IsDebugMode")]
        public void DataObject_IsDebugMode_IsDebugIsTrueAndRunWorkflowAsyncIsTrue_RemoteInvokeNotDebug()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid(), "<x>1</x>");
            dataObject.RunWorkflowAsync = true;
            dataObject.IsDebug = true;
            dataObject.RemoteNonDebugInvoke = true;
            //------------Execute Test---------------------------
            var isDebug = dataObject.IsDebugMode();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(isDebug);
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("DataObject_IsDebugMode")]
        public void DataObject_IsDebugMode_IsDebugIsTrueAndRunWorkflowAsyncIsTrue_RemoteInvokeNotDebugRemote()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid(), "<x>1</x>");
            dataObject.RunWorkflowAsync = false;
            dataObject.IsDebug = false;
            dataObject.RemoteNonDebugInvoke = true;
            dataObject.RemoteInvoke = true;
            //------------Execute Test---------------------------
            var isDebug = dataObject.IsDebugMode();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(isDebug);
        }


        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataObject_IsDebugMode")]
        public void DataObject_IsDebugMode_IsDebugIsTrueAndRunWorkflowAsyncIsFalse_IsDebugModeIsTrue()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid(), "<x>1</x>");
            dataObject.RunWorkflowAsync = false;
            dataObject.IsDebug = true;
            //------------Execute Test---------------------------
            var isDebug = dataObject.IsDebugMode();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(isDebug);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataObject_IsDebugMode")]
        public void DataObject_IsDebugMode_RemoteInvokeIsTrueAndRunWorkflowAsyncIsTrue_IsDebugModeIsFalse()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid(), "<x>1</x>");
            dataObject.RunWorkflowAsync = true;
            dataObject.RemoteInvoke = true;
            //------------Execute Test---------------------------
            var isDebug = dataObject.IsDebugMode();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(isDebug);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataObject_IsDebugMode")]
        public void DataObject_IsDebugMode_RemoteInvokeIsTrueAndRunWorkflowAsyncIsFalse_IsDebugModeIsTrue()
        {
            //------------Setup for test--------------------------
            IDSFDataObject dataObject = new DsfDataObject(string.Empty, Guid.NewGuid(), "<x>1</x>");
            dataObject.RunWorkflowAsync = false;
            dataObject.RemoteInvoke = true;
            //------------Execute Test---------------------------
            var isDebug = dataObject.IsDebugMode();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(isDebug);
        }

        #endregion

        #region Constructor Test

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataObject_Ctor")]
        public void DataObject_Ctor_WhenXmlStringContainsAllButDataMergePropertiesSet_ExpectParseAndSet()
        {
            //------------Setup for test--------------------------
            var debugID = Guid.NewGuid();
            var envID = Guid.NewGuid();
            var exeID = Guid.NewGuid();
            var bookmarkID = Guid.NewGuid();
            var parentID = Guid.NewGuid();
            var instID = Guid.NewGuid();
            var versionNumber = "2";

            var xmlStr = "<Payload>" +
                         "<IsDebug>true</IsDebug>" +
                         "<DebugSessionID>" + debugID + "</DebugSessionID>" +
                         "<EnvironmentID>" + envID + "</EnvironmentID>" +
                         "<VersionNumber>" + versionNumber + "</VersionNumber>" +
                         "<IsOnDemandSimulation>true</IsOnDemandSimulation>" +
                         "<ParentServiceName>TestParentService</ParentServiceName>" +
                         "<ExecutionCallbackID>" + exeID + "</ExecutionCallbackID>" +
                         "<BookmarkExecutionCallbackID>" + bookmarkID + "</BookmarkExecutionCallbackID>" +
                         "<ParentInstanceID>" + parentID + "</ParentInstanceID>" +
                         "<NumberOfSteps>5</NumberOfSteps>" +
                         "<CurrentBookmarkName>MyBookmark</CurrentBookmarkName>" +
                         "<WorkflowInstanceId>" + instID + "</WorkflowInstanceId>" +
                         "<IsDataListScoped>true</IsDataListScoped>" +
                         "<Service>MyTestService</Service>" +
                         "</Payload>";


            //------------Execute Test---------------------------
            var dataObjct = new DsfDataObject(xmlStr, Guid.NewGuid(), "<x>1</x>");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(dataObjct.IsDebug);
            NUnit.Framework.StringAssert.Contains(dataObjct.DebugSessionID.ToString(), debugID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.EnvironmentID.ToString(), envID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.VersionNumber.ToString(), versionNumber);
            NUnit.Framework.Assert.IsTrue(dataObjct.IsOnDemandSimulation);
            NUnit.Framework.StringAssert.Contains(dataObjct.ParentServiceName, "TestParentService");
            NUnit.Framework.StringAssert.Contains(dataObjct.ExecutionCallbackID.ToString(), exeID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.BookmarkExecutionCallbackID.ToString(), bookmarkID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.ParentInstanceID, parentID.ToString());
            NUnit.Framework.Assert.AreEqual(5, dataObjct.NumberOfSteps, "Wrong number of steps");
            NUnit.Framework.StringAssert.Contains(dataObjct.CurrentBookmarkName, "MyBookmark");
            NUnit.Framework.StringAssert.Contains(dataObjct.WorkflowInstanceId.ToString(), instID.ToString());
            NUnit.Framework.Assert.IsTrue(dataObjct.IsDataListScoped);
            NUnit.Framework.StringAssert.Contains(dataObjct.ServiceName, "MyTestService");
            NUnit.Framework.StringAssert.Contains(dataObjct.RawPayload.ToString(), xmlStr);

            // Default Data Merge Checks
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeID.ToString(), Guid.Empty.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeType.ToString(), enDataListMergeTypes.Intersection.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeDepth.ToString(), Common.Interfaces.DataList.Contract.enTranslationDepth.Data_With_Blank_OverWrite.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeFrequency.ToString(), DataListMergeFrequency.OnCompletion.ToString());

            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeID.ToString(), Guid.Empty.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeType.ToString(), enDataListMergeTypes.Intersection.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeDepth.ToString(), Common.Interfaces.DataList.Contract.enTranslationDepth.Data_With_Blank_OverWrite.ToString());

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataObject_Ctor")]
        public void DataObject_Ctor_WhenXmlStringgDoesNotHaveIsDebugModeSetButHasBDSDebugModeSet_ExpectParseAndSetAndIsDebugStillTrue()
        {
            //------------Setup for test--------------------------
            var debugID = Guid.NewGuid();
            var envID = Guid.NewGuid();
            var exeID = Guid.NewGuid();
            var bookmarkID = Guid.NewGuid();
            var parentID = Guid.NewGuid();
            var instID = Guid.NewGuid();
            var versionNumber = "1";

            var xmlStr = "<Payload>" +
                         "<BDSDebugMode>true</BDSDebugMode>" +
                         "<DebugSessionID>" + debugID + "</DebugSessionID>" +
                         "<EnvironmentID>" + envID + "</EnvironmentID>" +
                         "<VersionNumber>" + versionNumber + "</VersionNumber>" +
                         "<IsOnDemandSimulation>true</IsOnDemandSimulation>" +
                         "<ParentServiceName>TestParentService</ParentServiceName>" +
                         "<ExecutionCallbackID>" + exeID + "</ExecutionCallbackID>" +
                         "<BookmarkExecutionCallbackID>" + bookmarkID + "</BookmarkExecutionCallbackID>" +
                         "<ParentInstanceID>" + parentID + "</ParentInstanceID>" +
                         "<NumberOfSteps>5</NumberOfSteps>" +
                         "<CurrentBookmarkName>MyBookmark</CurrentBookmarkName>" +
                         "<WorkflowInstanceId>" + instID + "</WorkflowInstanceId>" +
                         "<IsDataListScoped>true</IsDataListScoped>" +
                         "<Service>MyTestService</Service>" +
                         "</Payload>";


            //------------Execute Test---------------------------
            var dataObjct = new DsfDataObject(xmlStr, Guid.NewGuid(), "<x>1</x>");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(dataObjct.IsDebug);
            NUnit.Framework.StringAssert.Contains(dataObjct.DebugSessionID.ToString(), debugID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.EnvironmentID.ToString(), envID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.VersionNumber.ToString(), versionNumber);
            NUnit.Framework.Assert.IsTrue(dataObjct.IsOnDemandSimulation);
            NUnit.Framework.StringAssert.Contains(dataObjct.ParentServiceName, "TestParentService");
            NUnit.Framework.StringAssert.Contains(dataObjct.ExecutionCallbackID.ToString(), exeID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.BookmarkExecutionCallbackID.ToString(), bookmarkID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.ParentInstanceID, parentID.ToString());
            NUnit.Framework.Assert.AreEqual(5, dataObjct.NumberOfSteps, "Wrong number of steps");
            NUnit.Framework.StringAssert.Contains(dataObjct.CurrentBookmarkName, "MyBookmark");
            NUnit.Framework.StringAssert.Contains(dataObjct.WorkflowInstanceId.ToString(), instID.ToString());
            NUnit.Framework.Assert.IsTrue(dataObjct.IsDataListScoped);
            NUnit.Framework.StringAssert.Contains(dataObjct.ServiceName, "MyTestService");
            NUnit.Framework.StringAssert.Contains(dataObjct.RawPayload.ToString(), xmlStr);

            // Default Data Merge Checks
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeID.ToString(), Guid.Empty.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeType.ToString(), enDataListMergeTypes.Intersection.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeDepth.ToString(), Common.Interfaces.DataList.Contract.enTranslationDepth.Data_With_Blank_OverWrite.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeFrequency.ToString(), DataListMergeFrequency.OnCompletion.ToString());

            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeID.ToString(), Guid.Empty.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeType.ToString(), enDataListMergeTypes.Intersection.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeDepth.ToString(), Common.Interfaces.DataList.Contract.enTranslationDepth.Data_With_Blank_OverWrite.ToString());

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataObject_Ctor")]
        public void DataObject_Ctor_WhenXmlStringDoesNotHaveBookmarkExecutionCallbackIDSet_ExpectParseAndSet()
        {
            //------------Setup for test--------------------------
            var debugID = Guid.NewGuid();
            var envID = Guid.NewGuid();
            var exeID = Guid.NewGuid();
            var parentID = Guid.NewGuid();
            var instID = Guid.NewGuid();
            var versionNumber = "4";

            var xmlStr = "<Payload>" +
                         "<IsDebug>true</IsDebug>" +
                         "<DebugSessionID>" + debugID + "</DebugSessionID>" +
                         "<EnvironmentID>" + envID + "</EnvironmentID>" +
                         "<VersionNumber>" + versionNumber + "</VersionNumber>" +
                         "<IsOnDemandSimulation>true</IsOnDemandSimulation>" +
                         "<ParentServiceName>TestParentService</ParentServiceName>" +
                         "<ExecutionCallbackID>" + exeID + "</ExecutionCallbackID>" +
                         "<ParentInstanceID>" + parentID + "</ParentInstanceID>" +
                         "<NumberOfSteps>5</NumberOfSteps>" +
                         "<CurrentBookmarkName>MyBookmark</CurrentBookmarkName>" +
                         "<WorkflowInstanceId>" + instID + "</WorkflowInstanceId>" +
                         "<IsDataListScoped>true</IsDataListScoped>" +
                         "<Service>MyTestService</Service>" +
                         "</Payload>";


            //------------Execute Test---------------------------
            var dataObjct = new DsfDataObject(xmlStr, Guid.NewGuid(), "<x>1</x>");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(dataObjct.IsDebug);
            NUnit.Framework.StringAssert.Contains(dataObjct.DebugSessionID.ToString(), debugID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.EnvironmentID.ToString(), envID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.VersionNumber.ToString(), versionNumber);
            NUnit.Framework.Assert.IsTrue(dataObjct.IsOnDemandSimulation);
            NUnit.Framework.StringAssert.Contains(dataObjct.ParentServiceName, "TestParentService");
            NUnit.Framework.StringAssert.Contains(dataObjct.ExecutionCallbackID.ToString(), exeID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.BookmarkExecutionCallbackID.ToString(), exeID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.ParentInstanceID, parentID.ToString());
            NUnit.Framework.Assert.AreEqual(5, dataObjct.NumberOfSteps, "Wrong number of steps");
            NUnit.Framework.StringAssert.Contains(dataObjct.CurrentBookmarkName, "MyBookmark");
            NUnit.Framework.StringAssert.Contains(dataObjct.WorkflowInstanceId.ToString(), instID.ToString());
            NUnit.Framework.Assert.IsTrue(dataObjct.IsDataListScoped);
            NUnit.Framework.StringAssert.Contains(dataObjct.ServiceName, "MyTestService");
            NUnit.Framework.StringAssert.Contains(dataObjct.RawPayload.ToString(), xmlStr);

            // Default Data Merge Checks
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeID.ToString(), Guid.Empty.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeType.ToString(), enDataListMergeTypes.Intersection.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeDepth.ToString(), Common.Interfaces.DataList.Contract.enTranslationDepth.Data_With_Blank_OverWrite.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeFrequency.ToString(), DataListMergeFrequency.OnCompletion.ToString());

            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeID.ToString(), Guid.Empty.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeType.ToString(), enDataListMergeTypes.Intersection.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeDepth.ToString(), Common.Interfaces.DataList.Contract.enTranslationDepth.Data_With_Blank_OverWrite.ToString());

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DataObject_Ctor")]
        public void DataObject_Ctor_WhenXmlStringContainsAllIncludingDataMergePropertiesSet_ExpectParseAndSet()
        {
            //------------Setup for test--------------------------
            var debugID = Guid.NewGuid();
            var envID = Guid.NewGuid();
            var exeID = Guid.NewGuid();
            var bookmarkID = Guid.NewGuid();
            var parentID = Guid.NewGuid();
            var instID = Guid.NewGuid();
            var mergeIDOut = Guid.NewGuid();
            var mergeIDIn = Guid.NewGuid();
            var versionNumber = "3";

            var xmlStr = "<Payload>" +
                         "<IsDebug>true</IsDebug>" +
                         "<DebugSessionID>" + debugID + "</DebugSessionID>" +
                         "<EnvironmentID>" + envID + "</EnvironmentID>" +
                         "<VersionNumber>" + versionNumber + "</VersionNumber>" +
                         "<IsOnDemandSimulation>true</IsOnDemandSimulation>" +
                         "<ParentServiceName>TestParentService</ParentServiceName>" +
                         "<ExecutionCallbackID>" + exeID + "</ExecutionCallbackID>" +
                         "<BookmarkExecutionCallbackID>" + bookmarkID + "</BookmarkExecutionCallbackID>" +
                         "<ParentInstanceID>" + parentID + "</ParentInstanceID>" +
                         "<NumberOfSteps>5</NumberOfSteps>" +
                         "<CurrentBookmarkName>MyBookmark</CurrentBookmarkName>" +
                         "<WorkflowInstanceId>" + instID + "</WorkflowInstanceId>" +
                         "<IsDataListScoped>true</IsDataListScoped>" +
                         "<Service>MyTestService</Service>" +
                         "<DatalistOutMergeID>" + mergeIDOut + "</DatalistOutMergeID>" +
                         "<DatalistOutMergeType>Union</DatalistOutMergeType>" +
                         "<DatalistOutMergeDepth>Data</DatalistOutMergeDepth>" +
                         "<DatalistOutMergeFrequency>Never</DatalistOutMergeFrequency>" +
                         "<DatalistInMergeID>" + mergeIDIn + "</DatalistInMergeID>" +
                         "<DatalistInMergeType>Union</DatalistInMergeType>" +
                         "<DatalistInMergeDepth>Data</DatalistInMergeDepth>" +
                         "</Payload>";


            //------------Execute Test---------------------------
            var dataObjct = new DsfDataObject(xmlStr, Guid.NewGuid(), "<x>1</x>");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(dataObjct.IsDebug);
            NUnit.Framework.StringAssert.Contains(dataObjct.DebugSessionID.ToString(), debugID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.EnvironmentID.ToString(), envID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.VersionNumber.ToString(), versionNumber);
            NUnit.Framework.Assert.IsTrue(dataObjct.IsOnDemandSimulation);
            NUnit.Framework.StringAssert.Contains(dataObjct.ParentServiceName, "TestParentService");
            NUnit.Framework.StringAssert.Contains(dataObjct.ExecutionCallbackID.ToString(), exeID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.BookmarkExecutionCallbackID.ToString(), bookmarkID.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.ParentInstanceID, parentID.ToString());
            NUnit.Framework.Assert.AreEqual(5, dataObjct.NumberOfSteps, "Wrong number of steps");
            NUnit.Framework.StringAssert.Contains(dataObjct.CurrentBookmarkName, "MyBookmark");
            NUnit.Framework.StringAssert.Contains(dataObjct.WorkflowInstanceId.ToString(), instID.ToString());
            NUnit.Framework.Assert.IsTrue(dataObjct.IsDataListScoped);
            NUnit.Framework.StringAssert.Contains(dataObjct.ServiceName, "MyTestService");
            NUnit.Framework.StringAssert.Contains(dataObjct.RawPayload.ToString(), xmlStr);

            // Data Merge Checks
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeID.ToString(), mergeIDOut.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeType.ToString(), enDataListMergeTypes.Union.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeDepth.ToString(), Common.Interfaces.DataList.Contract.enTranslationDepth.Data.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistOutMergeFrequency.ToString(), DataListMergeFrequency.OnCompletion.ToString());

            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeID.ToString(), mergeIDIn.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeType.ToString(), enDataListMergeTypes.Union.ToString());
            NUnit.Framework.StringAssert.Contains(dataObjct.DatalistInMergeDepth.ToString(), Common.Interfaces.DataList.Contract.enTranslationDepth.Data.ToString());

        }

        #endregion
    }
}
