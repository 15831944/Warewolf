using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dev2.Common.Interfaces.Data;
using Dev2.Data.Interfaces;
using Dev2.Data.TO;
using Dev2.DataList.Contract;
using NUnit.Framework;

namespace Dev2.Data.Tests
{
    [TestFixture]
    [SetUpFixture]
    public class DataListFactoryTests
    {
        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Construct()
        {
            var dlf = new DataListFactoryImplementation();
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_CreateOutputParser()
        {
            var dlf = new DataListFactoryImplementation();
            var parser = dlf.CreateOutputParser();
            var outputs = parser.Parse("<Outputs><Output Name =\"scalar1\" MapsTo=\"[[scalar1]]\" Value=\"[[scalar1]]\" DefaultValue=\"1234\" /></Outputs>");

            NUnit.Framework.Assert.AreEqual(1, outputs.Count);
            NUnit.Framework.Assert.AreEqual("1234", outputs[0].DefaultValue);
            NUnit.Framework.Assert.AreEqual("scalar1", outputs[0].MapsTo);
            NUnit.Framework.Assert.AreEqual("scalar1", outputs[0].Value);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].EmptyToNull);
            NUnit.Framework.Assert.AreEqual(true, outputs[0].IsEvaluated);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].IsRequired);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].IsObject);
            NUnit.Framework.Assert.AreEqual("[[scalar1]]", outputs[0].RawValue);
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_CreateScalarList()
        {
            t(true);
        }
        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_CreateScalarList2()
        {
            t(false);
        }
        void t(bool a)
        {
            var dlf = new DataListFactoryImplementation();
            var parser = dlf.CreateOutputParser();
            var outputs = parser.Parse("<Outputs><Output Name =\"scalar1\" MapsTo=\"[[scalar1]]\" Value=\"[[scalar1]]\" DefaultValue=\"1234\" /></Outputs>");
            var scalars = dlf.CreateScalarList(outputs, a).ToArray();

            NUnit.Framework.Assert.AreEqual(1, scalars.Length);
            NUnit.Framework.Assert.AreEqual("1234", scalars[0].DefaultValue);
            NUnit.Framework.Assert.AreEqual("scalar1", scalars[0].MapsTo);
            NUnit.Framework.Assert.AreEqual("scalar1", scalars[0].Value);
            NUnit.Framework.Assert.AreEqual(false, scalars[0].EmptyToNull);
            NUnit.Framework.Assert.AreEqual(true, scalars[0].IsEvaluated);
            NUnit.Framework.Assert.AreEqual(false, scalars[0].IsRequired);
            NUnit.Framework.Assert.AreEqual(false, scalars[0].IsObject);
            NUnit.Framework.Assert.AreEqual("[[scalar1]]", scalars[0].RawValue);
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_CreateScalarList_WithOnlyRecordsetData()
        {
            var dlf = new DataListFactoryImplementation();
            var parser = dlf.CreateOutputParser();
            var outputs = parser.Parse("<Outputs><Output Name=\"name\" MapsTo=\"[[name]]\" Value=\"[[person(*).name]]\" Recordset=\"person\" DefaultValue=\"bob1\" /></Outputs>");
            var scalars = dlf.CreateScalarList(outputs, true).ToArray();

            NUnit.Framework.Assert.AreEqual(0, scalars.Length);
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_CreateRecordSetCollection()
        {
            var dlf = new DataListFactoryImplementation();
            var parser = dlf.CreateOutputParser();
            var outputs = parser.Parse("<Outputs><Output Name=\"name\" MapsTo=\"[[name]]\" Value=\"[[person(*).name]]\" Recordset=\"person\" DefaultValue=\"bob1\" /></Outputs>");
            var collection = dlf.CreateRecordSetCollection(outputs, true);

            NUnit.Framework.Assert.AreEqual(1, outputs.Count);
            NUnit.Framework.Assert.AreEqual("person", outputs[0].RecordSetName);
            NUnit.Framework.Assert.AreEqual("name", outputs[0].MapsTo);
            NUnit.Framework.Assert.AreEqual("person(*).name", outputs[0].Value);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].EmptyToNull);
            NUnit.Framework.Assert.AreEqual(true, outputs[0].IsEvaluated);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].IsRequired);
            NUnit.Framework.Assert.AreEqual(true, outputs[0].IsRecordSet);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].IsObject);
            NUnit.Framework.Assert.AreEqual("[[person(*).name]]", outputs[0].RawValue);
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_CreateObjectList()
        {
            var dlf = new DataListFactoryImplementation();
            var parser = dlf.CreateOutputParser();
            var outputs = parser.Parse("<Outputs><Output Name=\"@obj.a\" MapsTo=\"[[a]]\" Value=\"[[@obj.a]]\" IsObject=\"True\" DefaultValue=\"1\" /></Outputs>");
            var collection = dlf.CreateObjectList(outputs);

            NUnit.Framework.Assert.AreEqual(1, outputs.Count);
            NUnit.Framework.Assert.AreEqual("@obj.a", outputs[0].Name);
            NUnit.Framework.Assert.AreEqual("a", outputs[0].MapsTo);
            NUnit.Framework.Assert.AreEqual("@obj.a", outputs[0].Value);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].EmptyToNull);
            NUnit.Framework.Assert.AreEqual(true, outputs[0].IsEvaluated);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].IsRequired);
            NUnit.Framework.Assert.AreEqual(false, outputs[0].IsRecordSet);
            NUnit.Framework.Assert.AreEqual(true, outputs[0].IsObject);
            NUnit.Framework.Assert.AreEqual("[[@obj.a]]", outputs[0].RawValue);
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Instance_Singleton()
        {
            var dlfs = new ConcurrentBag<IDataListFactory>();
            var threads = new List<Thread>();
            for (var i=0; i<100; i++)
            {
                var t = new Thread(() => {
                    var instance = DataListFactory.Instance;
                    dlfs.Add(instance);
                });
                threads.Add(t);
            }
            foreach (var t in threads)
            {
                t.Start();
            }
            foreach (var t in threads)
            {
                t.Join();
            }

            NUnit.Framework.Assert.AreEqual(1, dlfs.Distinct().Count());
            NUnit.Framework.Assert.AreEqual(DataListFactory.Instance, dlfs.Distinct().First());
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_CreateOutputParser()
        {
            NUnit.Framework.Assert.IsNotNull(DataListFactory.CreateOutputParser());
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_CreateInputParser()
        {
            NUnit.Framework.Assert.IsNotNull(DataListFactory.CreateInputParser());
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_GenerateIntellisensePartsFromDataList()
        {
            var filterTo = new IntellisenseFilterOpsTO();
            var dataList = "<DataList><scalar1>s1</scalar1><rs><f1>f1Value</f1><f2>f2Value</f2></rs></DataList>";

            var result = DataListFactory.GenerateIntellisensePartsFromDataList(dataList, filterTo);

            NUnit.Framework.Assert.AreEqual(2, result.Count);
            NUnit.Framework.Assert.AreEqual("", result[0].Description);
            NUnit.Framework.Assert.AreEqual("scalar1", result[0].Name);
            NUnit.Framework.Assert.IsNull(result[0].Children);

            NUnit.Framework.Assert.AreEqual("", result[1].Description);
            NUnit.Framework.Assert.AreEqual("rs", result[1].Name);
            NUnit.Framework.Assert.AreEqual(2, result[1].Children.Count);

            NUnit.Framework.Assert.AreEqual("", result[1].Children[0].Description);
            NUnit.Framework.Assert.AreEqual("f1", result[1].Children[0].Name);
            NUnit.Framework.Assert.IsNull(result[1].Children[0].Children);

            NUnit.Framework.Assert.AreEqual("", result[1].Children[1].Description);
            NUnit.Framework.Assert.AreEqual("f2", result[1].Children[1].Name);
            NUnit.Framework.Assert.IsNull(result[1].Children[1].Children);
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_CreateIntellisensePart()
        {
            var part = DataListFactory.CreateIntellisensePart("name", "desc");
            NUnit.Framework.Assert.IsNotNull(part);
            NUnit.Framework.Assert.AreEqual("name", part.Name);
            NUnit.Framework.Assert.AreEqual("desc", part.Description);
            NUnit.Framework.Assert.IsNull(part.Children);
        }

        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_CreateIntellisensePartWithChildren()
        {
            var children = new List<IDev2DataLanguageIntellisensePart> {
                new Dev2DataLanguageIntellisensePart("child1", "child1 desc", null),
                new Dev2DataLanguageIntellisensePart("child2", "child2 desc", null),
            };
            var part = DataListFactory.CreateIntellisensePart("name", "desc", children);
            NUnit.Framework.Assert.IsNotNull(part);
            NUnit.Framework.Assert.AreEqual("name", part.Name);
            NUnit.Framework.Assert.AreEqual("desc", part.Description);
            NUnit.Framework.Assert.AreEqual(2, part.Children.Count);

            NUnit.Framework.Assert.AreEqual("child1", part.Children[0].Name);
            NUnit.Framework.Assert.AreEqual("child1 desc", part.Children[0].Description);
            NUnit.Framework.Assert.IsNull(part.Children[0].Children);
            NUnit.Framework.Assert.AreEqual("child2", part.Children[1].Name);
            NUnit.Framework.Assert.AreEqual("child2 desc", part.Children[1].Description);
            NUnit.Framework.Assert.IsNull(part.Children[1].Children);
        }


        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_CreateOutputTO()
        {
            var to = DataListFactory.CreateOutputTO("desc");
            NUnit.Framework.Assert.IsNotNull(to);
            NUnit.Framework.Assert.AreEqual("desc", to.OutPutDescription);
            NUnit.Framework.Assert.AreEqual(0, to.OutputStrings.Count);
        }
        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_CreateOutputTO_OutList()
        {
            var to = DataListFactory.CreateOutputTO("desc", new List<string> { "string1", "string2" });
            NUnit.Framework.Assert.IsNotNull(to);
            NUnit.Framework.Assert.AreEqual("desc", to.OutPutDescription);
            NUnit.Framework.Assert.AreEqual(2, to.OutputStrings.Count);
            NUnit.Framework.Assert.AreEqual("string1", to.OutputStrings[0]);
            NUnit.Framework.Assert.AreEqual("string2", to.OutputStrings[1]);
        }
        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_CreateOutputTO_OutString()
        {
            var to = DataListFactory.CreateOutputTO("desc", "string1");
            NUnit.Framework.Assert.IsNotNull(to);
            NUnit.Framework.Assert.AreEqual("desc", to.OutPutDescription);
            NUnit.Framework.Assert.AreEqual(1, to.OutputStrings.Count);
            NUnit.Framework.Assert.AreEqual("string1", to.OutputStrings[0]);
        }


        [Test]
        [Author("Rory McGuire")]
        public void DataListFactory_Static_CreateDev2Column()
        {
            var col = DataListFactory.CreateDev2Column("name", "desc", true, Interfaces.Enums.enDev2ColumnArgumentDirection.Both);
            NUnit.Framework.Assert.AreEqual("name", col.ColumnName);
            NUnit.Framework.Assert.AreEqual("desc", col.ColumnDescription);
            NUnit.Framework.Assert.AreEqual(Interfaces.Enums.enDev2ColumnArgumentDirection.Both, col.ColumnIODirection);
            NUnit.Framework.Assert.AreEqual(true, col.IsEditable);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("DataListFactory_CreateLanguageParser")]
        public void DataListFactory_Static_CreateLanguageParser_IsNew_NewLanguageParser()
        {
            //------------Setup for test--------------------------


            //------------Execute Test---------------------------
            var dev2DataLanguageParser = DataListFactory.CreateLanguageParser();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(dev2DataLanguageParser);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("DataListFactory_CreateDefinition")]
        public void DataListFactory_Static_CreateDefinition_IsNew_PassThrouh()
        {
            //------------Setup for test--------------------------
            var dev2DataLanguageParser = DataListFactory.CreateDefinition_Recordset("a", "b", "c", "", false, "", false, "", false);

            //------------Execute Test---------------------------
            NUnit.Framework.Assert.IsNotNull(dev2DataLanguageParser);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual("a", dev2DataLanguageParser.Name);
            NUnit.Framework.Assert.AreEqual("b", dev2DataLanguageParser.MapsTo);
            NUnit.Framework.Assert.AreEqual("c", dev2DataLanguageParser.Value);
            NUnit.Framework.Assert.AreEqual("", dev2DataLanguageParser.RecordSetName);
            NUnit.Framework.Assert.AreEqual(false, dev2DataLanguageParser.IsEvaluated);
            NUnit.Framework.Assert.AreEqual("", dev2DataLanguageParser.DefaultValue);
            NUnit.Framework.Assert.AreEqual(false, dev2DataLanguageParser.IsRequired);
            NUnit.Framework.Assert.AreEqual("", dev2DataLanguageParser.RawValue);
            NUnit.Framework.Assert.AreEqual(false, dev2DataLanguageParser.EmptyToNull);
            NUnit.Framework.Assert.AreEqual(false, dev2DataLanguageParser.IsJsonArray);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("DataListFactory_CreateDefinition")]
        public void DataListFactory_Static_CreateDefinition_IsNewAndIsArray_PassThrouh()
        {
            //------------Setup for test--------------------------
            var dev2DataLanguageParser = DataListFactory.CreateDefinition_JsonArray("a", "b", "c", false, "", false, "", false, true);

            //------------Execute Test---------------------------
            NUnit.Framework.Assert.IsNotNull(dev2DataLanguageParser);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual("a", dev2DataLanguageParser.Name);
            NUnit.Framework.Assert.AreEqual("b", dev2DataLanguageParser.MapsTo);
            NUnit.Framework.Assert.AreEqual("c", dev2DataLanguageParser.Value);
            NUnit.Framework.Assert.AreEqual("", dev2DataLanguageParser.RecordSetName);
            NUnit.Framework.Assert.AreEqual(false, dev2DataLanguageParser.IsEvaluated);
            NUnit.Framework.Assert.AreEqual("", dev2DataLanguageParser.DefaultValue);
            NUnit.Framework.Assert.AreEqual(false, dev2DataLanguageParser.IsRequired);
            NUnit.Framework.Assert.AreEqual("", dev2DataLanguageParser.RawValue);
            NUnit.Framework.Assert.AreEqual(false, dev2DataLanguageParser.EmptyToNull);
            NUnit.Framework.Assert.AreEqual(true, dev2DataLanguageParser.IsJsonArray);
        }
    }
}
