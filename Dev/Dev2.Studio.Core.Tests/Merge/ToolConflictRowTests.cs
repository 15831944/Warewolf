using Dev2.Core.Tests.Merge.Utils;
using NUnit.Framework;

namespace Dev2.Core.Tests.Merge
{
    [TestFixture]
    [SetUpFixture]
    public class ToolConflictRowTests : MergeTestUtils
    {
        [Test]
        [Author("Pieter Terblanche")]
        public void ToolConflictRow_CreateConflictRow()
        {
            var conflictRow = CreateConflictRow();

            Assert.IsNotNull(conflictRow.Current);
            Assert.IsNotNull(conflictRow.Different);
            Assert.IsNotNull(conflictRow.Connectors);

            Assert.AreNotEqual(conflictRow.Current, conflictRow.Different);
            Assert.AreNotEqual(conflictRow.Different, conflictRow.Current);
        }

        [Test]
        [Author("Pieter Terblanche")]
        public void ToolConflictRow_CreateStartRow()
        {
            var conflictRow = CreateStartRow();

            Assert.IsNotNull(conflictRow.Current);
            Assert.IsNotNull(conflictRow.Different);

            Assert.AreEqual(conflictRow.Current, conflictRow.Different);
            Assert.AreEqual(conflictRow.Different, conflictRow.Current);
        }
    }
}
