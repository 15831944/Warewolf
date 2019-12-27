using Dev2.Core.Tests.Merge.Utils;
using NUnit.Framework;

namespace Dev2.Core.Tests.Merge
{
    [TestFixture]
    public class MergePreviewWorkflowStateApplierTests : MergeTestUtils
    {
        [Test]
        [Author("Pieter Terblanche")]
        public void MergePreviewWorkflowStateApplier_Constructor()
        {
            var mergePreviewWorkflowStateApplier = CreateMergePreviewWorkflowStateApplier();
            Assert.IsNotNull(mergePreviewWorkflowStateApplier);
        }
    }
}
