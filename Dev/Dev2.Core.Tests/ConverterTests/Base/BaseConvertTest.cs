/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common.Interfaces.Core.Convertors.Base;
using Dev2.Converters;
using NUnit.Framework;

namespace Dev2.Tests.ConverterTests.Base
{
    [TestFixture]
    [SetUpFixture]
    public class BaseConvertTest
    {
        static readonly Dev2BaseConversionFactory Fac = new Dev2BaseConversionFactory();
        public static object TestGuard = new object();

        public TestContext TestContext { get; set; }

        [Test]
        
        public void Factory_Can_Create_Converter_Expected_HexConverter()
        {
            var converter = Fac.CreateConverter(enDev2BaseConvertType.Hex);

            Assert.AreEqual(enDev2BaseConvertType.Hex, converter.HandlesType());
        }


        #region Text Test
        [Test]
        public void Broker_Can_Convert_Formats_Expected_Text_to_Text()
        {
            var from = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var to = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "this is a line of text, how does that make you feel";

            var result = broker.Convert(payload);
            const string expected = "this is a line of text, how does that make you feel";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Text_to_Hex()
        {
            var from = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var to = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "this is a line of text, how does that make you feel";

            var result = broker.Convert(payload);
            const string expected = "0x746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Text_to_Base64()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "this is a line of text, how does that make you feel";

            var result = broker.Convert(payload);
            const string expected = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVs";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Text_to_Binary()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "this is a line of text, how does that make you feel";

            var result = broker.Convert(payload);
            const string expected = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101100";

            Assert.AreEqual(expected, result);
        }
        #endregion

        #region hex test
        [Test]
        public void Broker_Can_Convert_Formats_Expected_Hex_to_Hex()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "0x746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            var result = broker.Convert(payload);
            const string expected = "0x746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            // test input
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Hex_to_Text()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "0x746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            var result = broker.Convert(payload);
            const string expected = "this is a line of text, how does that make you feel";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_No_Leading0x_Expected_Hex_to_Text()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            var result = broker.Convert(payload);
            const string expected = "this is a line of text, how does that make you feel";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Hex_to_Binary()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            var result = broker.Convert(payload);
            const string expected = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101100";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Hex_to_Base64()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "0x746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            var result = broker.Convert(payload);
            const string expected = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVs";

            Assert.AreEqual(expected, result);
        }
        #endregion

        #region Base64 Test
        [Test]
        public void Broker_Can_Convert_Formats_Expected_Base64_to_Base64()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVs";

            var result = broker.Convert(payload);
            const string expected = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVs";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Base64_to_Text()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVs";

            var result = broker.Convert(payload);
            const string expected = "this is a line of text, how does that make you feel";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Base64_to_Hex()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVs";

            var result = broker.Convert(payload);
            const string expected = "0x746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Base64_to_Binary()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVs";

            var result = broker.Convert(payload);
            const string expected = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101100";

            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Binary Test
        [Test]
        public void Broker_Can_Convert_Formats_Expected_Binary_to_Binary()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101100";

            var result = broker.Convert(payload);
            const string expected = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101100";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Binary_to_Text()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101100";

            var result = broker.Convert(payload);
            const string expected = "this is a line of text, how does that make you feel";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Binary_to_Hex()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101100";

            var result = broker.Convert(payload);
            const string expected = "0x746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656c";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Broker_Can_Convert_Formats_Expected_Binary_to_Base64()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101100";

            var result = broker.Convert(payload);
            const string expected = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVs";

            Assert.AreEqual(expected, result);
        }

        #endregion

        #region negative test
        [Test]
        [ExpectedException(typeof(BaseTypeException))]
        public void Format_MisMatch_Binary_Expect_Exception()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Binary);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "011101000110100001101001011100110010000001101001011100110010000001100001001000000110110001101001011011100110010100100000011011110110011000100000011101000110010101111000011101000010110000100000011010000110111101110111001000000110010001101111011001010111001100100000011101000110100001100001011101000010000001101101011000010110101101100101001000000111100101101111011101010010000001100110011001010110010101101102";

            broker.Convert(payload);
        }

        [Test]
        [ExpectedException(typeof(BaseTypeException))]
        public void Format_MisMatch_Base64_Expect_Exception()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Base64);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "dGhpcyBpcyBhIGxpbmUgb2YgdGV4dCwgaG93IGRvZXMgdGhhdCBtYWtlIHlvdSBmZWVsqzdfs";

            broker.Convert(payload);
        }

        [Test]
        [ExpectedException(typeof(BaseTypeException))]
        public void Format_MisMatch_Hex_Expect_Exception()
        {
            var to = Fac.CreateConverter(enDev2BaseConvertType.Text);
            var from = Fac.CreateConverter(enDev2BaseConvertType.Hex);
            var broker = Fac.CreateBroker(from, to);

            const string payload = "0x746869732069732061206c696e65206f6620746578742c20686f7720646f65732074686174206d616b6520796f75206665656";

            broker.Convert(payload);
        }
        
        #endregion


    }
}
