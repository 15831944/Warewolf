using Dev2.Core.Tests.Merge.Utils;
using NUnit.Framework;

namespace Dev2.Core.Tests.Merge
{
    [TestFixture]
    [SetUpFixture]
    public class ConflictListStateApplierTests : MergeTestUtils
    {
        [Test]
        [Author("Pieter Terblanche")]
        public void MergePreviewWorkflowStateApplier_Constructor()
        {
            var conflictListStateApplier = CreateConflictListStateApplier();
            Assert.IsNotNull(conflictListStateApplier);
        }
    }
}
