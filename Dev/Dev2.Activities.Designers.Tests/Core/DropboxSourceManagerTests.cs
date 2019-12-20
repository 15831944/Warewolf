using System;
using Dev2.Activities.Designers.Tests.DropBox2016;
using Dev2.Activities.Designers2.Core;
using Dev2.Data.ServiceModel;
using Dev2.Util;
using NUnit.Framework;


namespace Dev2.Activities.Designers.Tests.Core
{
    [TestFixture]
    [SetUpFixture]
    public class DropboxSourceManagerTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void OnCreattion_GivenIsNew_ShouldShouldNotThrowException()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------
            AppUsageStats.LocalHost = "LocalHost";
            //---------------Execute Test ----------------------
            try
            {
                var dropboxSourceManager = new DropboxSourceManager(TestResourceCatalog.EnvLazy.Value);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            //---------------Test Result -----------------------
            
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void FetchSources_GivenEnvironment_ShouldReturnSources()
        {
            //---------------Set up test pack-------------------
            var dropboxSourceManager = new DropboxSourceManager(TestResourceCatalog.EnvLazy.Value);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(dropboxSourceManager);
            //---------------Execute Test ----------------------
            var dropBoxSources = dropboxSourceManager.FetchSources<DropBoxSource>();
            //---------------Test Result -----------------------
            Assert.IsNotNull(dropBoxSources);
        }
    }
}
