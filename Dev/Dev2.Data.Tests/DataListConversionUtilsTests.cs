/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Data.Interfaces.Enums;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace Dev2.Data.Tests
{
    [TestFixture]
    public class DataListConversionUtilsTests
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(DataListConversionUtils))]
        public void DataListConversionUtils_CreateListToBindTo_GetInputs()
        {
            var scalarListOne = new List<IScalar>
            {
                new Scalar { Name = "[[a]]", Value = "1", IODirection = enDev2ColumnArgumentDirection.Input },
                new Scalar { Name = "[[b]]", Value = "2", IODirection = enDev2ColumnArgumentDirection.Both }
            };

            var scalarListTwo = new List<IScalar>
            {
                new Scalar { Name = "[[a]]", Value = "1", IODirection = enDev2ColumnArgumentDirection.Input },
                new Scalar { Name = "[[b]]", Value = "2", IODirection = enDev2ColumnArgumentDirection.Both }
            };

            var recSetColOne = new Dictionary<int, List<IScalar>> { { 1, scalarListOne } };
            var recSetColTwo = new Dictionary<int, List<IScalar>> { { 2, scalarListTwo } };

            var recordSetList = new List<IRecordSet>
            {
                new RecordSet { Name = "[[rec().a]]", Columns = recSetColOne, Value = "1", IODirection = enDev2ColumnArgumentDirection.Input },
                new RecordSet { Name = "[[rec().b]]", Columns = recSetColTwo, Value = "2", IODirection = enDev2ColumnArgumentDirection.Both }
            };

            var complexObjectsList = new List<IComplexObject>
            {
                new ComplexObject { Name = "@item", Value = "1", IODirection = enDev2ColumnArgumentDirection.Input },
                new ComplexObject { Name = "@newItem", Value = "2", IODirection = enDev2ColumnArgumentDirection.Both }
            };

            var mockDataListModel = new Mock<IDataListModel>();
            mockDataListModel.Setup(dataListModel => dataListModel.Scalars).Returns(scalarListOne);
            mockDataListModel.Setup(dataListModel => dataListModel.RecordSets).Returns(recordSetList);
            mockDataListModel.Setup(dataListModel => dataListModel.ComplexObjects).Returns(complexObjectsList);

            var dataListConversionUtils = new DataListConversionUtils();

            var listItems = dataListConversionUtils.CreateListToBindTo(mockDataListModel.Object);

            NUnit.Framework.Assert.AreEqual(8, listItems.Count);

            NUnit.Framework.Assert.AreEqual("[[a]]", listItems[0].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[a]]", listItems[0].Field);
            NUnit.Framework.Assert.AreEqual("1", listItems[0].Value);
            NUnit.Framework.Assert.IsNull(listItems[0].Index);
            NUnit.Framework.Assert.IsFalse(listItems[0].IsObject);
            NUnit.Framework.Assert.IsNull(listItems[0].Recordset);

            NUnit.Framework.Assert.AreEqual("[[b]]", listItems[1].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[b]]", listItems[1].Field);
            NUnit.Framework.Assert.AreEqual("2", listItems[1].Value);
            NUnit.Framework.Assert.IsNull(listItems[1].Index);
            NUnit.Framework.Assert.IsFalse(listItems[1].IsObject);
            NUnit.Framework.Assert.IsNull(listItems[1].Recordset);

            NUnit.Framework.Assert.AreEqual("[[rec().a]](1).[[a]]", listItems[2].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[a]]", listItems[2].Field);
            NUnit.Framework.Assert.AreEqual("1", listItems[2].Value);
            NUnit.Framework.Assert.AreEqual("1", listItems[2].Index);
            NUnit.Framework.Assert.IsFalse(listItems[2].IsObject);
            NUnit.Framework.Assert.AreEqual("[[rec().a]]", listItems[2].Recordset);

            NUnit.Framework.Assert.AreEqual("[[rec().a]](1).[[b]]", listItems[3].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[b]]", listItems[3].Field);
            NUnit.Framework.Assert.AreEqual("2", listItems[3].Value);
            NUnit.Framework.Assert.AreEqual("1", listItems[3].Index);
            NUnit.Framework.Assert.IsFalse(listItems[3].IsObject);
            NUnit.Framework.Assert.AreEqual("[[rec().a]]", listItems[3].Recordset);

            NUnit.Framework.Assert.AreEqual("[[rec().b]](2).[[a]]", listItems[4].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[a]]", listItems[4].Field);
            NUnit.Framework.Assert.AreEqual("1", listItems[4].Value);
            NUnit.Framework.Assert.AreEqual("2", listItems[4].Index);
            NUnit.Framework.Assert.IsFalse(listItems[4].IsObject);
            NUnit.Framework.Assert.AreEqual("[[rec().b]]", listItems[4].Recordset);

            NUnit.Framework.Assert.AreEqual("[[rec().b]](2).[[b]]", listItems[5].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[b]]", listItems[5].Field);
            NUnit.Framework.Assert.AreEqual("2", listItems[5].Value);
            NUnit.Framework.Assert.AreEqual("2", listItems[5].Index);
            NUnit.Framework.Assert.IsFalse(listItems[5].IsObject);
            NUnit.Framework.Assert.AreEqual("[[rec().b]]", listItems[5].Recordset);

            NUnit.Framework.Assert.AreEqual("@item", listItems[6].DisplayValue);
            NUnit.Framework.Assert.AreEqual("item", listItems[6].Field);
            NUnit.Framework.Assert.AreEqual("1", listItems[6].Value);
            NUnit.Framework.Assert.IsNull(listItems[6].Index);
            NUnit.Framework.Assert.IsTrue(listItems[6].IsObject);
            NUnit.Framework.Assert.IsNull(listItems[6].Recordset);

            NUnit.Framework.Assert.AreEqual("@newItem", listItems[7].DisplayValue);
            NUnit.Framework.Assert.AreEqual("newItem", listItems[7].Field);
            NUnit.Framework.Assert.AreEqual("2", listItems[7].Value);
            NUnit.Framework.Assert.IsNull(listItems[7].Index);
            NUnit.Framework.Assert.IsTrue(listItems[7].IsObject);
            NUnit.Framework.Assert.IsNull(listItems[7].Recordset);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(DataListConversionUtils))]
        public void DataListConversionUtils_CreateListToBindTo_GetOutputs()
        {
            var scalarListOne = new List<IScalar>
            {
                new Scalar { Name = "[[a]]", Value = "1", IODirection = enDev2ColumnArgumentDirection.Output },
                new Scalar { Name = "[[b]]", Value = null, IODirection = enDev2ColumnArgumentDirection.Both }
            };

            var scalarListTwo = new List<IScalar>
            {
                new Scalar { Name = "[[a]]", Value = "1", IODirection = enDev2ColumnArgumentDirection.Output },
                new Scalar { Name = "[[b]]", Value = "2", IODirection = enDev2ColumnArgumentDirection.Both }
            };

            var recSetColOne = new Dictionary<int, List<IScalar>> { { 1, scalarListOne } };
            var recSetColTwo = new Dictionary<int, List<IScalar>> { { 2, scalarListTwo } };

            var recordSetList = new List<IRecordSet>
            {
                new RecordSet { Name = "[[rec().a]]", Columns = recSetColOne, Value = "1", IODirection = enDev2ColumnArgumentDirection.Output },
                new RecordSet { Name = "[[rec().b]]", Columns = recSetColTwo, Value = "2", IODirection = enDev2ColumnArgumentDirection.Both }
            };

            var complexObjectsList = new List<IComplexObject>
            {
                new ComplexObject { Name = "@item", Value = "1", IODirection = enDev2ColumnArgumentDirection.Output },
                new ComplexObject { Name = "@newItem", Value = "2", IODirection = enDev2ColumnArgumentDirection.Both }
            };

            var mockDataListModel = new Mock<IDataListModel>();
            mockDataListModel.Setup(dataListModel => dataListModel.Scalars).Returns(scalarListOne);
            mockDataListModel.Setup(dataListModel => dataListModel.RecordSets).Returns(recordSetList);
            mockDataListModel.Setup(dataListModel => dataListModel.ComplexObjects).Returns(complexObjectsList);

            var dataListConversionUtils = new DataListConversionUtils();

            var listItems = dataListConversionUtils.GetOutputs(mockDataListModel.Object);

            NUnit.Framework.Assert.AreEqual(8, listItems.Count);

            NUnit.Framework.Assert.AreEqual("[[a]]", listItems[0].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[a]]", listItems[0].Field);
            NUnit.Framework.Assert.AreEqual("1", listItems[0].Value);
            NUnit.Framework.Assert.IsNull(listItems[0].Index);
            NUnit.Framework.Assert.IsFalse(listItems[0].IsObject);
            NUnit.Framework.Assert.IsNull(listItems[0].Recordset);

            NUnit.Framework.Assert.AreEqual("[[b]]", listItems[1].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[b]]", listItems[1].Field);
            NUnit.Framework.Assert.IsNull(listItems[1].Value);
            NUnit.Framework.Assert.IsNull(listItems[1].Index);
            NUnit.Framework.Assert.IsFalse(listItems[1].IsObject);
            NUnit.Framework.Assert.IsNull(listItems[1].Recordset);

            NUnit.Framework.Assert.AreEqual("[[rec().a]](1).[[a]]", listItems[2].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[a]]", listItems[2].Field);
            NUnit.Framework.Assert.AreEqual("1", listItems[2].Value);
            NUnit.Framework.Assert.AreEqual("1", listItems[2].Index);
            NUnit.Framework.Assert.IsFalse(listItems[2].IsObject);
            NUnit.Framework.Assert.AreEqual("[[rec().a]]", listItems[2].Recordset);

            NUnit.Framework.Assert.AreEqual("[[rec().a]](1).[[b]]", listItems[3].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[b]]", listItems[3].Field);
            NUnit.Framework.Assert.IsNull(listItems[3].Value);
            NUnit.Framework.Assert.AreEqual("1", listItems[3].Index);
            NUnit.Framework.Assert.IsFalse(listItems[3].IsObject);
            NUnit.Framework.Assert.AreEqual("[[rec().a]]", listItems[3].Recordset);

            NUnit.Framework.Assert.AreEqual("[[rec().b]](2).[[a]]", listItems[4].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[a]]", listItems[4].Field);
            NUnit.Framework.Assert.AreEqual("1", listItems[4].Value);
            NUnit.Framework.Assert.AreEqual("2", listItems[4].Index);
            NUnit.Framework.Assert.IsFalse(listItems[4].IsObject);
            NUnit.Framework.Assert.AreEqual("[[rec().b]]", listItems[4].Recordset);

            NUnit.Framework.Assert.AreEqual("[[rec().b]](2).[[b]]", listItems[5].DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[b]]", listItems[5].Field);
            NUnit.Framework.Assert.AreEqual("2", listItems[5].Value);
            NUnit.Framework.Assert.AreEqual("2", listItems[5].Index);
            NUnit.Framework.Assert.IsFalse(listItems[5].IsObject);
            NUnit.Framework.Assert.AreEqual("[[rec().b]]", listItems[5].Recordset);

            NUnit.Framework.Assert.AreEqual("@item", listItems[6].DisplayValue);
            NUnit.Framework.Assert.AreEqual("item", listItems[6].Field);
            NUnit.Framework.Assert.AreEqual("1", listItems[6].Value);
            NUnit.Framework.Assert.IsNull(listItems[6].Index);
            NUnit.Framework.Assert.IsTrue(listItems[6].IsObject);
            NUnit.Framework.Assert.IsNull(listItems[6].Recordset);

            NUnit.Framework.Assert.AreEqual("@newItem", listItems[7].DisplayValue);
            NUnit.Framework.Assert.AreEqual("newItem", listItems[7].Field);
            NUnit.Framework.Assert.AreEqual("2", listItems[7].Value);
            NUnit.Framework.Assert.IsNull(listItems[7].Index);
            NUnit.Framework.Assert.IsTrue(listItems[7].IsObject);
            NUnit.Framework.Assert.IsNull(listItems[7].Recordset);
        }
    }
}
