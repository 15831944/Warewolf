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
using System.IO;
using Dev2.Data.Interfaces;
using Dev2.Data.Interfaces.Enums;
using Dev2.Data.PathOperations;
using Dev2.PathOperations;
using Dev2.Tests;
using NUnit.Framework;


namespace Unlimited.UnitTest.Framework.PathOperationTests
{
    [TestFixture]
    [SetUpFixture]
    public class ActivityIOFactoryTests
    {
        #region Create Path From String Tests

        [Test]
        public void CreatePathFromString_Expected_IActivityIOPath_FileSystem_Type()
        {
            var result = ActivityIOFactory.CreatePathFromString(ParserStrings.PathOperations_FileSystem_Path, "", "","");

            Assert.AreEqual(enActivityIOPathType.FileSystem, result.PathType);
        }

        [Test]
        public void CreatePathFromString_Expected_IActivityIOPath_FTP_Type()
        {
            var result = ActivityIOFactory.CreatePathFromString(ParserStrings.PathOperations_FTP_Path, "", "");

            Assert.AreEqual(enActivityIOPathType.FTP, result.PathType);
        }

        [Test]
        [ExpectedException(typeof(IOException))]
        [Author("Leon Rajindrapersadh")]
        [Category("ActivityIOFactory_CreateOperationEndPointFromIOPath")]
        public void CreatePathFromString_NonRootedPath_ExpectException() => ActivityIOFactory.CreatePathFromString("", "", "");

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Author("Leon Rajindrapersadh")]
        [Category("ActivityIOFactory_CreateOperationEndPointFromIOPath")]
        public void CreatePathFromString_NonRootedPath_ExpectArgumentNullException() => ActivityIOFactory.CreatePathFromString(null, "", "");

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ActivityIOFactory_CreateOperationEndPointFromIOPath")]
        public void CreatePathFromString_AssertAllPropertiesAreSet_ExpectCorrectValues()
        {
            var path = ActivityIOFactory.CreatePathFromString(@"c:\bob","dave","monkey", false,"");
            Assert.AreEqual(@"c:\bob",path.Path);
            Assert.AreEqual(@"dave", path.Username);
            Assert.AreEqual(@"monkey", path.Password);
            Assert.IsFalse( path.IsNotCertVerifiable);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ActivityIOFactory_CreateUnzipTO")]
        public void CreatePathFromString_CreateUnzipTO_AssertCorrectType()
        {
            var operationTO = ActivityIOFactory.CreateZipTO("20", "ss", "name", true);
            Assert.AreEqual("20", operationTO.CompressionRatio);
            Assert.AreEqual("ss", operationTO.ArchivePassword);
            Assert.AreEqual("name", operationTO.ArchiveName);
            Assert.IsTrue(operationTO.Overwrite);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ActivityIOFactory_CreateUnzipTO")]
        public void CreatePathFromString_CreateUnzipTO_PWD_OverWrite_AssertCorrectType()
        {
            var operationTO = ActivityIOFactory.CreateUnzipTO("20", true);
            Assert.AreEqual("20", operationTO.ArchivePassword);
            Assert.IsTrue(operationTO.Overwrite);
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ActivityIOFactory_Dev2PutRawOperationTO")]
        public void CreatePathFromString_CreatePathFromString_AssertCorrectType()
        {
            var operationTO = ActivityIOFactory.CreatePathFromString(@"c:\moon",true);
            Assert.AreEqual(@"c:\moon", operationTO.Path);
        }

        [Test]
        public void CreatePathFromString_Expected_IActivityIOPath_FTPS_Type()
        {
            var result = ActivityIOFactory.CreatePathFromString(ParserStrings.PathOperations_FTPS_AuthPath, "", "");

            Assert.AreEqual(enActivityIOPathType.FTPS, result.PathType);
        }

        #endregion Create Path From String Tests

        #region Create Operation EndPoint From IO Path Tests

        /// <summary>
        /// Create type of the operation end point from IO path expected activity IO operations end point file sysytem 
        /// provider.
        /// </summary>
        [Test]

        public void CreateOperationEndPointFromIOPath_Expected_IActivityIOOperationsEndPoint_FileSysytemProvider_Type()

        {

            var path = ActivityIOFactory.CreatePathFromString(ParserStrings.PathOperations_FileSystem_Path, "", "");
            var result = ActivityIOFactory.CreateOperationEndPointFromIOPath(path);

            Assert.IsTrue(result.GetType() == typeof(Dev2FileSystemProvider));
        }

        /// <summary>
        /// Create type of the operation end point from IO path expected activity IO operations 
        /// end point FTP provider.
        /// </summary>
        [Test]

        public void CreateOperationEndPointFromIOPath_Expected_IActivityIOOperationsEndPoint_FTPProvider_Type()

        {

            var path = ActivityIOFactory.CreatePathFromString(ParserStrings.PathOperations_FTP_Path, "", "");
            var result = ActivityIOFactory.CreateOperationEndPointFromIOPath(path);

            Assert.IsTrue(result.GetType() == typeof(Dev2FTPProvider));
        }

        #endregion Create Operation EndPoint From IO Path Tests

        #region Create Operation Broker Tests

        /// <summary>
        /// Create type of the operations broker expected activity operations broker file sysytem provider.
        /// </summary>
        [Test]

        public void CreateOperationsBroker_Expected_IActivityOperationsBroker_FileSysytemProvider_Type()

        {

            var result = ActivityIOFactory.CreateOperationsBroker();

            Assert.IsTrue(result != null);
        }

        #endregion Create Operation Broker Tests


    }
}
