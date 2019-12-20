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
using Dev2.Common.ExtMethods;
using Dev2.Common.Interfaces.Infrastructure.Providers.Errors;
using Dev2.Common.Interfaces.Infrastructure.SharedModels;
using Dev2.Data.ServiceModel.Messages;
using NUnit.Framework;


namespace Dev2.Data.Tests.TO
{
    [TestFixture]
    [SetUpFixture]
    public class CompileMessageTOTests
    {
        [Test]
        [Category("CompileMessageTOUnitTest")]
        [Description("Test for CompileMessageTO's 'ToErrorInfo' method: A valid CompileMessageTO is constructed and converted to an ErrorInfo object successfully")]
        [Author("Ashley Lewis")]
        public void CompileMessageTO_CompileMessageTOUnitTest_ToErrorInfo_CorrectErrorInfoReturned()
        {
            //init
            var message = new CompileMessageTO();
            var expectedID = Guid.NewGuid();
            message.UniqueID = expectedID;
            message.ErrorType = ErrorType.Critical;
            const FixType expectedFixType = FixType.ReloadMapping;
            message.MessageType = CompileMessageType.MappingChange;
            message.MessagePayload = "Test Fix Data";

            //exe
            var actual = message.ToErrorInfo();

            //aserts
            NUnit.Framework.Assert.AreEqual(expectedID, actual.InstanceID, "ToErrorInfo created an error info object with an incorrect InstanceID");
            NUnit.Framework.Assert.AreEqual(ErrorType.Critical, actual.ErrorType, "ToErrorInfo created an error info object with an incorrect ErrorType");
            NUnit.Framework.Assert.AreEqual(expectedFixType, actual.FixType, "ToErrorInfo created an error info object with an incorrect FixType");
            NUnit.Framework.Assert.AreEqual(CompileMessageType.MappingChange.GetDescription(), actual.Message, "ToErrorInfo created an error info object with an incorrect Message");
            NUnit.Framework.Assert.AreEqual("Test Fix Data", actual.FixData, "ToErrorInfo created an error info object with incorrect FixData");
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("CompileMessageTO_Clone")]
        public void CompileMessageTO_Clone_ShouldCloneAllProperties()
        {
            //------------Setup for test--------------------------
            var message = new CompileMessageTO();
            var uniqueID = Guid.NewGuid();
            var workspaceID = Guid.NewGuid();
            const string serviceName = "Some Service Name";
            var messageID = Guid.NewGuid();
            var serviceID = Guid.NewGuid();
            const ErrorType errorType = ErrorType.Critical;
            const FixType fixType = FixType.ReloadMapping;
            const CompileMessageType messageType = CompileMessageType.MappingChange;
            const string messagePayload = "Test Fix Data";
            message.UniqueID = uniqueID;
            message.WorkspaceID = workspaceID;
            message.ServiceID = serviceID;
            message.MessageID = messageID;
            message.ErrorType = errorType;
            message.ServiceName = serviceName;
            message.MessageType = messageType;
            message.MessagePayload = messagePayload;
            //------------Execute Test---------------------------
            var clonedTO = message.Clone();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(workspaceID, clonedTO.WorkspaceID);
            NUnit.Framework.Assert.AreEqual(messageID, clonedTO.MessageID);
            NUnit.Framework.Assert.AreEqual(serviceID, clonedTO.ServiceID);
            NUnit.Framework.Assert.AreEqual(uniqueID, clonedTO.UniqueID);
            NUnit.Framework.Assert.AreEqual(serviceName, clonedTO.ServiceName);
            NUnit.Framework.Assert.AreEqual(errorType, clonedTO.ErrorType);
            NUnit.Framework.Assert.AreEqual(fixType, clonedTO.ToFixType());
            NUnit.Framework.Assert.AreEqual(messageType, clonedTO.MessageType);
            NUnit.Framework.Assert.AreEqual(messagePayload, clonedTO.MessagePayload);
        }
    }
}
