using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dev2.Activities.Designers2.Core;
using Dev2.Activities.Designers2.Core.Source;
using Dev2.Common;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Core;
using Dev2.Common.Interfaces.ServerProxyLayer;
using Dev2.Common.Interfaces.ToolBase;
using Dev2.Common.Interfaces.WebService;
using Dev2.Studio.Core.Activities.Utils;
using NUnit.Framework;
using Moq;





namespace Dev2.Activities.Designers.Tests.Core
{
    [TestFixture]
    public class WebInputRegionTest
    {

        [Test]
        public void TestInputCtor()
        {
            var id = Guid.NewGuid();
            var act = new DsfWebGetActivity() { SourceId = id };
            var src = new Mock<IWebServiceSource>();

            var mod = new Mock<IWebServiceModel>();
            mod.Setup(a => a.RetrieveSources()).Returns(new List<IWebServiceSource>());
            var srcreg = new WebSourceRegion(mod.Object, ModelItemUtils.CreateModelItem(new DsfWebGetActivity()));
            var region = new WebGetInputRegion( ModelItemUtils.CreateModelItem(act),srcreg);
            Assert.AreEqual(region.IsEnabled, false);
            Assert.AreEqual(region.Errors.Count,0);
        }

        [Test]
        public void TestInputCtorEmpty()
        {
            var id = Guid.NewGuid();
            var act = new DsfWebGetActivity() { SourceId = id };
            var src = new Mock<IWebServiceSource>();

            var mod = new Mock<IWebServiceModel>();
            mod.Setup(a => a.RetrieveSources()).Returns(new List<IWebServiceSource>());
            var srcreg = new WebSourceRegion(mod.Object, ModelItemUtils.CreateModelItem(new DsfWebGetActivity()));
            var region = new WebGetInputRegion();
            Assert.AreEqual(region.IsEnabled, false);
        }


        [Test]
        public void TestClone()
        {
            var id = Guid.NewGuid();
            var act = new DsfWebGetActivity() { SourceId = id };
            var src = new Mock<IWebServiceSource>();

            var mod = new Mock<IWebServiceModel>();
            mod.Setup(a => a.RetrieveSources()).Returns(new List<IWebServiceSource>());
            var srcreg = new WebSourceRegion(mod.Object, ModelItemUtils.CreateModelItem(new DsfWebGetActivity()));
            var region = new WebGetInputRegion(ModelItemUtils.CreateModelItem(act), srcreg);
            Assert.AreEqual(region.IsEnabled, false);
            Assert.AreEqual(region.Errors.Count, 0);
            if (region.CloneRegion() is WebGetInputRegion clone)
            {
                Assert.AreEqual(clone.IsEnabled, false);
                Assert.AreEqual(clone.Errors.Count, 0);
            }
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WebInputRegion_RestoreFromPrevios")]
        public void WebGetInputRegion_RestoreFromPrevios_Restore_ExpectValuesChanged()
        {
            //------------Setup for test--------------------------
            var id = Guid.NewGuid();
            var act = new DsfWebGetActivity() { SourceId = id };
            var src = new Mock<IWebServiceSource>();

            var mod = new Mock<IWebServiceModel>();
            mod.Setup(a => a.RetrieveSources()).Returns(new List<IWebServiceSource>());
            var srcreg = new WebSourceRegion(mod.Object, ModelItemUtils.CreateModelItem(new DsfWebGetActivity()));
            var region = new WebGetInputRegion(ModelItemUtils.CreateModelItem(act), srcreg);
            var regionToRestore = new WebGetInputRegion(ModelItemUtils.CreateModelItem(act), srcreg);
            regionToRestore.IsEnabled = true;
            regionToRestore.QueryString = "blob";
            regionToRestore.Headers = new ObservableCollection<INameValue>{new NameValue("a","b")};
            //------------Execute Test---------------------------
            region.RestoreRegion(regionToRestore as IToolRegion);
            //------------Assert Results-------------------------

            Assert.AreEqual(region.QueryString, "blob");
            Assert.AreEqual(region.Headers.First().Name, "a");
            Assert.AreEqual(region.Headers.First().Value, "b");
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("WebInputRegion_RestoreFromPrevios")]
        public void WebInputRegion_SrcChanged_UpdateValues()
        {
            //------------Setup for test--------------------------
            var id = Guid.NewGuid();
            var act = new DsfWebGetActivity() { SourceId = id };
            var src = new Mock<IWebServiceSource>();

            var mod = new Mock<IWebServiceModel>();
            var  lst = new List<IWebServiceSource> { new WebServiceSourceDefinition(){HostName = "bob",DefaultQuery = "Dave"} , new WebServiceSourceDefinition(){HostName = "f",DefaultQuery = "g"} };
            mod.Setup(a => a.RetrieveSources()).Returns(lst);
            var srcreg = new WebSourceRegion(mod.Object, ModelItemUtils.CreateModelItem(new DsfWebGetActivity()));
            var region = new WebGetInputRegion(ModelItemUtils.CreateModelItem(act), srcreg);
            var regionToRestore = new WebGetInputRegion(ModelItemUtils.CreateModelItem(act), srcreg);

            srcreg.SelectedSource = lst[0];
            Assert.AreEqual(region.QueryString,"Dave");
            Assert.AreEqual(region.RequestUrl, "bob");

        }

    }
}