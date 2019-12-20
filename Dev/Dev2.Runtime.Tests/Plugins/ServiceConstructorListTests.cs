using System.Collections.Generic;
using Dev2.Communication;
using Dev2.Runtime.ServiceModel.Data;
using NUnit.Framework;


namespace Dev2.Tests.Runtime.Plugins
{
    [TestFixture]
    public class ServiceConstructorListTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ToString_GivenIsNotNull_ShouldJsonFormat()
        {
            //---------------Set up test pack-------------------
            var constructorList = new ServiceConstructorList();
            //---------------Assert Precondition----------------
            Assert.IsNotNull(constructorList);
            //---------------Execute Test ----------------------
            Assert.IsInstanceOf(constructorList.GetType(), typeof(List<ServiceConstructor>));
            //---------------Test Result -----------------------
            var s = constructorList.ToString();
            var dev2JsonSerializer = new Dev2JsonSerializer();
            var serialize = dev2JsonSerializer.Serialize(s);
            Assert.IsTrue(!string.IsNullOrEmpty(serialize));
        }
    }
}
