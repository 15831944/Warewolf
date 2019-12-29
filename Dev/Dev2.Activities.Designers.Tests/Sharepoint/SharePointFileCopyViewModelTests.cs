using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using Dev2.Activities.Designers2.SharePointCopyFile;
using Dev2.Activities.Sharepoint;
using Dev2.Common.Interfaces.Help;
using Dev2.Common.Interfaces.Infrastructure.Providers.Errors;
using Dev2.Providers.Errors;
using Dev2.Studio.Core.Activities.Utils;
using Dev2.Studio.Interfaces;
using Dev2.Threading;
using NUnit.Framework;
using Moq;

namespace Dev2.Activities.Designers.Tests.Sharepoint
{
    [TestFixture]
    public class SharePointFileCopyViewModelTests
    {
        public const string Category = "SharePoint";

        ModelItem CreateModelItem()
        {
            var fileUploadactivity = new SharepointCopyFileActivity();
            return ModelItemUtils.CreateModelItem(fileUploadactivity);
        }

        [Test]
        [Category(Category)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SharePointCopyFileDesignerViewModel_Constructor_NullModelItem_ThrowsException() => new SharePointCopyFileDesignerViewModel(null);

        [Test]
        [Category(Category)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SharePointCopyFileDesignerViewModel_Constructor_NullAsyncWorker_ThrowsException() => new SharePointCopyFileDesignerViewModel(CreateModelItem(), null, new Mock<IServer>().Object);

        [Test]
        [Category(Category)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SharePointCopyFileDesignerViewModel_Constructor_NullEnvironmentModel_ThrowsException() => new SharePointCopyFileDesignerViewModel(CreateModelItem(), new SynchronousAsyncWorker(), null);

        [Test]
        [Category(Category)]
        public void SharePointCopyFileDesignerViewModel_InitilizeProperties_ReturnsSuccess()
        {
            //------------Setup for test--------------------------
            var sharepointFileCopyDesignerViewModel = new SharePointCopyFileDesignerViewModel(CreateModelItem(), new SynchronousAsyncWorker(), new Mock<IServer>().Object);

            //------------Execute Test---------------------------
            sharepointFileCopyDesignerViewModel.UpdateHelpDescriptor("Test");

            //------------Assert Results-------------------------
            Assert.IsNotNull(sharepointFileCopyDesignerViewModel);
            Assert.IsNotNull(sharepointFileCopyDesignerViewModel.CollectionName);
        }

        [Test]
        [Category(Category)]
        public void SharePointCopyFileDesignerViewModel_SetProperties_ReturnsSuccess()
        {
            //------------Setup for test--------------------------
            var modelItem = CreateModelItem();
            modelItem.SetProperty("ServerInputPathFrom", "TestFolder");
            modelItem.SetProperty("ServerInputPathTo", "TestFolder");
            modelItem.SetProperty("SharepointServerResourceId", Guid.NewGuid());

            //------------Execute Test---------------------------
            var sharepointFileCopyDesignerViewModel = new SharePointCopyFileDesignerViewModel(modelItem, new SynchronousAsyncWorker(), new Mock<IServer>().Object);
            sharepointFileCopyDesignerViewModel.Errors = new List<IActionableErrorInfo> { new ActionableErrorInfo() { Message = "Please Select a SharePoint Server" } };
            sharepointFileCopyDesignerViewModel.Validate();
            var inputPathfrom = modelItem.GetProperty<string>("ServerInputPathFrom");
            var inputPathTo = modelItem.GetProperty<string>("ServerInputPathTo");
            var sourceId = modelItem.GetProperty<Guid>("SharepointServerResourceId");

            //------------Assert Results-------------------------
            Assert.IsNotNull(inputPathfrom);
            Assert.IsNotNull(inputPathTo);
            Assert.IsNotNull(sharepointFileCopyDesignerViewModel.ServerInputPathFrom);
            Assert.IsNotNull(sharepointFileCopyDesignerViewModel.ServerInputPathTo);
            Assert.AreNotEqual(sourceId, Guid.Empty);
        }

        [Test]
        [Category(Category)]
        public void SharePointFileUploadDesignerViewModel_SetPropertiesNullSource_ReturnsSuccess()
        {
            //------------Setup for test--------------------------

            var modelItem = CreateModelItem();
            modelItem.SetProperty("ServerInputPathFrom", "TestFolder");
            modelItem.SetProperty("ServerInputPathTo", "TestFolder");

            //------------Execute Test---------------------------
            var sharepointFileCopyDesignerViewModel = new SharePointCopyFileDesignerViewModel(modelItem, new SynchronousAsyncWorker(), new Mock<IServer>().Object);
            sharepointFileCopyDesignerViewModel.Errors = new List<IActionableErrorInfo> { new ActionableErrorInfo() { Message = "Please Select a SharePoint Server" } };
            sharepointFileCopyDesignerViewModel.Validate();
            var inputPathfrom = modelItem.GetProperty<string>("ServerInputPathFrom");
            var inputPathTo = modelItem.GetProperty<string>("ServerInputPathTo");

            //------------Assert Results-------------------------
            Assert.IsNotNull(inputPathfrom);
            Assert.IsNotNull(inputPathTo);
            Assert.IsNotNull(sharepointFileCopyDesignerViewModel.ServerInputPathFrom);
            Assert.IsNotNull(sharepointFileCopyDesignerViewModel.ServerInputPathTo);
        }

        [Test]
        [Category(Category)]
        public void SharePointFileUploadDesignerViewModel_SetPropertiesNullLocalPathFrom_ReturnsSuccess()
        {
            //------------Setup for test--------------------------

            var modelItem = CreateModelItem();

            modelItem.SetProperty("ServerInputPathTo", "TestFolder");
            modelItem.SetProperty("SharepointServerResourceId", Guid.NewGuid());
            //------------Execute Test---------------------------
            var sharepointFileCopyDesignerViewModel = new SharePointCopyFileDesignerViewModel(modelItem, new SynchronousAsyncWorker(), new Mock<IServer>().Object);
            sharepointFileCopyDesignerViewModel.Errors = new List<IActionableErrorInfo> { new ActionableErrorInfo() { Message = "Please Select a SharePoint Server" } };
            sharepointFileCopyDesignerViewModel.Validate();

            var inputPathTo = modelItem.GetProperty<string>("ServerInputPathTo");

            //------------Assert Results-------------------------
            Assert.IsNotNull(inputPathTo);
            Assert.IsNotNull(sharepointFileCopyDesignerViewModel.ServerInputPathTo);
        }

        [Test]
        [Category(Category)]
        public void SharePointFileUploadDesignerViewModel_SetPropertiesNullLocalPathTo_ReturnsSuccess()
        {
            //------------Setup for test--------------------------

            var modelItem = CreateModelItem();

            modelItem.SetProperty("ServerInputPathFrom", "TestFolder");
            modelItem.SetProperty("SharepointServerResourceId", Guid.NewGuid());
            //------------Execute Test---------------------------
            var sharepointFileCopyDesignerViewModel = new SharePointCopyFileDesignerViewModel(modelItem, new SynchronousAsyncWorker(), new Mock<IServer>().Object);
            sharepointFileCopyDesignerViewModel.Errors = new List<IActionableErrorInfo> { new ActionableErrorInfo() { Message = "Please Select a SharePoint Server" } };
            sharepointFileCopyDesignerViewModel.Validate();
        
            var inputPathfrom = modelItem.GetProperty<string>("ServerInputPathFrom");
            var mockMainViewModel = new Mock<IShellViewModel>();
            var mockHelpWindowViewModel = new Mock<IHelpWindowViewModel>();
            mockMainViewModel.Setup(model => model.HelpViewModel).Returns(mockHelpWindowViewModel.Object);
            CustomContainer.Register(mockMainViewModel.Object);

            //------------Assert Results-------------------------
            sharepointFileCopyDesignerViewModel.UpdateHelpDescriptor("Test");
            Assert.IsNotNull(inputPathfrom);
            Assert.IsNotNull(sharepointFileCopyDesignerViewModel.ServerInputPathTo);
        }
    }
}
