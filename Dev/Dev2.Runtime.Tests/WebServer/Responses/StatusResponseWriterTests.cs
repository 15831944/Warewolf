/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Collections.Specialized;
using System.Net;
using Dev2.Runtime.WebServer;
using Dev2.Runtime.WebServer.Responses;
using NUnit.Framework;

namespace Dev2.Tests.Runtime.WebServer.Responses
{
    [TestFixture]
    [Category("Runtime WebServer")]
    public class StatusResponseWriterTests
    {
        [Test]
        [Author("Trevor Williams-Ros")]
        [Category("StatusResponseWriter_Write")]
        public void StatusResponseWriter_Write_WebServerContext_WritesTheStatus()
        {
            //------------Setup for test--------------------------
            const HttpStatusCode Expected = HttpStatusCode.PaymentRequired;
            var request = WebServerRequestTests.CreateHttpRequest(out string content, out NameValueCollection boundVars, out NameValueCollection queryStr, out NameValueCollection headers);

            var context = new WebServerContext(request, boundVars);

            var responseWriter = new StatusResponseWriter(Expected);

            //------------Execute Test---------------------------
            responseWriter.Write(context);

            //------------Assert Results-------------------------
            Assert.AreEqual(Expected, context.ResponseMessage.StatusCode);
        }
    }
}
