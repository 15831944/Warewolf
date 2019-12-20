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
using System.Linq;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Search;
using Dev2.Runtime.Interfaces;
using Dev2.Runtime.Search;
using Dev2.Tests.Activities.ActivityTests;
using NUnit.Framework;
using Moq;
using Warewolf.ResourceManagement;

namespace Dev2.Tests.Runtime.Search
{
    [TestFixture]
    [SetUpFixture]
    public class ActivitySearcherTests
    {
        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ActivitySearcher))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ActivitySearcher_NullResourceCatalog_ShouldThrowException()
        {
            var searcher = new ActivitySearcher(null);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ActivitySearcher))]
        public void ActivitySearcher_GetSearchResults_WhenToolTitleHasValue_ShouldReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();
            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockResource.Object);
            var searcher = new ActivitySearcher(mockResourceCatalog.Object);
            var search = new Common.Search.Search
            {
                SearchInput = "Set",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true
                }
            };
            var mockResourceActivityCache = new Mock<IResourceActivityCache>();
            var cache = new System.Collections.Concurrent.ConcurrentDictionary<Guid, IDev2Activity>();
            var startAct = new TestActivity
            {
                DisplayName = "Start Tool",
                NextNodes = new List<IDev2Activity>
                {
                     new TestActivity
                     {
                         DisplayName = "Set a value"
                     }
                }
            };
            cache.TryAdd(Guid.Empty, startAct);
            mockResourceActivityCache.Setup(c => c.Cache).Returns(cache);
            mockResourceCatalog.Setup(res => res.GetResourceActivityCache(It.IsAny<Guid>())).Returns(mockResourceActivityCache.Object);
            var searchResults = searcher.GetSearchResults(search);
            NUnit.Framework.Assert.AreEqual(1, searchResults.Count);
            var searchResult = searchResults[0];
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResult.ResourceId);
            NUnit.Framework.Assert.AreEqual("Set a value", searchResult.Match);
            NUnit.Framework.Assert.AreEqual("Test Resource", searchResult.Name);
            NUnit.Framework.Assert.AreEqual("Folder", searchResult.Path);
            NUnit.Framework.Assert.AreEqual(Common.Interfaces.Search.SearchItemType.ToolTitle, searchResult.Type);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ActivitySearcher))]
        public void ActivitySearcher_GetSearchResults_WhenToolTitleDoesNotHaveValue_ShouldNotReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();
            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockResource.Object);
            var searcher = new ActivitySearcher(mockResourceCatalog.Object);
            var searchValue = new Common.Search.Search
            {
                SearchInput = "Bob",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true
                }
            };
            var mockResourceActivityCache = new Mock<IResourceActivityCache>();
            var cache = new System.Collections.Concurrent.ConcurrentDictionary<Guid, IDev2Activity>();
            var startAct = new TestActivity
            {
                DisplayName = "Start Tool",
                NextNodes = new List<IDev2Activity>
                {
                     new TestActivity
                     {
                         DisplayName = "Set a value"
                     }
                }
            };
            cache.TryAdd(Guid.Empty, startAct);
            mockResourceActivityCache.Setup(c => c.Cache).Returns(cache);
            mockResourceCatalog.Setup(res => res.GetResourceActivityCache(It.IsAny<Guid>())).Returns(mockResourceActivityCache.Object);
            var searchResults = searcher.GetSearchResults(searchValue);
            NUnit.Framework.Assert.AreEqual(0, searchResults.Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ActivitySearcher))]
        public void ActivitySearcher_GetSearchResults_WhenComplexFlowHaveValue_ShouldReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();
            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(mockResource.Object);
            var searcher = new ActivitySearcher(mockResourceCatalog.Object);
            var searchValue = new Common.Search.Search
            {
                SearchInput = "Bob",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true
                }
            };
            var mockResourceActivityCache = new Mock<IResourceActivityCache>();
            var cache = new System.Collections.Concurrent.ConcurrentDictionary<Guid, IDev2Activity>();
            var firstFlow = new TestActivity
            {
                DisplayName = "Start Tool",
                NextNodes = new List<IDev2Activity>
                {
                     new TestActivity
                     {
                         DisplayName = "Set a value",
                         NextNodes = new List<IDev2Activity>
                         {
                             new TestActivity
                             {
                                 DisplayName = "Set Bob Name"
                             },
                             new TestActivity
                             {
                                 DisplayName = "Retrive",
                                 NextNodes = new List<IDev2Activity>
                                 {
                                     new TestActivity
                                     {
                                         DisplayName = "Get Bob Name"
                                     }
                                 }
                             }
                         }
                     }
                }
            };
            var secondFlow = new TestActivity
            {
                DisplayName = "Start Tool",
                NextNodes = new List<IDev2Activity>
                {
                     new TestActivity
                     {
                         DisplayName = "Set a value"
                     }
                }
            };
            cache.TryAdd(Guid.Empty, firstFlow);
            cache.TryAdd(Guid.NewGuid(), secondFlow);
            mockResourceActivityCache.Setup(c => c.Cache).Returns(cache);
            mockResourceCatalog.Setup(res => res.GetResourceActivityCache(It.IsAny<Guid>())).Returns(mockResourceActivityCache.Object);
            var searchResults = searcher.GetSearchResults(searchValue);
            NUnit.Framework.Assert.AreEqual(2, searchResults.Count);
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResults[0].ResourceId);
            NUnit.Framework.Assert.AreEqual("Set Bob Name", searchResults[0].Match);
            NUnit.Framework.Assert.AreEqual(Guid.Empty, searchResults[1].ResourceId);
            NUnit.Framework.Assert.AreEqual("Get Bob Name", searchResults[1].Match);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ActivitySearcher))]
        public void ActivitySearcher_GetSearchResults_WhenMatchInTwoResources_ShouldReturnResult()
        {
            var mockResourceCatalog = new Mock<IResourceCatalog>();

            var mockResource = new Mock<IResource>();
            mockResource.Setup(r => r.ResourceID).Returns(Guid.Empty);
            mockResource.Setup(r => r.ResourceName).Returns("Test Resource");
            mockResource.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");

            var otherResourceId = Guid.NewGuid();
            var mockResource2 = new Mock<IResource>();
            mockResource2.Setup(r => r.ResourceID).Returns(otherResourceId);
            mockResource2.Setup(r => r.ResourceName).Returns("Test Resource 2");
            mockResource2.Setup(r => r.GetResourcePath(It.IsAny<Guid>())).Returns("Folder");

            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), Guid.Empty)).Returns(mockResource.Object);
            mockResourceCatalog.Setup(res => res.GetResource(It.IsAny<Guid>(), otherResourceId)).Returns(mockResource2.Object);

            var searcher = new ActivitySearcher(mockResourceCatalog.Object);
            var searchValue = new Common.Search.Search
            {
                SearchInput = "Bob",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true,
                }
            };
            var mockResourceActivityCache = new Mock<IResourceActivityCache>();
            var cache = new System.Collections.Concurrent.ConcurrentDictionary<Guid, IDev2Activity>();
            var firstFlow = new TestActivity
            {
                DisplayName = "Start Tool",
                NextNodes = new List<IDev2Activity>
                {
                     new TestActivity
                     {
                         DisplayName = "Set a value",
                         NextNodes = new List<IDev2Activity>
                         {
                             new TestActivity
                             {
                                 DisplayName = "Set Bob Name"
                             },
                             new TestActivity
                             {
                                 DisplayName = "Retrive",
                                 NextNodes = new List<IDev2Activity>
                                 {
                                     new TestActivity
                                     {
                                         DisplayName = "Get Bob Name"
                                     }
                                 }
                             }
                         }
                     }
                }
            };
            var secondFlow = new TestActivity
            {
                DisplayName = "Start Tool",
                NextNodes = new List<IDev2Activity>
                {
                     new TestActivity
                     {
                         DisplayName = "What's bobs name"
                     }
                }
            };
            cache.TryAdd(Guid.Empty, firstFlow);
            cache.TryAdd(otherResourceId, secondFlow);
            mockResourceActivityCache.Setup(c => c.Cache).Returns(cache);
            mockResourceCatalog.Setup(res => res.GetResourceActivityCache(It.IsAny<Guid>())).Returns(mockResourceActivityCache.Object);
            var searchResults = searcher.GetSearchResults(searchValue);

            NUnit.Framework.Assert.AreEqual(3, searchResults.Count);

            var firstItem = searchResults.First(result => result.Match == "Set Bob Name");
            var secondItem = searchResults.First(result => result.Match == "Get Bob Name");
            var otherItem = searchResults.First(result => result.ResourceId == otherResourceId);

            NUnit.Framework.Assert.AreEqual(Guid.Empty, firstItem.ResourceId);
            NUnit.Framework.Assert.AreEqual("Set Bob Name", firstItem.Match);
            NUnit.Framework.Assert.AreEqual(Guid.Empty, secondItem.ResourceId);
            NUnit.Framework.Assert.AreEqual("Get Bob Name", secondItem.Match);
            NUnit.Framework.Assert.AreEqual(otherResourceId, otherItem.ResourceId);
            NUnit.Framework.Assert.AreEqual("What's bobs name", otherItem.Match);
        }
    }
}
