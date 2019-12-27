/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Studio.Core.AppResources.Browsers;
using NUnit.Framework;

namespace Dev2.Core.Tests.AppResources.Browsers
{
    [TestFixture]
	[Category("Studio Resources Core")]
    public class BrowserPopupControllerTests
    {
        #region ShowPopup

        [Test]
        public void ExternalBrowserPopupControllerShowPopupExpectedNoUnhandledExceptions()
        {
            var controller = new ExternalBrowserPopupController();
            controller.ShowPopup(null);
        }

        #endregion


    }
}
