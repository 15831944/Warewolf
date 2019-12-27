/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Globalization;
using System.Windows;
using Dev2.AppResources.Converters;
using NUnit.Framework;

namespace Dev2.Core.Tests.AppResources.Converters
{
    [TestFixture]
	[Category("Studio Resources Core")]
    
    public class PaddingThicknessConverterTests
    {
        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("PaddingThicknessConverter_Convert")]
        public void PaddingThicknessConverter_Convert_WhenInputIsEmpty_ReturnsAZeroThicknessValue()
        {
            //------------Setup for test--------------------------
            var converter = new PaddingThicknessConverter();

            //------------Execute Test---------------------------
            var result = converter.Convert("", typeof(string), null, CultureInfo.CurrentCulture);

            //------------Assert Results-------------------------
            Assert.AreEqual(new Thickness(0, 0, 0, 0), result);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("PaddingThicknessConverter_Convert")]
        public void PaddingThicknessConverter_Convert_WhenInputHasData_ReturnsAValidThicknessValue()
        {
            //------------Setup for test--------------------------
            var converter = new PaddingThicknessConverter();

            //------------Execute Test---------------------------
            var result = converter.Convert("D", typeof(string), null, CultureInfo.CurrentCulture);

            //------------Assert Results-------------------------
            Assert.AreEqual(new Thickness(2, 0, 0, 0), result);
        }
    }
}
