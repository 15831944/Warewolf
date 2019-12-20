/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Data.Interfaces;
using Dev2.Data.PathOperations.Extension;
using Dev2.PathOperations;
using NUnit.Framework;
using Moq;

namespace Dev2.Data.Tests.PathOperations.Extention
{
    [TestFixture]
    [SetUpFixture]
    public class PathExtensionsTests
    {
        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(PathExtensions))]
        public void PathExtensions_Combine_EndsWith_PathSeperator()
        {
            var mockDst = new Mock<IActivityIOOperationsEndPoint>();
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            mockActivityIOPath.Setup(activityIOPath => activityIOPath.Path).Returns("test\\");
            mockDst.Setup(o => o.IOPath).Returns(mockActivityIOPath.Object);

            var dstEndPoint = ActivityIOFactory.CreateOperationEndPointFromIOPath(mockActivityIOPath.Object);

            var endpoint = dstEndPoint.Combine("@");
            NUnit.Framework.Assert.AreEqual("test\\@", endpoint);
        }

        [Test]
        [Author("Pieter Terblanche")]
        [Category(nameof(PathExtensions))]
        public void PathExtensions_Combine_DoesNot_EndsWith_PathSeperator()
        {
            var mockDst = new Mock<IActivityIOOperationsEndPoint>();
            var mockActivityIOPath = new Mock<IActivityIOPath>();
            mockActivityIOPath.Setup(activityIOPath => activityIOPath.Path).Returns("test");
            mockDst.Setup(o => o.IOPath).Returns(mockActivityIOPath.Object);

            var dstEndPoint = ActivityIOFactory.CreateOperationEndPointFromIOPath(mockActivityIOPath.Object);

            var endpoint = dstEndPoint.Combine("@");
            NUnit.Framework.Assert.AreEqual("test\\@", endpoint);
        }
    }
}
