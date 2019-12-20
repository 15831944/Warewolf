using System.Collections.Generic;
using Dev2.Data.Binary_Objects;
using Dev2.Data.MathOperations;
using Dev2.Data.SystemTemplates;
using Dev2.DataList.Contract;
using Dev2.Runtime.ServiceModel.Data;
using NUnit.Framework;

namespace Dev2.Data.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class ExtensionClassesTests
    {
        [Test]
        public void DataListConstants_ShouldHave_AllConstants()
        {
            NUnit.Framework.Assert.IsNotNull(DataListConstants.DefaultCase);
            NUnit.Framework.Assert.IsNotNull(DataListConstants.DefaultDecision);
            NUnit.Framework.Assert.IsNotNull(DataListConstants.DefaultStack);
            NUnit.Framework.Assert.IsNotNull(DataListConstants.DefaultSwitch);
            NUnit.Framework.Assert.IsNotNull(DataListConstants.EmptyRowStartIdx);
            NUnit.Framework.Assert.IsNotNull(DataListConstants.MinRowSize);
            NUnit.Framework.Assert.IsNotNull(DataListConstants.RowGrowthFactor);
        }

        
        [Test]
        public void GivenResourceName_ResourceForTree_ToString_ShouldReturtnResourceName()
        {
            var resourceForTree = new ResourceForTree();
            NUnit.Framework.Assert.IsNotNull(resourceForTree);
            resourceForTree.ResourceName = "SomeName";
            var res = resourceForTree.ToString();
            NUnit.Framework.Assert.AreEqual("SomeName", res);
        }

        [Test]
        public void GivenTagName_SystemTag_ShouldSurroundNameWithTags()
        {
            var tag = new SystemTag("SomeName");
            NUnit.Framework.Assert.IsNotNull(tag);
            NUnit.Framework.Assert.AreEqual("<SomeName>", tag.StartTag);
            NUnit.Framework.Assert.AreEqual("</SomeName>", tag.EndTag);
        }

        [Test]
        public void GivenName_InputDefinition_ShouldNameWithTags()
        {
            var inputDefinition = new InputDefinition("SomeName", "MapsToSomething", false);
            NUnit.Framework.Assert.IsNotNull(inputDefinition);
            NUnit.Framework.Assert.AreEqual("SomeName", inputDefinition.Name);
            NUnit.Framework.Assert.AreEqual("MapsToSomething", inputDefinition.MapsTo);
            NUnit.Framework.Assert.AreEqual("<MapsToSomething>", inputDefinition.StartTagSearch);
            NUnit.Framework.Assert.AreEqual("</MapsToSomething>", inputDefinition.EndTagSearch);
            NUnit.Framework.Assert.AreEqual("<SomeName>", inputDefinition.StartTagReplace);
            NUnit.Framework.Assert.AreEqual("</SomeName>", inputDefinition.EndTagReplace);
            NUnit.Framework.Assert.IsFalse(inputDefinition.IsEvaluated);
        }
       

        [Test]
        public void SearchTO_ShouldHaveConstructor()
        {
            var searchTo = new SearchTO("searchField", "seacrchType", "searchCriteria", "result");
            NUnit.Framework.Assert.IsNotNull(searchTo);
            NUnit.Framework.Assert.AreEqual("searchField", searchTo.FieldsToSearch);
            NUnit.Framework.Assert.AreEqual("seacrchType", searchTo.SearchType);
            NUnit.Framework.Assert.AreEqual("searchCriteria", searchTo.SearchCriteria);
            NUnit.Framework.Assert.AreEqual("result", searchTo.Result);
        }

        [Test]
        public void CreateEvaluationFunctionTO_ShouldHaveConstructor()
        {
            var evaluationFunctionTo = MathOpsFactory.CreateEvaluationFunctionTO("someFunction");
            NUnit.Framework.Assert.IsNotNull(evaluationFunctionTo);
            NUnit.Framework.Assert.AreEqual("someFunction", evaluationFunctionTo.Function);
        }

        
        [Test]
        public void ListOfIndex_ShouldHaveConstructor()
        {
            var indexes = new List<int> {1,2};
            var listOfIndex = new ListOfIndex(indexes);
            NUnit.Framework.Assert.IsNotNull(listOfIndex);
            var maxIndex = listOfIndex.GetMaxIndex();
            NUnit.Framework.Assert.AreEqual(2, maxIndex);
        }

        [Test]
        public void ListOfIndex_MaxIndex_ShouldReturn2()
        {
            var indexes = new List<int> { 1, 2 };
            var listOfIndex = new ListOfIndex(indexes);
            NUnit.Framework.Assert.IsNotNull(listOfIndex);
            var count = listOfIndex.Count();
            NUnit.Framework.Assert.AreEqual(2, count);
        }
    }
}
