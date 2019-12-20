/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common.Exchange;
using Dev2.Common.Interfaces;
using Microsoft.Exchange.WebServices.Data;
using NUnit.Framework;
using Moq;
using System;

namespace Dev2.Tests.Runtime
{
    [TestFixture]
    [SetUpFixture]
    public class ExchangeEmailSenderTests
    {
        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ExchangeEmailSender))]
        public void ExchangeEmailSender_InValid_Send_AutoDiscoverUrl_IsNullOrEmpty_ExpectServiceValidationException()
        {
            //---------------------------Arrange---------------------------
            var mockExchange = new Mock<IExchange>();

            var exchangeEmailSender = new ExchangeEmailSender(mockExchange.Object);
            //---------------------------Act-------------------------------
            //---------------------------Assert----------------------------
            NUnit.Framework.Assert.Throws<ServiceValidationException>(()=> exchangeEmailSender.Send(new ExchangeServiceFactory().Create(), new EmailMessage(new ExchangeServiceFactory().Create())));
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ExchangeEmailSender))]
        public void ExchangeEmailSender_InValid_Send_AutoDiscoverUrl_IsNotNullOrEmpty_ExpectUriFormatException()
        {
            //---------------------------Arrange---------------------------
            var mockExchange = new Mock<IExchange>();

            mockExchange.Setup(o => o.AutoDiscoverUrl).Returns("testAutoDiscoverUrl");

            var exchangeEmailSender = new ExchangeEmailSender(mockExchange.Object);
            //---------------------------Act-------------------------------
            //---------------------------Assert----------------------------
            NUnit.Framework.Assert.Throws<UriFormatException>(()=> exchangeEmailSender.Send(new ExchangeServiceFactory().Create(), new EmailMessage(new ExchangeServiceFactory().Create())));
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ExchangeEmailSender))]
        public void ExchangeEmailSender_InValid_Send_AutoDiscoverUrl_IsNotNullOrEmpty_ExpectServiceLocalException()
        {
            //---------------------------Arrange---------------------------
            var mockExchange = new Mock<IExchange>();

            mockExchange.Setup(o => o.AutoDiscoverUrl).Returns("https://testAutoDiscoverUrl");

            var exchangeEmailSender = new ExchangeEmailSender(mockExchange.Object);
            //---------------------------Act-------------------------------
            //---------------------------Assert----------------------------
            NUnit.Framework.Assert.Throws<ServiceLocalException>(() => exchangeEmailSender.Send(new ExchangeServiceFactory().Create(), new EmailMessage(new ExchangeServiceFactory().Create())));
        }

        [Test]
        [Author("Siphamandla Dube")]
        [Category(nameof(ExchangeEmailSender))]
        public void ExchangeEmailSender_InValid_Send_AutoDiscoverUrl_IsNotNullOrEmpty_ExpectFormatException()
        {
            //---------------------------Arrange---------------------------
            var mockExchange = new Mock<IExchange>();

            mockExchange.Setup(o => o.UserName).Returns("TestUsername");
            var exchangeEmailSender = new ExchangeEmailSender(mockExchange.Object);
            //---------------------------Act-------------------------------
            //---------------------------Assert----------------------------
            NUnit.Framework.Assert.Throws<FormatException>(()=> exchangeEmailSender.Send(new ExchangeServiceFactory().Create(), new EmailMessage(new ExchangeServiceFactory().Create())));
        }
    }
}
