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
using System.Linq;
using System.Xml.Linq;
using Dev2.Runtime.ServiceModel.Data;
using Dev2.Tests.Runtime.XML;
using NUnit.Framework;
using Unlimited.Framework.Converters.Graph.String.Xml;

namespace Dev2.Tests.Runtime.ServiceModel.Data
{
    // BUG 9500 - 2013.05.31 - TWR : added proper testing

    /// <summary>
    /// Summary description for DbServiceTests
    /// </summary>
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class PluginServiceTests
    {
        #region CTOR

        [Test]
        public void PluginServiceContructorWithDefaultExpectedInitializesProperties()
        {
            var service = new PluginService();
            NUnit.Framework.Assert.AreEqual(Guid.Empty, service.ResourceID);
            NUnit.Framework.Assert.AreEqual("PluginService", service.ResourceType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PluginServiceContructorWithNullXmlExpectedThrowsArgumentNullException()
        {
            var service = new PluginService(null);
        }

        [Test]
        public void PluginServiceContructorWithInvalidXmlExpectedDoesNotThrowExceptionAndInitializesProperties()
        {
            var xml = new XElement("root");
            var service = new PluginService(xml);
            NUnit.Framework.Assert.AreNotEqual(Guid.Empty, service.ResourceID);
            NUnit.Framework.Assert.IsTrue(service.IsUpgraded);
            NUnit.Framework.Assert.AreEqual("PluginService", service.ResourceType);
        }

        [Test]
        public void PluginServiceContructorWithValidXmlExpectedInitializesProperties()
        {
            var xml = XmlResource.Fetch("PluginService");

            var service = new PluginService(xml);
            VerifyEmbeddedPluginService(service);
        }

        #endregion

        #region ToXml

        [Test]
        public void PluginServiceToXmlExpectedSerializesProperties()
        {
            var expected = new PluginService
            {
                Source = new PluginSource
                {
                    ResourceID = Guid.NewGuid(),
                    ResourceName = "TestWebSource",
                },
                Namespace = "abc.pqr",
            };

            expected.Method.Parameters.AddRange(
                new[]
                {
                    new MethodParameter
                    {
                        Name = "Param1",
                        DefaultValue = "123"
                    },
                    new MethodParameter
                    {
                        Name = "Param2",
                        DefaultValue = "456"
                    }
                });

            var rs1 = new Recordset
            {
                Name = "Recordset1()"
            };
            rs1.Fields.AddRange(new[]
            {
                new RecordsetField
                {
                    Name = "Field1",
                    Alias = "Alias1"
                },
                new RecordsetField
                {
                    Name = "Field2",
                    Alias = "Alias2",
                    Path = new XmlPath("actual", "display", "outputExpression", "sampleData")
                },
                new RecordsetField
                {
                    Name = "Field3",
                    Alias = null
                }
            });
            expected.Recordsets.Add(rs1);

            var xml = expected.ToXml();

            var actual = new PluginService(xml);

            NUnit.Framework.Assert.AreEqual(expected.Source.ResourceType, actual.Source.ResourceType);
            NUnit.Framework.Assert.AreEqual(expected.Source.ResourceID, actual.Source.ResourceID);
            NUnit.Framework.Assert.AreEqual(expected.Source.ResourceName, actual.Source.ResourceName);
            NUnit.Framework.Assert.AreEqual(expected.ResourceType, actual.ResourceType);
            NUnit.Framework.Assert.AreEqual(expected.Namespace, actual.Namespace);

            foreach(var expectedParameter in expected.Method.Parameters)
            {
                var actualParameter = actual.Method.Parameters.First(p => p.Name == expectedParameter.Name);
                NUnit.Framework.Assert.AreEqual(expectedParameter.DefaultValue, actualParameter.DefaultValue);
            }

            foreach(var expectedRecordset in expected.Recordsets)
            {
                // expect actual to have removed recordset notation ()...
                var actualRecordset = actual.Recordsets.First(rs => rs.Name == expectedRecordset.Name.Replace("()", ""));
                foreach(var expectedField in expectedRecordset.Fields)
                {
                    var actualField = actualRecordset.Fields.FirstOrDefault(f => expectedField.Name == null ? f.Name == "" : f.Name == expectedField.Name);
                    NUnit.Framework.Assert.IsNotNull(actualField);
                    NUnit.Framework.Assert.AreEqual(expectedField.Alias ?? "", actualField.Alias);
                    if(actualField.Path != null)
                    {
                        NUnit.Framework.Assert.AreEqual(expectedField.Path.ActualPath, actualField.Path.ActualPath);
                        NUnit.Framework.Assert.AreEqual(expectedField.Path.DisplayPath, actualField.Path.DisplayPath);
                        NUnit.Framework.Assert.AreEqual(string.Format("[[{0}]]", expectedField.Alias), actualField.Path.OutputExpression);
                        NUnit.Framework.Assert.AreEqual(expectedField.Path.SampleData, actualField.Path.SampleData);
                    }
                }
            }
        }

        #endregion

        #region VerifyEmbeddedPluginService

        public static void VerifyEmbeddedPluginService(PluginService service)
        {
            NUnit.Framework.Assert.AreEqual(Guid.Parse("89098b76-ac11-40b2-b3e8-b175314cb3bb"), service.ResourceID);
            NUnit.Framework.Assert.AreEqual("PluginService", service.ResourceType);
            NUnit.Framework.Assert.AreEqual(Guid.Parse("00746beb-46c1-48a8-9492-e2d20817fcd5"), service.Source.ResourceID);
            NUnit.Framework.Assert.AreEqual("PluginTesterSource", service.Source.ResourceName);
            NUnit.Framework.Assert.AreEqual("Dev2.Terrain.Mountain", service.Namespace);
            NUnit.Framework.Assert.AreEqual("Echo", service.Method.Name);

            NUnit.Framework.Assert.AreEqual("<root>hello</root>", service.Method.Parameters.First(p => p.Name == "text").DefaultValue);

            NUnit.Framework.Assert.AreEqual("reverb", service.Recordsets[0].Fields.First(f => f.Name == "echo").Alias);
        }

        #endregion


    }
}
