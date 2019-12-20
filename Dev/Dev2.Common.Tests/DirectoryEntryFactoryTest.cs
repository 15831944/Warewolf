﻿/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using NUnit.Framework;
using Dev2.Common.Interfaces.Wrappers;
using Dev2.Common.Wrappers;

namespace Dev2.Common.Tests
{
    [TestFixture]
    public class DirectoryEntryFactoryTest
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DirectoryEntryFactory))]
        public void DirectoryEntryFactory_EntryNameAndMachineName_AreEqual()
        {
            //-----------------Arrage------------------
            var path = "WinNT://" + Environment.MachineName + ",computer";
            IDirectoryEntryFactory _directoryEntryFactory = new DirectoryEntryFactory();
            //-----------------Act------------------
            var entry = _directoryEntryFactory.Create(path);
            //-----------------Assert------------------
            Assert.IsNotNull(entry);
            Assert.AreEqual(Environment.MachineName, entry.Name);

            var count = 0;
            foreach (var child in entry.Children)
            {
                count++;
            }
            Assert.IsTrue(count > 0);
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DirectoryEntryFactory))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void DirectoryEntryFactory_DirectoryEntry_IsDisposed()
        {
            //-----------------Arrage------------------
            var path = "WinNT://" + Environment.MachineName + ",computer";
            IDirectoryEntryFactory _directoryEntryFactory = new DirectoryEntryFactory();
            //-----------------Act------------------
            var entry = _directoryEntryFactory.Create(path);
            //-----------------Assert------------------
            Assert.IsNotNull(entry);
            Assert.AreEqual(Environment.MachineName, entry.Name);
            //----------------Test if it does dispose-----------
            //----------------Act-------------------------------
            entry.Dispose();
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(DirectoryEntryFactory))]
        public void DirectoryEntryFactory_DirectoryEntryPath_IsTrue()
        {
            //-----------------Arrage------------------
            IDirectoryEntryFactory _directoryEntryFactory = new DirectoryEntryFactory();
            //-----------------Act------------------
            var entry = _directoryEntryFactory.Create("Administrator");
            //-----------------Assert------------------
            Assert.IsNotNull(entry);
            Assert.IsTrue(entry.Instance.Path == "Administrator");
        }
    }
}
