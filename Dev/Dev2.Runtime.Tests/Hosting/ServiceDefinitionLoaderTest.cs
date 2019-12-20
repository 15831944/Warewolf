/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Runtime.Hosting;
using NUnit.Framework;

namespace Dev2.Tests.Runtime.Hosting
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class ServiceDefinitionLoaderTest
    {
        // GenerateServiceGraph
        [Test]
        [Author("Travis Frisinger")]
        [Category("ServiceDefinitionLoader_GenerateServiceGraph")]
        public void ServiceDefinitionLoader_GenerateServiceGraph_WhenLoadingSource_SourceIsLoaded()
        {
            //------------Setup for test--------------------------
            var serviceDefinitionLoader = new ServiceDefinitionLoader();
            
            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("ServiceDefinitionLoader_GenerateServiceGraph")]
        public void ServiceDefinitionLoader_GenerateServiceGraph_WhenLoadingWorkflow_WorkflowIsLoaded()
        {
            //------------Setup for test--------------------------
            var serviceDefinitionLoader = new ServiceDefinitionLoader();

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("ServiceDefinitionLoader_GenerateServiceGraph")]
        public void ServiceDefinitionLoader_GenerateServiceGraph_WhenLoadingWorkerService_ServiceIsLoaded()
        {
            //------------Setup for test--------------------------
            var serviceDefinitionLoader = new ServiceDefinitionLoader();

            //------------Execute Test---------------------------

            //------------Assert Results-------------------------
        }
    }
}
