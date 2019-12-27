using System;
using NUnit.Framework;
using Dev2.Common.Utils;
using Dev2.Common.Search;

namespace Dev2.Common.Tests.Utils
{
    [TestFixture]
    public class SearchUtilTests
    {
        [Test]
        [Author("Rory McGuire")]
        [Category("SearchUtils_FilterText")]
        public void SearchUtils_FilterText_MatchingWord_ShouldReturnTrue()
        {
            var searchValue = new Search.Search
            {
                SearchInput = "Set",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true
                }
            };
            var result = SearchUtils.FilterText("Set", searchValue);
            Assert.IsTrue(result);
        }
        [Test]
        [Author("Rory McGuire")]
        [Category("SearchUtils_FilterText")]
        public void SearchUtils_FilterText_MatchingWholeWordOneWord_ShouldReturnTrue()
        {
            var searchValue = new Search.Search
            {
                SearchInput = "Set",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true,
                    IsMatchWholeWordSelected = true
                }
            };
            var result = SearchUtils.FilterText("Set", searchValue);
            Assert.IsTrue(result);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category("SearchUtils_FilterText")]
        public void SearchUtils_FilterText_MatchingWholeWord_ShouldReturnTrue()
        {
            var searchValue = new Search.Search
            {
                SearchInput = "Set",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true,
                    IsMatchWholeWordSelected = true
                }
            };
            var result = SearchUtils.FilterText("this Set asdf", searchValue);
            Assert.IsTrue(result);
        }


        [Test]
        [Author("Rory McGuire")]
        [Category("SearchUtils_FilterText")]
        public void SearchUtils_FilterText_MatchingWholeWord_ShouldReturnFalse()
        {
            var searchValue = new Search.Search
            {
                SearchInput = "Set",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true,
                    IsMatchWholeWordSelected = true
                }
            };
            var result = SearchUtils.FilterText("this Setasdf", searchValue);
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category("SearchUtils_FilterText")]
        public void SearchUtils_FilterText_MatchingWord2_ShouldReturnTrue()
        {
            var searchValue = new Search.Search
            {
                SearchInput = "Set",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true
                }
            };
            var result = SearchUtils.FilterText("this Setasdf", searchValue);
            Assert.IsTrue(result);
        }
        [Test]
        [Author("Rory McGuire")]
        [Category("SearchUtils_FilterText")]
        public void SearchUtils_FilterText_MatchingWord2_ShouldReturnFalse()
        {
            var searchValue = new Search.Search
            {
                SearchInput = "Set",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsToolTitleSelected = true
                }
            };
            var result = SearchUtils.FilterText("this teSasdf", searchValue);
            Assert.IsFalse(result);
        }
        [Test]
        [Author("Rory McGuire")]
        [Category("SearchUtils_FilterText")]
        public void SearchUtils_FilterText_MatchingWholeWord_RegexShouldReturnTrue()
        {
            var searchValue = new Search.Search
            {
                SearchInput = "Hello World",
                SearchOptions = new SearchOptions
                {
                    IsAllSelected = false,
                    IsWorkflowNameSelected = true,
                    IsMatchCaseSelected = false,
                    IsMatchWholeWordSelected = true
                }
            };
            var result = SearchUtils.FilterText("Hello World", searchValue);
            Assert.IsTrue(result);
        }
    }
}
