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
using Dev2.Common.ExtMethods;
using NUnit.Framework;


namespace Dev2.Tests
{
    /// <summary>
    /// Summary description for TypeExtentionTest
    /// </summary>
    [TestFixture]
    public class TypeExtentionTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Standard Types

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenString")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenString_ExpectString()
        {
            //------------Setup for test--------------------------
            const string myType = "string";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(string), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenBoolean")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenBoolean_ExpectBoolean()
        {
            //------------Setup for test--------------------------
            const string myType = "boolean";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(bool), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenBool")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenBool_ExpectBool()
        {
            //------------Setup for test--------------------------
            const string myType = "bool";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(bool), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenByte")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenByte_ExpectByte()
        {
            //------------Setup for test--------------------------
            const string myType = "byte";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(byte), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenChar")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenChar_ExpectChar()
        {
            //------------Setup for test--------------------------
            const string myType = "char";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(char), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenDateTime")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenDateTime_ExpectDateTime()
        {
            //------------Setup for test--------------------------
            const string myType = "DateTime";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(DateTime), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenDateTimeOffset")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenDateTimeOffset_ExpectDateTimeOffset()
        {
            //------------Setup for test--------------------------
            const string myType = "DateTimeOffset";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(DateTimeOffset), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenDecimal")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenDecimal_ExpectDecimal()
        {
            //------------Setup for test--------------------------
            const string myType = "Decimal";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(Decimal), result);
        }


        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenDouble")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenDouble_ExpectDouble()
        {
            //------------Setup for test--------------------------
            const string myType = "Double";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(Double), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenFloat")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenFloat_ExpectFloat()
        {
            //------------Setup for test--------------------------
            const string myType = "Float";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(float), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenShort")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenShort_ExpectShort()
        {
            //------------Setup for test--------------------------
            const string myType = "Short";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(short), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenInt16")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenInt16_ExpectInt16()
        {
            //------------Setup for test--------------------------
            const string myType = "Int16";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(Int16), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenInt32")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenInt32_ExpectInt32()
        {
            //------------Setup for test--------------------------
            const string myType = "Int32";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(Int32), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenInt")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenInt_ExpectInt()
        {
            //------------Setup for test--------------------------
            const string myType = "Int";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(int), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenInt64")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenInt64_ExpectInt64()
        {
            //------------Setup for test--------------------------
            const string myType = "Int64";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(Int64), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenObject")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenObject_ExpectObject()
        {
            //------------Setup for test--------------------------
            const string myType = "Object";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(Object), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenSByte")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenSByte_ExpectSByte()
        {
            //------------Setup for test--------------------------
            const string myType = "SByte";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(SByte), result);
        }


        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenTimeSpan")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenTimeSpan_ExpectTimeSpan()
        {
            //------------Setup for test--------------------------
            const string myType = "TimeSpan";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(TimeSpan), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenUInt16")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenUInt16_ExpectUInt16()
        {
            //------------Setup for test--------------------------
            const string myType = "UInt16";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(UInt16), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenUShort")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenUShort_ExpectUShort()
        {
            //------------Setup for test--------------------------
            const string myType = "UShort";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(ushort), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenUInt32")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenUInt32_ExpectUInt32()
        {
            //------------Setup for test--------------------------
            const string myType = "UInt32";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(UInt32), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenUInt64")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenUInt64_ExpectUInt64()
        {
            //------------Setup for test--------------------------
            const string myType = "UInt64";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(UInt64), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenULong")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenULong_ExpectULong()
        {
            //------------Setup for test--------------------------
            const string myType = "ULong";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(ulong), result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenGuid")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenGuid_ExpectGuid()
        {
            //------------Setup for test--------------------------
            const string myType = "Guid";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(Guid), result);
        }

        #endregion

        #region Arrays

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenString")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenStringArray_ExpectStringArray()
        {
            //------------Setup for test--------------------------
            const string myType = "string[]";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(string[]), result);
        }


        #endregion

        #region Question Mark

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenString")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenStringWithQuestionMark_ExpectString()
        {
            //------------Setup for test--------------------------
            const string myType = "int?";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(Type.GetType("System.Nullable`1[System.Int32]"), result);
        }


        #endregion

        #region Case

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenString")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenStringAllUpperCase_ExpectString()
        {
            //------------Setup for test--------------------------
            const string myType = "STRING";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(string), result);
        }


        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenString")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenStringAllLowerCase_ExpectString()
        {
            //------------Setup for test--------------------------
            const string myType = "string";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(string), result);
        }


        #endregion

        #region System dot

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenString")]
        public void TypeExtentions_GetTypeFromSimpleName_WhenSystemDotString_ExpectString()
        {
            //------------Setup for test--------------------------
            const string myType = "System.String";

            //------------Execute Test---------------------------

            var result = TypeExtensions.GetTypeFromSimpleName(myType);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(string), result);
        }

        #endregion

        #region Execptions

        [Test]
        [Author("Travis Frisinger")]
        [Category("TypeExtentions_WhenNull")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TypeExtentions_GetTypeFromSimpleName_WhenNull_ExpectNullArgumentException()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------

            TypeExtensions.GetTypeFromSimpleName(null);

            //------------Assert Results-------------------------
        }

        #endregion
    }
}
