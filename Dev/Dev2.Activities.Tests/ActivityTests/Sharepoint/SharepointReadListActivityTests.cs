using System;
using System.Collections.Generic;
using System.Linq;
using ActivityUnitTests;
using Dev2.Activities.Sharepoint;
using Dev2.Common.Interfaces;
using Dev2.Common.State;
using Dev2.Communication;
using Dev2.Data.ServiceModel;
using Dev2.DynamicServices;
using Dev2.Runtime.Interfaces;
using Dev2.TO;
using Dev2.Utilities;
using NUnit.Framework;
using Moq;
using static Dev2.Tests.Activities.ActivityTests.Sharepoint.SharepointCopyFileActivityTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Tests.Activities.ActivityTests.Sharepoint
{
    [TestFixture]
    [SetUpFixture]
    public class SharepointReadListActivityTests : BaseActivityUnitTest
    {
        SharepointReadListActivity CreateActivity()
        {
            return new SharepointReadListActivity();
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("SharepointReadListActivity_Construct")]
        public void SharepointReadListActivity_Construct_GivenInstance_ShouldNotBeNull()
        {
            //------------Setup for test--------------------------
            var sharepointReadListActivity = CreateActivity();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(sharepointReadListActivity);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("SharepointReadList_Execute")]
        public void SharepointSource_DoesNotExist_OnResourceCatalog_ShouldSetSharepointSource_ToGuidEmpty()
        {
            //------------Setup for test--------------------------
            const string activityName = "SharepointReadList";
            var resourceId = Guid.NewGuid();
            var sharepointReadListActivity = new SharepointReadListActivity
            {
                DisplayName = activityName,
                SharepointServerResourceId = resourceId,
                ReadListItems = new List<SharepointReadListTo>(),
                FilterCriteria = new List<SharepointSearchTo>(),
                RequireAllCriteriaToMatch = true,
                SharepointUtils = new SharepointUtils()
            };

            var dataObj = new DsfDataObject(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>());

            var resourceCatalog = new Mock<IResourceCatalog>();
            var mockSharepointSource = new Mock<SharepointSource>();

            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(sharepointReadListActivity);
            privateObject.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------
            privateObject.Invoke("ExecuteTool", dataObj, 0);

            NUnit.Framework.Assert.AreEqual(resourceId, sharepointReadListActivity.SharepointServerResourceId);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("SharepointReadList_Execute")]
        public void SharepointSource_Exists_OnResourceCatalog_BlankRecordSet()
        {
            //------------Setup for test--------------------------
            const string activityName = "SharepointReadList";
            var resourceId = Guid.NewGuid();
            var sharepointReadListActivity = new SharepointReadListActivity
            {
                DisplayName = activityName,
                SharepointServerResourceId = resourceId,
                ReadListItems = new List<SharepointReadListTo>(),
                FilterCriteria = new List<SharepointSearchTo>(),
                RequireAllCriteriaToMatch = true,
                SharepointUtils = new SharepointUtils()
            };

            var dataObj = new DsfDataObject("", Guid.NewGuid(), "");

            var resourceCatalog = new Mock<IResourceCatalog>();

            var mockSharepointHelper = new Mock<ISharepointHelper>();
            mockSharepointHelper.Setup(helper => helper.CopyFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns("Success");

            var mockSharepointSource = new MockSharepointSource
            {
                MockSharepointHelper = mockSharepointHelper.Object
            };

            resourceCatalog.Setup(r => r.GetResource<SharepointSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockSharepointSource);

            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(sharepointReadListActivity);
            privateObject.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------
            privateObject.Invoke("ExecuteTool", dataObj, 0);
            //------------Assert Result--------------------------
            GetRecordSetFieldValueFromDataList(dataObj.Environment, "Files", "Name", out IList<string> result, out string error);
            NUnit.Framework.Assert.IsNotNull(result);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("SharepointReadListActivity_GetState")]
        public void SharepointReadListActivity_GetState()
        {
            //------------Setup for test--------------------------
            const string activityName = "SharepointReadList";
            var resourceId = Guid.NewGuid();
            var filterCriteria = new List<SharepointSearchTo>()
                {
                    new SharepointSearchTo("A","A","",1)
                };

            var sharepointList = "SharepointList";
            var requireAllCriteriaToMatch = true;
            var readListItems = new List<SharepointReadListTo>();
            var sharepointUtils = new SharepointUtils();
            var sharepointReadListActivity = new SharepointReadListActivity
            {
                DisplayName = activityName,
                SharepointServerResourceId = resourceId,
                ReadListItems = readListItems,
                FilterCriteria = filterCriteria,
                SharepointList = sharepointList,
                RequireAllCriteriaToMatch = requireAllCriteriaToMatch,
                SharepointUtils = sharepointUtils
            };
            var dataObj = new DsfDataObject(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>());
            var resourceCatalog = new Mock<IResourceCatalog>();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(sharepointReadListActivity);
            privateObject.SetProperty("ResourceCatalog", resourceCatalog.Object);
            //------------Execute Test---------------------------
            privateObject.Invoke("ExecuteTool", dataObj, 0);
            var serializer = new Dev2JsonSerializer();
            var expectedResults = new[]
      {
                 new StateVariable
                {
                    Name="SharepointServerResourceId",
                    Type = StateVariable.StateType.Input,
                    Value = resourceId.ToString()
                 },
                 new StateVariable
                {
                    Name="ReadListItems",
                    Type = StateVariable.StateType.InputOutput,
                    Value =  ActivityHelper.GetSerializedStateValueFromCollection(readListItems)
                 },
                new StateVariable
                {
                    Name="FilterCriteria",
                    Type = StateVariable.StateType.Input,
                    Value = ActivityHelper.GetSerializedStateValueFromCollection(filterCriteria)
                },
                new StateVariable
                {
                    Name="RequireAllCriteriaToMatch",
                    Type = StateVariable.StateType.Input,
                    Value = requireAllCriteriaToMatch.ToString()
                },
                new StateVariable
                {
                    Name="SharepointList",
                    Type = StateVariable.StateType.Input,
                    Value = sharepointList
                }
                
            };
            //---------------Test Result -----------------------
            var stateItems = sharepointReadListActivity.GetState();
            NUnit.Framework.Assert.AreEqual(5, stateItems.Count());
            var iter = stateItems.Select(
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
