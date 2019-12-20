using System.Collections;
using Dev2.Data.Interfaces.Enums;
using Dev2.DataList.Contract.Translators;
using NUnit.Framework;


namespace Dev2.Data.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class TranslationConstantsTest
    {
        [Test]
        [Author("Sanele Mthembu")]
        public void TranslationConstants_ShouldReturn()
        {
            var list = TranslationConstants.systemTags as IList;
            NUnit.Framework.Assert.IsTrue(list.Contains(enSystemTag.PostData));
            NUnit.Framework.Assert.IsTrue(list.Contains(enSystemTag.InstanceId));
            NUnit.Framework.Assert.IsTrue(list.Contains(enSystemTag.SystemModel));
        }        
    }
}
