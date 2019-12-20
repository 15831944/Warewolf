/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common.Interfaces;
using Dev2.Data.TO;
using NUnit.Framework;

namespace Dev2.Data.Tests.TO
{
    [TestFixture]
    [SetUpFixture]
    public class IntellisenseFilterOpsTOTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(IntellisenseFilterOpsTO))]
        public void IntellisenseFilterOpsTO_SetProperty_IsEqual_SetValue()
        {
            //-----------------------Arrange------------------------
            var intellisenseFilterOpsTO = new IntellisenseFilterOpsTO();
            //-----------------------Act----------------------------
            intellisenseFilterOpsTO.FilterCondition = "TestFilterCondition";
            intellisenseFilterOpsTO.FilterType = enIntellisensePartType.JsonObject;
            //-----------------------Assert-------------------------
            NUnit.Framework.Assert.AreEqual("TestFilterCondition", intellisenseFilterOpsTO.FilterCondition);
            NUnit.Framework.Assert.AreEqual(enIntellisensePartType.JsonObject, intellisenseFilterOpsTO.FilterType);
        }
    }
}
