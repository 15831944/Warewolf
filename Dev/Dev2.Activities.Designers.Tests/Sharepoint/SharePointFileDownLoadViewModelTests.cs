using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using Dev2.Activities.Designers2.SharePointFileDownload;
using Dev2.Activities.Sharepoint;
using Dev2.Common.Interfaces.Help;
using Dev2.Common.Interfaces.Infrastructure.Providers.Errors;
using Dev2.Providers.Errors;
using Dev2.Studio.Core.Activities.Utils;
using Dev2.Studio.Interfaces;
using Dev2.Threading;
using NUnit.Framework;
using Moq;
using Warewolf.UnitTestAttributes;

namespace Dev2.Activities.Designers.Tests.Sharepoint
{
    [TestFixture]
    public class SharePointFileDownLoadViewModelTests
    {
        public const string Category = "SharePoint";

        ModelItem CreateModelItem()
        {
            var readListActivity = new SharepointFileDownLoadActivity();
            return ModelItemUtils.CreateModelItem(readListActivity);
        }

        [Test]
        [Category(Category)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SharepointFileDownloadDesignerViewModel_Constructor_NullModelItem_ThrowsException() => new SharePointFileDownLoadDesignerViewModel(null);

        [Test]
        [Category(Category)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SharepointFileDownloadDesignerViewModel_Constructor_NullAsyncWorker_ThrowsException() => new SharePointFileDownLoadDesignerViewModel(CreateModelItem(), null, new Mock<IServer>().Object);

        [Test]
        [Category(Category)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SharePointFileDownLoadDesignerViewModel_Constructor_NullEnvironmentModel_ThrowsException() => new SharePointFileDownLoadDesignerViewModel(CreateModelItem(), new SynchronousAsyncWorker(), null);

        [Test]
        [Category(Category)]
        public void SharePointFileDownLoadDesignerViewModel_InitilizeProperties_ReturnsSuccess()
        {
            //------------Execute Test---------------------------
            var sharepointFileDownLoadDesignerViewModel = new SharePointFileDownLoadDesignerViewModel(CreateModelItem(), new SynchronousAsyncWorker(), new Mock<IServer>().Object);

            sharepointFileDownLoadDesignerViewModel.UpdateHelpDescriptor("Test");

            //------------Assert Results-------------------------
            Assert.IsNotNull(sharepointFileDownLoadDesignerViewModel);
            Assert.IsNotNull(sharepointFileDownLoadDesignerViewModel.CollectionName);
        }

        [Test]
        [Category(Category)]
        public void SharePointFileDownLoadDesignerViewModel_SetProperties_ReturnsSuccess()
        {
            //------------Setup for test--------------------------
            var modelItem = CreateModelItem();
            modelItem.SetProperty("LocalInputPath", "TestFolder");
            modelItem.SetProperty("SharepointServerResourceId", Guid.NewGuid());

            //------------Execute Test---------------------------
            var sharepointReadFolderDesignerViewModel = new SharePointFileDownLoadDesignerViewModel(modelItem, new SynchronousAsyncWorker(), new Mock<IServer>().Object);
            sharepointReadFolderDesignerViewModel.Errors = new List<IActionableErrorInfo> { new ActionableErrorInfo() { Message = "Please Select a SharePoint Server" } };
            sharepointReadFolderDesignerViewModel.Validate();
            var inputPath = modelItem.GetProperty<string>("LocalInputPath");
            var sourceId = modelItem.GetProperty<Guid>("SharepointServerResourceId");

            var mockMainViewModel = new Mock<IShellViewModel>();
            var mockHelpWindowViewModel = new Mock<IHelpWindowViewModel>();
            mockMainViewModel.Setup(model => model.HelpViewModel).Returns(mockHelpWindowViewModel.Object);
            CustomContainer.Register(mockMainViewModel.Object);
            sharepointReadFolderDesignerViewModel.UpdateHelpDescriptor("Test");

            //------------Assert Results-------------------------
            Assert.IsNotNull(inputPath);
            Assert.IsNotNull(sharepointReadFolderDesignerViewModel.LocalInputPath);
            Assert.AreNotEqual(sourceId, Guid.Empty);
        }

        [Test]
        [Category(Category)]
        public void SharePointFileDownLoadDesignerViewModel_SetPropertiesNullSource_ReturnsSuccess()
        {
            //------------Setup for test--------------------------
            var modelItem = CreateModelItem();
            modelItem.SetProperty("LocalInputPath", "TestFolder");

            //------------Execute Test---------------------------
            var sharepointReadFolderDesignerViewModel = new SharePointFileDownLoadDesignerViewModel(modelItem, new SynchronousAsyncWorker(), new Mock<IServer>().Object);
            sharepointReadFolderDesignerViewModel.Errors = new List<IActionableErrorInfo> { new ActionableErrorInfo() { Message = "Please Select a SharePoint Server" } };
            sharepointReadFolderDesignerViewModel.Validate();
            var inputPath = modelItem.GetProperty<string>("LocalInputPath");
            var sourceId = modelItem.GetProperty<Guid>("SharepointServerResourceId");

            //------------Assert Results-------------------------
            Assert.IsNotNull(inputPath);
            Assert.IsNotNull(sharepointReadFolderDesignerViewModel.LocalInputPath);
        }

        [Test]
        [Category(Category)]
        public void SharePointFileDownLoadDesignerViewModel_SetPropertiesNullLocalPath_ReturnsSuccess()
        {
            //------------Setup for test--------------------------
            var modelItem = CreateModelItem();
            modelItem.SetProperty("SharepointServerResourceId", Guid.NewGuid());

            //------------Execute Test---------------------------
            var sharepointReadFolderDesignerViewModel = new SharePointFileDownLoadDesignerViewModel(modelItem, new SynchronousAsyncWorker(), new Mock<IServer>().Object);
            sharepointReadFolderDesignerViewModel.Errors = new List<IActionableErrorInfo> { new ActionableErrorInfo() { Message = "Please Select a SharePoint Server" } };
            sharepointReadFolderDesignerViewModel.Validate();

            //------------Assert Results-------------------------
            Assert.IsNotNull(sharepointReadFolderDesignerViewModel.LocalInputPath);
        }
    }
}
