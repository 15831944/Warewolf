using System;
using System.Threading.Tasks;
using Dev2.Network;
using Dev2.SignalR.Wrappers;
using Dev2.Studio.Interfaces;
using NUnit.Framework;
using Moq;

namespace Dev2.Core.Tests.Network
{
    public partial class ServerProxyWithFallbackTests
    {
 
        //Given a ServerProxy
        //When I Execute the AddDebugWriter
        //Then the EsbProxy is invoked with AddDebugWriter
        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServerProxy_AddDebugWriter")]
        public void ServerProxy_AddDebugWriter_WithArgs_ShouldInvokeCorrectly()
        {
            //------------Setup for test--------------------------
            var mockHubProxy = new Mock<IHubProxyWrapper>();
            mockHubProxy.Setup(proxy => proxy.Invoke("AddDebugWriter", It.IsAny<Guid>())).Returns(new Task(() => { }));
            var serverProxy = new TestServerProxy();
            serverProxy.SetEsbProxy(mockHubProxy.Object);
            //------------Execute Test---------------------------
            serverProxy.AddDebugWriter(Guid.NewGuid());
            //------------Assert Results-------------------------
            mockHubProxy.VerifyAll();
        }


        [Test]
        [Timeout(5000)]
        [Author("Hagashen Naidu")]
        [Category("ServerProxy_Constructor")]
        public void ServerProxy_FallbackOnConnectWithError()
        {
            //------------Setup for test--------------------------
            var serverProxy = new ServerProxy(new Uri("http://bob"));
            var serverGuid = Guid.NewGuid();
            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(serverProxy);
            var wrapped = new Mock<IEnvironmentConnection>();
            var fallback = new Mock<IEnvironmentConnection>();
            wrapped.Setup(a => a.Connect(It.IsAny<Guid>())).Throws(new FallbackException());
            p.SetField("_wrappedConnection",wrapped.Object);
            try
            {
                serverProxy.Connect(serverGuid);
            }
            
            catch
            
            {

            }
            var con = p.GetField("_wrappedConnection");
            Assert.IsNotNull(con);
           
        }





        [Test]
        [Timeout(5000)]
        [Author("Hagashen Naidu")]
        [Category("ServerProxy_Constructor")]
        public void ServerProxy_NoFallbackOnConnectIfNormalException()
        {
            //------------Setup for test--------------------------
            var serverProxy = new ServerProxy(new Uri("http://bob"));
            var serverGuid = Guid.NewGuid();
            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(serverProxy);
            var wrapped = new Mock<IEnvironmentConnection>();
            var fallback = new Mock<IEnvironmentConnection>();
            wrapped.Setup(a => a.Connect(It.IsAny<Guid>())).Throws(new Exception());
            p.SetField("_wrappedConnection", wrapped.Object);

            try
            {
                serverProxy.Connect(serverGuid);

            }
            
            catch
            
            {


            }
            var con = p.GetField("_wrappedConnection");
           Assert.IsNotNull(con);

        }
    }
}