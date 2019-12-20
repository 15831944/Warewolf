/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using NUnit.Framework;

namespace Dev2.Core.Tests.Settings
{
    [TestFixture]
    [SetUpFixture]
    [Category("Studio Settings Core")]
    public class SettingsItemViewModelTests
    {
        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("SettingsItemViewModel_Constructor")]
        public void SettingsItemViewModel_Constructor_Properties_Initialized()
        {
            //------------Setup for test--------------------------

            //------------Execute Test---------------------------
            var settingsItemViewModel = new TestSettingsItemViewModel();

            //------------Assert Results-------------------------
            Assert.IsNotNull(settingsItemViewModel.CloseHelpCommand);
        }
    }
}
