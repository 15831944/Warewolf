using Dev2.DataList.Contract;
using NUnit.Framework;

namespace Dev2.Data.Tests.TO
{
    [TestFixture]
    public class DateTimeVerifyPartTests
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
        public void DateTimeVerifyPart_ShouldCreateDateTiimePart()
        {
            var dataListVerifyPart = IntellisenseFactory.CreateDateTimePart("2008", "Year");
            Assert.IsNotNull(dataListVerifyPart);
        }
    }
}
