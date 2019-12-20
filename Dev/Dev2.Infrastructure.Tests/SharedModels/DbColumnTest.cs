/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Data;
using Dev2.Runtime.ServiceModel.Data;
using NUnit.Framework;

namespace Dev2.Infrastructure.Tests.SharedModels
{
    /// <summary>
    /// When adding logic behind properties... TEST!
    /// </summary>
    [TestFixture]
    [SetUpFixture]
    public class DbColumnTest
    {
        

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_Constructor")]
        public void DbColumn_Constructor_WhenUsingDbTable_ExpectAllPropertiesTransfered()
        {
            //------------Setup for test--------------------------
            var dc = new DataColumn { AllowDBNull = true, AutoIncrement = false, ColumnName = "myColumn", MaxLength = 20, DataType = typeof(string) };
            //------------Execute Test---------------------------
            var dbColumn = new DbColumn(dc);

            //------------Assert Results-------------------------
            Assert.IsTrue(dbColumn.IsNullable);
            Assert.IsFalse(dbColumn.IsAutoIncrement);
            Assert.AreEqual(20, dbColumn.MaxLength);
            Assert.AreEqual(typeof(string), dbColumn.DataType);
            StringAssert.Contains("myColumn", dbColumn.ColumnName);

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_Constructor")]
        public void DbColumn_Constructor_WhenEmptyConstuctor_ExpectDefaultProperties()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var dbColumn = new DbColumn();

            //------------Assert Results-------------------------
            Assert.IsFalse(dbColumn.IsNullable);
            Assert.IsFalse(dbColumn.IsAutoIncrement);
            Assert.AreEqual(0, dbColumn.MaxLength);
            Assert.IsNull(dbColumn.DataType);
            Assert.IsNull(dbColumn.ColumnName);

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenVarCharWithNoMaxLength_ExpectVarCharOfZero()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.VarChar };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "varchar (0)");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenVarCharWithMaxLength_ExpectVarCharOfTen()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.VarChar, MaxLength = 10 };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "varchar (10)");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenCharWithNoMaxLength_ExpectCharOfZero()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Char };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "char (0)");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenCharWithMaxLength_ExpectCharOfTen()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Char, MaxLength = 10 };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "char (10)");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenNVarCharWithNoMaxLength_ExpectNVarCharOfZero()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.NVarChar };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "nvarchar (0)");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenNVarCharWithMaxLength_ExpectNVarCharOfTen()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.NVarChar, MaxLength = 10 };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "nvarchar (10)");

        }


        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenNCharWithNoMaxLength_ExpectNCharOfZero()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.NChar };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "nchar (0)");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenNCharWithMaxLength_ExpectNCharOfTen()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.NChar, MaxLength = 10 };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "nchar (10)");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenBigIntNoMaxLength_ExpectBigInt()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.BigInt };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "bigint");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenBinaryNoMaxLength_ExpectBinary()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Binary };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "binary (0)");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenBinaryMaxLength_ExpectBinaryTen()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Binary, MaxLength = 10 };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "binary (10)");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenBitNoMaxLength_ExpectBit()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Bit };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "bit");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenDateNoMaxLength_ExpectDate()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Date };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "date");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenDateTimeNoMaxLength_ExpectDateTime()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.DateTime };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "datetime");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenDateTime2NoMaxLength_ExpectDateTime2()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.DateTime2 };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "datetime2");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenDateTimeOffsetNoMaxLength_ExpectDateTimeOffset()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.DateTimeOffset };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "datetimeoffset");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenDecimalNoMaxLength_ExpectDecimal()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Decimal };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "decimal");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenFloatNoMaxLength_ExpectFloat()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Float };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "float");

        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenImageNoMaxLength_ExpectImage()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Image };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "image");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenIntNoMaxLength_ExpectInt()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Int };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "int");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenMoneyNoMaxLength_ExpectMoney()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Money };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "money");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenRealNoMaxLength_ExpectReal()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Real };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "real");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenSmallDateTimeNoMaxLength_ExpectSmallDateTime()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.SmallDateTime };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "smalldatetime");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenSmallIntNoMaxLength_ExpectSmallInt()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.SmallInt };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "smallint");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenSmallMoneyNoMaxLength_ExpectSmallMoney()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.SmallMoney };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "smallmoney");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenSctructedNoMaxLength_ExpectStructured()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Structured };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "structured");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenTextNoMaxLength_ExpectText()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Text };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "text");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenTimeNoMaxLength_ExpectTime()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Time };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "time");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenTimestampNoMaxLength_ExpectTimestamp()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Timestamp };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "timestamp");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenTinyIntNoMaxLength_ExpectTinyInt()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.TinyInt };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "tinyint");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenUdtNoMaxLength_ExpectUdt()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Udt };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "udt");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenUniqueIdentifierNoMaxLength_ExpectUniqueIdentifier()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.UniqueIdentifier };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "uniqueidentifier");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenVarBinaryNoMaxLength_ExpectVarBinary()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.VarBinary };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "varbinary (0)");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenVarBinaryWithMaxLength_ExpectVarBinaryTen()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.VarBinary, MaxLength = 10 };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "varbinary (10)");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenVariantNoMaxLength_ExpectVariant()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Variant };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "variant");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("DbColumn_DataTypeName")]
        public void DbColumn_DataTypeName_WhenXmlNoMaxLength_ExpectXml()
        {
            //------------Setup for test--------------------------
            var dbColumn = new DbColumn { SqlDataType = SqlDbType.Xml };

            //------------Execute Test---------------------------
            var result = dbColumn.DataTypeName;

            //------------Assert Results-------------------------
            StringAssert.Contains(result, "xml");
        }


    }
}
