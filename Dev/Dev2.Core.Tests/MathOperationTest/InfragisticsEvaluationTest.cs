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
using System.Globalization;
using Infragistics.Calculations.CalcManager;
using NUnit.Framework;


namespace Dev2.Tests.MathOperationTest
{

    /// <summary>
    /// This Test class exists to test any changes that are made on the Infragistics source code. Ideally, if infragistics was part of the build then this would reside in that solution
    /// </summary>
    [TestFixture]
    public class InfragisticsEvaluationTest
    {

        Dev2CalculationManager _manager;

        [SetUp]
        public void Init()
        {
            _manager = new Dev2CalculationManager();
            
        }

        [Author("Leon Rajindrapersadh")]
        [Category("Infragistics source code changes")]
        [Test]
        public void IsNumber_Correct_For_Numbers()
        {
            AssertValues(new Tuple<string, string>("true", "isnumber(\"1\")"), new Tuple<string, string>("true", "isnumber(\"-1\")"), new Tuple<string, string>("true", "isnumber(\"-1.1\")"), new Tuple<string, string>("true", "isnumber(\"1.1\")"));
        }

        [Author("Leon Rajindrapersadh")]
        [Category("Infragistics source code changes")]
        [Test]
        public void IsNumber_Correct_For_Empty()
        {
            AssertValues(new Tuple<string, string>("false", "isnumber(\"\")"), new Tuple<string, string>("false", "isnumber(\"g\")"), new Tuple<string, string>("false", "isnumber(\",\")"));
        }

        [Author("Leon Rajindrapersadh")]
        [Category("Infragistics source code changes")]
        [Test]
        public void Value_Correct_For_Numbers()
        {
            AssertValues(new Tuple<string, string>("1", "value(\"1\")"), new Tuple<string, string>("-1", "value(\"-1\")"), new Tuple<string, string>("-1.1", "value(\"-1.1\")"), new Tuple<string, string>("1.1", "value(\"1.1\")"));
        }

        [Author("Leon Rajindrapersadh")]
        [Category("Infragistics source code changes")]
        [Test]
        public void Value_Correct_For_NonNumbers()
        {
            AssertValues(new Tuple<string, string>("#num!", "value(\"a\")"), new Tuple<string, string>("#num!", "value(\"\")"), new Tuple<string, string>("#num!", "value(\" \")"), new Tuple<string, string>("#num!", "value(\",\")"));
        }

        [Author("Leon Rajindrapersadh")]
        [Category("Infragistics source code changes")]
        [Test]
        public void IsEven_Correct_For_NonNumbers()
        {
            AssertValues(new Tuple<string, string>("#num!", "iseven(\"a\")"), new Tuple<string, string>("#num!", "iseven(\"\")"), new Tuple<string, string>("#num!", "iseven(\" \")"), new Tuple<string, string>("#num!", "iseven(\",\")"));
        }

        [Author("Leon Rajindrapersadh")]
        [Category("Infragistics source code changes")]
        [Test]
        public void IsEven_Correct_For_Numbers()
        {
            AssertValues(new Tuple<string, string>("true", "iseven(\"2\")"),new Tuple<string, string>("true", "iseven(\"-4\")"),new Tuple<string, string>("false", "iseven(\"1\")"), new Tuple<string, string>("false", "iseven(\"-1\")"));
        }


        [Author("Leon Rajindrapersadh")]
        [Category("Infragistics source code changes")]
        [Test]
        public void IsOdd_Correct_For_NonNumbers()
        {
            AssertValues(new Tuple<string, string>("#num!", "isodd(\"a\")"), new Tuple<string, string>("#num!", "isodd(\"\")"), new Tuple<string, string>("#num!", "isodd(\" \")"), new Tuple<string, string>("#num!", "isodd(\",\")"));
        }

        [Author("Leon Rajindrapersadh")]
        [Category("Infragistics source code changes")]
        [Test]
        public void IsOdd_Correct_For_Numbers()
        {
            AssertValues(new Tuple<string, string>("false", "isodd(\"2\")"), new Tuple<string, string>("false", "isodd(\"-4\")"), new Tuple<string, string>("true", "isodd(\"1\")"), new Tuple<string, string>("true", "isodd(\"-1\")"));
        }

        void AssertValues(params Tuple<string, string>[] functions)
        {
            foreach (var function in functions)
            {
                Assert.AreEqual(function.Item1, _manager.CalculateFormula(function.Item2).ToString(CultureInfo.InvariantCulture).ToLower());
            }
        }
    }
}
