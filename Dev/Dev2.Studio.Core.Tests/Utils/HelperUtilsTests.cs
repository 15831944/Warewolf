/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/


using Dev2.Common.Interfaces.Studio.Controller;
using Dev2.Studio.Controller;
using Dev2.Utils;
using NUnit.Framework;
using Moq;
using System;
using System.Windows;

namespace Dev2.Core.Tests.Utils
{
    [TestFixture]
    [SetUpFixture]
    public class HelperUtilsTests
    {
        [Test]
        [Author("Candice Daniel")]
        [Category("HelperUtils")]
        public void HelperUtils_GetStudioLogSettingsConfigFile()
        {
            var settingsConfigFile = HelperUtils.GetStudioLogSettingsConfigFile();
            Assert.IsTrue(settingsConfigFile.Contains("Settings.config"));
        }
       
        [Test]
        [Author("Candice Daniel")]
        [Category("HelperUtils")]
        public void HelperUtils_SanitizePath()
        {            
            var path = "C:\\\\ProgramData\\Warewolf\\Server Log";
            var resourceName = "warewolf-Server.log";
            var resCat = HelperUtils.SanitizePath(path, resourceName);
            Assert.AreEqual("C:\\ProgramData\\Warewolf\\Server Log\\warewolf-Server.log", resCat);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("HelperUtils")]
        public void HelperUtils_SanitizePath_StartsWith_Backslash_Root()
        {
            var path = "root\\\\Warewolf\\Server Log";
            var resourceName = "warewolf-Server.log";
            var resCat = HelperUtils.SanitizePath(path, resourceName);
            Assert.AreEqual("Warewolf\\Server Log\\warewolf-Server.log", resCat);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("HelperUtils")]
        public void HelperUtils_SanitizePath_StartsWith_Backslash()
        {
            var path = "\\\\Warewolf\\Server Log";
            var resourceName = "warewolf-Server.log";
            var resCat = HelperUtils.SanitizePath(path, resourceName);
            Assert.AreEqual("\\Warewolf\\Server Log\\warewolf-Server.log", resCat);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("HelperUtils")]
        public void HelperUtils_SanitizePath_Equals_Root()
        {
            var path = "root";
            var resourceName = "warewolf-Server.log";
            var resCat = HelperUtils.SanitizePath(path, resourceName);
            Assert.AreEqual("warewolf-Server.log", resCat);
        }
        [Test]
        [Author("Candice Daniel")]
        [Category("HelperUtils")]
        public void HelperUtils_SanitizePath_Empty_ReturnNothing()
        {
            var resourceName = "warewolf-Server.log";
            var resCat = HelperUtils.SanitizePath("", resourceName);
            Assert.AreEqual("", resCat);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("HelperUtils")]
        public void HelperUtils_GetServerLogSettingsConfigFile()
        {
            var serverLogSettingsConfigFile = HelperUtils.GetServerLogSettingsConfigFile();
            Assert.IsTrue(serverLogSettingsConfigFile.Contains("warewolf-Server.log"));
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category("HelperUtils")]
        public void HelperUtils_ShowTrustRelationshipError_ShowTrustRelationshipError_AreEqual_Expect()
        {
            //-----------------------Arrange--------------------------
            var mockPopupController = new Mock<IPopupController>();
            
            var vv = new SystemException("The trust relationship between this workstation and the primary domain failed");
            //-----------------------Act------------------------------
            HelperUtils.ShowTrustRelationshipError(mockPopupController.Object, vv);
            //-----------------------Assert---------------------------
            mockPopupController.VerifySet(o => o.Header = "Error connecting to server", Times.Once);
            mockPopupController.VerifySet(o => o.ImageType = MessageBoxImage.Error, Times.Once);
            mockPopupController.Verify(o => o.Show(), Times.Once);
            Assert.AreEqual(MessageBoxButton.OK, mockPopupController.Object.Buttons);
        }
    }
}
