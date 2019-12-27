/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Runtime.ServiceModel.Data;
using NUnit.Framework;


namespace Dev2.Infrastructure.Tests
{
    [TestFixture]
    public class DbTableTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("DbTable_FullName")]
        public void DbTable_FullName_HasSchema_ShouldContainSchemaDotTableName()
        {
            //------------Setup for test--------------------------
            var dbTable = new DbTable { TableName = "Test", Schema = "dbo" };
            //------------Execute Test---------------------------
            var fullName = dbTable.FullName;
            //------------Assert Results-------------------------
            Assert.AreEqual("dbo.Test", fullName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DbTable_FullName")]
        public void DbTable_FullName_HasEmptySchema_ShouldContainTableName()
        {
            //------------Setup for test--------------------------
            var dbTable = new DbTable { TableName = "Test", Schema = "" };
            //------------Execute Test---------------------------
            var fullName = dbTable.FullName;
            //------------Assert Results-------------------------
            Assert.AreEqual("Test", fullName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DbTable_FullName")]
        public void DbTable_FullName_HasNullSchema_ShouldContainTableName()
        {
            //------------Setup for test--------------------------
            var dbTable = new DbTable { TableName = "Test", Schema = null };
            //------------Execute Test---------------------------
            var fullName = dbTable.FullName;
            //------------Assert Results-------------------------
            Assert.AreEqual("Test", fullName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DbTable_FullName")]
        public void DbTable_FullName_HasEmptyTableName_ShouldEmptyString()
        {
            //------------Setup for test--------------------------
            var dbTable = new DbTable { Schema = "Test", TableName = "" };
            //------------Execute Test---------------------------
            var fullName = dbTable.FullName;
            //------------Assert Results-------------------------
            Assert.AreEqual("", fullName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("DbTable_FullName")]
        public void DbTable_FullName_HasTableNameNull_ShouldEmptyString()
        {
            //------------Setup for test--------------------------
            var dbTable = new DbTable { Schema = "Test", TableName = null };
            //------------Execute Test---------------------------
            var fullName = dbTable.FullName;
            //------------Assert Results-------------------------
            Assert.AreEqual("", fullName);
        }
    }
}
