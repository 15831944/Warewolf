/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Dev2.Activities.Preview;
using NUnit.Framework;

namespace Dev2.Activities.Designers.Tests.Preview
{
    [TestFixture]
    public class PreviewViewModelTests
    {
        [Test]
        [Author("Trevor Williams-Ros")]
        [NUnit.Framework.Category("PreviewViewModel_Constructor")]
        public void PreviewViewModel_Constructor_Properties_Initialized()
        {
            //------------Execute Test---------------------------
            var previewViewModel = new PreviewViewModel();

            //------------Assert Results-------------------------
            Assert.IsNotNull(previewViewModel.Inputs);
            Assert.IsInstanceOf(previewViewModel.Inputs.GetType(), typeof(ObservableCollection<ObservablePair<string, string>>));
            Assert.AreEqual(0, previewViewModel.Inputs.Count);

            Assert.IsNotNull(previewViewModel.PreviewCommand);
            Assert.AreEqual(Visibility.Visible, previewViewModel.InputsVisibility);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [NUnit.Framework.Category("PreviewViewModel_Output")]
        public void PreviewViewModel_Implementation_INotifyPropertyChanged()
        {
            //------------Execute Test---------------------------
            var previewViewModel = new PreviewViewModel();

            //------------Assert Results-------------------------
            Assert.IsInstanceOf(previewViewModel.GetType(), typeof(INotifyPropertyChanged));
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [NUnit.Framework.Category("PreviewViewModel_Output")]
        public void PreviewViewModel_Output_Changed_ValueSetAndFiresNotifyPropertyChangedEvent()
        {
            //------------Setup for test--------------------------
            const string OutputValue = "Test Output";

            var actualPropertyName = string.Empty;

            var previewViewModel = new PreviewViewModel();
            previewViewModel.PropertyChanged += (sender, args) => actualPropertyName = args.PropertyName;

            //------------Execute Test---------------------------
            previewViewModel.Output = OutputValue;

            //------------Assert Results-------------------------
            Assert.AreEqual("Output", actualPropertyName);
            Assert.AreEqual(OutputValue, previewViewModel.Output);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [NUnit.Framework.Category("PreviewViewModel_Output")]
        public void PreviewViewModel_PreviewCommand_Executed_FiresPreviewRequestedEvent()
        {
            //------------Setup for test--------------------------
            var previewRequested = false;

            var previewViewModel = new PreviewViewModel();
            previewViewModel.PreviewRequested += (sender, args) =>
            {
                previewRequested = true;
            };

            //------------Execute Test---------------------------
            previewViewModel.PreviewCommand.Execute(null);

            //------------Assert Results-------------------------
            Assert.IsTrue(previewRequested);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [NUnit.Framework.Category("PreviewViewModel_Output")]
        public void PreviewViewModel_PreviewCommand_CanExecute_EqualsCanPreview()
        {
            //------------Setup for test--------------------------
            var previewViewModel = new PreviewViewModel();

            //------------Execute Test---------------------------
            previewViewModel.CanPreview = false;
            var canExecuteFalse = previewViewModel.PreviewCommand.CanExecute(null);

            previewViewModel.CanPreview = true;
            var canExecuteTrue = previewViewModel.PreviewCommand.CanExecute(null);

            //------------Assert Results-------------------------
            Assert.IsFalse(canExecuteFalse);
            Assert.IsTrue(canExecuteTrue);
        }
    }
}
