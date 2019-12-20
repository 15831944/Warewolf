/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Warewolf.Pooling;

namespace Warewolf.Common.NetStandard20.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class ObjectPoolTests
    {
        [Test]
        [Category("ObjectPool")]
        [Author("Devaji Chotaliya")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ObjectPool_Constructor_NullFunction_ShouldThrowException()
        {
            //--------------Arrange------------------------------
            //--------------Act----------------------------------
            var result = new ObjectPoolFactory<TestClass>().New(null);
            //--------------Assert-------------------------------
        }

        [Test]
        [Category("ObjectPool")]
        [Author("Devaji Chotaliya")]
        public void ObjectPool_AcquireReleaseObject_CheckWithParallelObjectCreation_ShouldReturnObject()
        {
            //--------------Arrange------------------------------
            TestClass expectedResult = new TestClass();
            var objectPool = new ObjectPoolFactory<TestClass>().New(() => expectedResult);

            Parallel.For(0, 1000, (i, loopState) =>
            {
                //--------------Act----------------------------------
                TestClass actualResult = objectPool.AcquireObject();

                //--------------Assert-------------------------------
                Assert.AreEqual(expectedResult, actualResult);

                Assert.AreEqual(expectedResult.GetValue(i), actualResult.GetValue(i));

                objectPool.ReleaseObject(actualResult);
            });
        }

        [Test]
        [Category("ObjectPool")]
        [Author("Devaji Chotaliya")]
        public void ObjectPool_Dispose_ShouldClearObjectList()
        {
            //--------------Arrange------------------------------
            TestClass expectedResult = new TestClass();
            var objectPool = new ObjectPoolFactory<TestClass>().New(() => expectedResult);

            Parallel.For(0, 1000, (i, loopState) =>
            {
                TestClass actualResult = objectPool.AcquireObject();

                Assert.AreEqual(expectedResult, actualResult);

                Assert.AreEqual(expectedResult.GetValue(i), actualResult.GetValue(i));

                objectPool.ReleaseObject(actualResult);
            });

            //--------------Act----------------------------------
            objectPool.Dispose();

            //--------------Assert-------------------------------
        }
    }

    public class TestClass
    {
        public int[] Nums { get; set; }
        public double GetValue(long i)
        {
            return Math.Sqrt(Nums[i]);
        }
        public TestClass()
        {
            Nums = new int[1000000];
            Random rand = new Random();
            for (int i = 0; i < Nums.Length; i++)
                Nums[i] = rand.Next();
        }

        public TestClass(int[] Nums)
        {
            this.Nums = Nums;
            Random rand = new Random();
            for (int i = 0; i < Nums.Length; i++)
                Nums[i] = rand.Next();
        }
    }
}
