using NUnit.Framework;
using Dev2.ServerLifeCycleWorkers;

using Dev2.Runtime;

namespace Dev2.Server.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class RegisterDependenciesWorkerTests
    {
        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RegisterDependenciesWorkerTests))]
        public void RegisterDependenciesWorker_Execute()
        {
            var worker = new RegisterDependenciesWorker();
            worker.Execute();
            Assert.IsNotNull(CustomContainer.Get<IActivityParser>());
            Assert.IsNotNull(CustomContainer.Get<IExecutionManager>());
            Assert.IsNotNull(CustomContainer.Get<IResumableExecutionContainerFactory>());
        }
    }
}
