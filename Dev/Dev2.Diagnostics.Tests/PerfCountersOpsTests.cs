using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dev2.Common;
using Dev2.Common.Interfaces.Monitoring;
using Dev2.PerformanceCounters.Counters;
using Dev2.PerformanceCounters.Management;
using NUnit.Framework;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Diagnostics.Test
{
    [TestFixture]
    [SetUpFixture]
    public class PerfCountersOpsTests
    {
        public static Guid ResourceGuid = Guid.NewGuid();
        const string CategoryName = "Warewolf";

        [SetUp]
        public void Init()
        {
            try
            {
                try
                {
                    PerformanceCounterCategory.Delete(CategoryName);
                    PerformanceCounterCategory.Delete("Warewolf Services");
                }
                catch
                {

                }
                var performanceCounterFactory = new Mock<IRealPerformanceCounterFactory>().Object;
                var register = new WarewolfPerformanceCounterRegister(new List<IPerformanceCounter>
                                                            {
                                                                new WarewolfCurrentExecutionsPerformanceCounter(performanceCounterFactory),
                                                                new WarewolfNumberOfErrors(performanceCounterFactory),    
                                                                new WarewolfRequestsPerSecondPerformanceCounter(performanceCounterFactory),
                                                                new WarewolfAverageExecutionTimePerformanceCounter(performanceCounterFactory),
                                                                new WarewolfNumberOfAuthErrors(performanceCounterFactory),
                                                                new WarewolfServicesNotFoundCounter(performanceCounterFactory),

                                                            }, new List<IResourcePerformanceCounter>{
                                                       new WarewolfCurrentExecutionsPerformanceCounterByResource(Guid.Empty, "", performanceCounterFactory),
                                                       new WarewolfNumberOfErrorsByResource(Guid.Empty, "", performanceCounterFactory),
                                                       new WarewolfRequestsPerSecondPerformanceCounterByResource(Guid.Empty, "", performanceCounterFactory),
                                                       new WarewolfAverageExecutionTimePerformanceCounterByResource(Guid.Empty, "", performanceCounterFactory),

                                                    });

                var manager = new WarewolfPerformanceCounterManager(register.Counters, new List<IResourcePerformanceCounter>(), register, new Mock<IPerformanceCounterPersistence>().Object, performanceCounterFactory);
                manager.CreateCounter( ResourceGuid,WarewolfPerfCounterType.ExecutionErrors, "bob");
                manager.CreateCounter(ResourceGuid, WarewolfPerfCounterType.AverageExecutionTime, "bob");
                manager.CreateCounter(ResourceGuid, WarewolfPerfCounterType.RequestsPerSecond, "bob");
                manager.CreateCounter(ResourceGuid, WarewolfPerfCounterType.ConcurrentRequests, "bob");
                                                                
                CustomContainer.Register<IWarewolfPerformanceCounterLocater>(manager);
            }
            catch (Exception err)
            {
                // ignored
                Dev2Logger.Error(err, GlobalConstants.WarewolfError);
            }
        }

        [Test]
        public void TestLocater()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(WarewolfPerfCounterType.ConcurrentRequests).FromSafe();
            var counter2 = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter("Concurrent requests currently executing").FromSafe();
            NUnit.Framework.Assert.AreEqual(counter, counter2);
        }

        [Test]
        public void ConcurrentCounterTest()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(WarewolfPerfCounterType.ConcurrentRequests).FromSafe();
            NUnit.Framework.Assert.AreEqual("Concurrent requests currently executing", counter.Name);
            NUnit.Framework.Assert.AreEqual(CategoryName, counter.Category);
        }
        [Test]
        public void ConcurrentCounterTestByResource()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(ResourceGuid, WarewolfPerfCounterType.ConcurrentRequests).FromSafe();
            NUnit.Framework.Assert.AreEqual("Concurrent requests currently executing", counter.Name);
            NUnit.Framework.Assert.AreEqual(counter.Category, GlobalConstants.WarewolfServices);

            if (counter is IResourcePerformanceCounter resourcePerformanceCounter)
            {
                NUnit.Framework.Assert.AreEqual(ResourceGuid, resourcePerformanceCounter.ResourceId);
            }
            else
            {
                NUnit.Framework.Assert.Fail("Type was not recognised as IResourcePerformanceCounter: " + counter);
            }
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfErrorCounter_TestOps")]
        public void WarewolfErrorCounter_TestOps_Valid_ExpectValidValues()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(WarewolfPerfCounterType.ExecutionErrors).FromSafe();
            NUnit.Framework.Assert.AreEqual("Total Errors", counter.Name);
            NUnit.Framework.Assert.AreEqual(CategoryName, counter.Category);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfErrorCounter_TestOps")]
        public void WarewolfErrorCounterResource_TestOps_Valid_ExpectValidValues()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(ResourceGuid, WarewolfPerfCounterType.ExecutionErrors).FromSafe();
            NUnit.Framework.Assert.AreEqual("Total Errors", counter.Name);
            NUnit.Framework.Assert.AreEqual(counter.Category, GlobalConstants.WarewolfServices);

            if (counter is IResourcePerformanceCounter resourcePerformanceCounter)
            {
                NUnit.Framework.Assert.AreEqual(ResourceGuid, resourcePerformanceCounter.ResourceId);
            }
            else
            {
                NUnit.Framework.Assert.Fail("Type was not recognised as IResourcePerformanceCounter: " + counter);
            }
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfEmptyCounter_TestOps")]
        public void WarewolfEmptyCounter_TestOps_Valid_ExpectValidValues()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(Guid.NewGuid(), WarewolfPerfCounterType.AverageExecutionTime).FromSafe();
            NUnit.Framework.Assert.AreEqual("Empty", counter.Name);
            NUnit.Framework.Assert.AreEqual(CategoryName, counter.Category);
            var po = new PrivateObject(counter);
            po.Invoke("Setup", new object[0]);

            NUnit.Framework.Assert.IsNull(counter.CreationData());
            counter.Setup();

            counter.Increment();

            counter.Decrement();

            counter.IncrementBy(3);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfRequestsPerSecondCounter_TestOps")]
        public void WarewolfRequestPerSecondCounter_TestOps_Valid_ExpectValidValues()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(WarewolfPerfCounterType.RequestsPerSecond).FromSafe();
            NUnit.Framework.Assert.AreEqual("Request Per Second", counter.Name);
            NUnit.Framework.Assert.AreEqual(CategoryName, counter.Category);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfRequestsPerSecondCounter_TestOps")]
        public void WarewolfRequestPerSecondCounterResource_TestOps_Valid_ExpectValidValues()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(ResourceGuid, WarewolfPerfCounterType.RequestsPerSecond).FromSafe();
            NUnit.Framework.Assert.AreEqual("Request Per Second", counter.Name);
            NUnit.Framework.Assert.AreEqual(counter.Category, GlobalConstants.WarewolfServices);

            if (counter is IResourcePerformanceCounter resourcePerformanceCounter)
            {
                NUnit.Framework.Assert.AreEqual(ResourceGuid, resourcePerformanceCounter.ResourceId);
            }
            else
            {
                NUnit.Framework.Assert.Fail("Type was not recognised as IResourcePerformanceCounter: " + counter);
            }
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfAverageExectionTimeCounter_TestOps")]
        public void WarewolfServicesNotFound_TestOps_Valid_ExpectValidValues()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(WarewolfPerfCounterType.ServicesNotFound).FromSafe();
            NUnit.Framework.Assert.AreEqual("Count of requests for workflows which don't exist", counter.Name);
            NUnit.Framework.Assert.AreEqual(CategoryName, counter.Category);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfAverageExectionTimeCounter_TestOps")]
        public void WarewolfServicesNotFound_NotCauseErrorForResource_TestOps_Valid_ExpectValidValues()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(ResourceGuid, WarewolfPerfCounterType.ServicesNotFound).FromSafe();
            NUnit.Framework.Assert.IsTrue(counter is EmptyCounter);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfAverageExectionTimeCounter_TestOps")]
        public void WarewolfAuthErrors_TestOps_Valid_ExpectValidValues()
        {
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(WarewolfPerfCounterType.NotAuthorisedErrors).FromSafe();
            NUnit.Framework.Assert.AreEqual("Count of Not Authorised errors", counter.Name);
            NUnit.Framework.Assert.AreEqual(CategoryName, counter.Category);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfAverageExectionTimeCounter_TestOps")]
        public void WarewolfAverageExectionTimeCounter_TestOps_Valid_ExpectValidValues()
        {
            //------------Setup for test--------------------------
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(WarewolfPerfCounterType.AverageExecutionTime).FromSafe();
            NUnit.Framework.Assert.AreEqual("Average workflow execution time", counter.Name);
            NUnit.Framework.Assert.AreEqual(CategoryName, counter.Category);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfAverageExectionTimeCounter_TestOps")]
        public void WarewolfAverageExectionTimeCounterResource_TestOps_Valid_ExpectValidValues()
        {
            //------------Setup for test--------------------------
            var counter = CustomContainer.Get<IWarewolfPerformanceCounterLocater>().GetCounter(ResourceGuid, WarewolfPerfCounterType.AverageExecutionTime).FromSafe();
            NUnit.Framework.Assert.AreEqual("Average workflow execution time", counter.Name);
            NUnit.Framework.Assert.AreEqual(counter.Category, GlobalConstants.WarewolfServices);

            if (counter is IResourcePerformanceCounter resourcePerformanceCounter)
            {
                NUnit.Framework.Assert.AreEqual(ResourceGuid, resourcePerformanceCounter.ResourceId);
            }
            else
            {
                NUnit.Framework.Assert.Fail("Type was not recognised as IResourcePerformanceCounter: " + counter);
            }
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WarewolfErrorCounter_TestOps")]
        public void WarewolfErrorCounterResource_SafeCounterSwallowsExceptions()
        {
            var incremented = false;
            var decremented = false;
            var incrementedBy = false;
            var setup = true;
            var guid = Guid.NewGuid();
            var inner = new Mock<IResourcePerformanceCounter>();
            inner.Setup(a => a.Decrement()).Callback(() => decremented = true).Throws(new AccessViolationException());
            inner.Setup(a => a.Increment()).Callback(() => incremented = true).Throws(new AccessViolationException());
            inner.Setup(a => a.IncrementBy(5)).Callback((long a) => incrementedBy = true).Throws(new AccessViolationException());
            inner.Setup(a => a.CategoryInstanceName).Returns("bob");
            inner.Setup(a => a.ResourceId).Returns(guid);
            inner.Setup(a => a.Category).Returns("Neo");
            inner.Setup(a => a.Name).Returns("Morpheus");
            inner.Setup(a => a.IsActive).Returns(true);
            inner.Setup(a => a.Setup()).Callback(() => { setup = true; });
            var safe = new SafeCounter(inner.Object);
            safe.Increment();
            NUnit.Framework.Assert.IsTrue(incremented);
            safe.Decrement();
            NUnit.Framework.Assert.IsTrue(decremented);
            safe.IncrementBy(5);
            NUnit.Framework.Assert.IsTrue(incrementedBy);
            NUnit.Framework.Assert.AreEqual(safe.InnerCounter, inner.Object);
            NUnit.Framework.Assert.AreEqual("Neo", safe.Category);
            NUnit.Framework.Assert.AreEqual("Morpheus", safe.Name);
            NUnit.Framework.Assert.IsTrue(setup);
        }
    }
}
