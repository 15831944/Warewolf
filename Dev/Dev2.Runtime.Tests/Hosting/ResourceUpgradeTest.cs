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
using System.Xml.Linq;
using Dev2.Runtime.Hosting;
using NUnit.Framework;

namespace Dev2.Tests.Runtime.Hosting
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class ResourceUpgradeTest
    {

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ResourceUpgrade_Ctor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResourceUpgrade_Ctor_Null_Params_Func()
        {
            //------------Setup for test--------------------------
            
            new ResourceUpgrade( null);
            

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("ResourceUpgrade_Ctor")]
        public void ResourceUpgrade_Properties()
        {
            //------------Setup for test--------------------------
            
            var x = new Func<XElement,XElement>( async=>async);
            var a = new ResourceUpgrade(x);
            NUnit.Framework.Assert.AreEqual(x,a.UpgradeFunc);
            

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }
    }
}
