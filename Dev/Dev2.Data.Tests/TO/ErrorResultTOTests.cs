
/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Text;
using Dev2.Data.TO;
using NUnit.Framework;


namespace Dev2.Data.Tests.TO
{
    [TestFixture]
    [SetUpFixture]
    public class ErrorResultTOTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_AddError_CheckForDuplicates_True_AddSameError_ExpectLstToBeSame()
        {
            var resultTo = new ErrorResultTO();
            resultTo.AddError("some message", true);
            resultTo.AddError("some message", true);

            NUnit.Framework.Assert.IsTrue(resultTo.HasErrors());
            NUnit.Framework.Assert.AreEqual(1, resultTo.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("some message", resultTo.FetchErrors()[0]);
            NUnit.Framework.Assert.AreEqual("<InnerError>some message</InnerError>", resultTo.MakeDataListReady());
        }
        
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_AddError_CheckForDuplicates_True_AddNewError_ExpectTrue()
        {
            var resultTo = new ErrorResultTO();
            resultTo.AddError("some message", true);
            resultTo.AddError("some message", true);
            resultTo.AddError("deferent message", true);

            NUnit.Framework.Assert.IsTrue(resultTo.HasErrors());
            NUnit.Framework.Assert.AreEqual(2, resultTo.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("some message", resultTo.FetchErrors()[0]);
            NUnit.Framework.Assert.AreEqual("deferent message", resultTo.FetchErrors()[1]);
            NUnit.Framework.Assert.AreEqual("<InnerError>some message</InnerError><InnerError>deferent message</InnerError>", resultTo.MakeDataListReady());
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_AddError_CheckForDuplicates_False_AddSameError_ExpectAdd()
        {
            var resultTo = new ErrorResultTO();
            resultTo.AddError("some message", false);
            resultTo.AddError("some message", false);
            resultTo.AddError("some message", true);
            resultTo.AddError("deferent message", false);

            NUnit.Framework.Assert.IsTrue(resultTo.HasErrors());
            NUnit.Framework.Assert.AreEqual(3, resultTo.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("some message", resultTo.FetchErrors()[0]);
            NUnit.Framework.Assert.AreEqual("some message", resultTo.FetchErrors()[1]);
            NUnit.Framework.Assert.AreEqual("deferent message", resultTo.FetchErrors()[2]);
            NUnit.Framework.Assert.AreEqual("<InnerError>some message</InnerError><InnerError>some message</InnerError><InnerError>deferent message</InnerError>", resultTo.MakeDataListReady());
        }
        
        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_AddError_NullMessage()
        {
            var resultTo = new ErrorResultTO();
            resultTo.AddError(null, true);

            NUnit.Framework.Assert.IsFalse(resultTo.HasErrors());
            // Shouldn't this be passing?
            NUnit.Framework.Assert.AreEqual(0, resultTo.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("", resultTo.MakeDataListReady());
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MakeErrorResultFromDataListStringWithMultipleErrorsExpectedCorrectErrorResultTO()
        {
            var makeErrorResultFromDataListString = ErrorResultTO.MakeErrorResultFromDataListString("<InnerError>First Error</InnerError><InnerError>Second Error</InnerError>");
            NUnit.Framework.Assert.IsTrue(makeErrorResultFromDataListString.HasErrors());
            NUnit.Framework.Assert.AreEqual(2, makeErrorResultFromDataListString.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("First Error", makeErrorResultFromDataListString.FetchErrors()[0]);
            NUnit.Framework.Assert.AreEqual("Second Error", makeErrorResultFromDataListString.FetchErrors()[1]);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MakeErrorResultFromDataListString_WhenErrorStringNotValidXML_ShouldJustAddTheError()
        {
            //------------Setup for test--------------------------
            //------------Execute Test---------------------------
            var makeErrorResultFromDataListString = ErrorResultTO.MakeErrorResultFromDataListString("<InnerError>Could not insert <> into a field</InnerError>");
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(1, makeErrorResultFromDataListString.FetchErrors().Count);
            NUnit.Framework.Assert.AreEqual("<Error><InnerError>Could not insert <> into a field</InnerError></Error>", makeErrorResultFromDataListString.FetchErrors()[0]);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MergeErrors_ShouldJustRemoveTheErrorInTheCollection()
        {
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("SomeError");
            NUnit.Framework.Assert.AreEqual(1, errorResultTo.FetchErrors().Count);

            var merge = new ErrorResultTO();
            merge.AddError("Error to merge");
            errorResultTo.MergeErrors(merge);

            NUnit.Framework.Assert.AreEqual(2, errorResultTo.FetchErrors().Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MergeErrors_EmptyOther()
        {
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("SomeError");

            NUnit.Framework.Assert.AreEqual(1, errorResultTo.FetchErrors().Count);

            var merge = new ErrorResultTO();
            errorResultTo.MergeErrors(merge);
            NUnit.Framework.Assert.AreEqual(1, errorResultTo.FetchErrors().Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MergeErrors_NullOtherDoesNotThrow()
        {
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("SomeError");

            NUnit.Framework.Assert.AreEqual(1, errorResultTo.FetchErrors().Count);

            errorResultTo.MergeErrors(null);
            NUnit.Framework.Assert.AreEqual(1, errorResultTo.FetchErrors().Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_Remove_ShouldJustRemoveTheErrorInTheCollection()
        {
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("SomeError");

            NUnit.Framework.Assert.AreEqual(1, errorResultTo.FetchErrors().Count);

            errorResultTo.RemoveError("SomeError");
            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_Clear_ShouldEmptyTheErrorCollection()
        {
            var errorResultTo = new ErrorResultTO();
            errorResultTo.AddError("SomeError");
            errorResultTo.AddError("AnotherError");

            NUnit.Framework.Assert.AreEqual(2, errorResultTo.FetchErrors().Count);

            errorResultTo.ClearErrors();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MakeDisplayReady_ShouldReturnAllErrorsAsOne()
        {
            var result = new StringBuilder();
            result.AppendLine("SomeError");
            result.Append("AnotherError");
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("SomeError");
            errorResultTo.AddError("AnotherError");

            NUnit.Framework.Assert.AreEqual(2, errorResultTo.FetchErrors().Count);

            var makeDisplayReady = errorResultTo.MakeDisplayReady();
            NUnit.Framework.Assert.AreEqual(result.ToString(), makeDisplayReady);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MakeDataListReady_ShouldReturnAllErrorsAsOne()
        {
            var result = "<InnerError>SomeError</InnerError><InnerError>AnotherError</InnerError>";
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("SomeError");
            errorResultTo.AddError("AnotherError");

            NUnit.Framework.Assert.AreEqual(2, errorResultTo.FetchErrors().Count);

            var makeDisplayReady = errorResultTo.MakeDataListReady();
            NUnit.Framework.Assert.AreEqual(result, makeDisplayReady);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MakeDataListReady_ExpectJson_ShouldEscapeQuotesInErrorMessages()
        {
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("this is some exception's \"message\" string");
            errorResultTo.AddError("Another \"Error\"");
            errorResultTo.AddError("\"Error\" message");

            var makeDisplayReady = errorResultTo.MakeDataListReady(false);
            var result = "\"errors\": [ \"this is some exception's \\\"message\\\" string\",\"Another \\\"Error\\\"\",\"\\\"Error\\\" message\"]";
            NUnit.Framework.Assert.AreEqual(result, makeDisplayReady);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MakeDataListReady_AsXmlFalseShouldReturnAllErrorsAsOne()
        {
            var result = "\"errors\": [ \"SomeError\",\"AnotherError\"]";
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("SomeError");
            errorResultTo.AddError("AnotherError");

            NUnit.Framework.Assert.AreEqual(2, errorResultTo.FetchErrors().Count);

            var makeDisplayReady = errorResultTo.MakeDataListReady(false);
            NUnit.Framework.Assert.AreEqual(result, makeDisplayReady);
        }

        [Test]
        [Author("Rory McGuire")]
        [Category(nameof(ErrorResultTO))]
        public void ErrorResultTO_MakeDataListReady_CannotSetUnknownMember_RemapsErrorMessage()
        {
            var result = "\"errors\": [ \"SomeError\",\"Resource has unrecognized formatting, this Warewolf Server may be to outdated to read this resource.\",\"Another Error\"]";
            var errorResultTo = new ErrorResultTO();

            NUnit.Framework.Assert.AreEqual(0, errorResultTo.FetchErrors().Count);
            errorResultTo.AddError("SomeError");
            errorResultTo.AddError("Cannot set unknown member");
            errorResultTo.AddError("Another Error");

            NUnit.Framework.Assert.AreEqual(3, errorResultTo.FetchErrors().Count);

            var makeDisplayReady = errorResultTo.MakeDataListReady(false);
            NUnit.Framework.Assert.AreEqual(result, makeDisplayReady);
        }
    }
}
