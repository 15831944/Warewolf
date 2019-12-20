using System;
using System.Globalization;
using System.Windows.Data;
using Dev2.CustomControls.Converters;
using NUnit.Framework;
using Moq;



namespace Dev2.CustomControls.Tests.Converters
{
    [TestFixture]
    [SetUpFixture]
    public class BoolToStringConvertTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ConvertBack_GivenAnyArg_ShouldReturnDoNothing()
        {
            //---------------Set up test pack-------------------
            var convert = new BoolToStringConvert();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var convertBack = convert.ConvertBack(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<object>(), It.IsAny<CultureInfo>());
            //---------------Test Result -----------------------
            Assert.AreEqual(Binding.DoNothing, convertBack);
        }
    }
}
