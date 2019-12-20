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
using System.Data;
using Dev2.Activities.SqlBulkInsert;
using NUnit.Framework;

namespace Dev2.Tests.Activities.ActivityTests
{
    [TestFixture]
    [SetUpFixture]
    public class SqlBulkCopyWrapperTest
    {
        [Test]
        [Author("Travis Frisinger")]
        [Category("SqlBulkCopyWrapper_WriteToServer")]
        [ExpectedException(typeof(ArgumentException))]
        
        public void SqlBulkCopyWrapper_WriteToServer_WhenNullBulkCopyObject_ExpectException()

        {
            //------------Setup for test--------------------------
            var sqlBulkCopyWrapper = new SqlBulkCopyWrapper(null);
            var dataTable = new DataTable("myTable");

            //------------Execute Test---------------------------
            sqlBulkCopyWrapper.WriteToServer(dataTable);

        }
    }
}
