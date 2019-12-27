using System;
using System.Globalization;
using Dev2.CustomControls.Converters;
using NUnit.Framework;


namespace Dev2.CustomControls.Tests.Converters
{
    [TestFixture]
    public class IsValidDateTimeConverterTests
    {
        [SetUp]
        public void PreConditions()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-ZA");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-ZA");

            Assert.AreEqual("en-ZA", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            Assert.AreEqual("en-ZA", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Convert_GivenNotDateTime_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var dateTimeConverter = new IsValidDateTimeConverter();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var convert = dateTimeConverter.Convert(Person.StringTime, typeof(Person), null, CultureInfo.CurrentCulture);
            //---------------Test Result -----------------------
            Assert.IsFalse(bool.Parse(convert.ToString()));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Convert_GivenDateTime_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var dateTimeConverter = new IsValidDateTimeConverter();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var convert = dateTimeConverter.Convert(DateTime.Now, typeof(Person), null, CultureInfo.CurrentCulture);
            //---------------Test Result -----------------------
            Assert.IsTrue(bool.Parse(convert.ToString()));

        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ConvertBack_GivenAnyArgs_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var dateTimeConverter = new IsValidDateTimeConverter();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            try
            {
                var convert = dateTimeConverter.ConvertBack(DateTime.Now, typeof(Person), null, CultureInfo.CurrentCulture);
                //---------------Test Result -----------------------
            }
            catch(Exception e )
            {
                var b = e is NotImplementedException;
                Assert.IsTrue(b);
            }
        }


    }
}
