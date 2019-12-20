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
using System.Text;
using Dev2.Common.Interfaces.Infrastructure.SharedModels;
using Dev2.Runtime.Compiler;
using NUnit.Framework;


namespace Dev2.Tests.Runtime.CompileMessages
{
    /// <summary>
    /// Summary description for ServiceModelCompilerTest
    /// </summary>
    [TestFixture]
    [SetUpFixture]
    public class ServiceModelCompilerTest
    {

        [Test]
        public void CanCompileWorkflowAndDetectMappingCountChange()
        {
            var smc = new ServiceModelCompiler();

            const string PreStr = @"<Service ID=""bd4ceb89-0e68-4094-ad6e-b79cd256c3e6"" Version=""1.0"" ServerID=""51a58300-7e9d-4927-a57b-e5d700b11b55"" Name=""Unsaved 1"" ResourceType=""WorkflowService""><DataList><a/></DataList><DisplayName>Unsaved 1</DisplayName><Category>Unassigned</Category><IsNewWorkflow>false</IsNewWorkflow><AuthorRoles></AuthorRoles><Comment></Comment><Tags></Tags><IconPath></IconPath><HelpLink></HelpLink><UnitTestTargetWorkflowService></UnitTestTargetWorkflowService><DataList /><Action Name=""InvokeWorkflow"" Type=""Workflow""><XamlDefinition>&lt;Activity x:Class=""Unsaved 1"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" xmlns:dc=""clr-namespace:Dev2.Common;assembly=Dev2.CommonDataUtils"" xmlns:ddc=""clr-namespace:Dev2.DataList.Contract;assembly=Dev2.Data"" xmlns:ddcb=""clr-namespace:Dev2.DataList.Contract.Binary_Objects;assembly=Dev2.Data"" xmlns:ddd=""clr-namespace:Dev2.Data.Decision;assembly=Dev2.Data"" xmlns:dddo=""clr-namespace:Dev2.Data.Decisions.Operations;assembly=Dev2.Data"" xmlns:ddsm=""clr-namespace:Dev2.Data.SystemTemplates.Models;assembly=Dev2.Data"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:sco=""clr-namespace:System.Collections.ObjectModel;assembly=mscorlib"" xmlns:uaba=""clr-namespace:Unlimited.Applications.BusinessDesignStudio.Activities;assembly=Dev2.Activities"" xmlns:uf=""clr-namespace:Unlimited.Framework;assembly=Dev2.Core"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""&gt;&lt;x:Members&gt;&lt;x:Property Name=""AmbientDataList"" Type=""InOutArgument(scg:List(x:String))"" /&gt;&lt;x:Property Name=""ParentWorkflowInstanceId"" Type=""InOutArgument(s:Guid)"" /&gt;&lt;x:Property Name=""ParentServiceName"" Type=""InOutArgument(x:String)"" /&gt;&lt;/x:Members&gt;&lt;mva:VisualBasic.Settings&gt;Assembly references and imported namespaces serialized as XML namespaces&lt;/mva:VisualBasic.Settings&gt;&lt;TextExpression.NamespacesForImplementation&gt;&lt;scg:List x:TypeArguments=""x:String"" Capacity=""7""&gt;&lt;x:String&gt;Dev2.CommonDataUtils&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.Decisions.Operations&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.SystemTemplates.Models&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract.Binary_Objects&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Framework&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Applications.BusinessDesignStudio.Activities&lt;/x:String&gt;&lt;/scg:List&gt;&lt;/TextExpression.NamespacesForImplementation&gt;&lt;TextExpression.ReferencesForImplementation&gt;&lt;sco:Collection x:TypeArguments=""AssemblyReference""&gt;&lt;AssemblyReference&gt;Dev2.CommonDataUtils&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Data&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Core&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Activities&lt;/AssemblyReference&gt;&lt;/sco:Collection&gt;&lt;/TextExpression.ReferencesForImplementation&gt;&lt;Flowchart DisplayName=""Unsaved 1""&gt;&lt;Flowchart.Variables&gt;&lt;Variable x:TypeArguments=""scg:List(x:String)"" Name=""InstructionList"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""LastResult"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""HasError"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""ExplicitDataList"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""IsValid"" /&gt;&lt;Variable x:TypeArguments=""uf:UnlimitedObject"" Name=""d"" /&gt;&lt;Variable x:TypeArguments=""uaba:Util"" Name=""t"" /&gt;&lt;Variable x:TypeArguments=""ddd:Dev2DataListDecisionHandler"" Name=""Dev2DecisionHandler"" /&gt;&lt;/Flowchart.Variables&gt;&lt;Flowchart.StartNode&gt;&lt;x:Null /&gt;&lt;/Flowchart.StartNode&gt;&lt;/Flowchart&gt;&lt;/Activity&gt;</XamlDefinition></Action><BizRule /><WorkflowActivityDef /><Source /></Service>";

            const string PostStr = @"<Service ID=""bd4ceb89-0e68-4094-ad6e-b79cd256c3e6"" Version=""1.0"" ServerID=""51a58300-7e9d-4927-a57b-e5d700b11b55"" Name=""Unsaved 1"" ResourceType=""WorkflowService""><DataList><a/><b/></DataList><DisplayName>Unsaved 1</DisplayName><Category>Unassigned</Category><IsNewWorkflow>false</IsNewWorkflow><AuthorRoles></AuthorRoles><Comment></Comment><Tags></Tags><IconPath></IconPath><HelpLink></HelpLink><UnitTestTargetWorkflowService></UnitTestTargetWorkflowService><DataList /><Action Name=""InvokeWorkflow"" Type=""Workflow""><XamlDefinition>&lt;Activity x:Class=""Unsaved 1"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" xmlns:dc=""clr-namespace:Dev2.Common;assembly=Dev2.CommonDataUtils"" xmlns:ddc=""clr-namespace:Dev2.DataList.Contract;assembly=Dev2.Data"" xmlns:ddcb=""clr-namespace:Dev2.DataList.Contract.Binary_Objects;assembly=Dev2.Data"" xmlns:ddd=""clr-namespace:Dev2.Data.Decision;assembly=Dev2.Data"" xmlns:dddo=""clr-namespace:Dev2.Data.Decisions.Operations;assembly=Dev2.Data"" xmlns:ddsm=""clr-namespace:Dev2.Data.SystemTemplates.Models;assembly=Dev2.Data"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:sco=""clr-namespace:System.Collections.ObjectModel;assembly=mscorlib"" xmlns:uaba=""clr-namespace:Unlimited.Applications.BusinessDesignStudio.Activities;assembly=Dev2.Activities"" xmlns:uf=""clr-namespace:Unlimited.Framework;assembly=Dev2.Core"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""&gt;&lt;x:Members&gt;&lt;x:Property Name=""AmbientDataList"" Type=""InOutArgument(scg:List(x:String))"" /&gt;&lt;x:Property Name=""ParentWorkflowInstanceId"" Type=""InOutArgument(s:Guid)"" /&gt;&lt;x:Property Name=""ParentServiceName"" Type=""InOutArgument(x:String)"" /&gt;&lt;/x:Members&gt;&lt;mva:VisualBasic.Settings&gt;Assembly references and imported namespaces serialized as XML namespaces&lt;/mva:VisualBasic.Settings&gt;&lt;TextExpression.NamespacesForImplementation&gt;&lt;scg:List x:TypeArguments=""x:String"" Capacity=""7""&gt;&lt;x:String&gt;Dev2.CommonDataUtils&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.Decisions.Operations&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.SystemTemplates.Models&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract.Binary_Objects&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Framework&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Applications.BusinessDesignStudio.Activities&lt;/x:String&gt;&lt;/scg:List&gt;&lt;/TextExpression.NamespacesForImplementation&gt;&lt;TextExpression.ReferencesForImplementation&gt;&lt;sco:Collection x:TypeArguments=""AssemblyReference""&gt;&lt;AssemblyReference&gt;Dev2.CommonDataUtils&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Data&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Core&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Activities&lt;/AssemblyReference&gt;&lt;/sco:Collection&gt;&lt;/TextExpression.ReferencesForImplementation&gt;&lt;Flowchart DisplayName=""Unsaved 1""&gt;&lt;Flowchart.Variables&gt;&lt;Variable x:TypeArguments=""scg:List(x:String)"" Name=""InstructionList"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""LastResult"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""HasError"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""ExplicitDataList"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""IsValid"" /&gt;&lt;Variable x:TypeArguments=""uf:UnlimitedObject"" Name=""d"" /&gt;&lt;Variable x:TypeArguments=""uaba:Util"" Name=""t"" /&gt;&lt;Variable x:TypeArguments=""ddd:Dev2DataListDecisionHandler"" Name=""Dev2DecisionHandler"" /&gt;&lt;/Flowchart.Variables&gt;&lt;Flowchart.StartNode&gt;&lt;x:Null /&gt;&lt;/Flowchart.StartNode&gt;&lt;/Flowchart&gt;&lt;/Activity&gt;</XamlDefinition></Action><BizRule /><WorkflowActivityDef /><Source /></Service>";

            var sID = Guid.NewGuid();

            var msgs = smc.Compile(sID, ServerCompileMessageType.WorkflowMappingChangeRule, new StringBuilder(PreStr), new StringBuilder(PostStr));

            NUnit.Framework.Assert.AreEqual(1, msgs.Count);
            NUnit.Framework.Assert.AreEqual(CompileMessageType.MappingChange, msgs[0].MessageType);
            var expected = @"<Args><Input>[{""Name"":""a"",""MapsTo"":"""",""Value"":"""",""IsRecordSet"":false,""RecordSetName"":"""",""IsEvaluated"":false,""DefaultValue"":"""",""IsRequired"":false,""RawValue"":"""",""EmptyToNull"":false,""IsTextResponse"":false,""IsObject"":false,""IsJsonArray"":false},{""Name"":""b"",""MapsTo"":"""",""Value"":"""",""IsRecordSet"":false,""RecordSetName"":"""",""IsEvaluated"":false,""DefaultValue"":"""",""IsRequired"":false,""RawValue"":"""",""EmptyToNull"":false,""IsTextResponse"":false,""IsObject"":false,""IsJsonArray"":false}]</Input><Output>[{""Name"":""a"",""MapsTo"":"""",""Value"":"""",""IsRecordSet"":false,""RecordSetName"":"""",""IsEvaluated"":false,""DefaultValue"":"""",""IsRequired"":false,""RawValue"":"""",""EmptyToNull"":false,""IsTextResponse"":false,""IsObject"":false,""IsJsonArray"":false},{""Name"":""b"",""MapsTo"":"""",""Value"":"""",""IsRecordSet"":false,""RecordSetName"":"""",""IsEvaluated"":false,""DefaultValue"":"""",""IsRequired"":false,""RawValue"":"""",""EmptyToNull"":false,""IsTextResponse"":false,""IsObject"":false,""IsJsonArray"":false}]</Output></Args>";
            var actual = msgs[0].MessagePayload;
            FixBreaks(ref expected, ref actual);
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanCompileWorkflowAndDetectMappingNameChange()
        {
            var smc = new ServiceModelCompiler();

            const string PreStr = @"<Service ID=""bd4ceb89-0e68-4094-ad6e-b79cd256c3e6"" Version=""1.0"" ServerID=""51a58300-7e9d-4927-a57b-e5d700b11b55"" Name=""Unsaved 1"" ResourceType=""WorkflowService""><DataList><a/></DataList><DisplayName>Unsaved 1</DisplayName><Category>Unassigned</Category><IsNewWorkflow>false</IsNewWorkflow><AuthorRoles></AuthorRoles><Comment></Comment><Tags></Tags><IconPath></IconPath><HelpLink></HelpLink><UnitTestTargetWorkflowService></UnitTestTargetWorkflowService><DataList /><Action Name=""InvokeWorkflow"" Type=""Workflow""><XamlDefinition>&lt;Activity x:Class=""Unsaved 1"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" xmlns:dc=""clr-namespace:Dev2.Common;assembly=Dev2.CommonDataUtils"" xmlns:ddc=""clr-namespace:Dev2.DataList.Contract;assembly=Dev2.Data"" xmlns:ddcb=""clr-namespace:Dev2.DataList.Contract.Binary_Objects;assembly=Dev2.Data"" xmlns:ddd=""clr-namespace:Dev2.Data.Decision;assembly=Dev2.Data"" xmlns:dddo=""clr-namespace:Dev2.Data.Decisions.Operations;assembly=Dev2.Data"" xmlns:ddsm=""clr-namespace:Dev2.Data.SystemTemplates.Models;assembly=Dev2.Data"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:sco=""clr-namespace:System.Collections.ObjectModel;assembly=mscorlib"" xmlns:uaba=""clr-namespace:Unlimited.Applications.BusinessDesignStudio.Activities;assembly=Dev2.Activities"" xmlns:uf=""clr-namespace:Unlimited.Framework;assembly=Dev2.Core"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""&gt;&lt;x:Members&gt;&lt;x:Property Name=""AmbientDataList"" Type=""InOutArgument(scg:List(x:String))"" /&gt;&lt;x:Property Name=""ParentWorkflowInstanceId"" Type=""InOutArgument(s:Guid)"" /&gt;&lt;x:Property Name=""ParentServiceName"" Type=""InOutArgument(x:String)"" /&gt;&lt;/x:Members&gt;&lt;mva:VisualBasic.Settings&gt;Assembly references and imported namespaces serialized as XML namespaces&lt;/mva:VisualBasic.Settings&gt;&lt;TextExpression.NamespacesForImplementation&gt;&lt;scg:List x:TypeArguments=""x:String"" Capacity=""7""&gt;&lt;x:String&gt;Dev2.CommonDataUtils&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.Decisions.Operations&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.SystemTemplates.Models&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract.Binary_Objects&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Framework&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Applications.BusinessDesignStudio.Activities&lt;/x:String&gt;&lt;/scg:List&gt;&lt;/TextExpression.NamespacesForImplementation&gt;&lt;TextExpression.ReferencesForImplementation&gt;&lt;sco:Collection x:TypeArguments=""AssemblyReference""&gt;&lt;AssemblyReference&gt;Dev2.CommonDataUtils&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Data&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Core&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Activities&lt;/AssemblyReference&gt;&lt;/sco:Collection&gt;&lt;/TextExpression.ReferencesForImplementation&gt;&lt;Flowchart DisplayName=""Unsaved 1""&gt;&lt;Flowchart.Variables&gt;&lt;Variable x:TypeArguments=""scg:List(x:String)"" Name=""InstructionList"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""LastResult"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""HasError"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""ExplicitDataList"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""IsValid"" /&gt;&lt;Variable x:TypeArguments=""uf:UnlimitedObject"" Name=""d"" /&gt;&lt;Variable x:TypeArguments=""uaba:Util"" Name=""t"" /&gt;&lt;Variable x:TypeArguments=""ddd:Dev2DataListDecisionHandler"" Name=""Dev2DecisionHandler"" /&gt;&lt;/Flowchart.Variables&gt;&lt;Flowchart.StartNode&gt;&lt;x:Null /&gt;&lt;/Flowchart.StartNode&gt;&lt;/Flowchart&gt;&lt;/Activity&gt;</XamlDefinition></Action><BizRule /><WorkflowActivityDef /><Source /></Service>";

            const string PostStr = @"<Service ID=""bd4ceb89-0e68-4094-ad6e-b79cd256c3e6"" Version=""1.0"" ServerID=""51a58300-7e9d-4927-a57b-e5d700b11b55"" Name=""Unsaved 1"" ResourceType=""WorkflowService""><DataList><A/></DataList><DisplayName>Unsaved 1</DisplayName><Category>Unassigned</Category><IsNewWorkflow>false</IsNewWorkflow><AuthorRoles></AuthorRoles><Comment></Comment><Tags></Tags><IconPath></IconPath><HelpLink></HelpLink><UnitTestTargetWorkflowService></UnitTestTargetWorkflowService><DataList /><Action Name=""InvokeWorkflow"" Type=""Workflow""><XamlDefinition>&lt;Activity x:Class=""Unsaved 1"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" xmlns:dc=""clr-namespace:Dev2.Common;assembly=Dev2.CommonDataUtils"" xmlns:ddc=""clr-namespace:Dev2.DataList.Contract;assembly=Dev2.Data"" xmlns:ddcb=""clr-namespace:Dev2.DataList.Contract.Binary_Objects;assembly=Dev2.Data"" xmlns:ddd=""clr-namespace:Dev2.Data.Decision;assembly=Dev2.Data"" xmlns:dddo=""clr-namespace:Dev2.Data.Decisions.Operations;assembly=Dev2.Data"" xmlns:ddsm=""clr-namespace:Dev2.Data.SystemTemplates.Models;assembly=Dev2.Data"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:sco=""clr-namespace:System.Collections.ObjectModel;assembly=mscorlib"" xmlns:uaba=""clr-namespace:Unlimited.Applications.BusinessDesignStudio.Activities;assembly=Dev2.Activities"" xmlns:uf=""clr-namespace:Unlimited.Framework;assembly=Dev2.Core"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""&gt;&lt;x:Members&gt;&lt;x:Property Name=""AmbientDataList"" Type=""InOutArgument(scg:List(x:String))"" /&gt;&lt;x:Property Name=""ParentWorkflowInstanceId"" Type=""InOutArgument(s:Guid)"" /&gt;&lt;x:Property Name=""ParentServiceName"" Type=""InOutArgument(x:String)"" /&gt;&lt;/x:Members&gt;&lt;mva:VisualBasic.Settings&gt;Assembly references and imported namespaces serialized as XML namespaces&lt;/mva:VisualBasic.Settings&gt;&lt;TextExpression.NamespacesForImplementation&gt;&lt;scg:List x:TypeArguments=""x:String"" Capacity=""7""&gt;&lt;x:String&gt;Dev2.CommonDataUtils&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.Decisions.Operations&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.SystemTemplates.Models&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract.Binary_Objects&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Framework&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Applications.BusinessDesignStudio.Activities&lt;/x:String&gt;&lt;/scg:List&gt;&lt;/TextExpression.NamespacesForImplementation&gt;&lt;TextExpression.ReferencesForImplementation&gt;&lt;sco:Collection x:TypeArguments=""AssemblyReference""&gt;&lt;AssemblyReference&gt;Dev2.CommonDataUtils&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Data&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Core&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Activities&lt;/AssemblyReference&gt;&lt;/sco:Collection&gt;&lt;/TextExpression.ReferencesForImplementation&gt;&lt;Flowchart DisplayName=""Unsaved 1""&gt;&lt;Flowchart.Variables&gt;&lt;Variable x:TypeArguments=""scg:List(x:String)"" Name=""InstructionList"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""LastResult"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""HasError"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""ExplicitDataList"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""IsValid"" /&gt;&lt;Variable x:TypeArguments=""uf:UnlimitedObject"" Name=""d"" /&gt;&lt;Variable x:TypeArguments=""uaba:Util"" Name=""t"" /&gt;&lt;Variable x:TypeArguments=""ddd:Dev2DataListDecisionHandler"" Name=""Dev2DecisionHandler"" /&gt;&lt;/Flowchart.Variables&gt;&lt;Flowchart.StartNode&gt;&lt;x:Null /&gt;&lt;/Flowchart.StartNode&gt;&lt;/Flowchart&gt;&lt;/Activity&gt;</XamlDefinition></Action><BizRule /><WorkflowActivityDef /><Source /></Service>";

            var sID = Guid.NewGuid();

            var msgs = smc.Compile(sID, ServerCompileMessageType.WorkflowMappingChangeRule, new StringBuilder(PreStr), new StringBuilder(PostStr));

            NUnit.Framework.Assert.AreEqual(1, msgs.Count);
            NUnit.Framework.Assert.AreEqual(CompileMessageType.MappingChange, msgs[0].MessageType);
            var expected = @"<Args><Input>[{""Name"":""A"",""MapsTo"":"""",""Value"":"""",""IsRecordSet"":false,""RecordSetName"":"""",""IsEvaluated"":false,""DefaultValue"":"""",""IsRequired"":false,""RawValue"":"""",""EmptyToNull"":false,""IsTextResponse"":false,""IsObject"":false,""IsJsonArray"":false}]</Input><Output>[{""Name"":""A"",""MapsTo"":"""",""Value"":"""",""IsRecordSet"":false,""RecordSetName"":"""",""IsEvaluated"":false,""DefaultValue"":"""",""IsRequired"":false,""RawValue"":"""",""EmptyToNull"":false,""IsTextResponse"":false,""IsObject"":false,""IsJsonArray"":false}]</Output></Args>";
            var actual = msgs[0].MessagePayload;
            FixBreaks(ref expected, ref actual);
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanCompileWorkflowAndDetectMappingChangeWhenIODirectionChanges()
        {
            var smc = new ServiceModelCompiler();

            const string PreStr = @"<Service ID=""bd4ceb89-0e68-4094-ad6e-b79cd256c3e6"" Version=""1.0"" ServerID=""51a58300-7e9d-4927-a57b-e5d700b11b55"" Name=""Unsaved 1"" ResourceType=""WorkflowService""><DataList><a ColumnIODirection=""Input""/><b ColumnIODirection=""Output""/></DataList><DisplayName>Unsaved 1</DisplayName><Category>Unassigned</Category><IsNewWorkflow>false</IsNewWorkflow><AuthorRoles></AuthorRoles><Comment></Comment><Tags></Tags><IconPath></IconPath><HelpLink></HelpLink><UnitTestTargetWorkflowService></UnitTestTargetWorkflowService><DataList /><Action Name=""InvokeWorkflow"" Type=""Workflow""><XamlDefinition>&lt;Activity x:Class=""Unsaved 1"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" xmlns:dc=""clr-namespace:Dev2.Common;assembly=Dev2.CommonDataUtils"" xmlns:ddc=""clr-namespace:Dev2.DataList.Contract;assembly=Dev2.Data"" xmlns:ddcb=""clr-namespace:Dev2.DataList.Contract.Binary_Objects;assembly=Dev2.Data"" xmlns:ddd=""clr-namespace:Dev2.Data.Decision;assembly=Dev2.Data"" xmlns:dddo=""clr-namespace:Dev2.Data.Decisions.Operations;assembly=Dev2.Data"" xmlns:ddsm=""clr-namespace:Dev2.Data.SystemTemplates.Models;assembly=Dev2.Data"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:sco=""clr-namespace:System.Collections.ObjectModel;assembly=mscorlib"" xmlns:uaba=""clr-namespace:Unlimited.Applications.BusinessDesignStudio.Activities;assembly=Dev2.Activities"" xmlns:uf=""clr-namespace:Unlimited.Framework;assembly=Dev2.Core"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""&gt;&lt;x:Members&gt;&lt;x:Property Name=""AmbientDataList"" Type=""InOutArgument(scg:List(x:String))"" /&gt;&lt;x:Property Name=""ParentWorkflowInstanceId"" Type=""InOutArgument(s:Guid)"" /&gt;&lt;x:Property Name=""ParentServiceName"" Type=""InOutArgument(x:String)"" /&gt;&lt;/x:Members&gt;&lt;mva:VisualBasic.Settings&gt;Assembly references and imported namespaces serialized as XML namespaces&lt;/mva:VisualBasic.Settings&gt;&lt;TextExpression.NamespacesForImplementation&gt;&lt;scg:List x:TypeArguments=""x:String"" Capacity=""7""&gt;&lt;x:String&gt;Dev2.CommonDataUtils&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.Decisions.Operations&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.SystemTemplates.Models&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract.Binary_Objects&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Framework&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Applications.BusinessDesignStudio.Activities&lt;/x:String&gt;&lt;/scg:List&gt;&lt;/TextExpression.NamespacesForImplementation&gt;&lt;TextExpression.ReferencesForImplementation&gt;&lt;sco:Collection x:TypeArguments=""AssemblyReference""&gt;&lt;AssemblyReference&gt;Dev2.CommonDataUtils&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Data&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Core&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Activities&lt;/AssemblyReference&gt;&lt;/sco:Collection&gt;&lt;/TextExpression.ReferencesForImplementation&gt;&lt;Flowchart DisplayName=""Unsaved 1""&gt;&lt;Flowchart.Variables&gt;&lt;Variable x:TypeArguments=""scg:List(x:String)"" Name=""InstructionList"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""LastResult"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""HasError"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""ExplicitDataList"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""IsValid"" /&gt;&lt;Variable x:TypeArguments=""uf:UnlimitedObject"" Name=""d"" /&gt;&lt;Variable x:TypeArguments=""uaba:Util"" Name=""t"" /&gt;&lt;Variable x:TypeArguments=""ddd:Dev2DataListDecisionHandler"" Name=""Dev2DecisionHandler"" /&gt;&lt;/Flowchart.Variables&gt;&lt;Flowchart.StartNode&gt;&lt;x:Null /&gt;&lt;/Flowchart.StartNode&gt;&lt;/Flowchart&gt;&lt;/Activity&gt;</XamlDefinition></Action><BizRule /><WorkflowActivityDef /><Source /></Service>";

            const string PostStr = @"<Service ID=""bd4ceb89-0e68-4094-ad6e-b79cd256c3e6"" Version=""1.0"" ServerID=""51a58300-7e9d-4927-a57b-e5d700b11b55"" Name=""Unsaved 1"" ResourceType=""WorkflowService""><DataList><a ColumnIODirection=""Input""/><b ColumnIODirection=""Input""/></DataList><DisplayName>Unsaved 1</DisplayName><Category>Unassigned</Category><IsNewWorkflow>false</IsNewWorkflow><AuthorRoles></AuthorRoles><Comment></Comment><Tags></Tags><IconPath></IconPath><HelpLink></HelpLink><UnitTestTargetWorkflowService></UnitTestTargetWorkflowService><DataList /><Action Name=""InvokeWorkflow"" Type=""Workflow""><XamlDefinition>&lt;Activity x:Class=""Unsaved 1"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" xmlns:dc=""clr-namespace:Dev2.Common;assembly=Dev2.CommonDataUtils"" xmlns:ddc=""clr-namespace:Dev2.DataList.Contract;assembly=Dev2.Data"" xmlns:ddcb=""clr-namespace:Dev2.DataList.Contract.Binary_Objects;assembly=Dev2.Data"" xmlns:ddd=""clr-namespace:Dev2.Data.Decision;assembly=Dev2.Data"" xmlns:dddo=""clr-namespace:Dev2.Data.Decisions.Operations;assembly=Dev2.Data"" xmlns:ddsm=""clr-namespace:Dev2.Data.SystemTemplates.Models;assembly=Dev2.Data"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities"" xmlns:s=""clr-namespace:System;assembly=mscorlib"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib"" xmlns:sco=""clr-namespace:System.Collections.ObjectModel;assembly=mscorlib"" xmlns:uaba=""clr-namespace:Unlimited.Applications.BusinessDesignStudio.Activities;assembly=Dev2.Activities"" xmlns:uf=""clr-namespace:Unlimited.Framework;assembly=Dev2.Core"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""&gt;&lt;x:Members&gt;&lt;x:Property Name=""AmbientDataList"" Type=""InOutArgument(scg:List(x:String))"" /&gt;&lt;x:Property Name=""ParentWorkflowInstanceId"" Type=""InOutArgument(s:Guid)"" /&gt;&lt;x:Property Name=""ParentServiceName"" Type=""InOutArgument(x:String)"" /&gt;&lt;/x:Members&gt;&lt;mva:VisualBasic.Settings&gt;Assembly references and imported namespaces serialized as XML namespaces&lt;/mva:VisualBasic.Settings&gt;&lt;TextExpression.NamespacesForImplementation&gt;&lt;scg:List x:TypeArguments=""x:String"" Capacity=""7""&gt;&lt;x:String&gt;Dev2.CommonDataUtils&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.Decisions.Operations&lt;/x:String&gt;&lt;x:String&gt;Dev2.Data.SystemTemplates.Models&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract&lt;/x:String&gt;&lt;x:String&gt;Dev2.DataList.Contract.Binary_Objects&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Framework&lt;/x:String&gt;&lt;x:String&gt;Unlimited.Applications.BusinessDesignStudio.Activities&lt;/x:String&gt;&lt;/scg:List&gt;&lt;/TextExpression.NamespacesForImplementation&gt;&lt;TextExpression.ReferencesForImplementation&gt;&lt;sco:Collection x:TypeArguments=""AssemblyReference""&gt;&lt;AssemblyReference&gt;Dev2.CommonDataUtils&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Data&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Core&lt;/AssemblyReference&gt;&lt;AssemblyReference&gt;Dev2.Activities&lt;/AssemblyReference&gt;&lt;/sco:Collection&gt;&lt;/TextExpression.ReferencesForImplementation&gt;&lt;Flowchart DisplayName=""Unsaved 1""&gt;&lt;Flowchart.Variables&gt;&lt;Variable x:TypeArguments=""scg:List(x:String)"" Name=""InstructionList"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""LastResult"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""HasError"" /&gt;&lt;Variable x:TypeArguments=""x:String"" Name=""ExplicitDataList"" /&gt;&lt;Variable x:TypeArguments=""x:Boolean"" Name=""IsValid"" /&gt;&lt;Variable x:TypeArguments=""uf:UnlimitedObject"" Name=""d"" /&gt;&lt;Variable x:TypeArguments=""uaba:Util"" Name=""t"" /&gt;&lt;Variable x:TypeArguments=""ddd:Dev2DataListDecisionHandler"" Name=""Dev2DecisionHandler"" /&gt;&lt;/Flowchart.Variables&gt;&lt;Flowchart.StartNode&gt;&lt;x:Null /&gt;&lt;/Flowchart.StartNode&gt;&lt;/Flowchart&gt;&lt;/Activity&gt;</XamlDefinition></Action><BizRule /><WorkflowActivityDef /><Source /></Service>";

            var sID = Guid.NewGuid();

            var msgs = smc.Compile(sID, ServerCompileMessageType.WorkflowMappingChangeRule, new StringBuilder(PreStr), new StringBuilder(PostStr));

            NUnit.Framework.Assert.AreEqual(1, msgs.Count);
            NUnit.Framework.Assert.AreEqual(CompileMessageType.MappingChange, msgs[0].MessageType);
            var expected = @"<Args><Input>[{""Name"":""a"",""MapsTo"":"""",""Value"":"""",""IsRecordSet"":false,""RecordSetName"":"""",""IsEvaluated"":false,""DefaultValue"":"""",""IsRequired"":false,""RawValue"":"""",""EmptyToNull"":false,""IsTextResponse"":false,""IsObject"":false,""IsJsonArray"":false},{""Name"":""b"",""MapsTo"":"""",""Value"":"""",""IsRecordSet"":false,""RecordSetName"":"""",""IsEvaluated"":false,""DefaultValue"":"""",""IsRequired"":false,""RawValue"":"""",""EmptyToNull"":false,""IsTextResponse"":false,""IsObject"":false,""IsJsonArray"":false}]</Input><Output>[]</Output></Args>";
            var actual = msgs[0].MessagePayload;
            FixBreaks(ref expected, ref actual);
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        void FixBreaks(ref string expected, ref string actual)
        {
            expected = new StringBuilder(expected).Replace(Environment.NewLine, "\n").Replace("\r", "").ToString();
            actual = new StringBuilder(actual).Replace(Environment.NewLine, "\n").Replace("\r", "").ToString();
        }

    }
}
