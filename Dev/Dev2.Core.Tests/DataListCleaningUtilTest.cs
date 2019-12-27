/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.DataList.Contract;
using NUnit.Framework;

namespace Dev2.Tests
{


    /// <summary>
    ///This is a test class for DataListUtilTest and is intended
    ///to contain all DataListUtilTest Unit Tests
    ///</summary>
    [TestFixture]
    
    public class DataListCleaningUtilTest
    {
 
        [Test]
        public void SplitIntoRegionsWithScalarsExpectedSeperateRegions()
        {
            //Initialize
            const string Expression = "[[firstregion]], [[secondRegion]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegions(Expression);
            //Assert
            Assert.AreEqual("[[firstregion]]", actual[0]);
            Assert.AreEqual("[[secondRegion]]", actual[1]);
        }

        [Test]
        public void SplitIntoRegionsWithRecSetsExpectedSeperateRegions()
        {
            //Initialize
            const string Expression = "[[firstregion().field]], [[secondRegion().field]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegions(Expression);
            //Assert
            Assert.AreEqual("[[firstregion().field]]", actual[0]);
            Assert.AreEqual("[[secondRegion().field]]", actual[1]);
        }

        [Test]
        public void SplitIntoRegionsWithBigGapBetweenRegionsExpectedSeperateRegions()
        {
            //Initialize
            const string Expression = "[[firstregion]],,,||###&&&/// [[secondRegion]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegions(Expression);
            //Assert
            Assert.AreEqual("[[firstregion]]", actual[0]);
            Assert.AreEqual("[[secondRegion]]", actual[1]);
        }

        [Test]
        public void SplitIntoRegionsWithInvalidRegionsExpectedCannotSeperateRegions()
        {
            //Initialize
            const string Expression = "[[firstregion[[ [[secondRegion[[";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegions(Expression);
            //Assert
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        public void SplitIntoRegionsWithNoOpenningRegionsExpectedCannotSeperateRegions()
        {
            //Initialize
            const string Expression = "]]firstregion]] ]]secondRegion]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegions(Expression);
            //Assert
            Assert.AreEqual(null, actual[0]);
        }

        [Test]
        public void SplitIntoRegionsWithRecordSetsAndScalarsRecordSetIndexsOfExpectedOneRegion()
        {
            //Initialize
            const string Expression = "[[firstregion1([[scalar]]).field]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegions(Expression);
            //Assert
            Assert.AreEqual(Expression, actual[0]);

        }

        [Test]
        public void SplitIntoRegionsWithScalarsRecordSetsIndexsOfExpectedSeperateRegions()
        {
            //Initialize
            const string Expression = "[[firstregion([[firstregion]]).field]], [[secondRegion([[secondRegion]]).field]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegionsForFindMissing(Expression);
            //Assert
            Assert.AreEqual("[[firstregion().field]]", actual[0]);
            Assert.AreEqual("[[firstregion]]", actual[1]);
            Assert.AreEqual("[[secondRegion().field]]", actual[2]);
            Assert.AreEqual("[[secondRegion]]", actual[3]);
        }

        [Test]
        public void SplitIntoRegionsWithRecordSetsAndScalarsRecordSetIndexsOfExpectedSeperateRegions()
        {
            //Initialize
            const string Expression = "[[firstregion([[firstregion1([[scalar]]).field]]).field]], [[secondRegion([[secondRegion1([[scalar1]]).field]]).field]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegionsForFindMissing(Expression);
            //Assert
            Assert.AreEqual("[[firstregion().field]]", actual[0]);
            Assert.AreEqual("[[firstregion1().field]]", actual[1]);
            Assert.AreEqual("[[scalar]]", actual[2]);
            Assert.AreEqual("[[secondRegion().field]]", actual[3]);
            Assert.AreEqual("[[secondRegion1().field]]", actual[4]);
            Assert.AreEqual("[[scalar1]]", actual[5]);
        }

        //Author: Massimo.Guerrera - Bug 9611
        [Test]
        public void SplitIntoRegionsForFindMissingWithRecordSetAndScalarInvalidRegionExpectedNoRegionsReturned()
        {
            //Initialize
            const string Expression = "[[rec().val]][a]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegionsForFindMissing(Expression);
            //Assert
            Assert.AreEqual(null, actual[0]);
        }

        //Author: Massimo.Guerrera - Bug 9611
        [Test]
        public void SplitIntoRegionsForFindMissingWithRecordSetAndScalarInvalidRegionExpectedRecordsetReturned()
        {
            //Initialize
            const string Expression = "[[rec().val][a]]";
            //Execute
            var actual = DataListCleaningUtils.SplitIntoRegionsForFindMissing(Expression);
            //Assert
            Assert.AreEqual("[[rec().val][a]]", actual[0]);
        }
        
    }   
}
