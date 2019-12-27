/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common.ExtMethods;
using Dev2.Data.Interfaces.Enums;
using NUnit.Framework;

namespace Dev2.Data.Tests.EnumTests
{
    /// <summary>
    /// Summary description for enTypeOfSystemInformationTests
    /// </summary>
    [TestFixture]
    public class enTypeOfSystemInformationTests
    {
        TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [OneTimeSetUp]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [Test]
        public void OperatingSystemEnumExpectedDiscriptionOfOperatingSystem()
        {
            var disc = enTypeOfSystemInformationToGather.OperatingSystem.GetDescription();
            NUnit.Framework.Assert.AreEqual("Operating System", disc);
        }

        [Test]
        public void ServicePackEnumExpectedDiscriptionOfServicePack()
        {
            var disc = enTypeOfSystemInformationToGather.ServicePack.GetDescription();
            NUnit.Framework.Assert.AreEqual("Service Pack", disc);
        }

        [Test]
        public void OsBitValueEnumExpectedDiscriptionOf32Slash64Bit()
        {
            var disc = enTypeOfSystemInformationToGather.OSBitValue.GetDescription();
            NUnit.Framework.Assert.AreEqual("32/64 Bit", disc);
        }

        [Test]
        public void DateAndTimeEnumExpectedDiscriptionOfDateAndTime()
        {
            var disc = enTypeOfSystemInformationToGather.FullDateTime.GetDescription();
            NUnit.Framework.Assert.AreEqual("Date & Time", disc);
        }

        [Test]
        public void DateTimeFormatEnumExpectedDiscriptionOfDateAndTimeFormat()
        {
            var disc = enTypeOfSystemInformationToGather.DateTimeFormat.GetDescription();
            NUnit.Framework.Assert.AreEqual("Date & Time Format", disc);
        }

        [Test]
        public void DiskAvailableEnumExpectedDiscriptionOfDiskAvailable()
        {
            var disc = enTypeOfSystemInformationToGather.DiskAvailable.GetDescription();
            NUnit.Framework.Assert.AreEqual("Disk Available (GB)", disc);
        }

        [Test]
        public void DiskTotalEnumExpectedDiscriptionOfDiskTotal()
        {
            var disc = enTypeOfSystemInformationToGather.DiskTotal.GetDescription();
            NUnit.Framework.Assert.AreEqual("Disk Total (GB)", disc);
        }

        [Test]
        public void MemoryAvailableEnumExpectedDiscriptionOfMemoryAvailable()
        {
            var disc = enTypeOfSystemInformationToGather.PhysicalMemoryAvailable.GetDescription();
            NUnit.Framework.Assert.AreEqual("RAM Available (MB)", disc);
        }

        [Test]
        public void MemoryTotalEnumExpectedDiscriptionOfMemoryTotal()
        {
            var disc = enTypeOfSystemInformationToGather.PhysicalMemoryTotal.GetDescription();
            NUnit.Framework.Assert.AreEqual("RAM Total (MB)", disc);
        } 

        [Test]
        public void CPUAvailableEnumExpectedDiscriptionOfCPUAvailable()
        {
            var disc = enTypeOfSystemInformationToGather.CPUAvailable.GetDescription();
            NUnit.Framework.Assert.AreEqual("CPU Available", disc);
        }

        [Test]
        public void CPUTotalEnumExpectedDiscriptionOfCPUTotal()
        {
            var disc = enTypeOfSystemInformationToGather.CPUTotal.GetDescription();
            NUnit.Framework.Assert.AreEqual("CPU Total", disc);
        }

        [Test]
        public void LanguageEnumExpectedDiscriptionOfLanguage()
        {
            var disc = enTypeOfSystemInformationToGather.Language.GetDescription();
            NUnit.Framework.Assert.AreEqual("Language", disc);
        }
        
        [Test]
        public void RegionEnumExpectedDiscriptionOfRegion()
        {
            var disc = enTypeOfSystemInformationToGather.Region.GetDescription();
            NUnit.Framework.Assert.AreEqual("Region", disc);
        }

        [Test]
        public void UserRolesEnumExpectedDiscriptionOfUserRoles()
        {
            var disc = enTypeOfSystemInformationToGather.UserRoles.GetDescription();
            NUnit.Framework.Assert.AreEqual("User Roles", disc);
        }

        [Test]
        public void UserNameEnumExpectedDiscriptionOfUserName()
        {
            var disc = enTypeOfSystemInformationToGather.UserName.GetDescription();
            NUnit.Framework.Assert.AreEqual("User Name", disc);
        }
        
        [Test]
        public void DomainEnumExpectedDiscriptionOfDomain()
        {
            var disc = enTypeOfSystemInformationToGather.Domain.GetDescription();
            NUnit.Framework.Assert.AreEqual("Domain", disc);
        }

    }
}
