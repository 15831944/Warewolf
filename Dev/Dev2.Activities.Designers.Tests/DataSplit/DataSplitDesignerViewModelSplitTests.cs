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
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using Dev2.Activities.Designers2.DataSplit;
using Dev2.Common.Interfaces.Help;
using Dev2.Studio.Core.Activities.Utils;
using Dev2.Studio.Interfaces;
using NUnit.Framework;
using Moq;
using Unlimited.Applications.BusinessDesignStudio.Activities;


namespace Dev2.Activities.Designers.Tests.DataSplit
{
    [TestFixture]
    public class DataSplitDesignerViewModelSplitTests
    {
        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataSplitDesignerViewModel_Constructor")]
        public void DataSplitDesignerViewModel_Constructor__ModelItemIsValid_ListHasFourItems()
        {
            var items = new List<DataSplitDTO> { new DataSplitDTO() };
            var viewModel = new DataSplitDesignerViewModel(CreateModelItem(items));
            Assert.AreEqual(6, viewModel.ItemsList.Count);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataSplitDesignerViewModel_Constructor")]
        public void DataSplitDesignerViewModel_Constructor__ModelItemIsValid_CollectionNameIsSetToResultsCollection()
        {
            var items = new List<DataSplitDTO> { new DataSplitDTO() };
            var viewModel = new DataSplitDesignerViewModel(CreateModelItem(items));
            Assert.AreEqual("ResultsCollection", viewModel.CollectionName);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataSplitDesignerViewModel_Constructor")]
        public void DataSplitDesignerViewModel_Constructor_ModelItemIsValid_ResultsCollectionHasTwoItems()
        {
            var modelItem = ModelItemUtils.CreateModelItem(new DsfDataSplitActivity());
            var viewModel = new DataSplitDesignerViewModel(modelItem);
            dynamic mi = viewModel.ModelItem;
            NUnit.Framework.Assert.AreEqual(2, mi.ResultsCollection.Count);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("DataSplitDesignerViewModel_Handle")]
        public void DataSplitDesignerViewModel_UpdateHelp_ShouldCallToHelpViewMode()
        {
            //------------Setup for test--------------------------      
            var mockMainViewModel = new Mock<IShellViewModel>();
            var mockHelpViewModel = new Mock<IHelpWindowViewModel>();
            mockHelpViewModel.Setup(model => model.UpdateHelpText(It.IsAny<string>())).Verifiable();
            mockMainViewModel.Setup(model => model.HelpViewModel).Returns(mockHelpViewModel.Object);
            CustomContainer.Register(mockMainViewModel.Object);
            var modelItem = ModelItemUtils.CreateModelItem(new DsfDataSplitActivity());
            var viewModel = new DataSplitDesignerViewModel(modelItem);
            //------------Execute Test---------------------------
            viewModel.UpdateHelpDescriptor("help");
            //------------Assert Results-------------------------
            mockHelpViewModel.Verify(model => model.UpdateHelpText(It.IsAny<string>()), Times.Once());
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataSplitDesignerViewModel_Constructor")]
        public void DataSplitDesignerViewModel_Constructor_ModelItemIsInitializedWith4Items_ResultsCollectionHasFourItems()
        {
            var items = new List<DataSplitDTO>
            {
                new DataSplitDTO("", DataSplitDTO.SplitTypeNone, "", 0),
                new DataSplitDTO("", DataSplitDTO.SplitTypeNone, "", 0),
                new DataSplitDTO("", DataSplitDTO.SplitTypeNone, "", 0),
                new DataSplitDTO("", DataSplitDTO.SplitTypeNone, "", 0)
            };
            var viewModel = new DataSplitDesignerViewModel(CreateModelItem(items));
            dynamic mi = viewModel.ModelItem;
            NUnit.Framework.Assert.AreEqual(5, mi.ResultsCollection.Count);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DataSplitDesignerViewModel_OnSplitTypeChanged")]
        public void DataSplitDesignerViewModel_OnSplitTypeChanged_EnableAt_SetCorrectly()
        {
            VerifySplitTypeAgainstEnabledAt(DataSplitDTO.SplitTypeIndex, true);
            VerifySplitTypeAgainstEnabledAt(DataSplitDTO.SplitTypeChars, true);
            VerifySplitTypeAgainstEnabledAt(DataSplitDTO.SplitTypeNewLine, false);
            VerifySplitTypeAgainstEnabledAt(DataSplitDTO.SplitTypeSpace, false);
            VerifySplitTypeAgainstEnabledAt(DataSplitDTO.SplitTypeTab, false);
            VerifySplitTypeAgainstEnabledAt(DataSplitDTO.SplitTypeEnd, false);
        }

        static void VerifySplitTypeAgainstEnabledAt(string splitType, bool expectedEnableAt)
        {
            var items = new List<DataSplitDTO> { new DataSplitDTO("", splitType, ",", 0) };
            var viewModel = new DataSplitDesignerViewModel(CreateModelItem(items));
            viewModel.SplitTypeUpdatedCommand.Execute(0);
            dynamic mi = viewModel.ModelItemCollection[0];
            var at = mi.At as string;
            var actualEnableAt = mi.EnableAt as bool?;
            Assert.AreEqual(expectedEnableAt ? "," : "", at);
            Assert.AreEqual(expectedEnableAt, actualEnableAt);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataSplitDesignerViewModel_OnSplitTypeChanged")]
        public void DataSplitDesignerViewModel_OnSplitTypeChanged_IsEscapeCharEnabled_SetCorrectly()
        {
            VerifySplitTypeAgainstIsEscapeCharEnabled(DataSplitDTO.SplitTypeIndex, false);
            VerifySplitTypeAgainstIsEscapeCharEnabled(DataSplitDTO.SplitTypeChars, true);
            VerifySplitTypeAgainstIsEscapeCharEnabled(DataSplitDTO.SplitTypeNewLine, true);
            VerifySplitTypeAgainstIsEscapeCharEnabled(DataSplitDTO.SplitTypeSpace, true);
            VerifySplitTypeAgainstIsEscapeCharEnabled(DataSplitDTO.SplitTypeTab, true);
            VerifySplitTypeAgainstIsEscapeCharEnabled(DataSplitDTO.SplitTypeEnd, false);
        }

        static void VerifySplitTypeAgainstIsEscapeCharEnabled(string splitType, bool expectedIsEscapeCharEnabled)
        {
            var items = new List<DataSplitDTO> { new DataSplitDTO("", splitType, "", 0) { EscapeChar = "'" } };
            var viewModel = new DataSplitDesignerViewModel(CreateModelItem(items));
            viewModel.SplitTypeUpdatedCommand.Execute(0);
            dynamic mi = viewModel.ModelItemCollection[0];
            var escapeChar = mi.EscapeChar as string;
            var actualIsEscapeCharEnabled = mi.IsEscapeCharEnabled as bool?;
            Assert.AreEqual(expectedIsEscapeCharEnabled ? "'" : "", escapeChar);
            Assert.AreEqual(expectedIsEscapeCharEnabled, actualIsEscapeCharEnabled);
        }


        static ModelItem CreateModelItem(IEnumerable<DataSplitDTO> items, string displayName = "Split")
        {
            var modelItem = ModelItemUtils.CreateModelItem(new DsfDataSplitActivity());
            modelItem.SetProperty("DisplayName", displayName);

            
            var modelItemCollection = modelItem.Properties["ResultsCollection"].Collection;
            foreach(var dto in items)
            {
                modelItemCollection.Add(dto);
            }
            
            return modelItem;
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataSplitDesignerViewModel_ValidateThis")]
        public void DataSplitDesignerViewModel_ValidateThis_SourceStringIsNotEmpty_DoesNotHaveErrors()
        {
            //------------Setup for test--------------------------
            var items = new List<DataSplitDTO> { new DataSplitDTO("", DataSplitDTO.SplitTypeChars, "", 0) };
            var mi = CreateModelItem(items);
            mi.SetProperty("SourceString", "a,b,c");
            var viewModel = new DataSplitDesignerViewModel(mi);
            viewModel.GetDatalistString = () =>
            {
                const string trueString = "True";
                const string noneString = "None";
                var datalist = string.Format("<DataList><var Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><a Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><b Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><h Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><r Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><rec Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" ><set Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /></rec></DataList>", trueString, noneString);
                return datalist;
            };

            //------------Execute Test---------------------------
            viewModel.Validate();

            //------------Assert Results-------------------------
            Assert.IsNull(viewModel.Errors);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataSplitDesignerViewModel_ValidateThis")]
        public void DataSplitDesignerViewModel_ValidateThis_SourceStringIsEmptyOrWhiteSpace_DoesHaveErrors()
        {
            //------------Setup for test--------------------------
            var items = new List<DataSplitDTO> { new DataSplitDTO("", DataSplitDTO.SplitTypeChars, "", 0) };
            var mi = CreateModelItem(items);
            mi.SetProperty("SourceString", " ");
            var viewModel = new DataSplitDesignerViewModel(mi);
            viewModel.GetDatalistString = () =>
            {
                const string trueString = "True";
                const string noneString = "None";
                var datalist = string.Format("<DataList><var Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><a Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><b Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><h Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><r Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><rec Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" ><set Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /></rec></DataList>", trueString, noneString);
                return datalist;
            };
            //------------Execute Test---------------------------
            viewModel.Validate();

            //------------Assert Results-------------------------
            Assert.AreEqual(1, viewModel.Errors.Count);
            StringAssert.Contains(viewModel.Errors[0].Message, Warewolf.Resource.Errors.ErrorResource.DataSplitStringToSplitErrorTest);
        }


        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataSplitDesignerViewModel_ValidateCollectionItem")]
        public void DataSplitDesignerViewModel_ValidateCollectionItem_ValidatesPropertiesOfDTO()
        {
            //------------Setup for test--------------------------
            var mi = ModelItemUtils.CreateModelItem(new DsfDataSplitActivity());
            mi.SetProperty("DisplayName", "Split");
            mi.SetProperty("SourceString", "a,b");

            var dto = new DataSplitDTO("a]]", DataSplitDTO.SplitTypeIndex, "a", 0);

            
            var miCollection = mi.Properties["ResultsCollection"].Collection;
            var dtoModelItem = miCollection.Add(dto);
            

            var viewModel = new DataSplitDesignerViewModel(mi);
            viewModel.GetDatalistString = () =>
            {
                const string trueString = "True";
                const string noneString = "None";
                var datalist = string.Format("<DataList><var Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><a Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><b Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><h Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><r Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><rec Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" ><set Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /></rec></DataList>", trueString, noneString);
                return datalist;
            };

            //------------Execute Test---------------------------
            viewModel.Validate();

            //------------Assert Results-------------------------
            Assert.AreEqual(2, viewModel.Errors.Count);

            StringAssert.Contains(viewModel.Errors[0].Message, Warewolf.Resource.Errors.ErrorResource.DataSplitInvalidExpressionErrorTest);
            Verify_IsFocused(dtoModelItem, viewModel.Errors[0].Do, "IsOutputVariableFocused");

            StringAssert.Contains(viewModel.Errors[1].Message, Warewolf.Resource.Errors.ErrorResource.DataSplitUsingNullErrorTest);
            Verify_IsFocused(dtoModelItem, viewModel.Errors[1].Do, "IsAtFocused");
        }

        void Verify_IsFocused(ModelItem modelItem, Action doError, string isFocusedPropertyName)
        {
            Assert.IsFalse(modelItem.GetProperty<bool>(isFocusedPropertyName));
            doError.Invoke();
            Assert.IsTrue(modelItem.GetProperty<bool>(isFocusedPropertyName));
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DataSplitDesignerViewModel_ProcessDirectionGroup")]
        public void DataSplitDesignerViewModel_ProcessDirectionGroup_IsUnique()
        {
            //------------Setup for test--------------------------

            //------------Execute Test---------------------------
            var viewModel1 = CreateViewModel();
            var viewModel2 = CreateViewModel();

            //------------Assert Results-------------------------
            Assert.AreNotEqual(viewModel1.ProcessDirectionGroup, viewModel2.ProcessDirectionGroup);
        }

        static DataSplitDesignerViewModel CreateViewModel()
        {
            var mi = ModelItemUtils.CreateModelItem(new DsfDataSplitActivity());
            mi.SetProperty("DisplayName", "Split");
            mi.SetProperty("SourceString", "a,b");

            var dto = new DataSplitDTO("a]]", DataSplitDTO.SplitTypeIndex, "a", 0);

            
            var miCollection = mi.Properties["ResultsCollection"].Collection;
            miCollection.Add(dto);
            

            var viewModel = new DataSplitDesignerViewModel(mi);
            return viewModel;
        }
    }
}
