using Dev2.Common.Interfaces;
using Dev2.Studio.Interfaces;
using NUnit.Framework;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warewolf.Studio.ViewModels.Tests
{
    [TestFixture]
    public class ManageRabbitMqSourceModelTests
    {
        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("ManageRabbitMQSourceModel_Ctor")]
        public void ManageRabbitMQSourceModel_Ctor_Valid_ExpectPropertiesSet()
        {
            //------------Setup for test--------------------------
            var updateManager = new Mock<IStudioUpdateManager>();
            var queryManager = new Mock<IQueryManager>();
            var shellViewModel = new Mock<IShellViewModel>();

            
            
            //------------Execute Test---------------------------
            var manageRabbitMQSourceModel = new ManageRabbitMQSourceModel(updateManager.Object, queryManager.Object, shellViewModel.Object);
            //------------Assert Results-------------------------
            var p = new PrivateObject(manageRabbitMQSourceModel);

            NUnit.Framework.Assert.IsNotNull(p.GetField("_updateManager"));
            NUnit.Framework.Assert.IsNotNull(p.GetField("_queryManager"));
            NUnit.Framework.Assert.IsNotNull(p.GetField("_shellViewModel"));
        }

        


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("ManageRabbitMQSourceModel_Retrieve")]
        public void ManageRabbitMQSourceModel_Retrieve_ExpectPassThrough()
        {
            //------------Setup for test--------------------------
            var updateManager = new Mock<IStudioUpdateManager>();
            var queryManager = new Mock<IQueryManager>();
            var shellViewModel = new Mock<IShellViewModel>();
            var manageRabbitMQSourceModel = new ManageRabbitMQSourceModel(updateManager.Object, queryManager.Object, shellViewModel.Object);


            //------------Execute Test---------------------------
            manageRabbitMQSourceModel.RetrieveSources();
            //------------Assert Results-------------------------

            queryManager.Verify(a=>a.FetchRabbitMQServiceSources(),Times.Once);
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("ManageRabbitMQSourceModel_Edit")]
        public void ManageRabbitMQSourceModel_Edit_ExpectPassThrough()
        {
            //------------Setup for test--------------------------
            var updateManager = new Mock<IStudioUpdateManager>();
            var queryManager = new Mock<IQueryManager>();
            var shellViewModel = new Mock<IShellViewModel>();
            var manageRabbitMQSourceModel = new ManageRabbitMQSourceModel(updateManager.Object, queryManager.Object, shellViewModel.Object);
            var src = new Mock<IRabbitMQServiceSourceDefinition>();

            //------------Execute Test---------------------------
            manageRabbitMQSourceModel.EditSource(src.Object);
            //------------Assert Results-------------------------

            shellViewModel.Verify(a => a.EditResource(src.Object), Times.Once);
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("ManageRabbitMQSourceModel_New")]
        public void ManageRabbitMQSourceModel_New_ExpectPassThrough()
        {
            //------------Setup for test--------------------------
            var updateManager = new Mock<IStudioUpdateManager>();
            var queryManager = new Mock<IQueryManager>();
            var shellViewModel = new Mock<IShellViewModel>();
            var manageRabbitMQSourceModel = new ManageRabbitMQSourceModel(updateManager.Object, queryManager.Object, shellViewModel.Object);
            var src = new Mock<IRabbitMQServiceSourceDefinition>();

            //------------Execute Test---------------------------
            manageRabbitMQSourceModel.CreateNewSource();
            //------------Assert Results-------------------------

            shellViewModel.Verify(a => a.NewRabbitMQSource(""), Times.Once);
        }


        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("ManageRabbitMQSourceModel_Test")]
        public void ManageRabbitMQSourceModel_Test_ExpectPassThrough()
        {
            //------------Setup for test--------------------------
            var updateManager = new Mock<IStudioUpdateManager>();
            var queryManager = new Mock<IQueryManager>();
            var shellViewModel = new Mock<IShellViewModel>();
            var manageRabbitMQSourceModel = new ManageRabbitMQSourceModel(updateManager.Object, queryManager.Object, shellViewModel.Object);
            var src = new Mock<IRabbitMQServiceSourceDefinition>();
            updateManager.Setup(a => a.TestConnection(src.Object)).Returns("bob");
            //------------Execute Test---------------------------
            var res = manageRabbitMQSourceModel.TestSource(src.Object);
            //------------Assert Results-------------------------

            NUnit.Framework.Assert.AreEqual("bob",res);
           
        }

        [Test]
        [NUnit.Framework.Timeout(60000)]
        [Author("Leon Rajindrapersadh")]
        [Category("ManageRabbitMQSourceModel_Save")]
        public void ManageRabbitMQSourceModel_Save_ExpectPassThrough()
        {
            //------------Setup for test--------------------------
            var updateManager = new Mock<IStudioUpdateManager>();
            var queryManager = new Mock<IQueryManager>();
            var shellViewModel = new Mock<IShellViewModel>();
            var manageRabbitMQSourceModel = new ManageRabbitMQSourceModel(updateManager.Object, queryManager.Object, shellViewModel.Object);
            var src = new Mock<IRabbitMQServiceSourceDefinition>();
           
            //------------Execute Test---------------------------
            manageRabbitMQSourceModel.SaveSource(src.Object);
            //------------Assert Results-------------------------

            updateManager.Verify(a => a.Save(src.Object),Times.Once);

        }
    }
}
