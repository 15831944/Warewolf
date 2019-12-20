﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.2.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Dev2.Activities.Specs.StudioTestFramework
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("StudioTestFrameworkWithHelloWorldWorkflow")]
    [NUnit.Framework.CategoryAttribute("StudioTestFrameworkWithHelloWorldWorkflow")]
    public partial class StudioTestFrameworkWithHelloWorldWorkflowFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "StudioTestFrameworkWithHelloWorldWorkflow.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "StudioTestFrameworkWithHelloWorldWorkflow", "\tIn order to test the Hello World workflow in warewolf \r\n\tAs a user\r\n\tI want to c" +
                    "reate, edit, delete and update tests in a test window", ProgrammingLanguage.CSharp, new string[] {
                        "StudioTestFrameworkWithHelloWorldWorkflow"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 8
#line 9
  testRunner.Given("test folder is cleaned", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table959 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input Var Name"});
            table959.AddRow(new string[] {
                        "[[a]]"});
#line 10
  testRunner.And("I have \"Workflow 1\" with inputs as", ((string)(null)), table959, "And ");
#line hidden
            TechTalk.SpecFlow.Table table960 = new TechTalk.SpecFlow.Table(new string[] {
                        "Ouput Var Name"});
            table960.AddRow(new string[] {
                        "[[outputValue]]"});
#line 13
  testRunner.And("\"Workflow 1\" has outputs as", ((string)(null)), table960, "And ");
#line hidden
            TechTalk.SpecFlow.Table table961 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input Var Name"});
            table961.AddRow(new string[] {
                        "[[rec().a]]"});
            table961.AddRow(new string[] {
                        "[[rec().b]]"});
#line 16
  testRunner.Given("I have \"Workflow 2\" with inputs as", ((string)(null)), table961, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table962 = new TechTalk.SpecFlow.Table(new string[] {
                        "Ouput Var Name"});
            table962.AddRow(new string[] {
                        "[[returnVal]]"});
#line 20
  testRunner.And("\"Workflow 2\" has outputs as", ((string)(null)), table962, "And ");
#line hidden
            TechTalk.SpecFlow.Table table963 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input Var Name"});
            table963.AddRow(new string[] {
                        "[[A]]"});
            table963.AddRow(new string[] {
                        "[[B]]"});
            table963.AddRow(new string[] {
                        "[[C]]"});
#line 23
  testRunner.Given("I have \"Workflow 3\" with inputs as", ((string)(null)), table963, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table964 = new TechTalk.SpecFlow.Table(new string[] {
                        "Ouput Var Name"});
            table964.AddRow(new string[] {
                        "[[message]]"});
#line 28
  testRunner.And("\"Workflow 3\" has outputs as", ((string)(null)), table964, "And ");
#line hidden
            TechTalk.SpecFlow.Table table965 = new TechTalk.SpecFlow.Table(new string[] {
                        "Input Var Name"});
            table965.AddRow(new string[] {
                        "[[input]]"});
#line 31
  testRunner.Given("I have \"WorkflowWithTests\" with inputs as", ((string)(null)), table965, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table966 = new TechTalk.SpecFlow.Table(new string[] {
                        "Ouput Var Name"});
            table966.AddRow(new string[] {
                        "[[outputValue]]"});
#line 34
  testRunner.And("\"WorkflowWithTests\" has outputs as", ((string)(null)), table966, "And ");
#line hidden
            TechTalk.SpecFlow.Table table967 = new TechTalk.SpecFlow.Table(new string[] {
                        "TestName",
                        "AuthenticationType",
                        "Error",
                        "TestFailing",
                        "TestPending",
                        "TestInvalid",
                        "TestPassed"});
            table967.AddRow(new string[] {
                        "Test1",
                        "Windows",
                        "false",
                        "false",
                        "false",
                        "false",
                        "true"});
            table967.AddRow(new string[] {
                        "Test2",
                        "Windows",
                        "false",
                        "true",
                        "false",
                        "false",
                        "false"});
            table967.AddRow(new string[] {
                        "Test3",
                        "Windows",
                        "false",
                        "false",
                        "false",
                        "true",
                        "false"});
            table967.AddRow(new string[] {
                        "Test4",
                        "Windows",
                        "false",
                        "false",
                        "true",
                        "false",
                        "false"});
#line 37
  testRunner.And("\"WorkflowWithTests\" Tests as", ((string)(null)), table967, "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Run a test with single scalar inputs and outputs failure")]
        public virtual void RunATestWithSingleScalarInputsAndOutputsFailure()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Run a test with single scalar inputs and outputs failure", ((string[])(null)));
#line 44
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 45
 testRunner.Given("the test builder is open with existing service \"Hello World\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 46
 testRunner.And("Tab Header is \"Hello World - Tests\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
 testRunner.When("I click New Test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 48
 testRunner.Then("a new test is added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 49
 testRunner.And("Tab Header is \"Hello World - Tests *\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And("test name starts with \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("username is blank", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And("password is blank", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table968 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table968.AddRow(new string[] {
                        "Name",
                        ""});
#line 53
 testRunner.And("inputs are", ((string)(null)), table968, "And ");
#line hidden
            TechTalk.SpecFlow.Table table969 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table969.AddRow(new string[] {
                        "Message",
                        ""});
#line 56
 testRunner.And("outputs as", ((string)(null)), table969, "And ");
#line 59
 testRunner.And("save is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.And("test status is pending", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
 testRunner.And("test is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table970 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table970.AddRow(new string[] {
                        "Name",
                        "Bob"});
#line 62
 testRunner.And("I update inputs as", ((string)(null)), table970, "And ");
#line hidden
            TechTalk.SpecFlow.Table table971 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table971.AddRow(new string[] {
                        "Message",
                        "Hello Mary."});
#line 65
 testRunner.And("I update outputs as", ((string)(null)), table971, "And ");
#line 68
 testRunner.And("I save", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
 testRunner.When("I run the test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 70
 testRunner.Then("test result is Failed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table972 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table972.AddRow(new string[] {
                        "[[Name]]",
                        "Bob"});
#line 71
 testRunner.Then("service debug inputs as", ((string)(null)), table972, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table973 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table973.AddRow(new string[] {
                        "[[Message]]",
                        "Hello Bob."});
#line 74
 testRunner.And("the service debug outputs as", ((string)(null)), table973, "And ");
#line 77
  testRunner.When("I delete \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 78
 testRunner.Then("The \"DeleteConfirmation\" popup is shown I click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Run a test with mock step")]
        public virtual void RunATestWithMockStep()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Run a test with mock step", ((string[])(null)));
#line 80
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 81
 testRunner.Given("the test builder is open with existing service \"Hello World\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 82
 testRunner.And("Tab Header is \"Hello World - Tests\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.When("I click New Test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 84
 testRunner.Then("a new test is added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 85
 testRunner.And("Tab Header is \"Hello World - Tests *\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
 testRunner.And("test name starts with \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
 testRunner.And("test is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table974 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table974.AddRow(new string[] {
                        "Name",
                        "Bob"});
#line 88
 testRunner.And("I update inputs as", ((string)(null)), table974, "And ");
#line hidden
            TechTalk.SpecFlow.Table table975 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table975.AddRow(new string[] {
                        "Message",
                        "Hello World."});
#line 91
 testRunner.And("I update outputs as", ((string)(null)), table975, "And ");
#line hidden
            TechTalk.SpecFlow.Table table976 = new TechTalk.SpecFlow.Table(new string[] {
                        "Step Name",
                        "Output Variable",
                        "Output Value",
                        "Activity Type"});
            table976.AddRow(new string[] {
                        "If [[Name]] <> (Not Equal)",
                        "Flow Arm",
                        "Blank Input",
                        "Decision"});
#line 94
 testRunner.And("I add mock steps as", ((string)(null)), table976, "And ");
#line 97
 testRunner.And("I save", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
 testRunner.When("I run the test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 99
 testRunner.Then("test result is Passed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table977 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table977.AddRow(new string[] {
                        "[[Name]]",
                        "Bob"});
#line 100
 testRunner.Then("service debug inputs as", ((string)(null)), table977, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table978 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table978.AddRow(new string[] {
                        "[[Message]]",
                        "Hello World."});
#line 103
 testRunner.And("the service debug outputs as", ((string)(null)), table978, "And ");
#line 106
 testRunner.When("I delete \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 107
 testRunner.Then("The \"DeleteConfirmation\" popup is shown I click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Run a test with mock step assign")]
        public virtual void RunATestWithMockStepAssign()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Run a test with mock step assign", ((string[])(null)));
#line 109
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 110
 testRunner.Given("the test builder is open with existing service \"Hello World\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 111
 testRunner.And("Tab Header is \"Hello World - Tests\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
 testRunner.When("I click New Test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 113
 testRunner.Then("a new test is added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 114
 testRunner.And("Tab Header is \"Hello World - Tests *\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 115
 testRunner.And("test name starts with \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 116
 testRunner.And("test is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table979 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table979.AddRow(new string[] {
                        "Name",
                        "Bob"});
#line 117
 testRunner.And("I update inputs as", ((string)(null)), table979, "And ");
#line hidden
            TechTalk.SpecFlow.Table table980 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table980.AddRow(new string[] {
                        "Message",
                        "hello mock"});
#line 120
 testRunner.And("I update outputs as", ((string)(null)), table980, "And ");
#line hidden
            TechTalk.SpecFlow.Table table981 = new TechTalk.SpecFlow.Table(new string[] {
                        "Step Name",
                        "Output Variable",
                        "Output Value",
                        "Activity Type",
                        "Output From",
                        "Output To"});
            table981.AddRow(new string[] {
                        "Set the output variable (1)",
                        "Message",
                        "hello mock",
                        "Assign",
                        "",
                        ""});
#line 123
 testRunner.And("I add mock steps as", ((string)(null)), table981, "And ");
#line 126
 testRunner.And("I save", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 127
 testRunner.When("I run the test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 128
 testRunner.Then("test result is Passed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table982 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table982.AddRow(new string[] {
                        "[[Name]]",
                        "Bob"});
#line 129
 testRunner.Then("service debug inputs as", ((string)(null)), table982, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table983 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table983.AddRow(new string[] {
                        "[[Message]]",
                        "hello mock"});
#line 132
 testRunner.And("the service debug outputs as", ((string)(null)), table983, "And ");
#line 135
 testRunner.When("I delete \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 136
 testRunner.Then("The \"DeleteConfirmation\" popup is shown I click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Run a test with Assert step assign")]
        public virtual void RunATestWithAssertStepAssign()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Run a test with Assert step assign", ((string[])(null)));
#line 138
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 139
 testRunner.Given("the test builder is open with existing service \"Hello World\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 140
 testRunner.And("Tab Header is \"Hello World - Tests\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 141
 testRunner.When("I click New Test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 142
 testRunner.Then("a new test is added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 143
 testRunner.And("Tab Header is \"Hello World - Tests *\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 144
 testRunner.And("test name starts with \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 145
 testRunner.And("test is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table984 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table984.AddRow(new string[] {
                        "Name",
                        "Bob"});
#line 146
 testRunner.And("I update inputs as", ((string)(null)), table984, "And ");
#line hidden
            TechTalk.SpecFlow.Table table985 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table985.AddRow(new string[] {
                        "Message",
                        "hello mock"});
#line 149
 testRunner.And("I update outputs as", ((string)(null)), table985, "And ");
#line hidden
            TechTalk.SpecFlow.Table table986 = new TechTalk.SpecFlow.Table(new string[] {
                        "Step Name",
                        "Output Variable",
                        "Output Value",
                        "Activity Type",
                        "Output From",
                        "Output To"});
            table986.AddRow(new string[] {
                        "Set the output variable (1)",
                        "Message",
                        "hello mock",
                        "Assign",
                        "",
                        ""});
#line 152
 testRunner.And("I add Assert steps as", ((string)(null)), table986, "And ");
#line 155
 testRunner.And("I save", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 156
 testRunner.When("I run the test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 157
 testRunner.Then("test result is Failed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table987 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table987.AddRow(new string[] {
                        "[[Name]]",
                        "Bob"});
#line 158
 testRunner.Then("service debug inputs as", ((string)(null)), table987, "Then ");
#line 161
 testRunner.When("I delete \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 162
 testRunner.Then("The \"DeleteConfirmation\" popup is shown I click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Run a test with Assert step")]
        public virtual void RunATestWithAssertStep()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Run a test with Assert step", ((string[])(null)));
#line 164
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 165
 testRunner.Given("the test builder is open with existing service \"Hello World\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 166
 testRunner.And("Tab Header is \"Hello World - Tests\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 167
 testRunner.When("I click New Test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 168
 testRunner.Then("a new test is added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 169
 testRunner.And("Tab Header is \"Hello World - Tests *\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 170
 testRunner.And("test name starts with \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 171
 testRunner.And("test is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table988 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table988.AddRow(new string[] {
                        "Name",
                        ""});
#line 172
 testRunner.And("I update inputs as", ((string)(null)), table988, "And ");
#line hidden
            TechTalk.SpecFlow.Table table989 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table989.AddRow(new string[] {
                        "Message",
                        "Hello World."});
#line 175
 testRunner.And("I update outputs as", ((string)(null)), table989, "And ");
#line hidden
            TechTalk.SpecFlow.Table table990 = new TechTalk.SpecFlow.Table(new string[] {
                        "Step Name",
                        "Output Variable",
                        "Output Value",
                        "Activity Type",
                        "Output From",
                        "Output To"});
            table990.AddRow(new string[] {
                        "If [[Name]] <> (Not Equal)",
                        "Message",
                        "Blank Input",
                        "Decision",
                        "",
                        ""});
#line 178
 testRunner.And("I add Assert steps as", ((string)(null)), table990, "And ");
#line 181
 testRunner.And("I save", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 182
 testRunner.When("I run the test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 183
 testRunner.Then("test result is Passed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table991 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table991.AddRow(new string[] {
                        "[[Name]]",
                        ""});
#line 184
 testRunner.Then("service debug inputs as", ((string)(null)), table991, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table992 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table992.AddRow(new string[] {
                        "[[Message]]",
                        "Hello World."});
#line 187
 testRunner.And("the service debug outputs as", ((string)(null)), table992, "And ");
#line 190
 testRunner.When("I delete \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 191
 testRunner.Then("The \"DeleteConfirmation\" popup is shown I click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Run a test with single scalar inputs and outputs")]
        public virtual void RunATestWithSingleScalarInputsAndOutputs()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Run a test with single scalar inputs and outputs", ((string[])(null)));
#line 194
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 195
 testRunner.Given("the test builder is open with existing service \"Hello World\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 196
 testRunner.And("Tab Header is \"Hello World - Tests\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 197
 testRunner.When("I click New Test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 198
 testRunner.Then("a new test is added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 199
 testRunner.And("Tab Header is \"Hello World - Tests *\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 200
 testRunner.And("test name starts with \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 201
 testRunner.And("username is blank", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 202
 testRunner.And("password is blank", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table993 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table993.AddRow(new string[] {
                        "Name",
                        ""});
#line 203
 testRunner.And("inputs are", ((string)(null)), table993, "And ");
#line hidden
            TechTalk.SpecFlow.Table table994 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table994.AddRow(new string[] {
                        "Message",
                        ""});
#line 206
 testRunner.And("outputs as", ((string)(null)), table994, "And ");
#line 209
 testRunner.And("save is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 210
 testRunner.And("test status is pending", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 211
 testRunner.And("test is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table995 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table995.AddRow(new string[] {
                        "Name",
                        "Bob"});
#line 212
 testRunner.And("I update inputs as", ((string)(null)), table995, "And ");
#line hidden
            TechTalk.SpecFlow.Table table996 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table996.AddRow(new string[] {
                        "Message",
                        "Hello Bob."});
#line 215
 testRunner.And("I update outputs as", ((string)(null)), table996, "And ");
#line 218
 testRunner.And("I save", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 219
 testRunner.When("I run the test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 220
 testRunner.Then("test result is Passed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table997 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table997.AddRow(new string[] {
                        "[[Name]]",
                        "Bob"});
#line 221
 testRunner.And("service debug inputs as", ((string)(null)), table997, "And ");
#line hidden
            TechTalk.SpecFlow.Table table998 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable",
                        "Value"});
            table998.AddRow(new string[] {
                        "[[Message]]",
                        "Hello Bob."});
#line 224
 testRunner.And("the service debug outputs as", ((string)(null)), table998, "And ");
#line 227
 testRunner.When("I delete \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 228
 testRunner.Then("The \"DeleteConfirmation\" popup is shown I click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Run a passing test and change step type")]
        public virtual void RunAPassingTestAndChangeStepType()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Run a passing test and change step type", ((string[])(null)));
#line 230
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 231
 testRunner.Given("the test builder is open with existing service \"Hello World\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 232
 testRunner.And("Tab Header is \"Hello World - Tests\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 233
 testRunner.When("I click New Test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 234
 testRunner.Then("a new test is added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 235
 testRunner.And("Tab Header is \"Hello World - Tests *\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 236
 testRunner.And("test name starts with \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 237
 testRunner.And("username is blank", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 238
 testRunner.And("password is blank", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 239
 testRunner.And("I Add Decision \"If [[Name]] <> (Not Equal)\" as TestStep", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 240
 testRunner.And("I change Decision \"If [[Name]] <> (Not Equal)\" arm to \"Blank Input\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 241
 testRunner.And("I Add \"Assign a value to Name if blank (1)\" as TestStep", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table999 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Condition",
                        "Value"});
            table999.AddRow(new string[] {
                        "[[Name]]",
                        "=",
                        "World"});
#line 242
 testRunner.And("I add \"Assign a value to Name if blank (1)\" StepOutputs as", ((string)(null)), table999, "And ");
#line 245
 testRunner.And("I Add \"Set the output variable (1)\" as TestStep", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1000 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Condition",
                        "Value"});
            table1000.AddRow(new string[] {
                        "[[Message]]",
                        "=",
                        "Hello World."});
#line 246
 testRunner.And("I add \"Set the output variable (1)\" StepOutputs as", ((string)(null)), table1000, "And ");
#line 249
 testRunner.And("save is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 250
 testRunner.And("test status is pending", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 251
 testRunner.And("test is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1001 = new TechTalk.SpecFlow.Table(new string[] {
                        "Variable Name",
                        "Value"});
            table1001.AddRow(new string[] {
                        "Message",
                        "Hello World."});
#line 252
 testRunner.And("I update outputs as", ((string)(null)), table1001, "And ");
#line 255
 testRunner.And("I save", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 256
 testRunner.When("I run the test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 257
 testRunner.Then("test result is Passed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 258
 testRunner.When("I change step \"If [[Name]] <> (Not Equal)\" to Mock", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 259
 testRunner.Then("I change Decision \"If [[Name]] <> (Not Equal)\" arm to \"Name Input\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 260
 testRunner.When("I run the test", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 261
 testRunner.Then("step \"Assign a value to Name if blank (1)\" is Pending", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 262
 testRunner.When("I delete \"Test 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 263
 testRunner.Then("The \"DeleteConfirmation\" popup is shown I click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
