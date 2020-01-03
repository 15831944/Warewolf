using System.Collections;
using Dev2.Data.Interfaces.Enums;
using Dev2.DataList.Contract.Translators;
using NUnit.Framework;


namespace Dev2.Data.Tests
{
    [TestFixture]
    public class TranslationConstantsTest
    {
        [Test]
        [Author("Sanele Mthembu")]
        public void TranslationConstants_ShouldReturn()
        {
            var list = TranslationConstants.systemTags as IList;
            Assert.IsTrue(list.Contains(enSystemTag.PostData));
            Assert.IsTrue(list.Contains(enSystemTag.InstanceId));
            Assert.IsTrue(list.Contains(enSystemTag.SystemModel));
        }        
    }
}
