using Dev2.Common.Interfaces.Data;
using Dev2.Common.Search;
using Dev2.Runtime.Interfaces;
using Dev2.Runtime.Search;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev2.Tests.Runtime.Search
{
    [TestFixture]
    [SetUpFixture]
    public class VariableListSearcherTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullResourceCatalog_ExpectException()
        {
            var variableListSearcher = new VariableListSearcher(null);
            NUnit.Framework.Assert.IsNull(variableListSearcher);
        }

        [Test]
        public void Constructor_ResourceCatalogTestCatalog_ExpectNoException()
        {
            var variableListSearcher = new VariableListSearcher(new Mock<IResourceCatalog>().Object);
            NUnit.Framework.Assert.IsNotNull(variableListSearcher);
        }

        [Test]
        public void GetSearchResults_WhenScalarNameHasValue_ShouldReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();
            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");
            mockResource.Setup(r => r.DataList).Returns(new StringBuilder("<DataList><scalar1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><scalar2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><Recset Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" ><Field1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /><Field2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /></Recset></DataList>"));
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockResource.Object);
            mockResourceCatalog.Setup(res => res.GetResources(It.IsAny<Guid>())).Returns(new List<IResource>
            {
                mockResource.Object
            });
            var searchOptions = new SearchOptions();
            searchOptions.UpdateAllStates(false);
            searchOptions.IsScalarNameSelected = true;
            var searchValue = new Common.Search.Search
            {
                SearchInput = "1",
                SearchOptions = searchOptions
            };            

            var variableListSearcher = new VariableListSearcher(mockResourceCatalog.Object);
            var searchResults = variableListSearcher.GetSearchResults(searchValue);
            NUnit.Framework.Assert.AreEqual(1, searchResults.Count);
            var searchResult = searchResults[0];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("scalar1", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.Scalar, searchResult.Type);
        }


        [Test]
        public void GetSearchResults_WhenScalarNameDoesNotHaveValue_ShouldNotReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();
            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");
            mockResource.Setup(r => r.DataList).Returns(new StringBuilder("<DataList><scalar1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><scalar2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><Recset Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" ><Field1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /><Field2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /></Recset></DataList>"));
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockResource.Object);
            mockResourceCatalog.Setup(res => res.GetResources(It.IsAny<Guid>())).Returns(new List<IResource>
            {
                mockResource.Object
            });
            var searchValue = new Common.Search.Search
            {
                SearchInput = "bob",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = false,
                    IsScalarNameSelected = true,
                }
            };

            var variableListSearcher = new VariableListSearcher(mockResourceCatalog.Object);
            var searchResults = variableListSearcher.GetSearchResults(searchValue);
            NUnit.Framework.Assert.AreEqual(0, searchResults.Count);            
        }

        [Test]
        public void GetSearchResults_WhenRecsetNameHasValue_ShouldReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();
            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");
            mockResource.Setup(r => r.DataList).Returns(new StringBuilder("<DataList><scalar1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><scalar2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><Recset Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" ><Field1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /><Field2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /></Recset></DataList>"));
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockResource.Object);
            mockResourceCatalog.Setup(res => res.GetResources(It.IsAny<Guid>())).Returns(new List<IResource>
            {
                mockResource.Object
            });
            var searchOptions = new SearchOptions();
            searchOptions.UpdateAllStates(false);
            searchOptions.IsRecSetNameSelected = true;
            var searchValue = new Common.Search.Search
            {
                SearchInput = "set",
                SearchOptions = searchOptions
            };
           

            var variableListSearcher = new VariableListSearcher(mockResourceCatalog.Object);
            var searchResults = variableListSearcher.GetSearchResults(searchValue);
            NUnit.Framework.Assert.AreEqual(2, searchResults.Count);
            var searchResult = searchResults[0];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("Recset", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.RecordSet, searchResult.Type);
            searchResult = searchResults[1];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("Recset", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.RecordSet, searchResult.Type);
        }


        [Test]
        public void GetSearchResults_WhenObjectNameHasValue_ShouldReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();
            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");
            mockResource.Setup(r => r.DataList).Returns(new StringBuilder("<DataList><scalar1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><scalar2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><Recset Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" ><Field1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /><Field2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /></Recset><Person IsJson=\"true\" Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" ><Name Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /><LastName Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /></Person></DataList>"));
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockResource.Object);
            mockResourceCatalog.Setup(res => res.GetResources(It.IsAny<Guid>())).Returns(new List<IResource>
            {
                mockResource.Object
            });
            var searchOptions = new SearchOptions();
            searchOptions.UpdateAllStates(false);
            searchOptions.IsObjectNameSelected = true;
            var searchValue = new Common.Search.Search
            {
                SearchInput = "per",
                SearchOptions = searchOptions
            };

            var variableListSearcher = new VariableListSearcher(mockResourceCatalog.Object);
            var searchResults = variableListSearcher.GetSearchResults(searchValue);
            NUnit.Framework.Assert.AreEqual(1, searchResults.Count);
            var searchResult = searchResults[0];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("@Person", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.Object, searchResult.Type);
            
        }

        [Test]
        public void GetSearchResults_WhenMultipleMatches_ShouldReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();
            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");
            mockResource.Setup(r => r.DataList).Returns(new StringBuilder("<DataList><scalar1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><scalar2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"Input\" /><Recset Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" ><Field1 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /><Field2 Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /></Recset><Person IsJson=\"true\" Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" ><Name Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /><LastName Description=\"\" IsEditable=\"True\" " +
                                                                          "ColumnIODirection=\"None\" /></Person></DataList>"));
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockResource.Object);
            mockResourceCatalog.Setup(res => res.GetResources(It.IsAny<Guid>())).Returns(new List<IResource>
            {
                mockResource.Object
            });
            var searchOptions = new SearchOptions();
            searchOptions.UpdateAllStates(false);
            searchOptions.IsObjectNameSelected = true;
            searchOptions.IsScalarNameSelected = true;
            searchOptions.IsRecSetNameSelected = true;
            var searchValue = new Common.Search.Search
            {
                SearchInput = "s",
                SearchOptions = searchOptions
            };
            

            var variableListSearcher = new VariableListSearcher(mockResourceCatalog.Object);
            var searchResults = variableListSearcher.GetSearchResults(searchValue);
            NUnit.Framework.Assert.AreEqual(5, searchResults.Count);
            var searchResult = searchResults[0];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("scalar1", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.Scalar, searchResult.Type);
            searchResult = searchResults[1];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("scalar2", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.Scalar, searchResult.Type);
            searchResult = searchResults[2];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("Recset", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.RecordSet, searchResult.Type);
            searchResult = searchResults[3];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("Recset", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.RecordSet, searchResult.Type);
            searchResult = searchResults[4];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("@Person", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.Object, searchResult.Type);

        }
    }
}
