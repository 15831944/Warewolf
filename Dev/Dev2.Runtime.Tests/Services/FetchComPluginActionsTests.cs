using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dev2.Common.Interfaces.Enums;
using Dev2.Runtime.ESB.Management.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Dev2.Tests.Runtime.Services
{
    [TestFixture]
    [SetUpFixture]
    public class FetchComPluginActionsTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [NUnit.Framework.Category("GetResourceID")]
        public void GetResourceID_ShouldReturnEmptyGuid()
        {
            //------------Setup for test--------------------------
            var comPluginActions = new FetchComPluginActions();

            //------------Execute Test---------------------------
            var resId = comPluginActions.GetResourceID(new Dictionary<string, StringBuilder>());
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(Guid.Empty, resId);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [NUnit.Framework.Category("GetResourceID")]
        public void GetAuthorizationContextForService_ShouldReturnContext()
        {
            //------------Setup for test--------------------------
            var fetchComPluginActions = new FetchComPluginActions();

            //------------Execute Test---------------------------
            var resId = fetchComPluginActions.GetAuthorizationContextForService();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(AuthorizationContext.Any, resId);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void BuildServiceInputName_GivenTypeNames_ShouldConcatinateTypeWithName()
        {
            //---------------Set up test pack-------------------
            var comPluginActions = new FetchComPluginActions();
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsNotNull(comPluginActions);
            //---------------Execute Test ----------------------
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(comPluginActions);
            var invoke = privateObject.Invoke("BuildServiceInputName", "Class2", "Project1.Class2&, Project1, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null");
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("Class2 (Project1.Class2)", invoke.ToString());
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void BuildServiceInputName_GivenCursorLocationEnumGetCorrectEnumName()
        {
            //---------------Set up test pack-------------------
            var comPluginActions = new FetchComPluginActions();
            //---------------Assert Precondition----------------
            NUnit.Framework.Assert.IsNotNull(comPluginActions);
            //---------------Execute Test ----------------------
            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(comPluginActions);

            var typeConverter = TypeDescriptor.GetConverter("ADODB.CursorLocationEnum, ADODB, Version=6.1.0.0, Culture=neutral, PublicKeyToken=null");

            var invoke = privateObject.Invoke("BuildServiceInputName", "Class2", "ADODB.CursorLocationEnum, ADODB, Version=6.1.0.0, Culture=neutral, PublicKeyToken=null");
            //---------------Test Result -----------------------
            NUnit.Framework.Assert.AreEqual("Class2 (ADODB.CursorLocationEnum)", invoke.ToString());
        }
    }
}
