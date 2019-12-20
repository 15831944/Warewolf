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
using System.Text;

namespace Dev2.Data.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class DataListTOTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DataListTO))]
        public void DataListTO_OneParamsCTOR_IgnoreColumnDirection_False_ExpectFail()
        {
            //--------------------------Arrange-------------------------------
            var dataList = new StringBuilder(
             "<DataList></DataList>");
            //--------------------------Act-----------------------------------
            var dataListTo = new DataListTO(dataList.ToString());
            //--------------------------Assert--------------------------------
            NUnit.Framework.Assert.AreEqual(0, dataListTo.Inputs.Count);
            NUnit.Framework.Assert.AreEqual(0, dataListTo.Outputs.Count);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DataListTO))]
        public void DataListTO_IgnoreColumnDirection_False_ExpectFail()
        {
            //--------------------------Arrange-------------------------------
            var dataList = new StringBuilder(
             "<DataList></DataList>");
            //--------------------------Act-----------------------------------
            var dataListTo = new DataListTO(dataList.ToString(), false);
            //--------------------------Assert--------------------------------
            NUnit.Framework.Assert.AreEqual(0, dataListTo.Inputs.Count);
            NUnit.Framework.Assert.AreEqual(0, dataListTo.Outputs.Count);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DataListTO))]
        public void DataListTO_IgnoreColumnDirection_True_ExpectSuccess()
        {
            //--------------------------Arrange-------------------------------
            var dataList = new StringBuilder(
             "<DataList>" +
             "<Child1>content</Child1>" +
               "<Child2>" +
                    "<Child3>content</Child3>" +
                    "<Child4>content</Child4>" +
                    "<Child5>content</Child5>" +
               "</Child2>"+
             "</DataList>");

            //--------------------------Act-----------------------------------
            var dataListTo = new DataListTO(dataList.ToString(), true);

            //--------------------------Assert--------------------------------
            NUnit.Framework.Assert.AreEqual(4, dataListTo.Inputs.Count);
            NUnit.Framework.Assert.AreEqual(0, dataListTo.Outputs.Count);

            NUnit.Framework.Assert.AreEqual("Child1", dataListTo.Inputs[0]);
            NUnit.Framework.Assert.AreEqual("[[Child2(*).Child3]]", dataListTo.Inputs[1]);
            NUnit.Framework.Assert.AreEqual("[[Child2(*).Child4]]", dataListTo.Inputs[2]);
            NUnit.Framework.Assert.AreEqual("[[Child2(*).Child5]]", dataListTo.Inputs[3]);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DataListTO))]
        public void DataListTO_IgnoreColumnDirection_False_MapForInputOutput_AllColumnIODirectionSet_IsJson_True_ExpectSuccess()
        {
            //--------------------------Arrange-------------------------------
            var dataList = new StringBuilder(
             "<DataList>" +
             "<Child0 ColumnIODirection='"+ enDev2ColumnArgumentDirection.Input + "' IsJson='True'>content</Child0>" +
             "<Child01 ColumnIODirection='" + enDev2ColumnArgumentDirection.Both + "' IsJson='True'>content</Child01>" +
             "<Child02 ColumnIODirection='" + enDev2ColumnArgumentDirection.Output + "' IsJson='True'>content</Child02>" +
             "<Child03 ColumnIODirection='" + enDev2ColumnArgumentDirection.None + "' IsJson='True'>content</Child03>" +
             "<Child1>content</Child1>" +
               "<Child2>" +
                    "<Child3>content</Child3>" +
                    "<Child4>content</Child4>" +
                    "<Child5>content</Child5>" +
               "</Child2>" +
             "</DataList>");

            //--------------------------Act-----------------------------------
            var dataListTo = new DataListTO(dataList.ToString(), false);

            //--------------------------Assert--------------------------------
            NUnit.Framework.Assert.AreEqual(2, dataListTo.Inputs.Count);
            NUnit.Framework.Assert.AreEqual(2, dataListTo.Outputs.Count);

            NUnit.Framework.Assert.AreEqual("Child0", dataListTo.Inputs[0]);
            NUnit.Framework.Assert.AreEqual("Child01", dataListTo.Inputs[1]);

            NUnit.Framework.Assert.AreEqual("Child01", dataListTo.Outputs[0]);
            NUnit.Framework.Assert.AreEqual("Child02", dataListTo.Outputs[1]);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DataListTO))]
        public void DataListTO_IgnoreColumnDirection_False_MapForInputOutput_AllColumnIODirectionSet_IsJson_False_ExpectFail()
        {
            //--------------------------Arrange-------------------------------
            var dataList = new StringBuilder(
             "<DataList>" +
             "<Child0 ColumnIODirection='" + enDev2ColumnArgumentDirection.Input + "' IsJson='False'>content</Child0>" +
             "<Child01 ColumnIODirection='" + enDev2ColumnArgumentDirection.Both + "' IsJson='False'>content</Child01>" +
             "<Child02 ColumnIODirection='" + enDev2ColumnArgumentDirection.Output + "' IsJson='False'>content</Child02>" +
             "<Child03 ColumnIODirection='" + enDev2ColumnArgumentDirection.None + "' IsJson='False'>content</Child03>" +
             "<Child1>content</Child1>" +
               "<Child2>" +
                    "<Child3>content</Child3>" +
                    "<Child4>content</Child4>" +
                    "<Child5>content</Child5>" +
               "</Child2>" +
             "</DataList>");

            //--------------------------Act-----------------------------------
            var dataListTo = new DataListTO(dataList.ToString(), false);

            //--------------------------Assert--------------------------------
            NUnit.Framework.Assert.AreEqual(2, dataListTo.Inputs.Count);
            NUnit.Framework.Assert.AreEqual(2, dataListTo.Outputs.Count);

            NUnit.Framework.Assert.AreEqual("Child0", dataListTo.Inputs[0]);
            NUnit.Framework.Assert.AreEqual("Child01", dataListTo.Inputs[1]);

            NUnit.Framework.Assert.AreEqual("Child01", dataListTo.Outputs[0]);
            NUnit.Framework.Assert.AreEqual("Child02", dataListTo.Outputs[1]);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DataListTO))]
        public void DataListTO_IgnoreColumnDirection_False_MapForInputOutput_ColumnIODirection_HasElements_True_ExpectSuccess()
        {
            //--------------------------Arrange-------------------------------
            var dataList = new StringBuilder(
             "<DataList>" +
             "<Child0 ColumnIODirection='" + enDev2ColumnArgumentDirection.Input + "' IsJson='False'>content</Child0>" +
             "<Child01 ColumnIODirection='" + enDev2ColumnArgumentDirection.Both + "' IsJson='False'>content</Child01>" +
                    "<Child011>child attribute content</Child011>" +
             "<Child02 ColumnIODirection='" + enDev2ColumnArgumentDirection.Output + "' IsJson='False'>content</Child02>" +
             "<Child03 ColumnIODirection='" + enDev2ColumnArgumentDirection.None + "' IsJson='False'>content</Child03>" +
             "<Child1>content</Child1>" +
               "<Child2>" +
                    "<Child3>content</Child3>" +
                    "<Child4>content</Child4>" +
                    "<Child5>content</Child5>" +
               "</Child2>" +
             "</DataList>");

            //--------------------------Act-----------------------------------
            var dataListTo = new DataListTO(dataList.ToString(), false);

            //--------------------------Assert--------------------------------
            NUnit.Framework.Assert.AreEqual(2, dataListTo.Inputs.Count);
            NUnit.Framework.Assert.AreEqual(2, dataListTo.Outputs.Count);

            NUnit.Framework.Assert.AreEqual("Child0", dataListTo.Inputs[0]);
            NUnit.Framework.Assert.AreEqual("Child01", dataListTo.Inputs[1]);

            NUnit.Framework.Assert.AreEqual("Child01", dataListTo.Outputs[0]);
            NUnit.Framework.Assert.AreEqual("Child02", dataListTo.Outputs[1]);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DataListTO))]
        public void DataListTO_IgnoreColumnDirection_False_MapForInputOutput_ColumnIODirection_HasElements_True_HasGrandChild_ExpectSuccess()
        {
            //--------------------------Arrange-------------------------------
            var dataList = new StringBuilder(
             "<DataList>" +
             "<Child0 ColumnIODirection='" + enDev2ColumnArgumentDirection.Input + "' IsJson='False'>content</Child0>" +
             "<Child01 ColumnIODirection='" + enDev2ColumnArgumentDirection.Both + "' IsJson='False'>content</Child01>" +
                    "<Child011>child attribute content</Child011>" +
             "<Child02 ColumnIODirection='" + enDev2ColumnArgumentDirection.Output + "' IsJson='False'>content</Child02>" +
             "<Child03 ColumnIODirection='" + enDev2ColumnArgumentDirection.None + "' IsJson='False'>content</Child03>" +
             "<Child1>content</Child1>" +
               "<Child2>" +
                    "<Child3 ColumnIODirection='" + enDev2ColumnArgumentDirection.Both + "'>content</Child3>" +
                    "<Child4 ColumnIODirection='" + enDev2ColumnArgumentDirection.Input + "'>content</Child4>" +
                    "<Child5 ColumnIODirection='" + enDev2ColumnArgumentDirection.Output + "'>content</Child5>" +
               "</Child2>" +
             "</DataList>");

            //--------------------------Act-----------------------------------
            var dataListTo = new DataListTO(dataList.ToString(), false);

            //--------------------------Assert--------------------------------
            NUnit.Framework.Assert.AreEqual(4, dataListTo.Inputs.Count);
            NUnit.Framework.Assert.AreEqual(4, dataListTo.Outputs.Count);

            NUnit.Framework.Assert.AreEqual("Child0", dataListTo.Inputs[0]);
            NUnit.Framework.Assert.AreEqual("Child01", dataListTo.Inputs[1]);
            NUnit.Framework.Assert.AreEqual("[[Child2(*).Child3]]", dataListTo.Inputs[2]);
            NUnit.Framework.Assert.AreEqual("[[Child2(*).Child4]]", dataListTo.Inputs[3]);

            NUnit.Framework.Assert.AreEqual("Child01", dataListTo.Outputs[0]);
            NUnit.Framework.Assert.AreEqual("Child02", dataListTo.Outputs[1]);
            NUnit.Framework.Assert.AreEqual("[[Child2(*).Child3]]", dataListTo.Outputs[2]);
            NUnit.Framework.Assert.AreEqual("[[Child2(*).Child5]]", dataListTo.Outputs[3]);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(DataListTO))]
        public void DataListTO_Load()
        {
            const string expectedResult = @"<DataList><Car Description=""A recordset of information about a car"" IsEditable=""True"" ColumnIODirection=""Both"" ><Make Description=""Make of vehicle"" IsEditable=""True"" ColumnIODirection=""None"" /><Model Description=""Model of vehicle"" IsEditable=""True"" ColumnIODirection=""None"" /></Car><Country Description=""name of Country"" IsEditable=""True"" ColumnIODirection=""Both"" /><Person Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ><Age Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ></Age><Name Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ></Name><School Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ><Name Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ></Name><Location Description="""" IsEditable=""True"" IsJson=""True"" IsArray=""False"" ColumnIODirection=""None"" ></Location></School></Person></DataList>";
            var dataList = new DataListTO(expectedResult, true);

            NUnit.Framework.Assert.AreEqual("Country", dataList.Inputs[0]);
            NUnit.Framework.Assert.AreEqual("[[Car(*).Make]]", dataList.Inputs[1]);
            NUnit.Framework.Assert.AreEqual("[[Car(*).Model]]", dataList.Inputs[2]);
            NUnit.Framework.Assert.AreEqual("[[Person(*).Age]]", dataList.Inputs[3]);
            NUnit.Framework.Assert.AreEqual("[[Person(*).Name]]", dataList.Inputs[4]);
            NUnit.Framework.Assert.AreEqual("[[Person(*).School]]", dataList.Inputs[5]);

            NUnit.Framework.Assert.AreEqual(6, dataList.Inputs.Count);
        }
    }
}
