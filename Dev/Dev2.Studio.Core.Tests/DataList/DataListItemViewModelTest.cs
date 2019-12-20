/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Studio.Core.Factories;
using Dev2.Studio.Interfaces.DataList;
using NUnit.Framework;

namespace Dev2.Core.Tests
{
    [TestFixture]
    [SetUpFixture]
    [Category("Studio Datalist Core")]
    public class DataListItemViewModelTest
    {
        #region Locals

        IDataListItemModel _dataListItemModel;
        int _count;


        #endregion

        #region Additional test attributes

        [OneTimeSetUp]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        [SetUp]
        public void MyTestInitialize()
        {
            _dataListItemModel = DataListItemModelFactory.CreateDataListModel("testItem");
        }


        #endregion

        #region Parent Property Tests

        /// <summary>
        ///A test for DataListItemViewModel Parent Property Setter
        ///</summary>
        //[Test]
        //public void CreationofItemAsChild_ExpectedCorrectReferenceToParent()
        //{
        //    IDataListItemModel childSet = DataListItemModelFactory.CreateDataListModel("TestChild", "", _dataListItemModel);

        //    Assert.AreEqual("testItem", childSet.Parent.DisplayName);
        //}

        #endregion Parent Property Tests

        #region AddChildren Tests

        /// <summary>
        ///A test for Adding DataListItems to the DataListViewModel
        ///</summary>
        //[Test]
        //public void AddChild_ExpectedChildCreationOnDataListItem()
        //{
        //    IDataListItemModel dataListItemToAdd = DataListItemModelFactory.CreateDataListModel("testName");
        //    int countBeforeAdd = _dataListItemModel.Children.Count;
        //    _dataListItemModel.Children.Add(dataListItemToAdd);
        //    Assert.IsTrue(_dataListItemModel.Children.Count > countBeforeAdd && _dataListItemModel.Children.Count < countBeforeAdd + 2);
        //}


        // Should this be checking for errors?
        /// <summary>
        /// A test for adding invalid children
        /// </summary>
        //[Test]
        //public void AddChild_InvalidChildrenCollection_Expected_ChildrenContainErrors()
        //{
        //    IDataListItemModel child = DataListItemModelFactory.CreateDataListModel("test!@#");

        //    _dataListItemModel.Children.Add(child);

        //    Assert.IsTrue(_dataListItemModel.Children.Count == 1 && _dataListItemModel.Children[0].HasError);
        //}

        #endregion AddChildren Tests

        #region RemoveChild Tests

        //[Test]
        //public void RemoveChild_ExpectRootDataListItemToHaveOneChild()
        //{
        //    IDataListItemModel dataListItemToAdd = DataListItemModelFactory.CreateDataListModel("testDataListItem");
        //    _dataListItemModel.Children.Add(dataListItemToAdd);

        //    int countBefore = _dataListItemModel.Children.Count;
        //    _dataListItemModel.Children.Remove(dataListItemToAdd);

        //    Assert.AreNotEqual(_dataListItemModel.Children.Count, countBefore);
        //}

        #endregion RemoveChild Tests

        #region Properties Tests

        /// <summary>
        ///A test for DisplayName
        ///</summary>
        //[Test]
        //public void GetDisplayName_ExpectedReturnDataListItemDisplayName()
        //{
        //    _dataListItemModel.Children.Add(DataListItemModelFactory.CreateDataListModel("testChild", "", _dataListItemModel));
        //    const string expected = "testChild";

        //    string actual = _dataListItemModel.Children[0].DisplayName;
        //    Assert.AreEqual(expected, actual);
        //}

        /// <summary>
        ///A test for IsRecordSet
        ///</summary>
        //[Test]
        //public void IsNotRecordSet_ExpectedDataItemNotContainChildren()
        //{
        //    const bool expected = false;
        //    bool actual = _dataListItemModel.CanHaveMutipleRows;
        //    Assert.AreEqual(expected, actual);
        //}

        /// <summary>
        ///A test for Name
        ///</summary>
        [Test]
        public void DataItemNameUpdate_ExpectedDataItemNameUpdated()
        {
            const string expected = "testItem";
            var actual = _dataListItemModel.DisplayName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests if an invalidly formed DataListItem is sent to the ActivityComparer
        ///</summary>
        [Test]
        public void MalformedDataListItemToDsfActivityComparer()
        {
            _dataListItemModel.HasError = true;
            _dataListItemModel.IsSelected = true;
            Assert.AreEqual(0, _count);

        }

        #endregion Properties Tests

        #region VerifyName Tests

        /// <summary>
        ///Verifying the name has no special chars
        ///</summary>
        [Test]
        public void VerifyNameHasNoSpecialChars()
        {
            _dataListItemModel.DisplayName = "test@";
            Assert.IsTrue(_dataListItemModel.HasError);
        }

        #endregion VerifyName Tests

         

        #region Static Method Tests

        // Sashen - 17-10-2012 Both the static methods "ProcessChildren" and "CreateDataListXmlElement" are tested by the ToDataListXML Method

        #endregion Static Method Tests

        #region Internal Test Methods

        public int MediatorRecieveTestMethod()
        {
            _count++;
            return _count;
        }

        #endregion Internal Test Methods
      
    }
}
