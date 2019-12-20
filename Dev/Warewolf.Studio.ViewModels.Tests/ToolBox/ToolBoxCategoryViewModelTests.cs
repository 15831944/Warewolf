﻿using NUnit.Framework;
using System.Collections.Generic;
using Dev2.Common.Interfaces.Toolbox;
using Moq;

namespace Warewolf.Studio.ViewModels.ToolBox.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class ToolBoxCategoryViewModelTests
    {
        [Test]
        [Timeout(60000)]
        public void TestToolBoxCategoryViewModel()
        {
            //arrange
            var toolDescriptorViewModelsMock = new Mock<ICollection<IToolDescriptorViewModel>>();
            var testName = "someName";
            var target = new ToolBoxCategoryViewModel(testName, toolDescriptorViewModelsMock.Object);

            //act
            var actualName = target.Name;
            var actualTools = target.Tools;

            //assert
            Assert.AreEqual(testName, actualName);
            Assert.AreSame(toolDescriptorViewModelsMock.Object, actualTools);
        }
    }
}