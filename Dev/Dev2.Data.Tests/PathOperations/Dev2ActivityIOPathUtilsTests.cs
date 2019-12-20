using System;
using Dev2.Data.Decision;
using Dev2.PathOperations;
using NUnit.Framework;
using Warewolf.Storage;

namespace Dev2.Data.Tests.PathOperations
{
    [TestFixture]
    [SetUpFixture]
    public class Dev2ActivityIOPathUtilsTests
    {
        [Test]
        public void ExtractFullDirectoryPath_Given_Directory()
        {
            const string resourcesPath = @"C:\ProgramData\Warewolf\Resources";
            var fullDir = Dev2ActivityIOPathUtils.ExtractFullDirectoryPath(resourcesPath);
            NUnit.Framework.Assert.AreEqual(resourcesPath, fullDir);
        }

        [Test]
        public void ExtractFullDirectoryPath_Given_FilePath()
        {
            const string serverLogFile = @"C:\ProgramData\Warewolf\Server Log\wareWolf-Server.log";
            const string containingFolder = @"C:\ProgramData\Warewolf\Server Log\";
            var results = Dev2ActivityIOPathUtils.ExtractFullDirectoryPath(serverLogFile);
            NUnit.Framework.Assert.AreEqual(containingFolder, results);
        }
        [Test]
        public void IsStarWildCard_Given_Star_In_Path_Returns_True()
        {
            const string resourcesPath = @"C:\ProgramData\Warewolf\*.*";
            var results = Dev2ActivityIOPathUtils.IsStarWildCard(resourcesPath);
            NUnit.Framework.Assert.IsTrue(results);
        }
        [Test]
        public void IsDirectory_Given_Drive_Returns_True()
        {
            const string resourcesPath = @"C:\\";
            var results = Dev2ActivityIOPathUtils.IsDirectory(resourcesPath);
            NUnit.Framework.Assert.IsTrue(results);
        }
    }
}
