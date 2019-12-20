/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Data.Decisions.Operations;
using NUnit.Framework;
using System;

namespace Dev2.Data.Tests.Operations
{
    [TestFixture]
    [SetUpFixture]
    public class Dev2DecisionOperationTests
    {
        #region Comparing Integers

        #region IsLessThan

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsLessThan with an array of strings that can be parsed to integers, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsLessThan_IsLessThanUnitTest_Invoke_TrueIsReturned()

        {
            //init
            var comparer = new IsLessThan();

            //exe
            var actual = comparer.Invoke(new[] { "2", "100" });
            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsLessThan returned the wrong result when comparing integers");

            //exe
            actual = comparer.Invoke(new[] { "SomeVal", "Val2" });
            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsLessThan returned the wrong result when comparing strings");

            //exe
            actual = comparer.Invoke(new[] { string.Empty });
            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsLessThan returned the wrong result when comparing empty string");
        }

        [Test]
        [Author("Sanele Mthmembu")]        
        public void IsLessThan_HandleType_ShouldReturnIsLessThan()
        {
            var decisionType = enDecisionType.IsLessThan;
            //------------Setup for test--------------------------
            var isLessThan = new IsLessThan();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isLessThan.HandlesType());
        }

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsLessThan with an array of strings that can be parsed to integers, false is expected")]
        [Author("Ashley Lewis")]
        
        public void IsLessThan_IsLessThanUnitTest_Invoke_FalseIsReturned()

        {
            //init
            var comparer = new IsLessThan();

            //exe
            var actual = comparer.Invoke(new[] { "100", "2" });

            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsLessThan returned the wrong result when comparing integers");
        }

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsLessThan with an array of strings that can be parsed to decimals, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsLessThan_IsLessThanUnitTest_InvokeWithDecimals_TrueIsReturned()

        {
            //init
            var comparer = new IsLessThan();

            //exe
            var actual = comparer.Invoke(new[] { "2.75", "100.25" });

            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsLessThan returned the wrong result when comparing integers");
        }

        #endregion

        #region IsGreaterThan

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsGreaterThan with an array of strings that can be parsed to integers, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsGreaterThan_IsGreaterThanUnitTest_Invoke_TrueIsReturned()

        {
            //init
            var comparer = new IsGreaterThan();

            //exe
            var actual = comparer.Invoke(new[] { "100", "2" });

            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsGreaterThan returned the wrong result when comparing integers");
        }

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsGreaterThan with an array of strings that can be parsed to integers, false is expected")]
        [Author("Ashley Lewis")]
        
        public void IsGreaterThan_IsGreaterThanUnitTest_Invoke_FalseIsReturned()

        {
            //init
            var comparer = new IsGreaterThan();

            //exe
            var actual = comparer.Invoke(new[] { "2", "100" });

            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsGreaterThan returned the wrong result when comparing integers");

            //exe
            actual = comparer.Invoke(new[] { "SomeVal", "AnotherVal" });
            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsGreaterThan returned the wrong result when comparing strings");

            //exe
            actual = comparer.Invoke(new[] { string.Empty });
            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsGreaterThan returned the wrong result when comparing empty string");
        }


