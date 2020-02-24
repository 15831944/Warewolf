﻿/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Activities.RabbitMQ.Publish;
using Dev2.Data.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Dev2.Runtime.Interfaces;
using Dev2.Common.State;
using System.Linq;

namespace Dev2.Tests.Activities.ActivityTests.RabbitMQ.Publish
{
    [TestClass]
    public class PublishRabbitMQActivityTests
    {
        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(PublishRabbitMQActivity))]
        public void PublishRabbitMQActivity_Construct_Paramterless_SetsDefaultPropertyValues()
        {
            //------------Setup for test--------------------------

            //------------Execute Test---------------------------
            var publishRabbitMQActivity = new PublishRabbitMQActivity();
            //------------Assert Results-------------------------
            Assert.IsNotNull(publishRabbitMQActivity);
            Assert.AreEqual("RabbitMQ Publish", publishRabbitMQActivity.DisplayName);
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(PublishRabbitMQActivity))]
        public void PublishRabbitMQActivity_Execute_Sucess()
        {
            //------------Setup for test--------------------------
            var publishRabbitMQActivity = new PublishRabbitMQActivity();

            const string queueName = "Q1", message = "Test Message";
            var body = Encoding.UTF8.GetBytes(message);
            var resourceCatalog = new Mock<IResourceCatalog>();
            var rabbitMQSource = new Mock<RabbitMQSource>();
            var connectionFactory = new Mock<ConnectionFactory>();
            var connection = new Mock<IConnection>();
            var channel = new Mock<IModel>();
            var mockBasicProperties = new Mock<IBasicProperties>();
            mockBasicProperties.SetupAllProperties();
          

            resourceCatalog.Setup(r => r.GetResource<RabbitMQSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(rabbitMQSource.Object);
            connectionFactory.Setup(c => c.CreateConnection()).Returns(connection.Object);
            connection.Setup(c => c.CreateModel()).Returns(channel.Object);
            channel.Setup(c => c.QueueDeclare(queueName, false, false, false, null));
            channel.Setup(c => c.BasicPublish(string.Empty, queueName, null, body));
            channel.Setup(c => c.CreateBasicProperties()).Returns(mockBasicProperties.Object);

            var p = new PrivateObject(publishRabbitMQActivity);
            p.SetProperty("ConnectionFactory", connectionFactory.Object);
            p.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------
            var result = p.Invoke("PerformExecution", new Dictionary<string, string> { { "QueueName", queueName }, { "Message", message } }) as List<string>;

            //------------Assert Results-------------------------
            resourceCatalog.Verify(r => r.GetResource<RabbitMQSource>(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            connectionFactory.Verify(c => c.CreateConnection(), Times.Once);
            connection.Verify(c => c.CreateModel(), Times.Once);
            channel.Verify(c => c.ExchangeDeclare(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()), Times.Once);
            channel.Verify(c => c.QueueDeclare(It.IsAny<String>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()), Times.Once);
            channel.Verify(c => c.BasicPublish(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<IBasicProperties>(), It.IsAny<byte[]>()), Times.Once);
            Assert.AreEqual(result[0], "Success");
            Assert.IsTrue(mockBasicProperties.Object.Persistent);
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(PublishRabbitMQActivity))]
        public void PublishRabbitMQActivity_Execute_With_CorrelationID_Sucess()
        {
            //------------Setup for test--------------------------
            var publishRabbitMQActivity = new PublishRabbitMQActivity();

            const string queueName = "Q1-CorrelationID", correlationID = "CorrelationID-test", message = "Test Message with CorrelationID";
            var body = Encoding.UTF8.GetBytes(message);
            var resourceCatalog = new Mock<IResourceCatalog>();
            var rabbitMQSource = new Mock<RabbitMQSource>();
            var connectionFactory = new Mock<ConnectionFactory>();
            var connection = new Mock<IConnection>();
            var channel = new Mock<IModel>();
            var mockBasicProperties = new Mock<IBasicProperties>();
            mockBasicProperties.SetupAllProperties();
            mockBasicProperties.SetupProperty(m => m.CorrelationId, correlationID);

            resourceCatalog.Setup(r => r.GetResource<RabbitMQSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(rabbitMQSource.Object);
            connectionFactory.Setup(c => c.CreateConnection()).Returns(connection.Object);
            connection.Setup(c => c.CreateModel()).Returns(channel.Object);
            channel.Setup(c => c.QueueDeclare(queueName, false, false, false, null));
            channel.Setup(c => c.BasicPublish(string.Empty, queueName, null, body));
            channel.Setup(c => c.CreateBasicProperties()).Returns(mockBasicProperties.Object);

            var p = new PrivateObject(publishRabbitMQActivity);
            p.SetProperty("ConnectionFactory", connectionFactory.Object);
            p.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------
            var result = p.Invoke("PerformExecution", new Dictionary<string, string> { { "QueueName", queueName }, { "CorrelationID", correlationID }, { "Message", message } }) as List<string>;

            //------------Assert Results-------------------------
            resourceCatalog.Verify(r => r.GetResource<RabbitMQSource>(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            connectionFactory.Verify(c => c.CreateConnection(), Times.Once);
            connection.Verify(c => c.CreateModel(), Times.Once);
            channel.Verify(c => c.ExchangeDeclare(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()), Times.Once);
            channel.Verify(c => c.QueueDeclare(It.IsAny<String>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()), Times.Once);
            channel.Verify(c => c.BasicPublish(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<IBasicProperties>(), It.IsAny<byte[]>()), Times.Once);
            Assert.AreEqual(result[0], "Success");
            Assert.IsTrue(mockBasicProperties.Object.Persistent);
            Assert.AreEqual("Prop2", mockBasicProperties.Object.CorrelationId);
        }
        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(PublishRabbitMQActivity))]
        public void PublishRabbitMQActivity_Execute_Failure_NullSource()
        {
            //------------Setup for test--------------------------
            var publishRabbitMQActivity = new PublishRabbitMQActivity();

            var resourceCatalog = new Mock<IResourceCatalog>();
            resourceCatalog.Setup(r => r.GetResource<RabbitMQSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns<RabbitMQSource>(null);

            var p = new PrivateObject(publishRabbitMQActivity);
            p.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            if (p.Invoke("PerformExecution", new Dictionary<string, string>()) is List<string> result)
            {
                Assert.AreEqual(result[0], "Failure: Source has been deleted.");
            }
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(PublishRabbitMQActivity))]
        public void PublishRabbitMQActivity_Execute_Failure_NoParams()
        {
            //------------Setup for test--------------------------
            var publishRabbitMQActivity = new PublishRabbitMQActivity();

            var resourceCatalog = new Mock<IResourceCatalog>();
            var rabbitMQSource = new Mock<RabbitMQSource>();

            resourceCatalog.Setup(r => r.GetResource<RabbitMQSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(rabbitMQSource.Object);

            var p = new PrivateObject(publishRabbitMQActivity);
            p.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            if (p.Invoke("PerformExecution", new Dictionary<string, string>()) is List<string> result)
            {
                Assert.AreEqual(result[0], "Failure: Queue Name and Message are required.");
            }
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(PublishRabbitMQActivity))]
        public void PublishRabbitMQActivity_Execute_Failure_InvalidParams()
        {
            //------------Setup for test--------------------------
            var publishRabbitMQActivity = new PublishRabbitMQActivity();

            var resourceCatalog = new Mock<IResourceCatalog>();
            var rabbitMQSource = new Mock<RabbitMQSource>();

            resourceCatalog.Setup(r => r.GetResource<RabbitMQSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(rabbitMQSource.Object);

            var p = new PrivateObject(publishRabbitMQActivity);
            p.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            if (p.Invoke("PerformExecution", new Dictionary<string, string> { { "Param1", "Blah1" }, { "Param2", "Blah2" } }) is List<string> result)
            {
                Assert.AreEqual(result[0], "Failure: Queue Name and Message are required.");
            }
        }

        [TestMethod]
        [Owner("Candice Daniel")]
        [TestCategory(nameof(PublishRabbitMQActivity))]
        [ExpectedException(typeof(Exception))]
        public void PublishRabbitMQActivity_Execute_Failure_NullException()
        {
            //------------Setup for test--------------------------
            var publishRabbitMQActivity = new PublishRabbitMQActivity();

            var resourceCatalog = new Mock<IResourceCatalog>();
            var rabbitMQSource = new Mock<RabbitMQSource>();
            var connectionFactory = new Mock<ConnectionFactory>();

            resourceCatalog.Setup(r => r.GetResource<RabbitMQSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(rabbitMQSource.Object);
            connectionFactory.Setup(c => c.CreateConnection()).Returns<IConnection>(null);

            var p = new PrivateObject(publishRabbitMQActivity);
            p.SetProperty("ConnectionFactory", connectionFactory.Object);
            p.SetProperty("ResourceCatalog", resourceCatalog.Object);

            //------------Execute Test---------------------------
            var result = p.Invoke("PerformExecution", new Dictionary<string, string> { { "QueueName", "Q1" }, { "Message", "Test message" } });

            //------------Assert Results-------------------------
            Assert.Fail("Exception not thrown");
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory(nameof(PublishRabbitMQActivity))]
        public void PublishRabbitMQActivity_GetState_ReturnsStateVariable()
        {
            //---------------Set up test pack-------------------
            var sourceId = Guid.NewGuid();
            //------------Setup for test--------------------------
            var act = new PublishRabbitMQActivity
            {
                QueueName = "bob",
                CorrelationID = "Correlation-id",
                IsDurable = true,
                IsExclusive = false,
                Message = "hello",
                RabbitMQSourceResourceId = sourceId,
                IsAutoDelete = false,
                Result = "[[res]]",
            };
            //------------Execute Test---------------------------
            var stateItems = act.GetState();
            Assert.AreEqual(8, stateItems.Count());

            var expectedResults = new[]
            {
                new StateVariable
                {
                    Name = "QueueName",
                    Type = StateVariable.StateType.Input,
                    Value = "bob"
                },
                 new StateVariable
                {
                    Name = "CorrelationID",
                    Type = StateVariable.StateType.Input,
                    Value = "Correlation-id"
                },
                new StateVariable
                {
                    Name = "IsDurable",
                    Type = StateVariable.StateType.Input,
                    Value = "True"
                },
                new StateVariable
                {
                    Name = "IsExclusive",
                    Type = StateVariable.StateType.Input,
                    Value = "False"
                },
                 new StateVariable
                {
                    Name = "Message",
                    Type = StateVariable.StateType.Input,
                    Value = "hello"
                },                 
                  new StateVariable
                {
                    Name = "RabbitMQSourceResourceId",
                    Type = StateVariable.StateType.Input,
                    Value = sourceId.ToString()
                },
                   new StateVariable
                {
                    Name = "IsAutoDelete",
                    Type = StateVariable.StateType.Input,
                    Value = "False"
                },              
                new StateVariable
                {
                    Name="Result",
                    Type = StateVariable.StateType.Output,
                    Value = "[[res]]"
                }
            };

            var iter = act.GetState().Select(
                (item, index) => new
                {
                    value = item,
                    expectValue = expectedResults[index]
                }
                );

            //------------Assert Results-------------------------
            foreach (var entry in iter)
            {
                Assert.AreEqual(entry.expectValue.Name, entry.value.Name);
                Assert.AreEqual(entry.expectValue.Type, entry.value.Type);
                Assert.AreEqual(entry.expectValue.Value, entry.value.Value);
            }
        }
    }
}