#pragma warning disable
﻿/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Collections.Generic;
using Dev2.Data.Interfaces;
using Dev2.Data.Util;
using NUnit.Framework;
using Moq;

namespace Dev2.Data.Tests.Util
{
    [TestFixture]
    [SetUpFixture]
    public class CommonRecordSetUtilTests
    {
        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ReplaceRecordBlankWithStar()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("[[rec(*)]]", instance.ReplaceRecordBlankWithStar("[[rec(*)]]"));
            NUnit.Framework.Assert.AreEqual("[[rec(*)]]", instance.ReplaceRecordBlankWithStar("[[rec()]]"));
            NUnit.Framework.Assert.AreEqual("[[rec]]", instance.ReplaceRecordBlankWithStar("[[rec]]"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ReplaceRecordsetBlankWithStar()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("[[rec(*).n]]", instance.ReplaceRecordsetBlankWithStar("[[rec().n]]"));
            NUnit.Framework.Assert.AreEqual("[[rec()]]", instance.ReplaceRecordsetBlankWithStar("[[rec()]]"));
            NUnit.Framework.Assert.AreEqual("[[rec(a)]]", instance.ReplaceRecordsetBlankWithStar("[[rec(a)]]"));
            NUnit.Framework.Assert.AreEqual("[[rec]]", instance.ReplaceRecordsetBlankWithStar("[[rec]]"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ReplaceRecordsetBlankWithIndex()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("[[rec(*).a]]", instance.ReplaceRecordsetBlankWithIndex("[[rec(*).a]]", 2));
            NUnit.Framework.Assert.AreEqual("[[rec(2).a]]", instance.ReplaceRecordsetBlankWithIndex("[[rec().a]]", 2));
            NUnit.Framework.Assert.AreEqual("[[rec]]", instance.ReplaceRecordsetBlankWithIndex("[[rec]]", 2));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ReplaceObjectBlankWithIndex()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("[[rec(*)]]", instance.ReplaceObjectBlankWithIndex("[[rec(*)]]", 2));
            NUnit.Framework.Assert.AreEqual("[[rec(2)]]", instance.ReplaceObjectBlankWithIndex("[[rec()]]", 2));
            NUnit.Framework.Assert.AreEqual("[[rec]]", instance.ReplaceObjectBlankWithIndex("[[rec]]", 2));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_CreateRecordsetDisplayValue()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("rec(2).col1", instance.CreateRecordsetDisplayValue("rec", "col1", "2"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_RemoveRecordsetBracketsFromValue()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("[[rec(*)]]", instance.RemoveRecordsetBracketsFromValue("[[rec(*)]]"));
            NUnit.Framework.Assert.AreEqual("rec(*)", instance.RemoveRecordsetBracketsFromValue("rec(*)"));
            NUnit.Framework.Assert.AreEqual("rec", instance.RemoveRecordsetBracketsFromValue("rec"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_GetRecordsetIndexType()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual(Interfaces.Enums.enRecordsetIndexType.Blank, instance.GetRecordsetIndexType("[[rec()]]"));
            NUnit.Framework.Assert.AreEqual(Interfaces.Enums.enRecordsetIndexType.Error, instance.GetRecordsetIndexType("[[rec(a)]]"));
            NUnit.Framework.Assert.AreEqual(Interfaces.Enums.enRecordsetIndexType.Numeric, instance.GetRecordsetIndexType("[[rec(3)]]"));
            NUnit.Framework.Assert.AreEqual(Interfaces.Enums.enRecordsetIndexType.Star, instance.GetRecordsetIndexType("[[rec(*)]]"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_IsStarIndex()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual(true, instance.IsStarIndex("[[rec(*)]]"));
            NUnit.Framework.Assert.AreEqual(false, instance.IsStarIndex("[[rec()]]"));
            NUnit.Framework.Assert.AreEqual(false, instance.IsStarIndex(""));
            NUnit.Framework.Assert.AreEqual(false, instance.IsStarIndex(null));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ExtractIndexRegionFromRecordset()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("*", instance.ExtractIndexRegionFromRecordset("[[rec(*)]]"));
            NUnit.Framework.Assert.AreEqual("a", instance.ExtractIndexRegionFromRecordset("[[rec(a)]]"));
            NUnit.Framework.Assert.AreEqual("2", instance.ExtractIndexRegionFromRecordset("[[rec(2)]]"));
            NUnit.Framework.Assert.AreEqual("2", instance.ExtractIndexRegionFromRecordset("[[rec(2"));
            NUnit.Framework.Assert.AreEqual("", instance.ExtractIndexRegionFromRecordset("[[rec]]"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_MakeValueIntoHighLevelRecordset()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("rec(*)", instance.MakeValueIntoHighLevelRecordset("[[rec]]", true));
            NUnit.Framework.Assert.AreEqual("rec()", instance.MakeValueIntoHighLevelRecordset("[[rec(]]", true));
            NUnit.Framework.Assert.AreEqual("rec(*)", instance.MakeValueIntoHighLevelRecordset("[[rec)]]", true));
            NUnit.Framework.Assert.AreEqual("rec()", instance.MakeValueIntoHighLevelRecordset("[[rec]]", false));
            NUnit.Framework.Assert.AreEqual("rec()", instance.MakeValueIntoHighLevelRecordset("[[rec(]]", false));
            NUnit.Framework.Assert.AreEqual("rec()", instance.MakeValueIntoHighLevelRecordset("[[rec)]]", false));
            NUnit.Framework.Assert.AreEqual("rec(*)", instance.MakeValueIntoHighLevelRecordset("rec", true));
            NUnit.Framework.Assert.AreEqual("rec()", instance.MakeValueIntoHighLevelRecordset("rec(", true));
            NUnit.Framework.Assert.AreEqual("rec(*)", instance.MakeValueIntoHighLevelRecordset("rec)", true));
            NUnit.Framework.Assert.AreEqual("rec()", instance.MakeValueIntoHighLevelRecordset("rec", false));
            NUnit.Framework.Assert.AreEqual("rec()", instance.MakeValueIntoHighLevelRecordset("rec(", false));
            NUnit.Framework.Assert.AreEqual("rec()", instance.MakeValueIntoHighLevelRecordset("rec)", false));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ExtractFieldNameOnlyFromValue()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("bab", instance.ExtractFieldNameOnlyFromValue("[[rec(*).bab]]"));
            NUnit.Framework.Assert.AreEqual("ab", instance.ExtractFieldNameOnlyFromValue("rec(*).ab"));
            NUnit.Framework.Assert.AreEqual("ab", instance.ExtractFieldNameOnlyFromValue("[[rec().ab]]"));
            NUnit.Framework.Assert.AreEqual("ab", instance.ExtractFieldNameOnlyFromValue("[[rec().ab"));
            NUnit.Framework.Assert.AreEqual("", instance.ExtractFieldNameOnlyFromValue("[[rec()."));
            NUnit.Framework.Assert.AreEqual("", instance.ExtractFieldNameOnlyFromValue("[[rec()"));
            NUnit.Framework.Assert.AreEqual("Name", instance.ExtractFieldNameOnlyFromValue("[[rec().Name]].sdgager()"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ExtractFieldNameFromValue()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("bab", instance.ExtractFieldNameFromValue("[[rec(*).bab]]"));
            NUnit.Framework.Assert.AreEqual("ab", instance.ExtractFieldNameFromValue("rec(*).ab"));
            NUnit.Framework.Assert.AreEqual("ab", instance.ExtractFieldNameFromValue("[[rec().ab]]"));
            NUnit.Framework.Assert.AreEqual("ab", instance.ExtractFieldNameFromValue("[[rec().ab"));
            NUnit.Framework.Assert.AreEqual("", instance.ExtractFieldNameFromValue("[[rec()."));
            NUnit.Framework.Assert.AreEqual("", instance.ExtractFieldNameFromValue("[[rec()"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ExtractRecordsetNameFromValue()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("rec", instance.ExtractRecordsetNameFromValue("[[rec(*).bab]]"));
            NUnit.Framework.Assert.AreEqual("rec", instance.ExtractRecordsetNameFromValue("rec(*).ab"));
            NUnit.Framework.Assert.AreEqual("rec", instance.ExtractRecordsetNameFromValue("[[rec().ab]]"));
            NUnit.Framework.Assert.AreEqual("rec", instance.ExtractRecordsetNameFromValue("[[rec().ab"));
            NUnit.Framework.Assert.AreEqual("rec", instance.ExtractRecordsetNameFromValue("[[rec()."));
            NUnit.Framework.Assert.AreEqual("rec", instance.ExtractRecordsetNameFromValue("[[rec()"));
            NUnit.Framework.Assert.AreEqual("", instance.ExtractRecordsetNameFromValue("rec"));
            NUnit.Framework.Assert.AreEqual("", instance.ExtractRecordsetNameFromValue("[[rec]]"));
            NUnit.Framework.Assert.AreEqual("", instance.ExtractRecordsetNameFromValue(null));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_IsValueRecordsetWithFields()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual(false, instance.IsValueRecordsetWithFields(null));
            NUnit.Framework.Assert.AreEqual(false, instance.IsValueRecordsetWithFields(""));
            NUnit.Framework.Assert.AreEqual(false, instance.IsValueRecordsetWithFields("a"));
            NUnit.Framework.Assert.AreEqual(false, instance.IsValueRecordsetWithFields("[[rec(*)]]"));
            NUnit.Framework.Assert.AreEqual(false, instance.IsValueRecordsetWithFields("[[rec()]]"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordsetWithFields("[[rec(*).a]]"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordsetWithFields("[[rec(*).asdf]]"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordsetWithFields("rec(*).a"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordsetWithFields("rec(*).asdf"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_IsValueRecordset()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual(false, instance.IsValueRecordset(null));
            NUnit.Framework.Assert.AreEqual(false, instance.IsValueRecordset(""));
            NUnit.Framework.Assert.AreEqual(false, instance.IsValueRecordset("a"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordset("[[rec(*)]]"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordset("[[rec()]]"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordset("[[rec(*).a]]"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordset("[[rec(*).asdf]]"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordset("rec(*).a"));
            NUnit.Framework.Assert.AreEqual(true, instance.IsValueRecordset("rec(*).asdf"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ReplaceRecordsetIndexWithStar()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("[[rec(*)]]", instance.ReplaceRecordsetIndexWithStar("[[rec(*)]]"));
            NUnit.Framework.Assert.AreEqual("[[rec(*)]]", instance.ReplaceRecordsetIndexWithStar("[[rec(2)]]"));
            NUnit.Framework.Assert.AreEqual("[[rec(*).a]]", instance.ReplaceRecordsetIndexWithStar("[[rec(2).a]]"));
            NUnit.Framework.Assert.AreEqual("rec(*).a", instance.ReplaceRecordsetIndexWithStar("rec(2).a"));
            NUnit.Framework.Assert.AreEqual("[[rec(*)]]", instance.ReplaceRecordsetIndexWithStar("[[rec(a)]]"));
            NUnit.Framework.Assert.AreEqual("[[rec()]]", instance.ReplaceRecordsetIndexWithStar("[[rec()]]"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ReplaceRecordsetIndexWithBlank()
        {
            var instance = new CommonRecordSetUtil();

            NUnit.Framework.Assert.AreEqual("[[rec()]]", instance.ReplaceRecordsetIndexWithBlank("[[rec(*)]]"));
            NUnit.Framework.Assert.AreEqual("[[rec()]]", instance.ReplaceRecordsetIndexWithBlank("[[rec(2)]]"));
            NUnit.Framework.Assert.AreEqual("[[rec().a]]", instance.ReplaceRecordsetIndexWithBlank("[[rec(2).a]]"));
            NUnit.Framework.Assert.AreEqual("rec().a", instance.ReplaceRecordsetIndexWithBlank("rec(2).a"));
            NUnit.Framework.Assert.AreEqual("[[rec()]]", instance.ReplaceRecordsetIndexWithBlank("[[rec(a)]]"));
            NUnit.Framework.Assert.AreEqual("[[rec()]]", instance.ReplaceRecordsetIndexWithBlank("[[rec()]]"));
            NUnit.Framework.Assert.AreEqual("()", instance.ReplaceRecordsetIndexWithBlank("[[rec)(]]"));
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_RemoveRecordSetBraces()
        {
            var instance = new CommonRecordSetUtil();
            var boolV = false;
            NUnit.Framework.Assert.AreEqual("rec", instance.RemoveRecordSetBraces("rec(*)", ref boolV));
            NUnit.Framework.Assert.IsTrue(boolV);
            boolV = false;
            NUnit.Framework.Assert.AreEqual("rec", instance.RemoveRecordSetBraces("rec()", ref boolV));
            NUnit.Framework.Assert.IsTrue(boolV);
            boolV = false;
            NUnit.Framework.Assert.AreEqual("a", instance.RemoveRecordSetBraces("a", ref boolV));
            NUnit.Framework.Assert.IsFalse(boolV);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ProcessRecordSetFields()
        {
            var instance = new CommonRecordSetUtil();

            var payload = new Mock<IParseTO>().Object;
            bool addCompleteParts = false;
            var result = new List<IIntellisenseResult>();
            var mockChild = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChild.Setup(o => o.Name).Returns("part1");
            mockChild.Setup(o => o.Description).Returns("mockchildintellip");
            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChild.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            instance.ProcessRecordSetFields(payload, addCompleteParts, result, intellisensePart);

            NUnit.Framework.Assert.AreEqual(4, result.Count);
            NUnit.Framework.Assert.AreEqual("mockintellip1 / Select a specific row or Close", result[0].Message);
            NUnit.Framework.Assert.AreEqual("mockintellip1 / Takes all rows ", result[1].Message);
            NUnit.Framework.Assert.AreEqual("mockintellip1 / Take last row", result[2].Message);
            NUnit.Framework.Assert.AreEqual("mockintellip1 / Use the field of a Recordset", result[3].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ProcessRecordSetFields_AddCompleteParts()
        {
            var instance = new CommonRecordSetUtil();

            var payload = new Mock<IParseTO>().Object;
            bool addCompleteParts = true;
            var result = new List<IIntellisenseResult>();
            var mockChild = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChild.Setup(o => o.Name).Returns("part1");
            mockChild.Setup(o => o.Description).Returns("mockchildintellip");
            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChild.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            instance.ProcessRecordSetFields(payload, addCompleteParts, result, intellisensePart);

            NUnit.Framework.Assert.AreEqual(3, result.Count);
            NUnit.Framework.Assert.AreEqual("mockintellip1 / Takes all rows ", result[0].Message);
            NUnit.Framework.Assert.AreEqual("mockintellip1 / Take last row", result[1].Message);
            NUnit.Framework.Assert.AreEqual("mockintellip1 / Use the field of a Recordset", result[2].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ProcessNonRecordsetFieldsWithParent()
        {
            var instance = new CommonRecordSetUtil();

            var mockPayload = new Mock<IParseTO>();
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockParent = new Mock<IParseTO>();
            mockParent.Setup(o => o.Payload).Returns("rec()");
            mockPayload.Setup(o => o.Parent).Returns(mockParent.Object);

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var intellisensePart = mockIntellisensePart.Object;

            instance.ProcessNonRecordsetFields(payload, result, intellisensePart);

            NUnit.Framework.Assert.AreEqual(1, result.Count);

            NUnit.Framework.Assert.AreEqual("mockintellip1Desc / Use row at this index", result[0].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ProcessNonRecordsetFieldsWithNonRecordsetParent()
        {
            var instance = new CommonRecordSetUtil();

            var mockPayload = new Mock<IParseTO>();
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockParent = new Mock<IParseTO>();
            mockParent.Setup(o => o.Payload).Returns("somename");
            mockPayload.Setup(o => o.Parent).Returns(mockParent.Object);

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var intellisensePart = mockIntellisensePart.Object;

            instance.ProcessNonRecordsetFields(payload, result, intellisensePart);

            NUnit.Framework.Assert.AreEqual(1, result.Count);

            NUnit.Framework.Assert.AreEqual("mockintellip1Desc", result[0].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ProcessNonRecordsetFields()
        {
            var instance = new CommonRecordSetUtil();

            var mockPayload = new Mock<IParseTO>();
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var intellisensePart = mockIntellisensePart.Object;

            instance.ProcessNonRecordsetFields(payload, result, intellisensePart);

            NUnit.Framework.Assert.AreEqual(1, result.Count);

            NUnit.Framework.Assert.AreEqual("mockintellip1Desc", result[0].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ProcessRecordSetMatch()
        {
            var instance = new CommonRecordSetUtil();

            var mockChild = new Mock<IParseTO>();
            mockChild.Setup(o => o.Payload).Returns("childpayload");

            var mockPayload = new Mock<IParseTO>();
            mockPayload.Setup(o => o.Payload).Returns("recset");
            mockPayload.Setup(o => o.IsLeaf).Returns(false);
            mockPayload.Setup(o => o.Child).Returns(mockChild.Object);
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var mockChildIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChildIntellisensePart.Setup(o => o.Name).Returns("childintellipartName");
            mockChildIntellisensePart.Setup(o => o.Description).Returns("childintellipartDesc");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChildIntellisensePart.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            instance.ProcessRecordSetMatch(payload, result, "rawrec", "searchrec", intellisensePart);

            NUnit.Framework.Assert.AreEqual(2, result.Count);
            NUnit.Framework.Assert.AreEqual("mockintellip1Desc / Select a specific row", result[0].Message);
            NUnit.Framework.Assert.AreEqual("childintellipartDesc / Select a specific field at a specific row", result[1].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_ProcessRecordSetMatch_NoChildren()
        {
            var instance = new CommonRecordSetUtil();

            var mockChild = new Mock<IParseTO>();
            mockChild.Setup(o => o.Payload).Returns("childpayload");

            var mockPayload = new Mock<IParseTO>();
            mockPayload.Setup(o => o.Payload).Returns("recset");
            mockPayload.Setup(o => o.IsLeaf).Returns(true);
            mockPayload.Setup(o => o.Child).Returns(mockChild.Object);
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var mockChildIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChildIntellisensePart.Setup(o => o.Name).Returns("childintellipartName");
            mockChildIntellisensePart.Setup(o => o.Description).Returns("childintellipartDesc");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChildIntellisensePart.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            instance.ProcessRecordSetMatch(payload, result, "rawrec", "searchrec", intellisensePart);

            NUnit.Framework.Assert.AreEqual(2, result.Count);
            NUnit.Framework.Assert.AreEqual("mockintellip1Desc / Select a specific row", result[0].Message);
            NUnit.Framework.Assert.AreEqual("childintellipartDesc / Select a specific field at a specific row", result[1].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_AddRecordSetIndex()
        {
            var instance = new CommonRecordSetUtil();

            var mockChild = new Mock<IParseTO>();
            mockChild.Setup(o => o.Payload).Returns("childpayload");

            var mockPayload = new Mock<IParseTO>();
            mockPayload.Setup(o => o.Payload).Returns("recset");
            mockPayload.Setup(o => o.IsLeaf).Returns(true);
            mockPayload.Setup(o => o.Child).Returns(mockChild.Object);
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var mockChildIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChildIntellisensePart.Setup(o => o.Name).Returns("childintellipartName");
            mockChildIntellisensePart.Setup(o => o.Description).Returns("childintellipartDesc");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChildIntellisensePart.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            bool addCompleteParts = false;
            string[] parts = { "rec(1)" };
            bool emptyOk = false;

            NUnit.Framework.Assert.AreEqual(true, instance.AddRecordSetIndex(payload, addCompleteParts, result, parts, intellisensePart, emptyOk));
            NUnit.Framework.Assert.AreEqual(0, result.Count);

            addCompleteParts = true;
            NUnit.Framework.Assert.AreEqual(false, instance.AddRecordSetIndex(payload, addCompleteParts, result, parts, intellisensePart, emptyOk));

            NUnit.Framework.Assert.AreEqual(1, result.Count);
            NUnit.Framework.Assert.AreEqual("mockintellip1Desc", result[0].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_RecordsetMatch()
        {
            var instance = new CommonRecordSetUtil();

            var mockChild = new Mock<IParseTO>();
            mockChild.Setup(o => o.Payload).Returns("childpayload");

            var mockPayload = new Mock<IParseTO>();
            mockPayload.Setup(o => o.HangingOpen).Returns(true);
            mockPayload.Setup(o => o.Payload).Returns("recset");
            mockPayload.Setup(o => o.IsLeaf).Returns(true);
            mockPayload.Setup(o => o.Child).Returns(mockChild.Object);
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var mockChildIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChildIntellisensePart.Setup(o => o.Name).Returns("childintellipartName");
            mockChildIntellisensePart.Setup(o => o.Description).Returns("childintellipartDesc");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChildIntellisensePart.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            bool addCompleteParts = false;
            string[] parts = { "rec(1)" };
            bool emptyOk = false;

            NUnit.Framework.Assert.AreEqual(false, instance.RecordsetMatch(payload, addCompleteParts, result, "rawsearch", "search", emptyOk, parts, intellisensePart));

            NUnit.Framework.Assert.AreEqual(2, result.Count);
            NUnit.Framework.Assert.AreEqual("mockintellip1Desc / Select a specific row", result[0].Message);
            NUnit.Framework.Assert.AreEqual("childintellipartDesc / Select a specific field at a specific row", result[1].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_RecordsetMatch_NoHangingOpen()
        {
            var instance = new CommonRecordSetUtil();

            var mockChild = new Mock<IParseTO>();
            mockChild.Setup(o => o.Payload).Returns("childpayload");

            var mockPayload = new Mock<IParseTO>();
            mockPayload.Setup(o => o.HangingOpen).Returns(false);
            mockPayload.Setup(o => o.Payload).Returns("recset");
            mockPayload.Setup(o => o.IsLeaf).Returns(true);
            mockPayload.Setup(o => o.Child).Returns(mockChild.Object);
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var mockChildIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChildIntellisensePart.Setup(o => o.Name).Returns("childintellipartName");
            mockChildIntellisensePart.Setup(o => o.Description).Returns("childintellipartDesc");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChildIntellisensePart.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            bool addCompleteParts = false;
            string[] parts = { "rec(1)" };
            bool emptyOk = false;

            NUnit.Framework.Assert.AreEqual(true, instance.RecordsetMatch(payload, addCompleteParts, result, "rawsearch", "search", emptyOk, parts, intellisensePart));

            NUnit.Framework.Assert.AreEqual(0, result.Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_OpenRecordsetItem()
        {
            var instance = new CommonRecordSetUtil();

            var mockChild = new Mock<IParseTO>();
            mockChild.Setup(o => o.Payload).Returns("childpayload");

            var mockPayload = new Mock<IParseTO>();
            mockPayload.Setup(o => o.HangingOpen).Returns(false);
            mockPayload.Setup(o => o.Payload).Returns("recset");
            mockPayload.Setup(o => o.IsLeaf).Returns(true);
            mockPayload.Setup(o => o.Child).Returns(mockChild.Object);
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var mockChildIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChildIntellisensePart.Setup(o => o.Name).Returns("childintellipartName");
            mockChildIntellisensePart.Setup(o => o.Description).Returns("childintellipartDesc");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChildIntellisensePart.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            bool addCompleteParts = false;
            string[] parts = { "rec(1)" };
            bool emptyOk = false;

            instance.OpenRecordsetItem(payload, result, intellisensePart);

            NUnit.Framework.Assert.AreEqual(2, result.Count);
            NUnit.Framework.Assert.AreEqual(" / Select a specific row", result[0].Message);
            NUnit.Framework.Assert.AreEqual(" / Select a specific row", result[1].Message);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(CommonRecordSetUtil))]
        public void CommonRecordSetUtil_OpenRecordsetItem_MalformedIndex()
        {
            var instance = new CommonRecordSetUtil();

            var mockChild = new Mock<IParseTO>();
            mockChild.Setup(o => o.Payload).Returns("childpayload()");

            var mockPayload = new Mock<IParseTO>();
            mockPayload.Setup(o => o.HangingOpen).Returns(false);
            mockPayload.Setup(o => o.Payload).Returns("recset(");
            mockPayload.Setup(o => o.IsLeaf).Returns(true);
            mockPayload.Setup(o => o.Child).Returns(mockChild.Object);
            var payload = mockPayload.Object;
            var result = new List<IIntellisenseResult>();

            var mockIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockIntellisensePart.Setup(o => o.Name).Returns("mockintellip1Name");
            mockIntellisensePart.Setup(o => o.Description).Returns("mockintellip1Desc");
            var mockChildIntellisensePart = new Mock<IDev2DataLanguageIntellisensePart>();
            mockChildIntellisensePart.Setup(o => o.Name).Returns("childintellipartName");
            mockChildIntellisensePart.Setup(o => o.Description).Returns("childintellipartDesc");
            mockIntellisensePart.Setup(o => o.Children).Returns(new List<IDev2DataLanguageIntellisensePart>
            {
                mockChildIntellisensePart.Object,
            });
            var intellisensePart = mockIntellisensePart.Object;

            bool addCompleteParts = false;
            string[] parts = { "rec(1)" };
            bool emptyOk = false;

            instance.OpenRecordsetItem(payload, result, intellisensePart);

            NUnit.Framework.Assert.AreEqual(2, result.Count);
            NUnit.Framework.Assert.AreEqual("[[recset([[childpayload(]])]]", result[0].Option.DisplayValue);
            NUnit.Framework.Assert.AreEqual("[[recset([[childpayload(]]).childintellipartName]]", result[1].Option.DisplayValue);
        }
    }
}
