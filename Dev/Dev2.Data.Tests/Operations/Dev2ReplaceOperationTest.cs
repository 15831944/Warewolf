using Dev2.Common.Interfaces.Data.TO;
using Dev2.Data.Interfaces;
using Dev2.Data.Operations;
using NUnit.Framework;

namespace Dev2.Data.Tests.Operations
{
    [TestFixture]
    public class Dev2ReplaceOperationTests
    {
        IDev2ReplaceOperation _dev2ReplaceOperation;

        [SetUp]
        public void Initialization()
        {
            _dev2ReplaceOperation = Dev2OperationsFactory.CreateReplaceOperation();
        }

        [Test]
        [Author("Sanele Mthembu")]
        public void Dev2ReplaceOperation_ShouldHaveInstance()
        {
            Assert.IsNotNull(_dev2ReplaceOperation);
        }

        [Test]
        [Author("Sanele Mthembu")]
        public void GivenReplaceFolderWithC_Dev2ReplaceOperation_Replace_ShouldReturnColder()
        {
            Assert.IsNotNull(_dev2ReplaceOperation);
            var replaceCount = 0;
            var result = _dev2ReplaceOperation.Replace("FOLDER", "F", "C", false, out IErrorResultTO errorResultTo, ref replaceCount);
            Assert.AreEqual("COLDER", result);
        }
    }
}
