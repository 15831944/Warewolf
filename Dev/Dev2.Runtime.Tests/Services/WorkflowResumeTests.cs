using NUnit.Framework;
using Dev2.Runtime.ESB.Management.Services;

namespace Dev2.Tests.Runtime.Services
{
    [TestFixture]
    [SetUpFixture]
    public class WorkflowResumeTests
    {       
        [Test]
        [Author("Rory McGuire")]
        [Category("WorkflowResume_Execute")]
        public void WorkflowResume_Returns_HandleType_WorkflowResume()
        {
            //------------Setup for test--------------------------
            var workflowResume = new WorkflowResume();
            //------------Execute Test--------------------------- 
            //------------Assert Results-------------------------
            Assert.AreEqual("WorkflowResume", workflowResume.HandlesType());
        }
    }
}
