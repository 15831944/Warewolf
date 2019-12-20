﻿using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ActivityUnitTests;
using Dev2.Activities.Scripting;
using Dev2.Common.Interfaces.Enums;
using Dev2.Development.Languages.Scripting;
using NUnit.Framework;
using Unlimited.Applications.BusinessDesignStudio.Activities;
using Warewolf.Core;


namespace Dev2.Tests.Activities.ActivityTests.Scripting
{
    [TestFixture]
    [SetUpFixture]
    public class DsfRubyActivityTests : BaseActivityUnitTest
    {
        [OneTimeSetUp]
        public static void Init(TestContext context)
        {
            File.WriteAllBytes(GetRbTmpFile(), Encoding.ASCII.GetBytes(@"def greaterBalanceThanFive(other) return 5 < other end"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Attribute_GivenIsNew_ShouldhaveCorrectValues()
        {
            //---------------Set up test pack-------------------
            var act = new DsfRubyActivity();
            //---------------Assert Precondition----------------
            Assert.IsNotNull(act);
            //---------------Execute Test ----------------------
            var toolDescriptorInfo = typeof(DsfRubyActivity).GetCustomAttributes(typeof(ToolDescriptorInfo), false).Single() as ToolDescriptorInfo;
            //---------------Test Result -----------------------
            
            Assert.AreEqual("Scripting", toolDescriptorInfo.Category );
            Assert.AreEqual("ruby script", toolDescriptorInfo.FilterTag );
            Assert.AreEqual("Scripting-Ruby", toolDescriptorInfo.Icon );
            Assert.AreEqual("Ruby", toolDescriptorInfo.Name );
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void OnConstruction_GivenType_ShouldInheritCorrectly()
        {
            //---------------Set up test pack-------------------
            var act = new DsfRubyActivity();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            Assert.IsInstanceOf(act.GetType(), typeof(DsfActivityAbstract<string>));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_Given_GivenIsNew_ShouldSetJavascript()
        {
            //---------------Set up test pack-------------------
            var act = new DsfRubyActivity();
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(act.GetType(), typeof(DsfActivityAbstract<string>));
            //---------------Execute Test ----------------------
            var displayName = act.DisplayName;
            //---------------Test Result -----------------------
            Assert.AreEqual("Ruby", displayName);

        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Script_GivenIsNew_ShouldBeEmpty()
        {
            //---------------Set up test pack-------------------
            var act = new DsfRubyActivity();
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(act.GetType(), typeof(DsfActivityAbstract<string>));
            //---------------Execute Test ----------------------
            var displayName = act.Script;
            //---------------Test Result -----------------------
            Assert.AreEqual("", displayName);

        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ScriptType_GivenIsNew_ShouldSetJavascript()
        {
            //---------------Set up test pack-------------------
            var act = new DsfRubyActivity();
            //---------------Assert Precondition----------------
            Assert.AreEqual("Ruby", act.DisplayName);
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.AreEqual(enScriptType.Ruby, act.ScriptType);

        }


        #region Should execute valid ruby script

        [Test]
        public void RubytmpHost_ShouldSetDefaultValues()
        {
            var win8Pal = new RubyContext.TmpHost.Win8PAL();
            Assert.IsNotNull(win8Pal);
            Assert.IsFalse(win8Pal.FileExists(win8Pal.CurrentDirectory));
            Assert.IsFalse(win8Pal.DirectoryExists(win8Pal.CurrentDirectory));
        }
        [Test]
        public void RubyOptionsAttribute_ShouldSetDefaultValues()
        {
            var optionsAttribute = new RubyContext.OptionsAttribute();
            Assert.IsNotNull(optionsAttribute);
            Assert.IsFalse(optionsAttribute.NoRuntime);
            Assert.IsFalse(optionsAttribute.PrivateBinding);
            Assert.IsNull(optionsAttribute.Pal);
        }

        [Test]
        public void ExecuteWithEscapeCharecters_RubyExpectedCorrectResultReturned()
        {
            SetupArguments("<DataList><Result>\"C:\test\"</Result></DataList>", "<DataList><Result/></DataList>", "[[Result]]",
                            "return [[Result]]", enScriptType.Ruby, true);

            var result = ExecuteProcess();
            GetScalarValueFromEnvironment(result.Environment, "Result", out string actual, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual("C:\test", actual, "Valid Ruby executed incorrectly");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        [Test]
        public void ExecuteWithoutEscapeCharecters_RubyExpectedCorrectResultReturned()
        {
            SetupArguments("<DataList><Result>C:\test</Result></DataList>", "<DataList><Result/></DataList>", "[[Result]]",
                            "return \"C:\\test\"", enScriptType.Ruby);

            var result = ExecuteProcess();
            GetScalarValueFromEnvironment(result.Environment, "Result", out string actual, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual("C:\test", actual, "Valid Ruby executed incorrectly");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        [Test]
        public void ExecuteWithValidRubyExpectedCorrectResultReturned()
        {
            SetupArguments("<DataList><Result>0</Result></DataList>", "<DataList><Result/></DataList>", "[[Result]]", @"return 1+1;", enScriptType.Ruby);

            var result = ExecuteProcess();
            GetScalarValueFromEnvironment(result.Environment, "Result", out string actual, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual("2", actual, "Valid Ruby executed incorrectly");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        [Test]
        public void ExecuteWithValidRubyAndVariableExpectedCorrectResultReturned()
        {
            SetupArguments("<DataList><Result>0</Result></DataList>", "<DataList><Result/></DataList>", "[[Result]]", @"i = 1 + 1;return i;", enScriptType.Ruby);

            var result = ExecuteProcess();
            GetScalarValueFromEnvironment(result.Environment, "Result", out string actual, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual("2", actual, "Valid Ruby with a variable executed incorrectly");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        [Test]
        public void ExecuteWithValidRubyWithScalarDataListRegionsInScriptExpectedCorrectResultReturned()
        {
            SetupArguments("<DataList><inputData>1</inputData><Result>0</Result></DataList>", "<DataList><inputData/><Result/></DataList>", "[[Result]]", @"i = [[inputData]] + [[inputData]];return i;", enScriptType.Ruby);

            var result = ExecuteProcess();
            GetScalarValueFromEnvironment(result.Environment, "Result", out string actual, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual("2", actual, "Valid Ruby with datalist region executed incorrectly");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        [Test]
        public void ExecuteWithValidRubyWithRecordAppendNotationDataListRegionsInScriptExpectedCorrectResultReturned()
        {
            SetupArguments("<DataList><inputData><field1>1</field1></inputData><inputData><field1>2</field1></inputData><inputData><field1>3</field1></inputData><inputData><field1>4</field1></inputData><Result>0</Result></DataList>", "<DataList><inputData><field1/></inputData><Result/></DataList>", "[[Result]]", @"i = [[inputData().field1]] + [[inputData().field1]];return i;", enScriptType.Ruby);

            var result = ExecuteProcess();
            GetScalarValueFromEnvironment(result.Environment, "Result", out string actual, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual("8", actual, "Valid Ruby with datalist region executed incorrectly");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        [Test]
        public void ExecuteWithValidRubyWithRecordStarNotationDataListRegionsInScriptExpectedCorrectResultReturned()
        {
            SetupArguments("<DataList><inputData><field1>1</field1></inputData><inputData><field1>2</field1></inputData><inputData><field1>3</field1></inputData><inputData><field1>4</field1></inputData></DataList>", "<DataList><inputData><field1/></inputData><Result><res/></Result></DataList>", "[[Result().res]]", @"i = '[[inputData(*).field1]]';return i;", enScriptType.Ruby);

            var result = ExecuteProcess();
            GetRecordSetFieldValueFromDataList(result.Environment, "Result", "res", out IList<string> dataListItems, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual("1", dataListItems[0], "Valid Rubyscript with datalist region executed incorrectly");
                Assert.AreEqual("2", dataListItems[1], "Valid Rubyscript with datalist region executed incorrectly");
                Assert.AreEqual("3", dataListItems[2], "Valid Rubyscript with datalist region executed incorrectly");
                Assert.AreEqual("4", dataListItems[3], "Valid Rubyscript with datalist region executed incorrectly");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        [Test]
        public void ExecuteWithValidRubyWithEmptyRecordStarNotationDataListRegionsInScriptExpectedCorrectResultReturned()
        {
            SetupArguments("<DataList><inputData><field1/></inputData><Result><res/></Result></DataList>", "<DataList><inputData><field1/></inputData><Result><res/></Result></DataList>", "[[Result().res]]", @"i = [[inputData(*).field1]] + [[inputData(*).field1]];return i;", enScriptType.Ruby);

            var result = ExecuteProcess();
            GetRecordSetFieldValueFromDataList(result.Environment, "Result", "res", out IList<string> dataListItems, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual(string.Empty, dataListItems[0], "Valid Ruby with empty Recordset did not evaluate with blank");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        [Test]
        public void ExecuteRubyWithNoReturnExpectedReturnsLastValue()
        {
            SetupArguments("<DataList><Result>0</Result></DataList>", "<DataList><Result/></DataList>", "[[Result]]", @"def Add(x,y); return x + y; end; Add(1,1);", enScriptType.Ruby);

            var result = ExecuteProcess();

            GetScalarValueFromEnvironment(result.Environment, "Result", out string actual, out string error);

            // remove test datalist ;)

            if (string.IsNullOrEmpty(error))
            {
                Assert.AreEqual("2", actual, "Valid Ruby with empty Recordset did not evaluate without return keyword");
            }
            else
            {
                Assert.Fail("The following errors occurred while retrieving datalist items\r\nerrors:{0}", error);
            }
        }

        #endregion Should execute valid ruby script

        #region Private Test Methods

        void SetupArguments(string currentDl, string testData, string result, string script, enScriptType type, bool escape = false)
        {
            TestStartNode = new FlowStep
            {
                Action = new DsfRubyActivity { Result = result, Script = script, ScriptType = type, EscapeScript = escape }
            };

            CurrentDl = testData;
            TestData = currentDl;
        }

        static string GetRbTmpFile()
        {
            var codeBase = Assembly.GetExecutingAssembly().Location;
            var directoryName = Path.GetDirectoryName(codeBase);
            return directoryName + "\\rubyFile.rb";
        }

        #endregion Private Test Methods
    }
}