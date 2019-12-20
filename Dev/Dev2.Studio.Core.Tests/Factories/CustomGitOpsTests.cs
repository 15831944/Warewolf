using Dev2.Common.Interfaces;
using Dev2.Factory;
using NUnit.Framework;
using Moq;
using System.Diagnostics;

namespace Dev2.Core.Tests.Factories
{
    [TestFixture]
    [SetUpFixture]
    public class CustomGitOpsTests
    {
        [Test]
        public void CustomGitOps_ExecutesSixCommands()
        {
            //------------Setup for test--------------------------
            var executor = new Mock<IExternalProcessExecutor>();
            CustomGitOps.SetCustomGitTool(executor.Object);
            executor.Verify(p => p.Start(It.IsAny<ProcessStartInfo>()), Times.Exactly(6));
        }

        [Test]
        public void CustomGitOps_ExecutesSixCommands_WithExceptions()
        {
            //------------Setup for test--------------------------
            var executor = new Mock<IExternalProcessExecutor>();
            executor.Setup(p => p.Start(It.IsAny<ProcessStartInfo>())).Throws(new System.Exception());
            CustomGitOps.SetCustomGitTool(executor.Object);
            executor.Verify(p => p.Start(It.IsAny<ProcessStartInfo>()), Times.Exactly(6));
        }
    }
}
