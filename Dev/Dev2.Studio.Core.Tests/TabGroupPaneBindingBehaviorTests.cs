using Dev2.Studio.AppResources.Behaviors;
using NUnit.Framework;
using Infragistics.Windows.DockManager;

namespace Dev2.Core.Tests
{
    [TestFixture]
    public class TabGroupPaneBindingBehaviorTests
    {
        [Test]
        public void TabGroupPaneBindingBehavior_SetDocumentHost_CanSetDocumentHost()
        {
            //------------Setup for test-------------------------
            var myTabGroupPaneBindingBehavior = new TabGroupPaneBindingBehavior
            {
                //------------Execute Test---------------------------
                DocumentHost = new DocumentContentHost()
            };
            //------------Assert Results-------------------------
            Assert.IsNotNull(myTabGroupPaneBindingBehavior.DocumentHost);
        }
    }
}
