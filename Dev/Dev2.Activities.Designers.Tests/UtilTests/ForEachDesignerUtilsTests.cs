/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Activities.Presentation.Model;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Windows;
using Castle.DynamicProxy.Generators;
using Dev2.Activities.Utils;
using Dev2.Common.Interfaces.PopupController;
using Dev2.Studio.Core.Activities.Utils;
using Dev2.Studio.Interfaces;
using NUnit.Framework;
using Moq;
using Unlimited.Applications.BusinessDesignStudio.Activities;



namespace Dev2.Core.Tests.Activities
{
    [TestFixture]
    [SetUpFixture]
    public class ForEachDesignerUtilsTests
    {
        [SetUp]
        public void MyTestInitialize()
        {
            AttributesToAvoidReplicating.Add(typeof(UIPermissionAttribute));
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_NoFormats_EnableDrop()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new string[] { });
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsTrue(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_NoWorkflowItemTypeNameFormatAndNoModelItemFormat_EnableDrop()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "SomeOtherFormat" });
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsTrue(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemFormat_Decision_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("Decision");
            //------------Execute Test---------------------------
            var shell = new Mock<IShellViewModel>();
            CustomContainer.Register(shell.Object);
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemsFormat_WithDecisionAndCount_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemsFormat" });
            var modelItemList = new List<ModelItem> { ModelItemUtils.CreateModelItem(new DsfCountRecordsetNullHandlerActivity()), ModelItemUtils.CreateModelItem(new FlowDecision()) };
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns(modelItemList);
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemsFormat_WithSwitchAndCount_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemsFormat" });
            var modelItemList = new List<ModelItem> { ModelItemUtils.CreateModelItem(new DsfCountRecordsetNullHandlerActivity()), ModelItemUtils.CreateModelItem(new FlowSwitch<string>()) };
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns(modelItemList);
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemFormat_Switch_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("Switch");
            //------------Execute Test---------------------------
            var shell = new Mock<IShellViewModel>();
            CustomContainer.Register(shell.Object);
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }
        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemFormat_NotDecision_DropNotPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("Act");
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsTrue(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemFormat_NotSwitch_DropNotPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("Activity");
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsTrue(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_WorkflowItemTypeNameFormat_Decision_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "WorkflowItemTypeNameFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("Decision");
            //------------Execute Test---------------------------
            var shell = new Mock<IShellViewModel>();
            CustomContainer.Register(shell.Object);
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_WorkflowItemTypeNameFormat_Switch_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "WorkflowItemTypeNameFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("Switch");
            //------------Execute Test---------------------------
            var shell = new Mock<IShellViewModel>();
            CustomContainer.Register(shell.Object);
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }
        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_WorkflowItemTypeNameFormat_NotDecision_DropNotPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "WorkflowItemTypeNameFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("Act");
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsTrue(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_WorkflowItemTypeNameFormat_NotSwitch_DropNotPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "WorkflowItemTypeNameFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("Activity");
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsTrue(dropEnabled);
        }



        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemFormat_DecisionModelItem_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemFormat" });
            var modelItem = ModelItemUtils.CreateModelItem(new FlowDecision());
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns(modelItem);
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemFormat_SwitchModelItem_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemFormat" });
            var modelItem = ModelItemUtils.CreateModelItem(new FlowSwitch<string>());
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns(modelItem);
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemFormat_NotSwitchDecision_DropNotPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemFormat" });
            var modelItem = ModelItemUtils.CreateModelItem(new DsfMultiAssignActivity());
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns(modelItem);
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsTrue(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_WorkflowItemTypeNameFormat_DecisionModelItem_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "WorkflowItemTypeNameFormat" });
            var modelItem = ModelItemUtils.CreateModelItem(new FlowDecision());
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns(modelItem);
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_WorkflowItemTypeNameFormat_SwitchModelItem_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var mock = new Mock<IShellViewModel>();
            mock.Setup(model => model.ShowPopup(It.IsAny<IPopupMessage>()));
            CustomContainer.Register(mock.Object);
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "WorkflowItemTypeNameFormat" });
            var modelItem = ModelItemUtils.CreateModelItem(new FlowSwitch<string>());
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns(modelItem);
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
            mock.VerifyAll();
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_WorkflowItemTypeNameFormat_NotSwitchDecisionModelItem_DropNotPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "WorkflowItemTypeNameFormat" });
            var modelItem = ModelItemUtils.CreateModelItem(new DsfMultiAssignActivity());
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns(modelItem);
            //------------Execute Test---------------------------
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsTrue(dropEnabled);
        }


        [Test]
        [Author("Pieter Terblanche")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_ModelItemFormat_SelectAndApplyModelItem_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "ModelItemFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("SelectAndApply");
            //------------Execute Test---------------------------
            var shell = new Mock<IShellViewModel>();
            CustomContainer.Register(shell.Object);
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("ForeachActivityDesignerUtils_LimitDragDropOptions")]
        public void ForeachActivityDesignerUtils_LimitDragDropOptions_WorkflowItemTypeNameFormat_SelectAndApplyModelItem_DropPrevented()
        {
            //------------Setup for test--------------------------
            var activityDesignerUtils = new DropEnabledActivityDesignerUtils();
            var dataObject = new Mock<IDataObject>();
            dataObject.Setup(o => o.GetFormats()).Returns(new[] { "WorkflowItemTypeNameFormat" });
            dataObject.Setup(o => o.GetData(It.IsAny<string>())).Returns("SelectAndApply");
            //------------Execute Test---------------------------
            var shell = new Mock<IShellViewModel>();
            CustomContainer.Register(shell.Object);
            var dropEnabled = activityDesignerUtils.LimitDragDropOptions(dataObject.Object);
            //------------Assert Results-------------------------
            Assert.IsFalse(dropEnabled);
        }
    }
}
