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
using Dev2.CustomControls.Converters;
using NUnit.Framework;

namespace Dev2.Core.Tests.AppResources.Converters
{
    [TestFixture]
	[Category("Studio Resources Core")]
    
    public class BoolToStringConvertTests
    {
        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("BoolToStringConvert_Convert")]
        public void BoolToStringConvert_Convert_WhenBooleanIsTrue_ReturnsSuccess()
        {
            //------------Setup for test--------------------------
            var converter = new BoolToStringConvert();
            //------------Execute Test---------------------------
            var result = converter.Convert(true, typeof(string), null, CultureInfo.CurrentCulture);
            //------------Assert Results-------------------------
            Assert.AreEqual("Success", result);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("BoolToStringConvert_Convert")]
        public void BoolToStringConvert_Convert_WhenBooleanIsFalse_ReturnsFailure()
        {
            //------------Setup for test--------------------------
            var converter = new BoolToStringConvert();
            //------------Execute Test---------------------------
            var result = converter.Convert(false, typeof(string), null, CultureInfo.CurrentCulture);
            //------------Assert Results-------------------------
            Assert.AreEqual("Failure", result);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("BoolToStringConvert_Convert")]
        public void BoolToStringConvert_Convert_WhenIsNotBoolean_DefaultsToFailure()
        {
            //------------Setup for test--------------------------
            var converter = new BoolToStringConvert();
            //------------Execute Test---------------------------
            var result = converter.Convert("", typeof(string), null, CultureInfo.CurrentCulture);
            //------------Assert Results-------------------------
            Assert.AreEqual("Failure", result);
        }
    }
}
