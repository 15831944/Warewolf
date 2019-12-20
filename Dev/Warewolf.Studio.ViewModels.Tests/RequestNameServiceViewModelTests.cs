using Dev2;
using Dev2.Studio.Interfaces;
using NUnit.Framework;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    public class RequestNameServiceViewModelTests
    {
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("DeploySourceExplorerViewModel_Ctor_valid")]
        public void TestDispose()
        {
            var serverRepo = new Mock<IServerRepository>();
            var connectionObject = new Mock<IEnvironmentConnection>();
            serverRepo.Setup(repository => repository.ActiveServer.Connection).Returns(connectionObject.Object);
            CustomContainer.Register(serverRepo.Object);
            var vm = new RequestServiceNameViewModel();
            var x = new Mock<IExplorerViewModel>();
            var p = new PrivateObject(vm);
            var env = new Mock<IEnvironmentViewModel>();
            p.SetField("_environmentViewModel", env.Object);
            vm.SingleEnvironmentExplorerViewModel = x.Object;
            vm.Dispose();
            x.Verify(a=>a.Dispose());
            env.Verify(a => a.Dispose());
        }
    }
}
