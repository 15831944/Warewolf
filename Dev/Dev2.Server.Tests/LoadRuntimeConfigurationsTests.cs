using System;
using Dev2.Common;
using Dev2.Runtime.Configuration;
using NUnit.Framework;
using Moq;

namespace Dev2.Server.Tests
{
    [TestFixture]
    public class LoadRuntimeConfigurationsTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(LoadRuntimeConfigurations))]
        public void LoadRuntimeConfigurations_WebServerUri_Null_ExpectFail()
        {
            //-------------------Arrange------------------
            EnvironmentVariables.WebServerUri = null;
            var mockWriter = new Mock<IWriter>();
            //-------------------Act----------------------
            new LoadRuntimeConfigurations(mockWriter.Object).Execute();
            //-------------------Assert-------------------
            mockWriter.Verify(o => o.Write("Loading settings provider...  "), Times.Once);
            mockWriter.Verify(o => o.WriteLine("fail."), Times.Once);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(LoadRuntimeConfigurations))]
        public void LoadRuntimeConfigurations_WebServerUri_NotNull_ExpectFail()
        {
            //-------------------Arrange------------------
            EnvironmentVariables.WebServerUri = "http://TESTUSERNAME:8080/";
            var mockWriter = new Mock<IWriter>();
            //-------------------Act----------------------
            new LoadRuntimeConfigurations(mockWriter.Object).Execute();
            //-------------------Assert-------------------
            mockWriter.Verify(o => o.Write("Loading settings provider...  "), Times.Once);
            mockWriter.Verify(o => o.WriteLine("done."), Times.Once);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(LoadRuntimeConfigurations))]
        public void LoadRuntimeConfigurations_WebServerUri_Null_ExpectFail1()
        {
            //-------------------Arrange------------------
            const string message = "exception message";
            EnvironmentVariables.WebServerUri = null;
            var mockWriter = new Mock<IWriter>();
            var mockSettingsprovider = new Mock<ISettingsProvider>();
            mockSettingsprovider.Setup(o => o.Configuration).Throws(new Exception(message));
            //-------------------Act----------------------

            new LoadRuntimeConfigurations(mockWriter.Object, mockSettingsprovider.Object).Execute();
            //-------------------Assert-------------------
            mockWriter.Verify(o => o.Write("Loading settings provider...  "), Times.Once);
            mockWriter.Verify(o => o.WriteLine("fail."), Times.Once);
        }
    }
}
