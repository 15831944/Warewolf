/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Linq;
using Dev2.Runtime.Configuration.ComponentModel;
using Dev2.Runtime.Configuration.Settings;
using Dev2.Runtime.Configuration.Tests.XML;
using NUnit.Framework;

namespace Dev2.Runtime.Configuration.Tests.Settings
{
    [TestFixture]
    [SetUpFixture]
    public class LoggingSettingsTests
    {
        [Test]
        public void LoggingSettingsConstructionEmptyExpectEmptyWorkflowsAndFalseSettings()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("Settings"));
            var logging = config.Logging;

            NUnit.Framework.Assert.IsTrue(logging.ServiceInput == "");
            NUnit.Framework.Assert.IsTrue(logging.LogFileDirectory == "");
            NUnit.Framework.Assert.IsFalse(logging.LogAll);
            NUnit.Framework.Assert.IsTrue(logging.NestedLevelCount == 0);
            NUnit.Framework.Assert.IsFalse(logging.IsOutputLogged);
            NUnit.Framework.Assert.IsFalse(logging.IsInputLogged);
            NUnit.Framework.Assert.IsFalse(logging.IsDataAndTimeLogged);
            NUnit.Framework.Assert.IsFalse(logging.IsDurationLogged);
            NUnit.Framework.Assert.IsFalse(logging.IsTypeLogged);
            NUnit.Framework.Assert.IsFalse(logging.IsVersionLogged);
            NUnit.Framework.Assert.IsFalse(logging.IsLoggingEnabled);
            NUnit.Framework.Assert.IsTrue(logging.Workflows.Count == 0);
        }

        [Test]
        public void LoggingSettingsConstructionWithoutPostWorkflowExpectWorkflowsAndSettingsSet()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("NonEmptySettings"));
            var logging = config.Logging;

            NUnit.Framework.Assert.IsTrue(logging.ServiceInput == "TestInput");
            NUnit.Framework.Assert.IsTrue(logging.LogFileDirectory == "TestDir");
            NUnit.Framework.Assert.IsFalse(logging.LogAll);
            NUnit.Framework.Assert.IsTrue(logging.NestedLevelCount == 2);
            NUnit.Framework.Assert.IsTrue(logging.IsOutputLogged);
            NUnit.Framework.Assert.IsTrue(logging.IsInputLogged);
            NUnit.Framework.Assert.IsTrue(logging.IsDataAndTimeLogged);
            NUnit.Framework.Assert.IsTrue(logging.IsDurationLogged);
            NUnit.Framework.Assert.IsTrue(logging.IsTypeLogged);
            NUnit.Framework.Assert.IsTrue(logging.IsVersionLogged);
            NUnit.Framework.Assert.IsTrue(logging.IsLoggingEnabled);
            NUnit.Framework.Assert.IsTrue(logging.Workflows.Count == 1);
            NUnit.Framework.Assert.IsFalse(logging.RunPostWorkflow);
            NUnit.Framework.Assert.IsNull(logging.PostWorkflow);
        }

        [Test]
        public void LoggingSettingsConstructionWithPostWorkflowExpectSettingsSet()
        {
            var config = new Configuration.Settings.Configuration(XmlResource.Fetch("SettingsWithPostWorkflow"));
            var logging = config.Logging;

            NUnit.Framework.Assert.IsTrue(logging.RunPostWorkflow);
            NUnit.Framework.Assert.IsNotNull(logging.PostWorkflow);
            NUnit.Framework.Assert.IsTrue(logging.Workflows.Any(wf => wf.ResourceID == logging.PostWorkflow.ResourceID));
            NUnit.Framework.Assert.IsFalse(logging.IsInitializing);
        }

        [Test]
        public void ToXmlSerializesCorrectly()
        {
            var logging = new LoggingSettings("localhost");

            logging.ServiceInput = "TestInput";
            logging.LogFileDirectory = "TestDir";
            logging.LogAll = true;
            logging.NestedLevelCount = 2;
            logging.IsOutputLogged = true;
            logging.IsInputLogged = true;
            logging.IsDataAndTimeLogged = true;
            logging.IsDurationLogged = true;
            logging.IsTypeLogged = true;
            logging.IsVersionLogged = true;
            logging.IsLoggingEnabled = true;
            var workflow = new WorkflowDescriptor(XmlResource.Fetch("Workflow"));
            logging.Workflows.Add(workflow);
            logging.PostWorkflow = workflow;

            var actual = logging.ToXml().ToString();
            var expected = XmlResource.Fetch("LoggingSettings").ToString();
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }
    }
}
