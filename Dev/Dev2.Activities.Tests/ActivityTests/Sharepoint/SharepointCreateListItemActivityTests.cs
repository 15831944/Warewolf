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
using NUnit.Framework;
using Moq;
using static Dev2.Tests.Activities.ActivityTests.Sharepoint.SharepointCopyFileActivityTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Tests.Activities.ActivityTests.Sharepoint
{
    [TestFixture]
    [SetUpFixture]
    public class SharepointCreateListItemActivityTests : BaseActivityUnitTest
    {
        SharepointCreateListItemActivity CreateActivity()
        {
            return new SharepointCreateListItemActivity();
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("SharepointCreateListItemActivity_Construct")]
        public void SharepointCreateListItemActivity_Construct_GivenInstance_ShouldNotBeNull()
        {
            //------------Setup for test--------------------------
            var sharepointCreateListItemActivity = CreateActivity();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(sharepointCreateListItemActivity);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("SharepointCreateListItem_Execute")]
        public void SharepointSource_DoesNotExist_OnResourceCatalog_ShouldSetSharepointSource_ToGuidEmpty()
        {
            //------------Setup for test--------------------------
            const string activityName = "SharepointCreateListItem";
            var resourceId = Guid.NewGuid();
            var sharepointCreateListItemActivity = new SharepointCreateListItemActivity
            {
                DisplayName = activityName,
                SharepointServerResourceId = resourceId,
                ReadListItems = new List<SharepointReadListTo>()
            };

            var dataObj = new DsfDataObject(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>());

            var resourceCatalog = new Mock<IResourceCatalog>();
            var mockSharepointSource = new Mock<SharepointSource>();

            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(sharepointCreateListItemActivity);
            privateObject.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------
            privateObject.Invoke("ExecuteTool", dataObj, 0);

            NUnit.Framework.Assert.AreEqual(resourceId, sharepointCreateListItemActivity.SharepointServerResourceId);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("SharepointCreateListItem_Execute")]
        public void SharepointSource_Exists_OnResourceCatalog_BlankRecordSet()
        {
            //------------Setup for test--------------------------
            const string activityName = "SharepointCreateListItem";
            var resourceId = Guid.NewGuid();
            var sharepointCreateListItemActivity = new SharepointCreateListItemActivity
            {
                DisplayName = activityName,
                SharepointServerResourceId = resourceId,
                ReadListItems = new List<SharepointReadListTo>()
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

            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(sharepointCreateListItemActivity);
            privateObject.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------
            privateObject.Invoke("ExecuteTool", dataObj, 0);
            //------------Assert Result--------------------------
            GetRecordSetFieldValueFromDataList(dataObj.Environment, "Files", "Name", out IList<string> result, out string error);
            NUnit.Framework.Assert.IsNotNull(result);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("SharepointCreateListItem_GetState")]
        public void SharepointCreateListItem_GetState()
        {
            //------------Setup for test--------------------------
            const string activityName = "SharepointCreateListItem";
            var resourceId = Guid.NewGuid();
            var dataObj = new DsfDataObject("", Guid.NewGuid(), "");
            var resourceCatalog = new Mock<IResourceCatalog>();
            var mockSharepointHelper = new Mock<ISharepointHelper>();
            mockSharepointHelper.Setup(helper => helper.CopyFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns("Success");
            var mockSharepointSource = new MockSharepointSource
            {
                MockSharepointHelper = mockSharepointHelper.Object
            };
            resourceCatalog.Setup(r => r.GetResource<SharepointSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockSharepointSource);

            var sharepointList = "List";
            var uniqueId = Guid.NewGuid().ToString();
            var result = "[[result]]";
            var readListItems = new List<SharepointReadListTo>()
                {
                    new SharepointReadListTo("a","a","a","a")
                };
            var sharepointCreateListItemActivity = new SharepointCreateListItemActivity
            {
                UniqueID = uniqueId,
                DisplayName = activityName,
                SharepointServerResourceId = resourceId,
                ReadListItems = readListItems,
                Result = result,
                SharepointList = sharepointList
            };
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(sharepointCreateListItemActivity);
            privateObject.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------
            privateObject.Invoke("ExecuteTool", dataObj, 0);
            //------------Assert Result--------------------------
            var serializer = new Dev2JsonSerializer();
            var inputReadListItems = serializer.Serialize(readListItems);
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
                    Type = StateVariable.StateType.Input,
                    Value = inputReadListItems
                },
                  new StateVariable
                {
                    Name="SharepointList",
                    Type = StateVariable.StateType.Input,
                    Value = sharepointList
                },
                  new StateVariable
                {
                    Name="UniqueID",
                    Type = StateVariable.StateType.Input,
                    Value = uniqueId
                },
                 new StateVariable
                {
                    Name="Result",
                    Type = StateVariable.StateType.Output,
                    Value = result
                 }
            };
            var stateItems = sharepointCreateListItemActivity.GetState();
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
