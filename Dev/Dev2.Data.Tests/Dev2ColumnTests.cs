using Dev2.Data.Interfaces.Enums;
using Dev2.DataList.Contract;
using NUnit.Framework;

namespace Dev2.Data.Tests
{
    [TestFixture]
    public class Dev2ColumnTests
    {
        [Test]
        public void GivenUnEqualColumns_Dev2Column_ShoudEquals_ShouldReturnFalse()
        {
            var dev2Column = DataListFactory.CreateDev2Column("Column1", "Column1Description", true,
                enDev2ColumnArgumentDirection.None);
            var other = DataListFactory.CreateDev2Column("OtherColumn", "OtherColumnDescription", true,
                enDev2ColumnArgumentDirection.None);
            NUnit.Framework.Assert.IsNotNull(dev2Column);
            NUnit.Framework.Assert.IsFalse(dev2Column.Equals(other));
            var dev2Column2 = dev2Column;
            NUnit.Framework.Assert.IsTrue(dev2Column == dev2Column2);
        }
    }
}
