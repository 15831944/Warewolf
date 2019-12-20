/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/
using Dev2.Common.SaveDialog;
using NUnit.Framework;

namespace Dev2.Common.Tests.SaveDialogue
{
    [TestFixture]
    [SetUpFixture]
    public class ResourceNameTests
    {

        [Test]
        [Author("Candice Daniel")]
        [Category(nameof(ResourceName))]
        public void ResourceName_Test()
        {
            const string path = "C:\\\\ProgramData\\Warewolf\\Server Log";
            const string name = "warewolf-Server.log";
            var ResourceName = new ResourceName(path, name);

            Assert.IsNotNull(ResourceName);
            Assert.AreEqual(path, ResourceName.Path);
            Assert.AreEqual(name, ResourceName.Name);
        }
    }
}
