using Dev2.DataList.Contract;
using NUnit.Framework;

namespace Dev2.Data.Tests.TO
{
    [TestFixture]
    [SetUpFixture]
    public class DateTimeVerifyPartTests
    {
        [SetUp]
        public void PreConditions()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-ZA");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-ZA");

            NUnit.Framework.Assert.AreEqual("en-ZA", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            NUnit.Framework.Assert.AreEqual("en-ZA", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
        }

        [Test]
        public void DateTimeVerifyPart_ShouldCreateDateTiimePart()
        {
            var dataListVerifyPart = IntellisenseFactory.CreateDateTimePart("2008", "Year");
            NUnit.Framework.Assert.IsNotNull(dataListVerifyPart);
        }
    }
}
