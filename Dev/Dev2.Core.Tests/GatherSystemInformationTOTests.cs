/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common.Interfaces.Interfaces;
using Dev2.Data.Interfaces.Enums;
using NUnit.Framework;


namespace Dev2.Tests
{
    [TestFixture]
    public class GatherSystemInformationTOTests
    {
        [Test]
        public void GatherSystemInformationTOShouldImplementIDev2TOFn()
        {
            //------------Setup for test--------------------------
            
            //------------Execute Test---------------------------
            var informationTO = new GatherSystemInformationTO();
            //------------Assert Results-------------------------
            Assert.IsInstanceOf(typeof(IDev2TOFn), informationTO);
        }

        [Test]
        public void ConstructorWhereParametersExpectSetsProperties()
        {
            //------------Setup for test--------------------------
            const string ResultVariable = "[[Output]]";
            const enTypeOfSystemInformationToGather TOGather = enTypeOfSystemInformationToGather.OperatingSystem;
            const int IndexNumber = 0;
            //------------Execute Test---------------------------
            var gatherSystemInformationTO = new GatherSystemInformationTO(TOGather,ResultVariable,IndexNumber);
            //------------Assert Results-------------------------
            Assert.IsNotNull(gatherSystemInformationTO);
            Assert.AreEqual(ResultVariable,gatherSystemInformationTO.Result);
            Assert.AreEqual(TOGather,gatherSystemInformationTO.EnTypeOfSystemInformation);
            Assert.AreEqual(IndexNumber,gatherSystemInformationTO.IndexNumber);

        }

        #region CanAdd Tests

        [Test]
        [Author("Massimo Guerrera")]
        [Category("GatherSystemInformationTO_CanAdd")]
        public void GatherSystemInformationTO_CanAdd_ResultEmpty_ReturnFalse()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var gatherSystemInformationTO = new GatherSystemInformationTO { Result = string.Empty };
            //------------Assert Results-------------------------
            Assert.IsFalse(gatherSystemInformationTO.CanAdd());
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("GatherSystemInformationTO_CanAdd")]
        public void GatherSystemInformationTO_CanAdd_ResultHasData_ReturnTrue()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var gatherSystemInformationTO = new GatherSystemInformationTO { Result = "Value" };
            //------------Assert Results-------------------------
            Assert.IsTrue(gatherSystemInformationTO.CanAdd());
        }

        #endregion

        #region CanRemove Tests

        [Test]
        [Author("Massimo Guerrera")]
        [Category("GatherSystemInformationTO_CanRemove")]
        public void GatherSystemInformationTO_CanRemove_ResultEmpty_ReturnTrue()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var gatherSystemInformationTO = new GatherSystemInformationTO { Result = string.Empty };
            //------------Assert Results-------------------------
            Assert.IsTrue(gatherSystemInformationTO.CanRemove());
        }

        [Test]
        [Author("Massimo Guerrera")]
        [Category("GatherSystemInformationTO_CanRemove")]
        public void GatherSystemInformationTO_CanRemove_ResultWithData_ReturnFalse()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var gatherSystemInformationTO = new GatherSystemInformationTO { Result = "Value" };
            //------------Assert Results-------------------------
            Assert.IsFalse(gatherSystemInformationTO.CanRemove());
        }

        #endregion
    }

  
}
