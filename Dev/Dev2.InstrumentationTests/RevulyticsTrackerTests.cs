using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using RUISDK_5_3_1;
using System.Configuration;
using Warewolf.UnitTestAttributes;

namespace Dev2.Instrumentation.Tests
{
    [TestFixture]
    public class RevulyticsTrackerTests
    {
        private RevulyticsTracker GetRevulyticsTracker()
        {
            var tracker = RevulyticsTracker.GetTrackerInstance();
            tracker.SdkFilePath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(RUISDK)).Location);
            tracker.ConfigFilePath = ConfigurationManager.AppSettings["ConfigFilePath"];
            tracker.ProductId = ConfigurationManager.AppSettings["ProductID"];
            tracker.AppName = ConfigurationManager.AppSettings["AppName"];
            tracker.ProductUrl = ConfigurationManager.AppSettings["ProductUrl"];
            tracker.AesHexKey = ConfigurationManager.AppSettings["AesHexKey"];

            return tracker;
        }

        private RevulyticsTracker GetRevulyticsTrackerWithIncorrectConfig()
        {
            var tracker = new RevulyticsTracker
            {
                SdkFilePath = "D:\\",
                ConfigFilePath = ConfigurationManager.AppSettings["IConfigFilePath"],
                ProductId = ConfigurationManager.AppSettings["IProductID"],
                AppName = ConfigurationManager.AppSettings["IAppName"],
                ProductUrl = ConfigurationManager.AppSettings["IProductUrl"],
                AesHexKey = ConfigurationManager.AppSettings["IAesHexKey"]
            };

            return tracker;
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_CreateRevulyticsConfigTestSdkException()
        {
            var tracker = RevulyticsTracker.GetTrackerInstance();
            var result = tracker.CreateRevulyticsConfig();
            Assert.AreEqual(result, RUIResult.ok);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_CreateRevulyticsConfigTest()
        {
            var tracker = GetRevulyticsTracker();
            var configResult = tracker.CreateRevulyticsConfig();
            Assert.AreEqual(configResult, RUIResult.ok, "configNotCreated");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_StartSdkTest()
        {
            var tracker = GetRevulyticsTracker();
            tracker.CreateRevulyticsConfig();
            var startSdkResult = tracker.StartSdk();
            Assert.AreEqual(startSdkResult, RUIResult.ok, "sdkNotStarted");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_StopSdkTest()
        {
            var tracker = GetRevulyticsTracker();
            tracker.CreateRevulyticsConfig();
            tracker.StartSdk();
            var stopResult = tracker.StopSdk();
            Assert.AreEqual(stopResult, RUIResult.ok, "sdkAlreadyStopped");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_StartSessionTest()
        {
            var tracker = GetRevulyticsTracker();
            tracker.CreateRevulyticsConfig();
            tracker.StartSdk();
            tracker.Username = "windows\\raju";
            var startSessionResult = tracker.StartSession();
            Assert.AreEqual(startSessionResult, RUIResult.ok, "sdkSuspended");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_StopSessionTest()
        {
            var tracker = GetRevulyticsTracker();
            tracker.CreateRevulyticsConfig();
            tracker.StartSdk();
            tracker.Username = "windows\\raju";
            tracker.StartSession();

            var stopSessionResult = tracker.StopSession();
            Assert.AreEqual(stopSessionResult, RUIResult.ok, "sdkAlreadyStopped");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_SetProductVersionTest()
        {
            var tracker = GetRevulyticsTracker();
            tracker.CreateRevulyticsConfig();
            tracker.StartSdk();
            tracker.ProductVersion = "0.0.0.1";
            var result = tracker.SetProductVersion();
            Assert.AreEqual(result, RUIResult.ok, "Error in setting product version");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_EnableAppplicationTrackerSdkException()
        {
            var tracker = GetRevulyticsTracker();
            tracker.CreateRevulyticsConfig();
            tracker.StartSdk();
            tracker.InformationalVersion = "Git Commit ID, branch name, etc...";
            var result = tracker.SetInformationalVersion();
            Assert.AreEqual(result, RUIResult.ok, "Error in setting informational version");
        }

        [Test]
        public void EnableApplicationTrackerSdkException()
        {
            var tracker = RevulyticsTracker.GetTrackerInstance();
            const string productVersion = "1.0.0.0";
            const string infoVersion = "Some extra info...";
            const string username = "windows\\raju";
            tracker.EnableApplicationTracker(productVersion, infoVersion, username);
            Assert.AreEqual(tracker.EnableApplicationResultStatus, RUIResult.ok);
            Assert.AreEqual(productVersion, tracker.ProductVersion);
            Assert.AreEqual(infoVersion, tracker.InformationalVersion);
            Assert.AreEqual(username, tracker.Username);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_EnableAppplicationTrackerArgumentNullException()
        {
            var tracker = GetRevulyticsTrackerWithIncorrectConfig();
            const string productVersion = "1.0.0.0";
            const string infoVersion = "Some extra info...";
            const string username = "windows\\raju";
            tracker.SdkFilePath = null;
            tracker.EnableApplicationTracker(productVersion, infoVersion, username);
            Assert.AreEqual(RUIResult.invalidConfigPath, tracker.EnableApplicationResultStatus);
            Assert.AreEqual(productVersion, tracker.ProductVersion);
            Assert.AreEqual(infoVersion, tracker.InformationalVersion);
            Assert.AreEqual(username, tracker.Username);
            Assert.IsNull(tracker.SdkFilePath);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_EnableApplicationTrackerTestWithCorrectConfig()
        {
            var tracker = GetRevulyticsTracker();
            const string productVersion = "1.0.0.0";
            const string infoVersion = "Some extra info...";
            const string username = "windows\\raju";
            tracker.EnableApplicationTracker(productVersion, infoVersion, username);
            Assert.IsTrue(tracker.RuiSdk.GetState() == RUIState.running || tracker.RuiSdk.GetState() == RUIState.startedNewRegRunning, "Revulytics Started");
            Assert.AreEqual(productVersion, tracker.ProductVersion);
            Assert.AreEqual(infoVersion, tracker.InformationalVersion);
            Assert.AreEqual(username, tracker.Username);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_EnableAppplicationTrackerTestWithInCorrectonfig()
        {
            var tracker = GetRevulyticsTrackerWithIncorrectConfig();
            const string productVersion = "1.0.0.0";
            const string infoVersion = "Some extra info...";
            const string username = "windows\\raju";
            tracker.EnableApplicationTracker(productVersion, infoVersion, username);
            Assert.AreNotEqual(tracker.EnableApplicationResultStatus, RUIResult.ok, "Config is not created");
            Assert.AreEqual(productVersion, tracker.ProductVersion);
            Assert.AreEqual(infoVersion, tracker.InformationalVersion);
            Assert.AreEqual(username, tracker.Username);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_TrackEventTest()
        {
            var tracker = RevulyticsTracker.GetTrackerInstance();
            const string productVersion = "1.0.0.0";
            const string infoVersion = "Some extra info...";
            const string username = "windows\\raju";
            tracker.EnableApplicationTracker(productVersion, infoVersion, username);
            tracker.TrackEvent("Test Event", "Unit Test");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_TrackCustomEventTest()
        {
            var tracker = RevulyticsTracker.GetTrackerInstance();
            const string productVersion = "1.0.0.0";
            const string infoVersion = "Some extra info...";
            const string username = "windows\\raju";
            tracker.EnableApplicationTracker(productVersion, infoVersion, username);
            tracker.TrackCustomEvent("Test Event", "Unit Test", "custom values");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        public void RevulyticsTracker_DisableAppplicationTrackerTest()
        {
            var tracker = GetRevulyticsTracker();
            const string productVersion = "1.0.0.0";
            const string infoVersion = "Some extra info...";
            const string username = "windows\\raju";
            tracker.EnableApplicationTracker(productVersion, infoVersion, username);
            tracker.DisableApplicationTracker();
            Assert.AreEqual(RUIState.stopped, tracker.RuiSdk.GetState(), "Revulytics stopped");
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(RevulyticsTracker))]
        [ExpectedException(typeof(NullReferenceException))]
        public void RevulyticsTracker_DisableAppplicationTrackerExceptionTest()
        {
            var tracker = GetRevulyticsTracker();
            const string productVersion = "1.0.0.0";
            const string infoVersion = "Some extra info...";
            const string username = "windows\\raju";
            tracker.EnableApplicationTracker(productVersion, infoVersion, username);
            tracker.RuiSdk = null;
            tracker.DisableApplicationTracker();
            tracker.RuiSdk.GetState();
        }
    }
}