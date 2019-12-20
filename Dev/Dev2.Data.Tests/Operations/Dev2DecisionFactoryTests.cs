using Dev2.Data.Decisions.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.Operations
{
    [TestFixture]
    [SetUpFixture]
    public class Dev2DecisionFactoryTests
    {
        [Test]
        [Author("Sanele Mthmembu")]        
        public void GivenIsLessThanDev2DecisionFactory_HandleType_ShouldReturnIsLessThan()
        {
            var decisionType = enDecisionType.IsLessThan;
            //------------Setup for test--------------------------
            var decisionFactory = new Dev2DecisionFactory();
            //------------Execute Test---------------------------
            var fetchDecisionFunction = decisionFactory.FetchDecisionFunction(decisionType);             
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, fetchDecisionFunction.HandlesType());
        }
        [Test]
        [Author("Sanele Mthmembu")]        
        public void Dev2DecisionFactory_Instance_ShouldHaveAStaticInstance()
        {            
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(Dev2DecisionFactory.Instance());            
        }
    }
}
