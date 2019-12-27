/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common.Interfaces.Wrappers;
using Dev2.Data.Interfaces;
using Dev2.Data.PathOperations;
using Dev2.Data.PathOperations.Operations;
using NUnit.Framework;
using Moq;
using System;
using System.IO;

namespace Dev2.Data.Tests.PathOperations
{
    [TestFixture]
    public class DoPutActionTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DoPutAction))]
        public void DoPutAction_ExecuteOperation_ExpectIOException()
        {
            //-------------------------Arrange-----------------------
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            var mockDev2CRUDOperationTO = new Mock<IDev2CRUDOperationTO>();

            var ss = new DoPutAction( new MemoryStream(), mockActivityIOPath.Object, mockDev2CRUDOperationTO.Object, "testWhereToPut");
            //-------------------------Act---------------------------
            //-------------------------Assert------------------------
            NUnit.Framework.Assert.Throws<IOException>(() => ss.ExecuteOperation());
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DoPutAction))]
        public void DoPutAction_ExecuteOperation_IsPathRooted_false_ExpectIOException()
        {
            //-------------------------Arrange-----------------------
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            var mockDev2CRUDOperationTO = new Mock<IDev2CRUDOperationTO>();
            var mockDev2LogonProvider = new Mock<IDev2LogonProvider>();
            var mockFileWrapper = new Mock<IFile>();
            var mockFilePath = new Mock<IFilePath>();
            var mockWindowsImpersonationContext = new Mock<IWindowsImpersonationContext>();

            mockFilePath.Setup(o => o.IsPathRooted(It.IsAny<string>())).Returns(false);

            var ss = new DoPutAction(new MemoryStream(), mockActivityIOPath.Object, mockDev2CRUDOperationTO.Object, "TestWhere", mockDev2LogonProvider.Object, mockFileWrapper.Object, mockFilePath.Object, (arg1, arg2)=> mockWindowsImpersonationContext.Object);
            //-------------------------Act---------------------------
            //-------------------------Assert------------------------
            NUnit.Framework.Assert.Throws<IOException>(()=> ss.ExecuteOperation());
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DoPutAction))]
        public void DoPutAction_ExecuteOperation_IsPathRooted_True_ExpectArgumentNullException()
        {
            //-------------------------Arrange-----------------------
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            var mockDev2CRUDOperationTO = new Mock<IDev2CRUDOperationTO>();
            var mockDev2LogonProvider = new Mock<IDev2LogonProvider>();
            var mockFileWrapper = new Mock<IFile>();
            var mockFilePath = new Mock<IFilePath>();
            var mockWindowsImpersonationContext = new Mock<IWindowsImpersonationContext>();

            mockFilePath.Setup(o => o.IsPathRooted(It.IsAny<string>())).Returns(true);

            var ss = new DoPutAction(new MemoryStream(), mockActivityIOPath.Object, mockDev2CRUDOperationTO.Object, "TestWhere", mockDev2LogonProvider.Object, mockFileWrapper.Object, mockFilePath.Object, (arg1, arg2) => mockWindowsImpersonationContext.Object);
            //-------------------------Act---------------------------
            //-------------------------Assert------------------------
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => ss.ExecuteOperation());
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DoPutAction))]
        public void DoPutAction_ExecuteOperation_IsPathRooted_True__ImpersonatedUser_IsNull_ExpectArgumentNullException()
        {
            //-------------------------Arrange-----------------------
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            var mockDev2CRUDOperationTO = new Mock<IDev2CRUDOperationTO>();
            var mockDev2LogonProvider = new Mock<IDev2LogonProvider>();
            var mockFileWrapper = new Mock<IFile>();
            var mockFilePath = new Mock<IFilePath>();

            mockFilePath.Setup(o => o.IsPathRooted(It.IsAny<string>())).Returns(true);

            var ss = new DoPutAction(new MemoryStream(), mockActivityIOPath.Object, mockDev2CRUDOperationTO.Object, null, mockDev2LogonProvider.Object, mockFileWrapper.Object, mockFilePath.Object, (arg1, arg2) => null);
            //-------------------------Act---------------------------
            //-------------------------Assert------------------------
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => ss.ExecuteOperation());
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DoPutAction))]
        public void DoPutAction_ExecuteOperation_IsPathRooted_True_ImpersonatedUser_IsNull_ExpectTrue()
        {
            //-------------------------Arrange-----------------------
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            var mockDev2CRUDOperationTO = new Mock<IDev2CRUDOperationTO>();
            var mockDev2LogonProvider = new Mock<IDev2LogonProvider>();
            var mockFileWrapper = new Mock<IFile>();
            var mockFilePath = new Mock<IFilePath>();
            var mockWindowsImpersonationContext = new Mock<IWindowsImpersonationContext>();

            mockFilePath.Setup(o => o.IsPathRooted(It.IsAny<string>())).Returns(true);
            mockActivityIOPath.Setup(o => o.Path).Returns("testPath");

            var ss = new DoPutAction(new MemoryStream(), mockActivityIOPath.Object, mockDev2CRUDOperationTO.Object, null, mockDev2LogonProvider.Object, mockFileWrapper.Object, mockFilePath.Object, (arg1, arg2) => mockWindowsImpersonationContext.Object);
            //-------------------------Act---------------------------
            var xx = ss.ExecuteOperation();
            //-------------------------Assert------------------------
            mockFilePath.VerifyAll();
            mockActivityIOPath.VerifyAll();
            NUnit.Framework.Assert.AreEqual(0,xx);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DoPutAction))]
        public void DoPutAction_ExecuteOperation_IsPathRooted_True__ImpersonatedUser_IsNotNull_ExpectIOException()
        {
            //-------------------------Arrange-----------------------
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            var mockDev2CRUDOperationTO = new Mock<IDev2CRUDOperationTO>();
            var mockDev2LogonProvider = new Mock<IDev2LogonProvider>();
            var mockFileWrapper = new Mock<IFile>();
            var mockFilePath = new Mock<IFilePath>();
            var mockWindowsImpersonationContext = new Mock<IWindowsImpersonationContext>();

            mockFilePath.Setup(o => o.IsPathRooted(It.IsAny<string>())).Returns(true);
            mockActivityIOPath.Setup(o => o.Path).Returns("testPath");
            mockDev2CRUDOperationTO.Setup(o => o.Overwrite).Returns(true);

            var ss = new DoPutAction(new MemoryStream(), mockActivityIOPath.Object, mockDev2CRUDOperationTO.Object, null, mockDev2LogonProvider.Object, mockFileWrapper.Object, mockFilePath.Object, (arg1, arg2) => mockWindowsImpersonationContext.Object);
            //-------------------------Act---------------------------
            var xx = ss.ExecuteOperation();
            //-------------------------Assert------------------------
            mockFilePath.VerifyAll();
            mockActivityIOPath.VerifyAll();
            mockDev2CRUDOperationTO.VerifyAll();
            NUnit.Framework.Assert.AreEqual(0, xx);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DoPutAction))]
        public void DoPutAction_ExecuteOperation_IsPathRooted_True_ImpersonatedUser_IsNotNull_FileExist_False_ExpectIOException()
        {
            //-------------------------Arrange-----------------------
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            var mockDev2CRUDOperationTO = new Mock<IDev2CRUDOperationTO>();
            var mockDev2LogonProvider = new Mock<IDev2LogonProvider>();
            var mockFileWrapper = new Mock<IFile>();
            var mockFilePath = new Mock<IFilePath>();
            var mockWindowsImpersonationContext = new Mock<IWindowsImpersonationContext>();

            mockFilePath.Setup(o => o.IsPathRooted(It.IsAny<string>())).Returns(true);
            mockActivityIOPath.Setup(o => o.Path).Returns("testPath");
            mockDev2CRUDOperationTO.Setup(o => o.Overwrite).Returns(false);

            var ss = new DoPutAction(new MemoryStream(), mockActivityIOPath.Object, mockDev2CRUDOperationTO.Object, null, mockDev2LogonProvider.Object, mockFileWrapper.Object, mockFilePath.Object, (arg1, arg2) => mockWindowsImpersonationContext.Object);
            //-------------------------Act---------------------------
            var xx = ss.ExecuteOperation();
            //-------------------------Assert------------------------
            mockFilePath.VerifyAll();
            mockActivityIOPath.VerifyAll();
            mockDev2CRUDOperationTO.VerifyAll();
            NUnit.Framework.Assert.AreEqual(0, xx);
        }
    }
}
