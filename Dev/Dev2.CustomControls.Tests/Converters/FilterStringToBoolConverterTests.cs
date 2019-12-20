using System;
using System.Globalization;
using Dev2.CustomControls.Converters;
using NUnit.Framework;


namespace Dev2.CustomControls.Tests.Converters
{
    [TestFixture]
    [SetUpFixture]
    public class FilterStringToBoolConverterTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Convert_GivenEmptyValue_ShouldReturnCorrectly()
        {
            //---------------Set up test pack-------------------
            var boolConverter = new FilterStringToBoolConverter();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            try
            {
                var convert = boolConverter.Convert(Person.EmptyVal, typeof(string), Person.StringTime, CultureInfo.CurrentCulture);
                Assert.IsFalse(bool.Parse(convert.ToString()));
            }
            catch(Exception ex)
            {
                //---------------Test Result -----------------------
              Assert.Fail(ex.Message);
            }
          
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Convert_GivenValue_ShouldReturnCorrectly()
        {
            //---------------Set up test pack-------------------
            var boolConverter = new FilterStringToBoolConverter();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            try
            {
                var convert = boolConverter.Convert(Person.StringTime, typeof(string), Person.StringTime, CultureInfo.CurrentCulture);
                Assert.IsTrue(bool.Parse(convert.ToString()));
            }
            catch(Exception ex)
            {
                //---------------Test Result -----------------------
              Assert.Fail(ex.Message);
            }
          
        }
    }
}
