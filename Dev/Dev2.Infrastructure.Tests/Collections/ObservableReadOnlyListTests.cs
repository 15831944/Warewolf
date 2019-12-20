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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Windows.Data;
using System.Windows.Threading;
using Dev2.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Dev2.Infrastructure.Tests.Collections
{
    [TestFixture]
    [SetUpFixture]
    public class ObservableReadOnlyListTests
    {
        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("ObservableReadOnlyList_IReadOnlyList")]
        public void ObservableReadOnlyList_IReadOnlyList_Implemented()
        {
            //------------Setup for test--------------------------
            var observableReadOnlyList = new ObservableReadOnlyList<string>();

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsInstanceOf(observableReadOnlyList.GetType(), typeof(IReadOnlyList<string>));

            observableReadOnlyList.Insert(0, "Item1");
            observableReadOnlyList.Insert(1, "Item3");
            observableReadOnlyList.Insert(1, "Item2");

            NUnit.Framework.Assert.AreEqual(3, observableReadOnlyList.Count);
            NUnit.Framework.Assert.AreEqual("Item1", observableReadOnlyList[0]);
            NUnit.Framework.Assert.AreEqual("Item2", observableReadOnlyList[1]);
            NUnit.Framework.Assert.AreEqual("Item3", observableReadOnlyList[2]);

            var idx = observableReadOnlyList.IndexOf("Item3");
            NUnit.Framework.Assert.AreEqual(2, idx);

            observableReadOnlyList.RemoveAt(1);
            NUnit.Framework.Assert.AreEqual(2, observableReadOnlyList.Count);
            NUnit.Framework.Assert.AreEqual("Item1", observableReadOnlyList[0]);
            NUnit.Framework.Assert.AreEqual("Item3", observableReadOnlyList[1]);

            observableReadOnlyList[1] = "Item5";
            NUnit.Framework.Assert.AreEqual("Item5", observableReadOnlyList[1]);
        }


        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("ObservableReadOnlyList_IReadOnlyCollection")]
        public void ObservableReadOnlyList_IReadOnlyCollection_Implemented()
        {
            //------------Setup for test--------------------------
            var observableReadOnlyList = new ObservableReadOnlyList<string>();

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsInstanceOf(observableReadOnlyList.GetType(), typeof(IReadOnlyCollection<string>));
            NUnit.Framework.Assert.IsTrue(observableReadOnlyList.IsReadOnly);

            observableReadOnlyList.Add("Item1");
            observableReadOnlyList.Add("Item2");
            observableReadOnlyList.Add("Item3");

            NUnit.Framework.Assert.AreEqual(3, observableReadOnlyList.Count);
            NUnit.Framework.Assert.AreEqual("Item1", observableReadOnlyList[0]);
            NUnit.Framework.Assert.AreEqual("Item2", observableReadOnlyList[1]);
            NUnit.Framework.Assert.AreEqual("Item3", observableReadOnlyList[2]);

            var contains = observableReadOnlyList.Contains("Item3");
            var notContains = observableReadOnlyList.Contains("Item4");
            NUnit.Framework.Assert.IsTrue(contains);
            NUnit.Framework.Assert.IsFalse(notContains);

            observableReadOnlyList.Remove("Item2");
            NUnit.Framework.Assert.AreEqual(2, observableReadOnlyList.Count);
            NUnit.Framework.Assert.AreEqual("Item1", observableReadOnlyList[0]);
            NUnit.Framework.Assert.AreEqual("Item3", observableReadOnlyList[1]);

            var array = new string[3];
            observableReadOnlyList.CopyTo(array, 1);

            NUnit.Framework.Assert.IsNull(array[0]);
            NUnit.Framework.Assert.AreEqual("Item1", array[1]);
            NUnit.Framework.Assert.AreEqual("Item3", array[2]);
        }


        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("ObservableReadOnlyList_IEnumerable")]
        public void ObservableReadOnlyList_IEnumerable_Implemented()
        {
            //------------Setup for test--------------------------
            var observableReadOnlyList = new ObservableReadOnlyList<string>(new List<string> { "Item1", "Item2", "Item3" });

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsInstanceOf(observableReadOnlyList.GetType(), typeof(IEnumerable<string>));

            var enumeratorCount = 0;
            var enumerator = observableReadOnlyList.GetEnumerator();
            while(enumerator.MoveNext())
            {
                NUnit.Framework.Assert.AreEqual("Item" + ++enumeratorCount, enumerator.Current);
            }

            NUnit.Framework.Assert.AreEqual(3, enumeratorCount);
        }


        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("ObservableReadOnlyList_INotifyCollectionChanged")]
        public void ObservableReadOnlyList_INotifyCollectionChanged_Implemented()
        {
            //------------Setup for test--------------------------
            var observableReadOnlyList = new ObservableReadOnlyList<string>(new List<string> { "Item1", "Item2", "Item3" });

            var collectionChanged = false;
            observableReadOnlyList.CollectionChanged += (sender, args) => collectionChanged = true;

            //------------Execute Test---------------------------
            observableReadOnlyList.Add("item4");

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsInstanceOf(observableReadOnlyList.GetType(), typeof(INotifyCollectionChanged));
            NUnit.Framework.Assert.IsTrue(collectionChanged);
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("ObservableReadOnlyList_CollectionChanged")]
        public void ObservableReadOnlyList_CollectionChanged_BoundToCollectionViewAndModifiedFromNonDispatcherThread_DoesNotThrowException()
        {
            //------------Setup for test--------------------------

            //------------Execute Test---------------------------
            VerifyCollectionChangedBoundToCollectionViewDoesNotThrowException(list => list.Clear());
            VerifyCollectionChangedBoundToCollectionViewDoesNotThrowException(list => list.Add("item3"));
            VerifyCollectionChangedBoundToCollectionViewDoesNotThrowException(list => list.Insert(0, "item4"));
            VerifyCollectionChangedBoundToCollectionViewDoesNotThrowException(list => list.RemoveAt(0));
            VerifyCollectionChangedBoundToCollectionViewDoesNotThrowException(list => list.Remove(list[0]));

            //------------Assert Results-------------------------
        }


        void VerifyCollectionChangedBoundToCollectionViewDoesNotThrowException(Action<ObservableReadOnlyList<string>> action)
        {
            //------------Setup for test--------------------------

            var otherDone = new ManualResetEventSlim(false);

            //
            // Create list and view on the Dispatcher.CurrentDispatcher thread
            // MUST bind to CollectionView!!
            //
            var observableReadOnlyList = new ObservableReadOnlyList<string> { "item1", "item2" };
            var px = new PrivateObject(observableReadOnlyList);
            px.SetProperty("TestDispatcherFrame", new DispatcherFrame());

            var collectionView = CollectionViewSource.GetDefaultView(observableReadOnlyList);

            //
            // Modify list from another thread
            //
            string exceptionMessage = null;
            var otherThread = new Thread(() =>
            {
                try
                {
                    action?.Invoke(observableReadOnlyList);

                    exceptionMessage = null;
                }
                catch(Exception ex)
                {
                    exceptionMessage = ex.Message;
                }
                finally
                {
                    otherDone.Set();
                }
            });

            //------------Execute Test---------------------------
            otherThread.Start();
       
            
            // Wait for thread to finish
            Dispatcher.PushFrame((DispatcherFrame)px.GetProperty("TestDispatcherFrame"));

            otherDone.Wait();

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNull(exceptionMessage);
        }
    }


}
