using System.Collections.Generic;
using Dev2.Activities.Designers.Tests.WebGetTool;
using Dev2.Activities.Designers2.Core;
using Dev2.Activities.Designers2.Web_Service_Get;
using Dev2.Common;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.DB;
using Dev2.Studio.Core.Activities.Utils;
using NUnit.Framework;
using Warewolf.Core;



namespace Dev2.Activities.Designers.Tests.Core
{
    [TestFixture]
    public class ManageWebServiceInputViewModelTests
    {
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("OutputsRegion_Ctor")]
        public void ManageWebServiceInputViewModel_Ctor()
        {
            var mod = new MyWebModel();
            var act = new DsfWebGetActivity()
            {
                SourceId = mod.Sources[0].Id,
                Outputs = new List<IServiceOutputMapping> { new ServiceOutputMapping("a", "b", "c"), new ServiceOutputMapping("d", "e", "f") },
                Headers = new List<INameValue> { new NameValue("a", "x") },
                QueryString = "Bob the builder",
                ServiceName = "dsfBob"
            };

            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);

            //------------Assert Results-------------------------
            var vm = new ManageWebServiceInputViewModel(webget, mod);
            Assert.IsNotNull(vm.CloseCommand);
            Assert.IsNotNull(vm.PasteResponseCommand);
            Assert.IsNotNull(vm.CloseCommand);

        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("OutputsRegion_Ctor")]
        public void ManageWebServiceInputViewModel_TestAction()
        {
            var called = false;
            var calledOk = false;

            var mod = new MyWebModel();
            var act = new DsfWebGetActivity()
            {
                SourceId = mod.Sources[0].Id,
                Outputs = new List<IServiceOutputMapping> { new ServiceOutputMapping("a", "b", "c"), new ServiceOutputMapping("d", "e", "f") },
                Headers = new List<INameValue> { new NameValue("a", "x") },
                QueryString = "Bob the builder",
                ServiceName = "dsfBob"
            };

            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);

            var vm = new ManageWebServiceInputViewModel(webget, mod);
            vm.TestAction = () => { called = true; };
            vm.OkAction = () =>
            {
                calledOk = true;
            };
            vm.TestAction();
            vm.OkAction();

            //------------Assert Results-------------------------

            Assert.IsTrue(called);
            Assert.IsTrue(calledOk);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModel_TestActionSetSourceAndTest()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();

            var act = new DsfWebGetActivity();
           
            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.Model = new WebServiceDefinition();
            //------------Execute Test---------------------------
            inputview.ExecuteTest();
            //------------Assert Results-------------------------
            Assert.IsTrue(inputview.InputArea.IsEnabled);
            Assert.IsTrue(inputview.OutputArea.IsEnabled);
            Assert.IsNotNull(inputview.OutputArea.Outputs);
            Assert.IsTrue(inputview.OutputArea.Outputs.Count>0);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModel_TestActionSetSourceAndTest_ExistingContent()
        {
            //------------Setup for test--------------------------
            var mod = new MyModel()
            {
                Response = "{\"NormalText\":\"\"}"
            };

            var act = new DsfWebGetActivity()
            {
                Headers = new List<INameValue>()
                {
                    new NameValue("Content-Type","Application/xml")
                }
            };
           
            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.Model = new WebServiceDefinition();
            Assert.AreEqual(2, webget.InputArea.Headers.Count);
            //------------Execute Test---------------------------
            inputview.ExecuteTest();
            //------------Assert Results-------------------------
            Assert.IsTrue(inputview.InputArea.IsEnabled);
            Assert.IsTrue(inputview.OutputArea.IsEnabled);
            Assert.IsNotNull(inputview.OutputArea.Outputs);
            Assert.IsTrue(inputview.OutputArea.Outputs.Count>0);
            Assert.AreEqual(2, webget.InputArea.Headers.Count);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModel_PropertyChangedHandler()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();

            var act = new DsfWebGetActivity();
            var called = false;
            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.PropertyChanged += (sender, args) => called = true;
            inputview.Model = new WebServiceDefinition();
            //------------Execute Test---------------------------
            inputview.ExecuteTest();

            //------------Assert Results-------------------------
            Assert.IsTrue(called);


        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModelCloneRegion_ReturnsNull()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();

            var act = new DsfWebGetActivity();
            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.Model = new WebServiceDefinition();
            
            //------------Execute Test---------------------------
            var clone = inputview.CloneRegion();

            //------------Assert Results-------------------------
            Assert.AreEqual(inputview,clone);
            
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModelTestAction_Exception()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();
            mod.HasRecError = true;

            var act = new DsfWebGetActivity();
            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.Model = null;

            //------------Execute Test---------------------------
            inputview.ExecuteTest();

            //------------Assert Results-------------------------
            Assert.IsTrue(inputview.Errors.Count == 1);

        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModelOkAction_Exception()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();
            mod.HasRecError = true;

            var act = new DsfWebGetActivity();
            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            webget.OutputsRegion.Outputs = null;

            //------------Execute Test---------------------------
            inputview.ExecuteOk();

            //------------Assert Results-------------------------
            Assert.IsTrue(inputview.Errors.Count == 1);

        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModel_RestoreRegion_DoesNothing()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();

            var act = new DsfWebGetActivity();
            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.Model = new WebServiceDefinition();

            //------------Execute Test---------------------------
            inputview.RestoreRegion(null);

            //------------Assert Results-------------------------
            Assert.IsTrue(true,"Error RestoreRegion should do nothing");

        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModel_TestActionSetSourceAndTestClickOk()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();

            var act = new DsfWebGetActivity();

            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.Model = new WebServiceDefinition();
            inputview.ExecuteTest();
            //------------Execute Test---------------------------
            Assert.IsTrue(inputview.InputArea.IsEnabled);
            Assert.IsTrue(inputview.OutputArea.IsEnabled);
            Assert.IsNotNull(inputview.OutputArea.Outputs);
            Assert.IsTrue(inputview.OutputArea.Outputs.Count > 0);

            inputview.ExecuteOk();
            //------------Execute Ok---------------------------
            Assert.IsTrue(webget.SourceRegion.IsEnabled);
            Assert.IsTrue(webget.OutputsRegion.IsEnabled);
            Assert.IsTrue(webget.InputArea.IsEnabled);
            Assert.IsTrue(webget.ErrorRegion.IsEnabled);
            Assert.IsFalse(webget.ManageServiceInputViewModel.InputArea.IsEnabled);

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModel_TestActionSetSourceAndTestClickPaste()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();

            var act = new DsfWebGetActivity();

            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.Model = new WebServiceDefinition();
            //------------Execute Test---------------------------
            inputview.ExecutePaste();

            //------------Assert Results-------------------------
            Assert.IsTrue(inputview.PasteResponseVisible);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("Webget_MethodName")]
        public void ManageWebServiceInputViewModel_TestActionSetSourceAndTestClickClose()
        {
            //------------Setup for test--------------------------
            var mod = new MyWebModel();

            var act = new DsfWebGetActivity();

            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);
            var inputview = new ManageWebServiceInputViewModel(webget, mod);
            inputview.Model = new WebServiceDefinition();
            inputview.ExecuteClose();
            //------------Execute Ok---------------------------
            Assert.IsNull(inputview.OutputArea.Outputs);
            Assert.IsTrue(webget.SourceRegion.IsEnabled);
            Assert.IsFalse(webget.OutputsRegion.IsEnabled);
            Assert.IsTrue(webget.InputArea.IsEnabled);
            Assert.IsTrue(webget.ErrorRegion.IsEnabled);
            Assert.IsFalse(webget.ManageServiceInputViewModel.InputArea.IsEnabled);

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("OutputsRegion_Ctor")]
        public void ManageWebServiceInputViewModel_Properties()
        {
            var mod = new MyWebModel();
            var act = new DsfWebGetActivity()
            {
                SourceId = mod.Sources[0].Id,
                Outputs = new List<IServiceOutputMapping> { new ServiceOutputMapping("a", "b", "c"), new ServiceOutputMapping("d", "e", "f") },
                Headers = new List<INameValue> { new NameValue("a", "x") },
                QueryString = "Bob the builder",
                ServiceName = "dsfBob"
            };

            var webget = new WebServiceGetViewModel(ModelItemUtils.CreateModelItem(act), mod);

            var vm = new ManageWebServiceInputViewModel(webget, mod);
            var lst = new List<IServiceInput>();
            vm.InputArea.Inputs = lst;
            Assert.AreEqual(lst.Count, vm.InputArea.Inputs.Count);
            var lsto = new List<IServiceOutputMapping>();
            vm.OutputArea.Outputs = lsto;
            Assert.AreEqual(lsto, vm.OutputArea.Outputs);
            vm.TestResults = "bob";
            Assert.AreEqual("bob", vm.TestResults);
            vm.TestResultsAvailable = true;
            Assert.IsTrue(vm.TestResultsAvailable);
            vm.OkSelected = true;
            Assert.IsTrue(vm.OkSelected);
            vm.IsTestResultsEmptyRows = true;
            Assert.IsTrue(vm.IsTestResultsEmptyRows);
            vm.IsTesting = true;
            Assert.IsTrue(vm.IsTesting);
            vm.PasteResponseVisible = true;
            Assert.IsTrue(vm.PasteResponseVisible);
            vm.PasteResponseAvailable = true;
            Assert.IsTrue(vm.PasteResponseAvailable);
            var b = new WebServiceDefinition() { Headers = new List<INameValue>() { new NameValue("a", "b") } };
            vm.Model = b;
            Assert.IsNotNull(vm.Model);
        }
    }
}
