using NUnit.Framework;


namespace Warewolf.Core.Tests
{
    [TestFixture]
    public class ServiceInputTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceInput_Constructor")]
        public void ServiceInput_Constructor_EmptyConstructor_ShouldStillConstruct()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var serviceOutputMapping = new ServiceInput();
            //------------Assert Results-------------------------
            Assert.IsNotNull(serviceOutputMapping);
            Assert.IsFalse(serviceOutputMapping.EmptyIsNull);
            Assert.IsFalse(serviceOutputMapping.RequiredField);
            Assert.IsTrue(string.IsNullOrEmpty(serviceOutputMapping.Dev2ReturnType));
            Assert.IsTrue(string.IsNullOrEmpty(serviceOutputMapping.FullName));
            Assert.IsTrue(string.IsNullOrEmpty(serviceOutputMapping.ShortTypeName));
            Assert.IsFalse(serviceOutputMapping.IsObject);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceInput_Constructor")]
        public void ServiceInput_Constructor_EmptyName_ShouldStillConsturct()
        {
            //------------Setup for test--------------------------
            const string mappingTo = "mapTo";
            //------------Execute Test---------------------------
            var serviceOutputMapping = new ServiceInput("", mappingTo);
            //------------Assert Results-------------------------
            Assert.IsNotNull(serviceOutputMapping);
            Assert.AreEqual(mappingTo, serviceOutputMapping.Value);
            Assert.AreEqual("", serviceOutputMapping.Name);
            Assert.IsTrue(string.IsNullOrEmpty(serviceOutputMapping.Dev2ReturnType));
            Assert.IsTrue(string.IsNullOrEmpty(serviceOutputMapping.FullName));
            Assert.IsTrue(string.IsNullOrEmpty(serviceOutputMapping.ShortTypeName));
            Assert.IsFalse(serviceOutputMapping.IsObject);
            Assert.IsTrue(serviceOutputMapping.EmptyIsNull);
            Assert.IsTrue(serviceOutputMapping.RequiredField);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("ServiceInput_Constructor")]
        public void ServiceInput_FullName_NameAndShortTypeName()
        {
            //------------Setup for test--------------------------
            const string mappingTo = "mapTo";
            //------------Execute Test---------------------------
            var serviceOutputMapping = new ServiceInput("", mappingTo)
            {
                Name = "name",
                ShortTypeName = typeof(string).Name
            };

            //------------Assert Results-------------------------
            Assert.IsNotNull(serviceOutputMapping);
            Assert.AreEqual($"name({typeof(string).Name})", serviceOutputMapping.FullName);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("ServiceInput_Constructor")]
        public void ServiceInput_FullName_NameAndNullShortTypeName()
        {
            //------------Setup for test--------------------------
            const string mappingTo = "mapTo";
            //------------Execute Test---------------------------
            var serviceOutputMapping = new ServiceInput("", mappingTo)
            {
                Name = "name",
            };

            //------------Assert Results-------------------------
            Assert.IsNotNull(serviceOutputMapping);
            Assert.AreEqual("name", serviceOutputMapping.FullName);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("ServiceInput_Constructor")]
        public void ServiceInput_FullName_NullNameAndShortTypeName()
        {
            //------------Setup for test--------------------------
            const string mappingTo = "mapTo";
            //------------Execute Test---------------------------
            var serviceOutputMapping = new ServiceInput("", mappingTo)
            {
                ShortTypeName = "name"
            };

            //------------Assert Results-------------------------
            Assert.IsNotNull(serviceOutputMapping);
            Assert.AreEqual("", serviceOutputMapping.FullName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceInput_Constructor")]
        public void ServiceInput_Constructor_EmptyValue_ShouldStillConsturct()
        {
            //------------Setup for test--------------------------
            const string mappingFrom = "mapFrom";

            //------------Execute Test---------------------------
            var serviceOutputMapping = new ServiceInput(mappingFrom, "");
            //------------Assert Results-------------------------
            Assert.IsNotNull(serviceOutputMapping);
            Assert.AreEqual("", serviceOutputMapping.Value);
            Assert.AreEqual(mappingFrom, serviceOutputMapping.Name);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceInput_Constructor")]
        public void ServiceInput_Constructor_WhenNameValue_ShouldConstructorScalarMappedTo()
        {
            //------------Setup for test--------------------------
            const string mappingFrom = "mapFrom";
            const string variableMapTo = "[[mapTo]]";
            //------------Execute Test---------------------------
            var serviceOutputMapping = new ServiceInput(mappingFrom, variableMapTo);
            //------------Assert Results-------------------------
            Assert.IsNotNull(serviceOutputMapping);
            Assert.AreEqual(mappingFrom, serviceOutputMapping.Name);
            Assert.AreEqual(variableMapTo, serviceOutputMapping.Value);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceInput_Constructor")]
        public void ServiceInput_Constructor_WhenCharInName_ShouldConstructorScalarMappedTo()
        {
            //------------Setup for test--------------------------
            const string mappingFrom = "`mapFrom`";
            const string variableMapTo = "[[mapTo]]";
            //------------Execute Test---------------------------
            var serviceOutputMapping = new ServiceInput(mappingFrom, variableMapTo);
            //------------Assert Results-------------------------
            Assert.IsNotNull(serviceOutputMapping);
            Assert.AreEqual(mappingFrom.Replace("`", ""), serviceOutputMapping.Name);
            Assert.AreEqual(variableMapTo, serviceOutputMapping.Value);
        }



    }
}