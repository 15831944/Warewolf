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
using System.Activities.Expressions;
using System.Linq.Expressions;
using System.Text;
using Caliburn.Micro;
using Dev2.Studio.Core.Activities.Interegators;
using Dev2.Studio.Core.Models;
using Dev2.Studio.Interfaces;
using NUnit.Framework;
using Moq;
using Unlimited.Applications.BusinessDesignStudio.Activities;


namespace Dev2.Core.Tests.Activities
{
    [TestFixture]
    [SetUpFixture]
    public class WorkerServicePropertyInterigatorTest
    {

        [Test]
        [Author("Travis Frisinger")]
        [Category("WorkerServicePropertyInterigator_SetActivityProperties")]
        public void WorkerServicePropertyInterigator_SetActivityProperties_WhenNullXML_ExpectSomePropertiesSet()
        {
            //------------Setup for test--------------------------
            IEventAggregator evtAg = new EventAggregator();
            var env = new Mock<IServer>();
            env.Setup(e => e.Name).Returns("My Env");
            var resource = new ResourceModel(env.Object, evtAg);

            var activity = new DsfActivity("FriendlyName", String.Empty, "ServiceName", string.Empty, string.Empty, string.Empty);

            //------------Execute Test---------------------------
            WorkerServicePropertyInterigator.SetActivityProperties(resource, ref activity, null);

            //------------Assert Results-------------------------
            Assert.IsFalse(activity.IsWorkflow);
            Assert.IsNull(((Literal<string>)activity.Type.Expression).Value);
            Assert.IsNull(activity.FriendlySourceName);
            Assert.IsNull(activity.ActionName);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WorkerServicePropertyInterigator_SetActivityProperties")]
        public void WorkerServicePropertyInterigator_SetActivityProperties_WhenNotNullXML_ExpectPropertiesSet()
        {
            //------------Setup for test--------------------------
            IEventAggregator evtAg = new EventAggregator();
            var env = new Mock<IServer>();
            env.Setup(e => e.Name).Returns("My Env");
            var resource = new ResourceModel(env.Object, evtAg)
            {
                WorkflowXaml =
                    new StringBuilder(
                        "<Action SourceName=\"TheSource\" Type=\"TheType\" SourceMethod=\"SourceMethod\"></Action>"),
                ServerResourceType = "TheType"
            };
            var activity = new DsfActivity("FriendlyName", string.Empty, "ServiceName", string.Empty, string.Empty, string.Empty);

            //------------Execute Test---------------------------
            WorkerServicePropertyInterigator.SetActivityProperties(resource, ref activity, null);

            //------------Assert Results-------------------------
            Assert.IsFalse(activity.IsWorkflow);
            Assert.AreEqual("TheType", ((Literal<string>)activity.Type.Expression).Value);
            Assert.AreEqual("TheSource", activity.FriendlySourceName.Expression.ToString());
            Assert.AreEqual("SourceMethod", activity.ActionName.Expression.ToString());
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WorkerServicePropertyInterigator_SetActivityProperties")]
        public void WorkerServicePropertyInterigator_SetActivityProperties_GetSourceNameFromResourceRepo()
        {
            //------------Setup for test--------------------------
            IEventAggregator evtAg = new EventAggregator();
            var env = new Mock<IServer>();
            var resRepo = new Mock<IResourceRepository>();
            var srcRes = new Mock<IContextualResourceModel>();
            srcRes.Setup(a => a.DisplayName).Returns("bob");
            resRepo.Setup(a => a.LoadContextualResourceModel(It.IsAny<Guid>())).Returns(srcRes.Object);

            env.Setup(e => e.Name).Returns("My Env");
            var resource = new ResourceModel(env.Object, evtAg)
            {
                WorkflowXaml =
                    new StringBuilder(
                        "<Action SourceName=\"TheSource\" Type=\"TheType\" SourceMethod=\"SourceMethod\" SourceID=\"123456\"></Action>"),
                ServerResourceType = "TheType"
            };
            var activity = new DsfActivity("FriendlyName", string.Empty, "ServiceName", string.Empty, string.Empty, string.Empty);

            //------------Execute Test---------------------------
            WorkerServicePropertyInterigator.SetActivityProperties(resource, ref activity, resRepo.Object);

            //------------Assert Results-------------------------
            Assert.IsFalse(activity.IsWorkflow);
            Assert.AreEqual("TheType", ((Literal<string>)activity.Type.Expression).Value);
            Assert.AreEqual("bob", activity.FriendlySourceName.Expression.ToString());
            Assert.AreEqual("SourceMethod", activity.ActionName.Expression.ToString());
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WorkerServicePropertyInterigator_SetActivityProperties")]
        public void WorkerServicePropertyInterigator_SetActivityProperties_NoSourceNameFromResourceRepo_NoSourceIdOnXML()
        {
            //------------Setup for test--------------------------
            IEventAggregator evtAg = new EventAggregator();
            var env = new Mock<IServer>();
            var resRepo = new Mock<IResourceRepository>();
            var srcRes = new Mock<IResourceModel>();
            srcRes.Setup(a => a.DisplayName).Returns("bob");
            resRepo.Setup(a => a.FindSingle(It.IsAny<Expression<Func<IResourceModel, bool>>>(), false, false)).Returns(srcRes.Object);

            env.Setup(e => e.Name).Returns("My Env");
            var resource = new ResourceModel(env.Object, evtAg)
            {
                WorkflowXaml =
                    new StringBuilder(
                        "<Action SourceName=\"TheSource\" Type=\"TheType\" SourceMethod=\"SourceMethod\"></Action>"),
                ServerResourceType = "TheType"
            };
            var activity = new DsfActivity("FriendlyName", string.Empty, "ServiceName", string.Empty, string.Empty, string.Empty);

            //------------Execute Test---------------------------
            WorkerServicePropertyInterigator.SetActivityProperties(resource, ref activity, resRepo.Object);

            //------------Assert Results-------------------------
            Assert.IsFalse(activity.IsWorkflow);
            Assert.AreEqual("TheType", ((Literal<string>)activity.Type.Expression).Value);
            Assert.AreEqual("TheSource", activity.FriendlySourceName.Expression.ToString());
            Assert.AreEqual("SourceMethod", activity.ActionName.Expression.ToString());
        }
        [Test]
        [Author("Travis Frisinger")]
        [Category("WorkerServicePropertyInterigator_SetActivityProperties")]
        public void WorkerServicePropertyInterigator_SetActivityProperties_WhenXMLWithOutAttributes_ExpectSomePropertiesSet()
        {
            //------------Setup for test--------------------------
            IEventAggregator evtAg = new EventAggregator();
            var env = new Mock<IServer>();
            env.Setup(e => e.Name).Returns("My Env");
            var resource = new ResourceModel(env.Object, evtAg) { WorkflowXaml = new StringBuilder("<Action></Action>") };

            var activity = new DsfActivity("FriendlyName", String.Empty, "ServiceName", string.Empty, string.Empty, string.Empty);

            //------------Execute Test---------------------------
            WorkerServicePropertyInterigator.SetActivityProperties(resource, ref activity, null);

            //------------Assert Results-------------------------
            Assert.IsFalse(activity.IsWorkflow);
            Assert.IsNull(((Literal<string>)activity.Type.Expression).Value);
            Assert.IsNull(activity.FriendlySourceName);
            Assert.IsNull(activity.ActionName);
        }
    }
}
