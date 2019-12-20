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
using System.Activities.Presentation.Model;
using System.IO;
using System.Reflection;
using System.Text;
using Dev2.Activities.Scripting;
using Dev2.Runtime.Configuration.ViewModels.Base;
using Dev2.Studio.Core.Activities.Utils;
using NUnit.Framework;



namespace Dev2.Activities.Designers.Tests.Script_Javascript
{
    [TestFixture]
    public class JavaScriptDesignerViewModelTests
    {

        static string GetJsTmpFile()
        {
            var codeBase = Assembly.GetExecutingAssembly().Location;
            var directoryName = Path.GetDirectoryName(codeBase);
            return directoryName + "\\jsFile.js ";
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        [OneTimeSetUp]
        public static void Init(TestContext context)
        {
            try
            {
                File.WriteAllBytes(GetJsTmpFile(), Encoding.ASCII.GetBytes(@"if (!String.prototype.endsWith) 
                        {
                            String.prototype.endsWith = function(searchString, position) 
                            {
                                var subjectString = this.toString();
                                if (typeof position !== 'number' || !isFinite(position) || Math.floor(position) !== position || position > subjectString.length)
                                {
                                    position = subjectString.length;
                                }
                                position -= searchString.length;
                                var lastIndex = subjectString.indexOf(searchString, position);
                                return lastIndex !== -1 && lastIndex === position;
                            };
                       }"));                
            }
            catch (Exception ex)
            {
                //supress exceptio
                Assert.Fail(ex.Message);
            }
        }
        [OneTimeTearDown]
        public static void Cleaner()
        {
            try
            {
                File.Delete(GetJsTmpFile());
            }
            catch (Exception)
            {
                //supress exceptio
            }
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("ScriptDesignerViewModel_Constructor")]
        public void ScriptDesignerViewModel_Constructor_PropertiesInitialized()
        {
            var modelItem = CreateModelItem();
            var viewModel = new TestJavaScriptDesignerViewModel(modelItem);
            Assert.IsTrue(viewModel.EscapeScript);
            Assert.IsTrue(viewModel.HasLargeView);
        }


        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("ScriptDesignerViewModel_SelectedScriptType")]
        public void ScriptDesignerViewModel_SelectedScriptType_JavaScript_PropertiesInitialized()
        {
            var modelItem = CreateModelItem();
            var viewModel = new TestJavaScriptDesignerViewModel(modelItem);
            Assert.IsTrue(viewModel.EscapeScript);
            Assert.IsTrue(string.IsNullOrEmpty(viewModel.IncludeFile));
            Assert.AreEqual("JavaScript Syntax", viewModel.ScriptTypeDefaultText);
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("ScriptDesignerViewModel_SelectedScriptType")]
        public void ScriptDesignerViewModel_ChooseDirectoryShould_ReturnFile()
        {
            var modelItem = CreateModelItem();
            var viewModel = new TestJavaScriptDesignerViewModel(modelItem);
            Assert.IsTrue(string.IsNullOrEmpty(viewModel.IncludeFile));
            viewModel.IncludeFile = GetJsTmpFile();
            viewModel.Validate();
            var command = new DelegateCommand(o => viewModel.ChooseScriptSources());
            Assert.IsTrue(viewModel.ChooseScriptSourceCommand.CanExecute(command));
            viewModel.ChooseScriptSourceCommand.Execute(command);
            Assert.IsFalse(string.IsNullOrEmpty(viewModel.IncludeFile));
        }

        static ModelItem CreateModelItem()
        {
            return ModelItemUtils.CreateModelItem(new DsfJavascriptActivity());
        }
    }
}
