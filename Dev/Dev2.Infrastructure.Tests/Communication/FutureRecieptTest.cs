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
using Dev2.Communication;
using NUnit.Framework;

namespace Dev2.Infrastructure.Tests.Communication
{
    [TestFixture]
    [SetUpFixture]
    public class FutureRecieptTest
    {

        static readonly Guid RequestID = Guid.NewGuid();



        [Test]
        [Author("Travis Frisinger")]
        [Category("FutureReceipt_ToKey")]
        public void FutureReceipt_ToKey_WhenValidKeyParts_ExpectKey()
        {
            //------------Setup for test--------------------------
            var futureReciept = new FutureReceipt { PartID = 1, RequestID = RequestID, User = "Bob" };

            //------------Execute Test---------------------------
            var result = futureReciept.ToKey();

            //------------Assert Results-------------------------
            StringAssert.Contains(result, RequestID+"-1-Bob!");
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("FutureReceipt_ToKey")]
        [ExpectedException(typeof(Exception))]
        public void FutureReceipt_ToKey_WhenPartIDLessThenZero_ExpectException()
        {
            //------------Setup for test--------------------------
            var futureReciept = new FutureReceipt { PartID = -1, RequestID = RequestID, User = "Bob" };

            //------------Execute Test---------------------------
            futureReciept.ToKey();
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("FutureReceipt_ToKey")]
        [ExpectedException(typeof(Exception))]
        public void FutureReceipt_ToKey_WhenRequestIDEmpty_ExpectException()
        {
            //------------Setup for test--------------------------
            var futureReciept = new FutureReceipt { PartID = 1,RequestID = Guid.Empty, User = "Bob" };

            //------------Execute Test---------------------------
            futureReciept.ToKey();
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("FutureReceipt_ToKey")]
        [ExpectedException(typeof(Exception))]
        public void FutureReceipt_ToKey_WhenRequestIDNotSet_ExpectException()
        {
            //------------Setup for test--------------------------
            var futureReciept = new FutureReceipt { PartID = 1, User = "Bob" };

            //------------Execute Test---------------------------
            futureReciept.ToKey();
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("FutureReceipt_ToKey")]
        [ExpectedException(typeof(Exception))]
        public void FutureReceipt_ToKey_WhenUserEmpty_ExpectException()
        {
            //------------Setup for test--------------------------
            var futureReciept = new FutureReceipt { PartID = 1, RequestID = RequestID, User = string.Empty };

            //------------Execute Test---------------------------
            futureReciept.ToKey();
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("FutureReceipt_ToKey")]
        [ExpectedException(typeof(Exception))]
        public void FutureReceipt_ToKey_WhenUserNull_ExpectException()
        {
            //------------Setup for test--------------------------
            var futureReciept = new FutureReceipt { PartID = 1, RequestID = RequestID, User = null };

            //------------Execute Test---------------------------
            futureReciept.ToKey();
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("FutureReceipt_ToKey")]
        [ExpectedException(typeof(Exception))]
        public void FutureReceipt_ToKey_WhenUserNotSet_ExpectException()
        {
            //------------Setup for test--------------------------
            var futureReciept = new FutureReceipt { PartID = 1, RequestID = RequestID };

            //------------Execute Test---------------------------
            futureReciept.ToKey();
        }



    }
}
