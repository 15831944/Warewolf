using Dev2.Communication;
using Dev2.Runtime.ESB.Management.Services;
using NUnit.Framework;

namespace Dev2.Tests.Runtime.Services
{
    [TestFixture]
    [SetUpFixture]
    public class ServerSettingsTest
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category("SaveServerSettings_Execute")]
        public void SaveServerSettings_Execute_NullValues_ErrorResult()
        {
            //------------Setup for test--------------------------
            var saveServerSettings = new SaveServerSettings();
            var serializer = new Dev2JsonSerializer();
            //------------Execute Test---------------------------
            var jsonResult = saveServerSettings.Execute(null, null);
            var result = serializer.Deserialize<ExecuteMessage>(jsonResult);
            //------------Assert Results-------------------------
            Assert.IsTrue(result.HasError);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category("GetServerSettings_Execute")]
        public void GetServerSettings_Execute_NullValues_ErrorResult()
        {
            //------------Setup for test--------------------------
            var saveServerSettings = new SaveServerSettings();
            var serializer = new Dev2JsonSerializer();
            //------------Execute Test---------------------------
            var jsonResult = saveServerSettings.Execute(null, null);
            var result = serializer.Deserialize<ExecuteMessage>(jsonResult);
            //------------Assert Results-------------------------
            Assert.IsTrue(result.HasError);
        }
    }
}
