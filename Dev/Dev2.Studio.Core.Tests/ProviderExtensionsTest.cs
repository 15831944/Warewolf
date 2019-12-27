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
using Dev2.Intellisense.Provider;
using Dev2.Studio.Interfaces;
using NUnit.Framework;

namespace Dev2.Core.Tests
{
    [TestFixture]
    
    public class ProviderExtensionsTest
    {
        
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_EmptyString_ExpectEmpty()
        {
            const string inputText = "";
            const int caretPosition = 13;
            const string expectedResult = "";
            FindTextTestHelper(caretPosition, inputText, expectedResult);
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_InvalidPositionNegative_ExpectEmpty()
        {

            FindTextTestHelper(-1, "bob", "");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_InvalidPosition_ExpectValue()
        {

            FindTextTestHelper(29, "bob", "bob");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_ClosedBraceAfterValue_ExpectValue()
        {

            FindTextTestHelper(9, "[[rec.(pp)]]", "pp");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_ClosedBrace_ExpectValue()
        {

            FindTextTestHelper(9, ")", "");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_ClosedSquareBraceAfterValue_ExpectValue()
        {

            FindTextTestHelper(5, "[[bob]]", "[[bob");
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_ClosedSquareBracePartialWord_ExpectValue()
        {

            FindTextTestHelper(4, "[[bob]]", "[[bo");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_UnbalancedSquareBracePartialWord_ExpectValue()
        {

            FindTextTestHelper(4, "[[bob", "[[bo");
        }



        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_Spaces_ExpectValue()
        {

            FindTextTestHelper(16, "[[rec.( dora bob )  ", "bob");
            FindTextTestHelper(12, "[[rec.( dora bob )  ", "dora");
            FindTextTestHelper(10, "[[rec  .( dora bob )  ", "");
        }


        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_ClosedSquareBraceEmpty_ExpectEmpty()
        {

            FindTextTestHelper(4, "[[ ]]", "");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_ClosedRoundBrace_ExpectEmpty()
        {

            FindTextTestHelper(2, "( )", "");
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_OpenRoundBrace_ExpectCorrectRegions()
        {

            FindTextTestHelper(2, "( ", "");
            FindTextTestHelper(1, "a( ", "a");
            FindTextTestHelper(1, "(a ", "");
        }
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_MatchingRoundBrace_ExpectCorrectRegions()
        {

            FindTextTestHelper(2, "( )", "");
            FindTextTestHelper(2, "(a) ", "a");
            FindTextTestHelper(2, "()a ()", "");
            FindTextTestHelper(7, "()([abc ()", "[abc");
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_SpecialCharacters_ExpectCorrectRegions()
        {

            FindTextTestHelper(4, "(a++)", "");
            FindTextTestHelper(2, "(a+*)", "a");
            FindTextTestHelper(3, "(a%^)", "");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_NullThrowsException()
        {

           ((IntellisenseProviderContext)null).FindTextToSearch();
         
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_UnbalalancedBoundary_ExpectValue()
        {
            FindTextTestHelper(2, "[ba )", "[b");
            FindTextTestHelper(8, "dave [ba)", "[ba");
            FindTextTestHelper(8, "dave (ba]", "ba");
        }


        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_InputTextIsNull_EmptyString()
        {
            FindTextTestHelper(2, null, "");
        }

        [Test]
        [Author("Tshepo Ntlhokoa")]
        [Category("ProviderExtensions_FindTextToSearch")]
        public void ProviderExtensions_FindTextToSearch_InputTextIsEmpty_EmptyString()
        {
            FindTextTestHelper(2, "", "");
        }

        static void FindTextTestHelper(int caretPosition, string inputText, string expectedResult)
        {
            var context = new IntellisenseProviderContext
            {
                CaretPosition = caretPosition,
                InputText = inputText,
                DesiredResultSet = IntellisenseDesiredResultSet.Default
            };

            var search = context.FindTextToSearch();
            Assert.AreEqual(expectedResult, search);
        }
    }
}
