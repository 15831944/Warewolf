/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.PathOperations;
using Dev2.Data.Interfaces.Enums;
using NUnit.Framework;


namespace Dev2.Data.Tests.PathOperations
{
    [TestFixture]
    public class Dev2PutRawOperationTOTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("Dev2PutRawOperationTO_Constructor")]
        public void Dev2PutRawOperation_Constructor_TakesContentsAndWriteTypeEnum()
        {
            //------------Setup for test--------------------------
            const WriteType WriteType = WriteType.AppendBottom;
            const string Contents = "Some test";
            //------------Execute Test---------------------------
            var dev2PutRawOperation = new Dev2PutRawOperationTO(WriteType,Contents);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(dev2PutRawOperation);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("Dev2PutRawOperationTO_Constructor")]
        public void Dev2PutRawOperationTO_Constructor_GivenParameter_ShouldSetProperties()
        {
            //------------Setup for test--------------------------
            const WriteType WriteType = WriteType.AppendBottom;
            const string Contents = "Some test";
            //------------Execute Test---------------------------
            var dev2PutRawOperation = new Dev2PutRawOperationTO(WriteType, Contents);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(WriteType,dev2PutRawOperation.WriteType);
            NUnit.Framework.Assert.AreEqual(Contents,dev2PutRawOperation.FileContents);
        }
    }
}
