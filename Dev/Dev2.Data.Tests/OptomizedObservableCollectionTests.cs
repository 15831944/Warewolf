/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/
using Dev2.Data.Interfaces;
using Dev2.Data.Interfaces.Enums;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;

namespace Dev2.Data.Tests
{
    [TestFixture]
    public class OptomizedObservableCollectionTests
    {
        [Test]
        [Author("Candice Daniel")]
        [Category("OptomizedObservableCollection")]
        public void OptomizedObservableCollectionTests_Ctor()
        {
            var items = GetInputTestDataDataNames();
            NUnit.Framework.Assert.AreEqual(2, items.Count);
            NUnit.Framework.Assert.AreEqual("scalar1", items[0].DisplayValue);
            NUnit.Framework.Assert.AreEqual("ScalarData1", items[0].Value);
            NUnit.Framework.Assert.AreEqual("scalar2", items[1].DisplayValue);
            NUnit.Framework.Assert.AreEqual("ScalarData2", items[1].Value);
        }

        [Test]
        [Author("Candice Daniel")]
        [Category("OptomizedObservableCollection")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OptomizedObservableCollectionTests_AddRange_Exception()
        {
            var items = new OptomizedObservableCollection<IDataListItem>();
            var scalars = new OptomizedObservableCollection<IDataListItem>();
            scalars = null;
            items.AddRange(scalars);
        }

        OptomizedObservableCollection<IDataListItem> GetInputTestDataDataNames()
        {
            var items = new OptomizedObservableCollection<IDataListItem>();
            items.AddRange(GetDataListItemScalar());
            return items;
        }

        IList<IDataListItem> GetDataListItemScalar()
        {
            IList<IDataListItem> scalars = new OptomizedObservableCollection<IDataListItem>
                                                                            {  CreateScalar("scalar1", "ScalarData1")
                                                                             , CreateScalar("scalar2", "ScalarData2")
                                                                            };
            return scalars;
        }

        IDataListItem CreateScalar(string scalarName, string scalarValue)
        {
            var item = new Mock<IDataListItem>();
            item.Setup(itemName => itemName.DisplayValue).Returns(scalarName);
            item.Setup(itemName => itemName.Field).Returns(scalarName);
            item.Setup(itemName => itemName.RecordsetIndexType).Returns(enRecordsetIndexType.Numeric);
            item.Setup(itemName => itemName.Value).Returns(scalarValue);
            return item.Object;
        }
    }
}
