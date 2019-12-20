/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Collections.Generic;
using System.IO;
using ActivityUnitTests;
using Dev2.Data.Interfaces;
using Dev2.Diagnostics;
using Dev2.Tests.Activities.Mocks;
using NUnit.Framework;
using Unlimited.Applications.BusinessDesignStudio.Activities;

namespace Dev2.Tests.Activities.ActivityTests
{
    [TestFixture]
    [SetUpFixture]
    public class UnZipTests : BaseActivityUnitTest
    {
        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("DsfUnZip_Constructor")]
        public void DsfUnZip_Constructor_DisplayName_Unzip()
        {
            //------------Setup for test--------------------------

            //------------Execute Test---------------------------
            var dsfUnZip = new DsfUnZip();

            //------------Assert Results-------------------------
            Assert.AreEqual("Unzip", dsfUnZip.DisplayName);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DsfUnZip_Execute")]
        public void Unzip_Execute_Workflow_SourceFile_And_DestinationFile_Has_Separate_Passwords_Both_Passwords_Are_Sent_To_OperationBroker()
        {
            var fileNames = new List<string>();
            var guid = Guid.NewGuid();
            fileNames.Add(Path.Combine(Environment.CurrentDirectory, guid + "Dev2.txt"));

            foreach (string fileName in fileNames)
            {
                File.Delete(fileName);
            }

            var activityOperationBrokerMock = new ActivityOperationBrokerMock();

            var act = new DsfUnZip
            {
                InputPath = @"c:\OldFile.txt",
                OutputPath = Path.Combine(Environment.CurrentDirectory, "NewName.txt"),
                Result = "[[res]]",
                DestinationUsername = "destUName",
                DestinationPassword = "destPWord",
                Username = "uName",
                Password = "pWord",
                GetOperationBroker = () => activityOperationBrokerMock
            };

            CheckPathOperationActivityDebugInputOutput(act, ActivityStrings.DebugDataListShape,
                                                       ActivityStrings.DebugDataListWithData, out List<DebugItem> inRes, out List<DebugItem> outRes);

            Assert.AreEqual(activityOperationBrokerMock.Destination.IOPath.Password, "destPWord");
            Assert.AreEqual(activityOperationBrokerMock.Destination.IOPath.Username, "destUName");
            Assert.AreEqual(activityOperationBrokerMock.Source.IOPath.Password, "pWord");
            Assert.AreEqual(activityOperationBrokerMock.Source.IOPath.Username, "uName");
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("DsfUnzip_Construct")]
        public void UnZip_Construct_Object_Must_Be_OfType_IDestinationUsernamePassword()
        {
            var unzip = new DsfUnZip();
            IDestinationUsernamePassword password = unzip;
            Assert.IsNotNull(password);
        }
    }
}
