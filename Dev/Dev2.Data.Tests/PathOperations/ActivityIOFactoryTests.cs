using Dev2.Data.Interfaces;
using Dev2.PathOperations;
using NUnit.Framework;

namespace Dev2.Data.Tests.PathOperations
{
    [TestFixture]
    public class ActivityIOFactoryTests
    {
        [Test]
        [Author("Rory McGuire")]
        public void Dev2ActivityIOBroker_CreateInstance_GivenThrowsNoExpetion_ShouldBeIActivityOperationsBroker()
        {
            var broker = ActivityIOFactory.CreateOperationsBroker();
            Assert.IsInstanceOf(typeof(IActivityOperationsBroker), broker);
        }
    }
}