        [Test]
        [Author("Sanele Mthmembu")]
        public void IsGreaterThan_HandleType_ShouldReturnIsGreaterThan()
        {
            var decisionType = enDecisionType.IsGreaterThan;
            //------------Setup for test--------------------------
            var greaterThan = new IsGreaterThan();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, greaterThan.HandlesType());
        }

        #endregion

        #region IsGreaterThanOrEqual

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsGreaterThanOrEqual with an array of strings that can be parsed to integers, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsGreaterThanOrEqual_IsGreaterThanOrEqualUnitTest_InvokeWithEqualStrings_TrueIsReturned()
    
        {
            //init
            var comparer = new IsGreaterThanOrEqual();

            //exe
            var actual = comparer.Invoke(new[] { "2", "2" });

            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsGreaterThanOrEqual returned the wrong result when comparing integers");
        }

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsGreaterThanOrEqual with an array of equal strings that can be parsed to integers, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsGreaterThanOrEqual_IsGreaterThanOrEqualUnitTest_Invoke_TrueIsReturned()
    
        {
            //init
            var comparer = new IsGreaterThanOrEqual();

            //exe
            var actual = comparer.Invoke(new[] { "100", "2" });

            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsGreaterThanOrEqual returned the wrong result when comparing integers");
            //exe
            actual = comparer.Invoke(new[] { "Vals", "Val2" });
            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsGreaterThanOrEqual returned the wrong result when comparing strings");

            //exe
            actual = comparer.Invoke(new[] { string.Empty });
            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsGreaterThanOrEqual returned the wrong result when comparing empty string");
        }

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsGreaterThanOrEqual with an array of strings that can be parsed to integers, false is expected")]
        [Author("Ashley Lewis")]
        
        public void IsGreaterThanOrEqual_IsGreaterThanOrEqualUnitTest_Invoke_FalseIsReturned()
    
        {
            //init
            var comparer = new IsGreaterThanOrEqual();

            //exe
            var actual = comparer.Invoke(new[] { "2", "100" });

            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsGreaterThanOrEqual returned the wrong result when comparing integers");
        }


        [Test]
        [Author("Sanele Mthmembu")]
        public void IsGreaterThanOrEqual_HandleType_ShouldReturbIsGreaterThanOrEqual()
        {
            var decisionType = enDecisionType.IsGreaterThanOrEqual;
            //------------Setup for test--------------------------
            var isGreaterThanOrEqual = new IsGreaterThanOrEqual();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isGreaterThanOrEqual.HandlesType());
        }


        #endregion

        #region IsLessThanOrEqual

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsLessThanOrEqual with an array of strings that can be parsed to integers, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsLessThanOrEqual_IsLessThanOrEqualUnitTest_Invoke_TrueIsReturned()

        {
            //init
            var comparer = new IsLessThanOrEqual();

            //exe
            var actual = comparer.Invoke(new[] { "2", "100" });

            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsLessThanOrEqual returned the wrong result when comparing integers");
        }

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsLessThanOrEqual with an array of equal strings that can be parsed to integers, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsLessThanOrEqual_IsLessThanOrEqualUnitTest_InvokeWithEqualStrings_TrueIsReturned()

        {
            //init
            var comparer = new IsLessThanOrEqual();

            //exe
            var actual = comparer.Invoke(new[] { "2", "2" });
            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsLessThanOrEqual returned the wrong result when comparing integers");

            //exe
            actual = comparer.Invoke(new[] { "SomeVal", "SomeVal" });
            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsLessThanOrEqual returned the wrong result when comparing strings");

            //exe
            actual = comparer.Invoke(new[] {string.Empty});
            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsLessThanOrEqual returned the wrong result when comparing empty string");
        }

        [Test]
        [Author("Sanele Mthmembu")]
        public void IsLessThanOrEqual_ShouldReturnedIsLessThanOrEqualDecisionType()
        {
            var decisionType = enDecisionType.IsLessThanOrEqual;
            //------------Setup for test--------------------------
            var isLessThanOrEqual = new IsLessThanOrEqual();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isLessThanOrEqual.HandlesType());
        }

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsLessThanOrEqual with an array of strings that can be parsed to integers, false is expected")]
        [Author("Ashley Lewis")]
        
        public void IsLessThanOrEqual_IsLessThanOrEqualUnitTest_Invoke_FalseIsReturned()

        {
            //init
            var comparer = new IsLessThanOrEqual();

            //exe
            var actual = comparer.Invoke(new[] { "100", "2" });

            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsLessThanOrEqual returned the wrong result when comparing integers");
        }


        #endregion

        #region Equal

        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsLessThanOrEqual with an array of strings that can be parsed to integers, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsEqual_IsEqualUnitTest_Invoke_TrueIsReturned()

        {
            //init
            var comparer = new IsEqual();

            //exe
            var actual = comparer.Invoke(new[] { "100", "100" });

            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsEqual returned the wrong result when comparing integers");
        }           
        
        [Test]
        [Category("UnitTest")]
        [Description("Test for invoking IsLessThanOrEqual with an array of strings that can be parsed to integers, true is expected")]
        [Author("Ashley Lewis")]
        
        public void IsEqual_IsEqualUnitTest_Invoke_TrueIsReturned_Decimal()

        {
            //init
            var comparer = new IsEqual();

            //exe
            var actual = comparer.Invoke(new[] { "1.8", "1.80" });

            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsEqual returned the wrong result when comparing integers");

            //exe
            actual = comparer.Invoke(new[] { "Val", "Val" });
            //assert
            NUnit.Framework.Assert.IsTrue(actual, "IsEqual returned the wrong result when comparing strings");

            //exe
            actual = comparer.Invoke(new[] { string.Empty, "Something" });
            //assert
            NUnit.Framework.Assert.IsFalse(actual, "IsEqual returned the wrong result when comparing empty string");
        }


        [Test]
        [Author("Sanele Mthmembu")]
        public void IsEqual_IsEqualUnitTest_HandleType_ShouldReturnIsEqual()
        {
            var decisionType = enDecisionType.IsEqual;
            //------------Setup for test--------------------------
            var isEqual = new IsEqual();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isEqual.HandlesType());
        }


        #endregion

        #region Equal

        [Test]
        [Author("Sanele Mthembu")]        
        public void IsNotEqual_IsNotEqualUnitTest_Invoke_TrueIsReturned()
        {
            var comparer = new IsNotEqual();            
            var actual = comparer.Invoke(new[] { "100", "100" });
            NUnit.Framework.Assert.IsFalse(actual, "IsNotEqual returned the wrong result when comparing integers");
        }

        [Test]
        [Author("Sanele Mthembu")]        
        public void IsNotEqual_IsNotEqualUnitTest_Invoke_TrueIsReturned_Decimal()

        {            
            var comparer = new IsNotEqual();
            var actual = comparer.Invoke(new[] { "1.08", "1.80" });
            NUnit.Framework.Assert.IsTrue(actual, "IsNotEqual returned the wrong result when comparing integers");
            
            actual = comparer.Invoke(new[] { "Val", "Val" });
            NUnit.Framework.Assert.IsFalse(actual, "IsNotEqual returned the wrong result when comparing strings");
        }


        [Test]
        [Author("Sanele Mthmembu")]
        public void IsNotEqual_IsNotEqualUnitTest_HandleType_ShouldReturnIsNotEqual()
        {
            var decisionType = enDecisionType.IsNotEqual;
            //------------Setup for test--------------------------
            var isNotEqual = new IsNotEqual();
            //------------Execute Test---------------------------
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(decisionType, isNotEqual.HandlesType());
        }


        #endregion

        #endregion

        #region Comparing DateTimes

        #region NotBetween

        [Test]
        [Author("Ashley Lewis")]
        [Category("NotBetween_Invoke")]
        public void NotBetween_InvokeWithDoubles_NotBetween_ReturnsFalse()
        {
            //------------Setup for test--------------------------
            var notBetween = new NotBetween();
            var cols = new String[3];
            cols[0] = DateTime.Now.ToString();
            cols[1] = (DateTime.Now - TimeSpan.FromMinutes(5)).ToString();
            cols[2] = (DateTime.Now + TimeSpan.FromMinutes(10)).ToString();

            //------------Execute Test---------------------------
            var result = notBetween.Invoke(cols);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(result);
        }

        [Test]
        [Author("Ashley Lewis")]
        [Category("NotBetween_Invoke")]
        public void NotBetween_InvokeWithDoubles_NotBetween_ReturnsTrue()
        {
            //------------Setup for test--------------------------
            var notBetween = new NotBetween();
            var cols = new string[3];
            cols[0] = "30.0";
            cols[1] = "10.0";
            cols[2] = "20.0";

            //------------Execute Test---------------------------
            var result = notBetween.Invoke(cols);

            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(result);
        }

        #endregion

        #endregion
    }
}
