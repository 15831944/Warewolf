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
using Dev2.Communication;
using Dev2.Providers.Events;
using NUnit.Framework;

namespace Dev2.Infrastructure.Tests.Providers.Events
{
    [TestFixture]
    [SetUpFixture]
    public class EventPublisherTests
    {
        [Test]
        [Description("GetEvent must add a new subject when invoked for the first time for the type.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]
        
        public void EventPublisherGetEvent_UnitTest_FirstTimeForType_New()

        {
            var publisher = new EventPublisher();
            Assert.AreEqual(0, publisher.Count);

            var actual = publisher.GetEvent<object>();
            Assert.AreEqual(1, publisher.Count);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf(actual.GetType(), typeof(IObservable<object>));
        }

        [Test]
        [Description("GetEvent must return an existing subject when invoked for the second time for the type.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]
        
        public void EventPublisherGetEvent_UnitTest_SecondTimeForType_Existing()

        {
            var publisher = new EventPublisher();
            Assert.AreEqual(0, publisher.Count);

            var first = publisher.GetEvent<object>();
            Assert.AreEqual(1, publisher.Count);
            Assert.IsNotNull(first);
            Assert.IsInstanceOf(first.GetType(), typeof(IObservable<object>));

            var second = publisher.GetEvent<object>();
            Assert.AreEqual(1, publisher.Count);
            Assert.IsNotNull(second);
            Assert.IsInstanceOf(second.GetType(), typeof(IObservable<object>));
        }

        [Test]
        [Description("Publish must find the subject and invoke OnNext on it for a type that has been previously requested by GetEvent.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]
        
        public void EventPublisherPublish_UnitTest_RegisteredType_FindsSubjectAndInvokesOnNext()

        {
            var memo = new DesignValidationMemo();

            var publisher = new EventPublisher();
            var subscription = publisher.GetEvent<DesignValidationMemo>().Subscribe(m => Assert.AreSame(memo, m));

            publisher.Publish(memo);
            subscription.Dispose();
        }

        [Test]
        [Description("Publish must find the subject and invoke OnNext on it for an object whose type has been previously requested by GetEvent.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]
        
        public void EventPublisherPublish_UnitTest_RegisteredObjectType_FindsSubjectAndInvokesOnNext()

        {
            var memo = new DesignValidationMemo() as object;

            var publisher = new EventPublisher();
            var subscription = publisher.GetEvent<DesignValidationMemo>().Subscribe(m => Assert.AreSame(memo, m));

            publisher.PublishObject(memo);
            subscription.Dispose();
        }


        [Test]
        [Description("Publish must not find the subject and not invoke OnNext for a type that has not been previously requested by GetEvent.")]
        [Category("UnitTest")]
        [Author("Trevor Williams-Ros")]
        
        public void EventPublisherPublish_UnitTest_UnregisteredType_DoesNotFindSubject()

        {
            var memo = new Memo();

            var publisher = new EventPublisher();

            publisher.Publish(memo);
            Assert.IsTrue(true);
        }


        [Test]
        [Category("EventPublisherPublish_RemoveEvent")]
        [Author("Trevor Williams-Ros")]
        
        public void EventPublisherPublish_RemoveEvent_RegisteredObjectType_Removed()

        {
            var publisher = new EventPublisher();
            var subscription = publisher.GetEvent<DesignValidationMemo>();
            Assert.AreEqual(1, publisher.Count);

            var result = publisher.RemoveEvent<DesignValidationMemo>();
            Assert.AreEqual(0, publisher.Count);
            Assert.IsTrue(result);
        }

        [Test]
        [Category("EventPublisherPublish_RemoveEvent")]
        [Author("Trevor Williams-Ros")]
        
        public void EventPublisherPublish_RemoveEvent_UnregisteredObjectType_NotRemoved()

        {
            var publisher = new EventPublisher();
            var subscription = publisher.GetEvent<DesignValidationMemo>();
            Assert.AreEqual(1, publisher.Count);

            var result = publisher.RemoveEvent<Memo>();
            Assert.AreEqual(1, publisher.Count);
            Assert.IsFalse(result);
        }
    }
}
