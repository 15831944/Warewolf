/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Threading;
using Dev2.Data;
using NUnit.Framework;
using Dev2.Common;

namespace Dev2.Infrastructure.Tests.Logs
{
    [TestFixture]
    [SetUpFixture]
    public class PulseLoggerTests
    {
        bool _elapsed;

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(PulseLogger))]
        public void PulseLogger_Ctor_CheckValues_ExpectInitialised()
        {
            //------------Setup for test--------------------------
            using (var pulseLogger = new PulseLogger(25))
            {
                Assert.AreEqual(25, pulseLogger.Interval);
                var timer = pulseLogger._timer;
                Assert.AreEqual(false, timer.Enabled);
            }
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(PulseLogger))]
        public void PulseLogger_Ctor_Start_ExpectInitialised()
        {
            //------------Setup for test--------------------------
            using (var pulseLogger = new PulseLogger(2000))
            {
                Assert.AreEqual(2000, pulseLogger.Interval);
                var timer = pulseLogger._timer;
                timer.Elapsed += (sender, e) =>
                    {
                        _elapsed = true;
                    };
                Assert.AreEqual(false, timer.Enabled);
                //------------Execute Test---------------------------
                pulseLogger.Start();
                Thread.Sleep(6000);
                //------------Assert Results-------------------------
                Assert.IsTrue(_elapsed);
            }
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(PulseTracker))]
        public void PulseLogger_Ctor_TimeoutElapse_ExpectResetExecutionWatcher()
        {
            //------------Setup for test--------------------------
            using (var pulseTracker = new PulseTracker(2000))
            {
                WorkflowExecutionWatcher.HasAWorkflowBeenExecuted = true;
                //------------Execute Test---------------------------
                pulseTracker.Start();
                Thread.Sleep(6000);
                //------------Assert Results-------------------------
                Assert.IsFalse(WorkflowExecutionWatcher.HasAWorkflowBeenExecuted, "Execution Watcher not reset after pulse tracker execute.");
            }
        }
    }
}
