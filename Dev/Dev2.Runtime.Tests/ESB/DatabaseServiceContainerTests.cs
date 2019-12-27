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
using Dev2.Data.TO;
using Dev2.Runtime.ESB.Execution;
using Dev2.Services.Execution;
using NUnit.Framework;
using Moq;


namespace Dev2.Tests.Runtime.ESB
{
    [TestFixture]
    [Category("Runtime ESB")]
    public class DatabaseServiceContainerTests
    {
        #region Execute

        [Test]
        public void DatabaseServiceContainer_UnitTest_ExecuteWhereHasDatabaseServiceExecution_Guid()
        {
            //------------Setup for test--------------------------
            var mockServiceExecution = new Mock<IServiceExecution>();
            ErrorResultTO errors;
            var expected = Guid.NewGuid();
            mockServiceExecution.Setup(execution => execution.Execute(out errors, 0)).Returns(expected);
            var databaseServiceContainer = new DatabaseServiceContainer(mockServiceExecution.Object);
            //------------Execute Test---------------------------
            var actual = databaseServiceContainer.Execute(out errors, 0);
            //------------Assert Results-------------------------
            Assert.AreEqual(expected, actual, "Execute should return the Guid from the service execution");
        }

        #endregion

        #region CreateDatabaseServiceContainer

        #endregion

        #region CreateServiceAction

        #endregion

    }
}
