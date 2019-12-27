using System;
using System.Collections.Concurrent;
using Dev2.Studio.Core;
using Microsoft.Practices.Prism.Mvvm;
using NUnit.Framework;

namespace Dev2.Core.Tests
{
    [TestFixture]
    public class ViewFactoryTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_HasCorrectType()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Test Result -----------------------
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenBadKey_returnsNull()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var view = viewFactory.GetViewGivenServerResourceType("crap");
            //---------------Test Result -----------------------
            Assert.IsNull(view);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenServer_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("Server"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenDev2Server_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("Dev2Server"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenServerSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("ServerSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenRabbitMQSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("RabbitMQSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenOauthSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("OauthSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenSharepointServerSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("SharepointServerSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenDropBoxSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("DropBoxSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenExchangeSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("ExchangeSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenEmailSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("EmailSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenWcfSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("WcfSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenComPluginSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("ComPluginSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenPluginSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("PluginSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenWebSource_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("WebSource"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenMySqlDatabase_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("MySqlDatabase"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenPostgreSQL_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("PostgreSQL"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenOracle_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("Oracle"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenODBC_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("ODBC"));
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void ViewFactory_GivenSqlDatabase_ShouldHaveKey()
        {
            //---------------Set up test pack-------------------
            var viewFactory = new ViewFactory();
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(viewFactory);
            //---------------Assert Precondition----------------
            Assert.IsInstanceOf(typeof(IViewFactory), viewFactory);
            //---------------Execute Test ----------------------
            var concurrentDictionary = privateObject.GetField("_viewMap") as ConcurrentDictionary<string, Func<IView>>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(concurrentDictionary);
            Assert.IsTrue(concurrentDictionary.ContainsKey("SqlDatabase"));
        }

    }
}
