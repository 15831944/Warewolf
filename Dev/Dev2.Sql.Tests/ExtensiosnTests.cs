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
using NUnit.Framework;
using Warewolf.ComponentModel;

namespace Dev2.Sql.Tests
{
    [TestFixture]
    public class ExtensiosnTests
    {
        [Test]
        public void ToStringSafeWithNullExpectedReturnsEmptyString()
        {
            var result = Extensions.ToStringSafe(null);
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void ToStringSafeWithStringExpectedReturnsString()
        {
            const string Expected = "hello";
            var result = Extensions.ToStringSafe(Expected);
            Assert.AreEqual(Expected, result);
        }

        [Test]
        public void ToStringSafeWithDbNullExpectedReturnsEmptyString()
        {
            var result = Extensions.ToStringSafe(Convert.DBNull);
            Assert.AreEqual(string.Empty, result);
        }

    }
}
