using System;
using NUnit.Framework;
using Dev2.Tests.Runtime.Util;
using System.Diagnostics;
using System.Threading;
using Dev2.Core.Tests;
using Dev2.Studio.Core.Factories;
using System.Globalization;
using Dev2.Common;
using Dev2.Data;
using Dev2.Common.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Integration.Tests
{
    [TestFixture]
    public class Load_Tests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("Load Tests")]
        public void ExecutionManager_StartRefresh_WaitsForCurrentExecutions()
        {
            //------------Setup for test--------------------------
            var executionManager = ExecutionManagerTests.GetConstructedExecutionManager();
            executionManager.AddExecution();
            var stopwatch = new Stopwatch();
            var t = new Thread(() =>
            {
                stopwatch.Start();
                Thread.Sleep(2000);
                executionManager.CompleteExecution();
            });
            t.Start();
            //------------Execute Test---------------------------
            executionManager.StartRefresh();
            stopwatch.Stop();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(stopwatch.ElapsedMilliseconds > 2000, stopwatch.ElapsedMilliseconds.ToString());
        }

        [Test]
        [Category("Load Tests")]
        public void Single_Token_Perfomance_Op()
        {
            var dtb = new Dev2TokenizerBuilder { ToTokenize = Properties.TestStrings.tokenizerBase.ToStringBuilder() };


            dtb.AddTokenOp("-", false);

            var dt = dtb.Generate();

            var opCnt = 0;
            var sw = new Stopwatch();
            sw.Start();
            while (dt.HasMoreOps() && opCnt < 100000)
            {
                dt.NextToken();
                opCnt++;
            }
            sw.Stop();

            var exeTime = sw.ElapsedMilliseconds;

            Console.WriteLine(@"Total Time : " + exeTime);
            NUnit.Framework.Assert.IsTrue(opCnt == 100000 && exeTime < 1300, "Expecting it to take 1300 ms but it took " + exeTime + " ms.");
        }

        [Test]
        [Category("Load Tests")]
        public void Three_Token_Perfomance_Op()
        {
            var dtb = new Dev2TokenizerBuilder { ToTokenize = Properties.TestStrings.tokenizerBase.ToStringBuilder() };


            dtb.AddTokenOp("AB-", false);

            var dt = dtb.Generate();

            var opCnt = 0;
            var sw = new Stopwatch();
            sw.Start();
            while (dt.HasMoreOps() && opCnt < 35000)
            {
                dt.NextToken();
                opCnt++;
            }
            sw.Stop();

            var exeTime = sw.ElapsedMilliseconds;

            Console.WriteLine("Total Time : " + exeTime);
            NUnit.Framework.Assert.IsTrue(opCnt == 35000 && exeTime < 2500, "It took [ " + exeTime + " ]");
        }

        [Test]
        [Category("Load Tests")]
        public void PulseTracker_Should()
        {
            var elapsed = false;
            var pulseTracker = new PulseTracker(2000);

            NUnit.Framework.Assert.AreEqual(2000, pulseTracker.Interval);
            var pvt = new PrivateObject(pulseTracker);
            var timer = (System.Timers.Timer)pvt.GetField("_timer");
            timer.Elapsed += (sender, e) =>
            {
                elapsed = true;
            };
            NUnit.Framework.Assert.AreEqual(false, timer.Enabled);
            pulseTracker.Start();
            Thread.Sleep(6000);
            NUnit.Framework.Assert.IsTrue(elapsed);
        }

        [Test]
        [Category("Load Tests")]
        public void SortLargeListOfScalarsExpectedLessThan5500Milliseconds()
        {
            //Initialize
            var (_, _dataListViewModel) = DataListViewModelTests.Setup();
            for (var i = 2500; i > 0; i--)
            {
                _dataListViewModel.Add(DataListItemModelFactory.CreateScalarItemModel("testVar" + i.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0')));
            }
            var timeBefore = DateTime.Now;

            //Execute
            _dataListViewModel.SortCommand.Execute(null);

            var endTime = DateTime.Now.Subtract(timeBefore);
            //Assert
            NUnit.Framework.Assert.AreEqual("Country", _dataListViewModel.ScalarCollection[0].DisplayName, "Sort datalist with large list failed");
            NUnit.Framework.Assert.AreEqual("testVar1000", _dataListViewModel.ScalarCollection[1000].DisplayName, "Sort datalist with large list failed");
            NUnit.Framework.Assert.AreEqual("testVar1750", _dataListViewModel.ScalarCollection[1750].DisplayName, "Sort datalist with large list failed");
            NUnit.Framework.Assert.AreEqual("testVar2500", _dataListViewModel.ScalarCollection[2500].DisplayName, "Sort datalist with large list failed");
            NUnit.Framework.Assert.IsTrue(endTime < TimeSpan.FromMilliseconds(5500), $"Sort datalist took longer than 5500 milliseconds to sort 2500 variables. Took {endTime}");

            DataListViewModelTests.SortCleanup(_dataListViewModel);
        }
    }
}