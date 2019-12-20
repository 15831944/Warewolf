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
using Dev2.Common.Interfaces.Wrappers;
using Dev2.Common.Wrappers;
using NUnit.Framework;

namespace Dev2.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class DirectoryWrapperTests
    {
        private static IDirectory NewIDirectoryInstance()
        {
            return new DirectoryWrapper();
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenCFolder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(@"C:\");
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenWindowsFolder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(@"C:\Windows");
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenSystem32Folder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(@"C:\Windows\System32");
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenDesktopFolder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenDesktopDirectoryFolder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenSystem32_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenSystemFolder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(Environment.GetFolderPath(Environment.SpecialFolder.System));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenUserProfileFolder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenProgramFilesFolder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenProgramFilesX86Folder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void DirectoryWrapper_CleanUp_GivenProgramsFolder_ExpectException()
        {
            NewIDirectoryInstance().CleanUp(Environment.GetFolderPath(Environment.SpecialFolder.Programs));
        }
    }
}
