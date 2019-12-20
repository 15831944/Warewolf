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
using System.Windows;
using Dev2.Activities.Designers2.Core.QuickVariableInput;
using Dev2.Activities.Preview;
using Dev2.Runtime.Configuration.ViewModels.Base;
using NUnit.Framework;

namespace Dev2.Activities.Designers.Tests.QuickVariableInput
{
    [TestFixture]
    [SetUpFixture]
    
    public partial class QuickVariableInputViewModelTests
    {
        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("QuickVariableInputViewModel_Constructor")]
        public void QuickVariableInputViewModel_Constructor_PreviewViewModel_NotNull()
        {
            //------------Setup for test--------------------------
            var qviViewModel = new QuickVariableInputViewModelMock();

            //------------Execute Test---------------------------
            var previewViewModel = qviViewModel.PreviewViewModel;

            //------------Assert Results-------------------------
            Assert.IsNotNull(previewViewModel);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("QuickVariableInputViewModel_Constructor")]
        public void QuickVariableInputViewModel_Constructor_PreviewViewModel_PreviewRequestedWiredUp()
        {
            //------------Setup for test--------------------------
            var qviViewModel = new QuickVariableInputViewModelMock();

            //------------Execute Test---------------------------
            qviViewModel.PreviewViewModel.PreviewCommand.Execute(null);

            //------------Assert Results-------------------------
            Assert.AreEqual(1, qviViewModel.DoPreviewHitCount);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("QuickVariableInputViewModel_Constructor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QuickVariableInputViewModel_Constructor_NullAddToCollectionArguments_ExceptionThrown()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
#pragma warning disable 168
            var qviViewModel = new QuickVariableInputViewModel(null);
#pragma warning restore 168
            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("QuickVariableInputViewModel_Constructor")]
        public void QuickVariableInputViewModel_Constructor_WithParameter_SetsDefaultPropertyValues()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var qviViewModel = new QuickVariableInputViewModel((source, overwrite) => { });

            //------------Assert Results-------------------------
            Assert.IsNotNull(qviViewModel);
            Assert.IsInstanceOf(qviViewModel.GetType(), typeof(DependencyObject));
            Assert.AreEqual(string.Empty, qviViewModel.SplitToken);
            Assert.AreEqual(string.Empty, qviViewModel.VariableListString);
            Assert.AreEqual(string.Empty, qviViewModel.Prefix);
            Assert.AreEqual(string.Empty, qviViewModel.Suffix);

            // The following 3 are related - SplitType determines the value of the other 2 properties
            Assert.AreEqual("Chars", qviViewModel.SplitType);
            Assert.IsFalse(qviViewModel.CanAdd);
            Assert.IsTrue(qviViewModel.IsSplitTokenEnabled);
            Assert.IsTrue(qviViewModel.IsOverwriteEnabled);
            Assert.IsTrue(qviViewModel.RemoveEmptyEntries);

            Assert.AreEqual(5, qviViewModel.SplitTypeList.Count);
            CollectionAssert.Contains(qviViewModel.SplitTypeList, QuickVariableInputViewModel.SplitTypeIndex);
            CollectionAssert.Contains(qviViewModel.SplitTypeList, QuickVariableInputViewModel.SplitTypeChars);
            CollectionAssert.Contains(qviViewModel.SplitTypeList, QuickVariableInputViewModel.SplitTypeNewLine);
            CollectionAssert.Contains(qviViewModel.SplitTypeList, QuickVariableInputViewModel.SplitTypeSpace);
            CollectionAssert.Contains(qviViewModel.SplitTypeList, QuickVariableInputViewModel.SplitTypeTab);

            Assert.IsNotNull(qviViewModel.ClearCommand);
            Assert.IsInstanceOf(qviViewModel.ClearCommand.GetType(), typeof(DelegateCommand));
            Assert.IsTrue(qviViewModel.ClearCommand.CanExecute(null));

            Assert.IsNotNull(qviViewModel.AddCommand);
            Assert.IsInstanceOf(qviViewModel.AddCommand.GetType(), typeof(RelayCommand));
            Assert.IsFalse(qviViewModel.AddCommand.CanExecute(null));

            Assert.IsNotNull(qviViewModel.PreviewViewModel);
            Assert.IsInstanceOf(qviViewModel.PreviewViewModel.GetType(), typeof(PreviewViewModel));
            Assert.AreEqual(Visibility.Collapsed, qviViewModel.PreviewViewModel.InputsVisibility);
        }
    }
}
