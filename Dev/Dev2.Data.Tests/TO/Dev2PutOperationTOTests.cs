using Dev2.PathOperations;
using NUnit.Framework;

namespace Dev2.Data.Tests.Operations
{
    [TestFixture]
    public class Dev2PutOperationTOTests
    {
        [Test]
        public void Dev2PutOperationTO_Should()
        {
            var operationToFactory = new Dev2PutOperationTOFactory();
            var dev2PutOperationTo = operationToFactory.CreateDev2PutOperationTO(true, "SomeContent", true);
            NUnit.Framework.Assert.IsNotNull(dev2PutOperationTo);
            NUnit.Framework.Assert.IsTrue(dev2PutOperationTo.Append);
            NUnit.Framework.Assert.IsFalse(string.IsNullOrEmpty(dev2PutOperationTo.FileContents));
        }
    }
}
