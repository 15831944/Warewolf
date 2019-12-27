/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Moq;
using Warewolf.OS;

namespace Dev2.Common.Tests
{
    [TestFixture]
    public class ExternalProcessExecutorTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ExternalProcessExecutor))]
        public void ExternalProcessExecutor_Start_ProcessStartInfo_VerifyCalls_ToBeOnce_ExpectTrue()
        {
            //---------------------Arrange-------------------------
            var mockProcessWrapper = new Mock<IProcessFactory>();
            var mockProcess = new Mock<IProcess>();

            var processStartInfo = new ProcessStartInfo();

            mockProcessWrapper.Setup(o => o.Start(It.IsAny<ProcessStartInfo>())).Returns(mockProcess.Object);

            var externalProcessExecutor = new ExternalProcessExecutor(mockProcessWrapper.Object);
            //---------------------Act-----------------------------
            externalProcessExecutor.Start(processStartInfo);
            //---------------------Assert--------------------------
            mockProcessWrapper.Verify(o => o.Start(It.IsAny<ProcessStartInfo>()), Times.Once);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ExternalProcessExecutor))]
        public void ExternalProcessExecutor_Start_OpenInBrowser_VerifyCalls_ToBeOnce_ExpectTrue()
        {
            //---------------------Arrange-------------------------
            var mockProcessWrapper = new Mock<IProcessFactory>();

            var uri = new Uri("https://testwarewolf.io");

            var externalProcessExecutor = new ExternalProcessExecutor(mockProcessWrapper.Object);
            //---------------------Act-----------------------------
            externalProcessExecutor.OpenInBrowser(uri);
            //---------------------Assert--------------------------
            mockProcessWrapper.Verify(o => o.Start(uri.ToString()), Times.Once);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ExternalProcessExecutor))]
        public void ExternalProcessExecutor_Start_Catch_TimeoutException_VerifyAll_ExpectTrue()
        {
            //---------------------Arrange-------------------------
            var mockProcessWrapper = new Mock<IProcessFactory>();

            var uri = new Uri("https://testwarewolf.io");

            mockProcessWrapper.Setup(o => o.Start(uri.ToString())).Throws<TimeoutException>();

            var externalProcessExecutor = new ExternalProcessExecutor(mockProcessWrapper.Object);
            //---------------------Act-----------------------------
            externalProcessExecutor.OpenInBrowser(uri);
            //---------------------Assert--------------------------
            mockProcessWrapper.VerifyAll();
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ExternalProcessExecutor))]
        public void ExternalProcessExecutor_Start_Catch_COMException_VerifyAll_ExpectTrue()
        {
            //---------------------Arrange-------------------------
            var mockProcessWrapper = new Mock<IProcessFactory>();

            var uri = new Uri("https://testwarewolf.io");

            mockProcessWrapper.Setup(o => o.Start(uri.ToString())).Throws<COMException>();

            var externalProcessExecutor = new ExternalProcessExecutor(mockProcessWrapper.Object);
            //---------------------Act-----------------------------
            externalProcessExecutor.OpenInBrowser(uri);
            //---------------------Assert--------------------------
            mockProcessWrapper.VerifyAll();
        }
    }
}
