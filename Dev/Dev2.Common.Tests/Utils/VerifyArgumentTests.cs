using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Dev2.Common.Tests.Utils
{
    [TestFixture]
    public class VerifyArgumentTests
    {
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(VerifyArgument))]
        public void VerifyArgument_IsNotNull_Passes()
        {
            object obj = new object();
            Dev2.Common.Utils.VerifyArgument.IsNotNull("message", obj);

        }
        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(VerifyArgument))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyArgument_IsNotNull_ArgumentNullException()
        {
            object obj = null;
            Dev2.Common.Utils.VerifyArgument.IsNotNull("message", obj);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(VerifyArgument))]
        public void VerifyArgument_AreNotNull_Passes()
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            list.Add("txt1", new Object());
            list.Add("txt2", new Object());

            Dev2.Common.Utils.VerifyArgument.AreNotNull(list);
        }

        [Test]
        [Author("Candice Daniel")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyArgument_AreNotNull_ArgumentNullException_WholeObjectIsNull()
        {
            Dictionary<string, object> list = null;
            Dev2.Common.Utils.VerifyArgument.AreNotNull(list);
        }

        [Test]
        [Author("Candice Daniel")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyArgument_AreNotNull_ArgumentNullException_ObjectsinDictionaryAreNull()
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            list.Add("obj1", new Object());
            list.Add("obj2", null);
            list.Add("obj3", new Object());
            Dev2.Common.Utils.VerifyArgument.AreNotNull(list);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(VerifyArgument))]
        public void VerifyArgument_IsNotNullOrWhitespace_Passes()
        {
            Dev2.Common.Utils.VerifyArgument.IsNotNullOrWhitespace("message", "testMessage");
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(VerifyArgument))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyArgument_IsNotNullOrWhitespace_ReturnException_IsNull()
        {
            Dev2.Common.Utils.VerifyArgument.IsNotNullOrWhitespace("message",null);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(VerifyArgument))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyArgument_IsNotNullOrWhitespace_ReturnException_IsEmpty()
        {
            Dev2.Common.Utils.VerifyArgument.IsNotNullOrWhitespace("message", "");
        }

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(VerifyArgument))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyArgument_IsNotNullOrWhitespace_ReturnException_IsWhiteSpace()
        {
            Dev2.Common.Utils.VerifyArgument.IsNotNullOrWhitespace("message", " ");
        }
    }
}
