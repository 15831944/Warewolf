using Dev2.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.IO;

namespace Dev2.Diagnostics.Test
{
    [TestFixture]
    public class Dev2LoggerTests
    {
        [Test]
        [Author("Sanele Mthembu")]
        public void GetLogMaxSize_Scenerio_Result()
        {
            //------------Setup for test-------------------------
            var value = Dev2Logger.GetLogMaxSize();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(value);
        }

        [Test]
        [Author("Sanele Mthembu")]
        public void GetFileLogLevel_Scenerio_Result()
        {
            //------------Setup for test-------------------------
            var logMaxSize = Dev2Logger.GetFileLogLevel();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(logMaxSize);
        }

        [Test]
        [Author("Sanele Mthembu")]
        public void GetEventLogLevel_Scenerio_Result()
        {
            //------------Setup for test-------------------------
            var value = Dev2Logger.GetEventLogLevel();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(value);
        }

        [Test]
        [Author("Sanele Mthembu")]
        public void GetMappingElement_Scenerio_Result()
        {
            //------------Setup for test-------------------------
            var pr = new PrivateType(typeof(Dev2Logger));
            var value = pr.InvokeStatic("GetMappingElement", "Level0", "ERROR");
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(value);
            NUnit.Framework.Assert.IsNotNull(value);
            NUnit.Framework.Assert.AreEqual(value.ToString(), @"<mapping>
  <level value=""Level0"" />
  <eventLogEntryType value=""ERROR"" />
</mapping>");
        }

        [Test]
        [Author("Ashley Lewis")]
        public void UpdateFileLoggerToProgramData_UpdateFromAsyncRollingFileAppender_ToParallelForwardingAppender()
        {
            //------------Setup for test-------------------------
            string readSettingsBefore = File.ReadAllText("Settings.config");
            NUnit.Framework.Assert.IsTrue(readSettingsBefore.Contains("Log4Net.Async.AsyncRollingFileAppender"));
            NUnit.Framework.Assert.IsFalse(readSettingsBefore.Contains("Log4Net.Async.ParallelForwardingAppender"));
            //------------Execute Test---------------------------
            Dev2Logger.UpdateFileLoggerToProgramData("Settings.config");
            //------------Assert Results-------------------------
            string readSettingsAfter = File.ReadAllText("Settings.config");
            NUnit.Framework.Assert.IsFalse(readSettingsAfter.Contains("Log4Net.Async.AsyncRollingFileAppender"));
            NUnit.Framework.Assert.IsTrue(readSettingsAfter.Contains("Log4Net.Async.ParallelForwardingAppender"));
        }
    };
}