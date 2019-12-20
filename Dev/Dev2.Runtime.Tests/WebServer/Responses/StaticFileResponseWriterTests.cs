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
using System.Collections.Specialized;
using System.Net.Http;
using Dev2.Runtime.WebServer;
using Dev2.Runtime.WebServer.Responses;
using NUnit.Framework;

namespace Dev2.Tests.Runtime.WebServer.Responses
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime WebServer")]
    public class StaticFileResponseWriterTests
    {
        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("StaticFileResponseWriter_Constructor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StaticFileResponseWriter_Constructor_FileIsNull_ThrowsArgumentNullException()
        {
            //------------Setup for test--------------------------

            //------------Execute Test---------------------------
            var responseWriter = new StaticFileResponseWriter(null, null);

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("StaticFileResponseWriter_Constructor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StaticFileResponseWriter_Constructor_ContentTypeIsNull_ThrowsArgumentNullException()
        {
            //------------Setup for test--------------------------

            //------------Execute Test---------------------------
            var responseWriter = new StaticFileResponseWriter("XXX", null);

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("StaticFileResponseWriter_Write")]
        public void StaticFileResponseWriter_Write_WebServerContext_WritesContent()
        {
            var request = WebServerRequestTests.CreateHttpRequest(out string content, out NameValueCollection boundVars, out NameValueCollection queryStr, out NameValueCollection headers);

            var context = new WebServerContext(request, boundVars);

            const string NewContent = "Hello world";

            var responseWriter = new TestStaticFileResponseWriter(NewContent, "text/plain");

            //------------Execute Test---------------------------
            responseWriter.Write(context);

            //------------Assert Results-------------------------
            Assert.AreEqual(ContentTypes.Plain, context.ResponseMessage.Content.Headers.ContentType);
            Assert.IsInstanceOf(context.ResponseMessage.Content.GetType(), typeof(PushStreamContent));
            var task = context.ResponseMessage.Content.ReadAsStringAsync();
            task.Wait();

            Assert.AreEqual(NewContent, task.Result);
        }
    }
}
