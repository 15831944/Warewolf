using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Dev2.Data
{
    [TestFixture]
    public class PulseTrackerTests
    {
        [Test]
        public void PulseTracker_ShouldStartTimerOnStart()
        {
            var pulseTracker =new PulseTracker(50);
            NUnit.Framework.Assert.IsNotNull(pulseTracker);
            var prObj = new PrivateObject(pulseTracker);
            NUnit.Framework.Assert.IsNotNull(prObj);
            var start = pulseTracker.Start();
            NUnit.Framework.Assert.IsNotNull(start);
            var timer = prObj.GetField("_timer") as System.Timers.Timer;
            NUnit.Framework.Assert.IsTrue(timer != null && timer.Enabled);
        }
    }
}