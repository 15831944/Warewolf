using System.Threading;
using Dev2.Runtime.ESB.Execution;
using NUnit.Framework;


namespace Dev2.Tests.Runtime.Util
{
    [TestFixture]
    [SetUpFixture]
    public class ExecutionManagerTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("ExecutionManager_Instance")]
        public void ExecutionManager_Instance_Accessed_ShouldHaveAllDefaults()
        {
            //------------Setup for test--------------------------


            //------------Execute Test---------------------------
            var executionManager =  new ExecutionManager();
            //------------Assert Results-------------------------
            Assert.IsNotNull(executionManager);
            Assert.IsFalse(executionManager.IsRefreshing);
        }

        public static ExecutionManager GetConstructedExecutionManager()
        {
            return new ExecutionManager();
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ExecutionManager_Instance")]
        public void ExecutionManager_StartRefreshing_ShouldSetIsRefreshingTrue()
        {
            //------------Setup for test--------------------------
            var executionManager = GetConstructedExecutionManager();
            //------------Execute Test---------------------------
            executionManager.StartRefresh();
            //------------Assert Results-------------------------
            Assert.IsNotNull(executionManager);
            Assert.IsTrue(executionManager.IsRefreshing);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ExecutionManager_Instance")]
        public void ExecutionManager_StopRefreshing_ShouldSetIsRefreshingTrue()
        {
            //------------Setup for test--------------------------
            var executionManager = GetConstructedExecutionManager();
            //------------Execute Test---------------------------
            executionManager.StartRefresh();
            //------------Assert Results-------------------------
            Assert.IsNotNull(executionManager);
            Assert.IsTrue(executionManager.IsRefreshing);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ExecutionManager_AddExecution")]
        public void ExecutionManager_AddExecution_ShouldIncreaseExecutionsCounts()
        {
            //------------Setup for test--------------------------
            var executionManager = GetConstructedExecutionManager();
            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(executionManager);
            //------------Execute Test---------------------------
            executionManager.AddExecution();
            //------------Assert Results-------------------------
            var currentExecutionsValue = p.GetFieldOrProperty("_currentExecutions");
            Assert.IsNotNull(currentExecutionsValue);
            Assert.AreEqual(1, currentExecutionsValue);

        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ExecutionManager_AddExecution")]
        public void ExecutionManager_CompleteExecution_ShouldDecreaseExecutionsCounts()
        {
            //------------Setup for test--------------------------
            var executionManager = GetConstructedExecutionManager();
            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(executionManager);
            //------------PreExecution Asserts-------------------
            executionManager.AddExecution();
            var currentExecutionsValue = p.GetFieldOrProperty("_currentExecutions");
            Assert.IsNotNull(currentExecutionsValue);
            Assert.AreEqual(1, currentExecutionsValue);
            //------------Execute Test---------------------------
            executionManager.CompleteExecution();
            var updatedCurrentExecutionsValue = p.GetFieldOrProperty("_currentExecutions");
            //------------Assert Results-------------------------
            Assert.AreEqual(0, updatedCurrentExecutionsValue);
        }
        
        [Test]
        [Author("Hagashen Naidu")]
        [Category("ExecutionManager_StopExecution")]
        public void ExecutionManager_StopRefresh_AfterWaitHandlesSet_ShouldSetToFalse()
        {
            //------------Setup for test--------------------------
            var executionManager = GetConstructedExecutionManager();
            var p = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(executionManager);
            var _threadTracker = false;
            var t = new Thread(()=>
            {
                executionManager.Wait();
                _threadTracker = true;
            });
            t.Start();
            //------------Execute Test---------------------------
            executionManager.StopRefresh();
            Thread.Sleep(1000);
            //------------Assert Results-------------------------
            Assert.IsTrue(_threadTracker);
        }
    }
}
