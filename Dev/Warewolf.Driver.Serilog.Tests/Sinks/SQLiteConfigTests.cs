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
using Warewolf.Driver.Serilog;

namespace Warewolf.Tests.Sinks
{
    [TestFixture]
    public class SQLiteConfigTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(SeriLogSQLiteConfig))]
        public void SeriLogSQLiteConfig_NoParamConstructor_Returns_Default()
        {
            //---------------------------------Arrange-----------------------------
            var sqliteConfig = new SeriLogSQLiteConfig();
            //---------------------------------Act---------------------------------
            //---------------------------------Assert------------------------------
            Assert.AreEqual(expected: @"C:\ProgramData\Warewolf\Audits\AuditDB.db", actual: sqliteConfig.ConnectionString);
            Assert.IsNotNull(sqliteConfig.Logger);
            Assert.IsNull(sqliteConfig.ServerLoggingAddress);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(SeriLogSQLiteConfig))]
        public void SeriLogSQLiteConfig_WithParamConstructor_Returns_Correct_Settings()
        {
            //---------------------------------Arrange-----------------------------
            var settings = new SeriLogSQLiteConfig.Settings
            {
                Database = "testDB.db",
                Path = @"C:\ProgramData\Warewolf\Tests",
                TableName = "testTableName"
            };
            var sqliteConfig = new SeriLogSQLiteConfig(settings);
            //---------------------------------Act---------------------------------
            //---------------------------------Assert------------------------------
            Assert.AreEqual(expected: @"C:\ProgramData\Warewolf\Tests\testDB.db", actual: sqliteConfig.ConnectionString);
            Assert.IsNotNull(sqliteConfig.Logger);
            Assert.IsNull(sqliteConfig.ServerLoggingAddress);
        }
    }
}
