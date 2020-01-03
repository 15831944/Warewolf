using Dev2.Data.TO;
using NUnit.Framework;

namespace Dev2.Data.Tests.Operations
{
    [TestFixture]
    public class UpsertTOTests
    {                               
        [Test]
        [Author("Sanele Mthembu")]
        public void GivenReplaceFolderWithC_Dev2ReplaceOperation_Replace_ShouldReturnColder()
        {
            const string someexpression = "SomeExpression";
            const string somepayload = "SomePayLoad";
            var upsertTo = new UpsertTO(someexpression, somepayload);
            Assert.IsNotNull(upsertTo);
            Assert.AreEqual(someexpression, upsertTo.Expression);
            Assert.AreEqual(somepayload, upsertTo.Payload);
        }
    }
}
