/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Windows;
using Dev2.AppResources.Converters;
using NUnit.Framework;

namespace Dev2.Core.Tests.AppResources.Converters
{
    [TestFixture]
	[Category("Studio Resources Core")]
    public class SchedulerMultiBoolToVisibilityConverterTests
    {
        [Test]
        [Author("Massimo Guerrera")]
        [Category("SchedulerMultiBoolToVisibilityConverter_Convert")]
        public void SchedulerMultiBoolToVisibilityConverter_Convert_WithTrueFalse_ReturnsCollapsed()
        {
            var schedulerMultiBoolToVisibilityConverter = new SchedulerMultiBoolToVisibilityConverter();
            object[] values = { true, false };
            var actual = schedulerMultiBoolToVisibilityConverter.Convert(values, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, actual);
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("SchedulerMultiBoolToVisibilityConverter_Convert")]
        public void SchedulerMultiBoolToVisibilityConverter_Convert_WithFalseFalse_ReturnsVisible()
        {
            var schedulerMultiBoolToVisibilityConverter = new SchedulerMultiBoolToVisibilityConverter();
            object[] values = { false, false };
            var actual = schedulerMultiBoolToVisibilityConverter.Convert(values, null, null, null);

            Assert.AreEqual(Visibility.Visible, actual);
        }
    }
}
