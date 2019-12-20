using System;
using System.Collections.Generic;
using System.Text;
using Dev2.Common.Interfaces.Enums;
using Dev2.Common.Interfaces.Monitoring;
using Dev2.Communication;
using Dev2.PerformanceCounters.Management;
using Dev2.Runtime.ESB.Management.Services;
using Dev2.Workspaces;
using NUnit.Framework;
using Moq;


namespace Dev2.Tests.Runtime.Services
{
    [TestFixture]
    [SetUpFixture]
    public class FetchPerformanceCountersTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("GetResourceID")]
        public void GetResourceID_ShouldReturnEmptyGuid()
        {
            //------------Setup for test--------------------------
            var fetchPerformanceCounters = new FetchPerformanceCounters();

            //------------Execute Test---------------------------
            var resId = fetchPerformanceCounters.GetResourceID(new Dictionary<string, StringBuilder>());
            //------------Assert Results-------------------------
            Assert.AreEqual(Guid.Empty, resId);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("GetResourceID")]
        public void GetAuthorizationContextForService_ShouldReturnContext()
        {
            //------------Setup for test--------------------------
            var fetchPerformanceCounters = new FetchPerformanceCounters();

            //------------Execute Test---------------------------
            var resId = fetchPerformanceCounters.GetAuthorizationContextForService();
            //------------Assert Results-------------------------
            Assert.AreEqual(AuthorizationContext.Any, resId);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("FetchPerformanceCounters_HandlesType")]
        public void FetchPerformanceCounters_HandlesType_Get_ReturnsKnownString()
        {
            //------------Setup for test--------------------------
            var fetchPerformanceCounters = new FetchPerformanceCounters();
            
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            Assert.AreEqual(fetchPerformanceCounters.HandlesType(), "FetchPerformanceCounters");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("FetchPerformanceCounters_HandlesType")]
        public void FetchPerformanceCounters_HandlesType_Get_DynamicServiceEntry()
        {
            //------------Setup for test--------------------------
            var fetchPerformanceCounters = new FetchPerformanceCounters();

            //------------Execute Test---------------------------
           var entry =  fetchPerformanceCounters.CreateServiceEntry();
            //------------Assert Results-------------------------

           Assert.AreEqual("<DataList><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>", entry.DataListSpecification.ToString());
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("FetchPerformanceCounters_Manager")]

        public void FetchPerformanceCounters_Manager_ExceptionIfContainerNotRegistered()
        {
            //------------Setup for test--------------------------
            var fetchPerformanceCounters = new FetchPerformanceCounters();
            CustomContainer.DeRegister<IPerformanceCounterRepository>();
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(fetchPerformanceCounters);
            var nll =   p.GetProperty("Manager");
            Assert.IsNull(nll);
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("FetchPerformanceCounters_Manager")]
        public void FetchPerformanceCounters_Manager_ValueIfRegistered()
        {

            //------------Setup for test--------------------------
            var mng = new Mock<IPerformanceCounterRepository>();
            CustomContainer.Register(mng.Object);
            var fetchPerformanceCounters = new FetchPerformanceCounters();

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(fetchPerformanceCounters);
            Assert.IsNotNull( p.GetProperty("Manager"));
           Assert.IsTrue(ReferenceEquals( mng.Object, p.GetProperty("Manager")));
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("FetchPerformanceCounters_Manager")]

        public void FetchPerformanceCounters_Manager_ExecuteReturnsAValidTo()
        {

            //------------Setup for test--------------------------
            var mng = new Mock<IPerformanceCounterRepository>();
            mng.Setup(a => a.Counters).Returns(new PerformanceCounterTo(new List<IPerformanceCounter>(), new List<IPerformanceCounter>()));
            CustomContainer.Register(mng.Object);
            var fetchPerformanceCounters = new FetchPerformanceCounters();

            //------------Execute Test---------------------------
           var output =  fetchPerformanceCounters.Execute(new Dictionary<string, StringBuilder>(),new Mock<IWorkspace>().Object);
            //------------Assert Results-------------------------
            Assert.IsNotNull(output);
            var ser = new Dev2JsonSerializer();
            var res =   ser.Deserialize<IPerformanceCounterTo>(output);
            Assert.IsNotNull(res);
        }

    }
}
