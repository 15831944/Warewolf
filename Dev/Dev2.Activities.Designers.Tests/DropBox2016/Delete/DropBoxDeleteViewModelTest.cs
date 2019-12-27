using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Dev2.Activities.Designers2.Core;
using Dev2.Activities.Designers2.DropBox2016.Delete;
using Dev2.Activities.DropBox2016.DeleteActivity;
using Dev2.Common.Interfaces.Help;
using Dev2.Data.ServiceModel;
using Dev2.Studio.Core.Activities.Utils;
using Dev2.Studio.Interfaces;
using NUnit.Framework;
using Moq;


namespace Dev2.Activities.Designers.Tests.DropBox2016.Delete
{
    [TestFixture]
    public class DropBoxDeleteViewModelTest
    {
        DropBoxDeleteViewModel CreateMockViewModel()
        {
            var dropBoxSourceManager = new Mock<IDropboxSourceManager>();
            var dropBoxDeleteViewModel = new DropBoxDeleteViewModel(CreateModelItem(), dropBoxSourceManager.Object);
            return dropBoxDeleteViewModel;
        }

        ModelItem CreateModelItem()
        {
            var modelItem = ModelItemUtils.CreateModelItem(new DsfDropBoxDeleteActivity());
            return modelItem;
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_Construct")]
        public void DropBoxDeleteViewModel_Construct_GivenNewInstance_ShouldBeActivityViewModel()
        {
            //------------Setup for test--------------------------
            var dropBoxDeleteViewModel = CreateMockViewModel();
            //------------Execute Test---------------------------
            Assert.IsNotNull(dropBoxDeleteViewModel);
            Assert.IsFalse(dropBoxDeleteViewModel.ShowLarge);
            Assert.AreEqual(dropBoxDeleteViewModel.ThumbVisibility, Visibility.Collapsed);
            //------------Assert Results-------------------------
            Assert.IsInstanceOf(typeof(ActivityDesignerViewModel), dropBoxDeleteViewModel);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_Handle")]
        public void DropBoxDeleteViewModel_UpdateHelp_ShouldCallToHelpViewMode()
        {
            //------------Setup for test--------------------------      
            var mockMainViewModel = new Mock<IShellViewModel>();
            var mockHelpViewModel = new Mock<IHelpWindowViewModel>();
            mockHelpViewModel.Setup(model => model.UpdateHelpText(It.IsAny<string>())).Verifiable();
            mockMainViewModel.Setup(model => model.HelpViewModel).Returns(mockHelpViewModel.Object);
            CustomContainer.Register(mockMainViewModel.Object);
            var viewModel = CreateMockViewModel();
            //------------Execute Test---------------------------
            viewModel.UpdateHelpDescriptor("help");
            //------------Assert Results-------------------------
            mockHelpViewModel.Verify(model => model.UpdateHelpText(It.IsAny<string>()), Times.Once());
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_Sources")]
        public void DropBoxDeleteViewModel_Sources_GivenANewDropBoxViewModel_ShouldHaveNotBeNull()
        {
            //------------Setup for test--------------------------
            var dropBoxDeleteViewModel = CreateMockViewModel();

            //------------Execute Test---------------------------
            dropBoxDeleteViewModel.Validate();
            //------------Assert Results-------------------------
            Assert.IsInstanceOf(typeof(ActivityDesignerViewModel), dropBoxDeleteViewModel);
            Assert.IsNotNull(dropBoxDeleteViewModel.Sources);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_DeletePath")]
        public void DropBoxDeleteViewModel_DeletePath_GivenActivityIsNew_ShouldBeNullOrEmpty()
        {
            //------------Setup for test--------------------------
            var dropBoxDeleteViewModel = CreateMockViewModel();

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.IsNotNull(dropBoxDeleteViewModel.Sources);
            Assert.IsTrue(string.IsNullOrEmpty(dropBoxDeleteViewModel.DeletePath));
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_Result")]
        public void DropBoxDeleteViewModel_Result_GivenActivityIsNew_ShouldBeNullOrEmpty()
        {
            //------------Setup for test--------------------------
            var dropBoxDeleteViewModel = CreateMockViewModel();
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.IsTrue(string.IsNullOrEmpty(dropBoxDeleteViewModel.DeletePath));
            Assert.IsTrue(string.IsNullOrEmpty(dropBoxDeleteViewModel.Result));
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_SelectedSourceName")]
        public void DropBoxDeleteViewModel_SelectedSourceName_GivenActivityIsNewAndNoSourceSelected_ShouldBeNullOrEmpty()
        {
            //------------Setup for test--------------------------
            var dropBoxDeleteViewModel = CreateMockViewModel();
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.IsTrue(string.IsNullOrEmpty(dropBoxDeleteViewModel.Result));
            Assert.IsNull(dropBoxDeleteViewModel.SelectedSource);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_Sources")]
        public void DropBoxDeleteViewModel_Sources_EditSource_PublishesMessage()
        {
            //------------Setup for test--------------------------

            var agg = new Mock<IEventAggregator>();
            var model = CreateModelItem();
            var mockShellViewModel = new Mock<IShellViewModel>();
            var serverMock = new Mock<IServer>();
            mockShellViewModel.Setup(viewModel => viewModel.OpenResource(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<IServer>()));
            mockShellViewModel.Setup(viewModel => viewModel.ActiveServer).Returns(() => serverMock.Object);
            CustomContainer.Register(mockShellViewModel.Object);
            //------------Execute Test---------------------------
            var dropBoxDeleteViewModel = new DropBoxDeleteViewModel(model, TestResourceCatalog.LazySourceManager.Value) { SelectedSource = new DropBoxSource() };
            dropBoxDeleteViewModel.EditDropboxSourceCommand.Execute(null);
            //------------Assert Results-------------------------
            mockShellViewModel.Verify(viewModel => viewModel.OpenResource(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<IServer>()));
            CustomContainer.DeRegister<IShellViewModel>();
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_Sources")]
        public void DropBoxDeleteViewModel_Sources_EditSource_OnlyAvailableIfSourceSelected()
        {
            //------------Setup for test--------------------------
            var agg = new Mock<IEventAggregator>();
            var model = CreateModelItem();
            //------------Execute Test---------------------------
            var dropBoxDeleteViewModel = new DropBoxDeleteViewModel(model, TestResourceCatalog.LazySourceManager.Value);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropBoxDeleteViewModel.IsDropboxSourceSelected);
            dropBoxDeleteViewModel.SelectedSource = new DropBoxSource();
            Assert.IsTrue(dropBoxDeleteViewModel.IsDropboxSourceSelected);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_Sources")]
        public void DropBoxDeleteViewModel_Sources_EditSource_AvailableIfSourceSelected()
        {
            //------------Setup for test--------------------------
            var agg = new Mock<IEventAggregator>();
            var model = CreateModelItem();
            //------------Execute Test---------------------------
            var dropBoxDeleteViewModel = new DropBoxDeleteViewModel(model, TestResourceCatalog.LazySourceManager.Value);

            //------------Assert Results-------------------------
            Assert.IsFalse(dropBoxDeleteViewModel.IsDropboxSourceSelected);
            dropBoxDeleteViewModel.SelectedSource = new DropBoxSource();
            Assert.IsTrue(dropBoxDeleteViewModel.IsDropboxSourceSelected);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_DeletePath")]
        public void DropBoxDeleteViewModel_DeletePath_GivenIsSet_ShouldSetModelItemProperty()
        {
            //------------Setup for test--------------------------
            var agg = new Mock<IEventAggregator>();
            var model = CreateModelItem();

            //------------Execute Test---------------------------
            
            var dropBoxDeleteViewModel = new DropBoxDeleteViewModel(model, TestResourceCatalog.LazySourceManager.Value);
            dropBoxDeleteViewModel.DeletePath = "A";

            //------------Assert Results-------------------------
            var property = model.Properties["DeletePath"];
            if (property == null)
            {
                Assert.Fail("Property Does not exist");
            }
            var modelPropertyValue = property.ComputedValue;
            Assert.AreEqual("A", modelPropertyValue);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_Result")]
        public void DropBoxDeleteViewModel_Result_GivenIsSet_ShouldSetModelItemProperty()
        {
            //------------Setup for test--------------------------
            var agg = new Mock<IEventAggregator>();
            var model = CreateModelItem();
            //------------Execute Test---------------------------
            
            var dropBoxDeleteViewModel = new DropBoxDeleteViewModel(model, TestResourceCatalog.LazySourceManager.Value);
            dropBoxDeleteViewModel.Result = "A";

            //------------Assert Results-------------------------
            var property = model.Properties["Result"];
            if (property == null)
            {
                Assert.Fail("Property Does not exist");
            }
            var modelPropertyValue = property.ComputedValue;
            Assert.AreEqual("A", modelPropertyValue);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DropBoxDeleteViewModel_CreateOAuthSource")]
        public void DropBoxDeleteViewModel_CreateOAuthSource_GivenCanPublish_ShouldResfreshSources()
        {
            //------------Setup for test--------------------------
            var agg = new Mock<IEventAggregator>();
            var model = CreateModelItem();
            var mock = new Mock<IDropboxSourceManager>();
            mock.Setup(catalog => catalog.FetchSources<DropBoxSource>()).Returns(new List<DropBoxSource>()
            {
                new DropBoxSource(), new DropBoxSource()
            });
            //------------Execute Test---------------------------
            var mockVM = new DropBoxDeleteViewModel(model, mock.Object);
            mockVM.Sources.Clear();
            var count = mockVM.Sources.Count();

            //------------Assert Results-------------------------
            Assert.AreEqual(0, count);

            mockVM.Sources = mockVM.LoadOAuthSources();
            Assert.AreEqual(2, mockVM.Sources.Count);
        }
    }
}
