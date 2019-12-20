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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dev2.Common;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.Wrappers;
using Dev2.Common.Wrappers;
using Dev2.Communication;
using Dev2.Data;
using Dev2.DataList.Contract;
using Dev2.Runtime;
using NUnit.Framework;



namespace Dev2.Tests.Runtime.Hosting
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class TestCatalogTests
    {
        public static IDirectory DirectoryWrapperInstance()
        {
            return new DirectoryWrapper();
        }
        [SetUp]
        public void CleanupTestDirectory()
        {
            if (Directory.Exists(EnvironmentVariables.TestPath))
            {
                DirectoryWrapperInstance().CleanUp(EnvironmentVariables.TestPath);
            }
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_Constructor")]
        public void TestCatalog_Constructor_TestPathDoesNotExist_ShouldCreateIt()
        {
            //------------Setup for test--------------------------
            //------------Assert Preconditions-------------------
            NUnit.Framework.Assert.IsFalse(Directory.Exists(EnvironmentVariables.TestPath));
            //------------Execute Test---------------------------
            new TestCatalog();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(Directory.Exists(EnvironmentVariables.TestPath));
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_SaveTests")]
        public void TestCatalog_SaveTests_WhenNullList_ShouldDoNothing()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            //------------Execute Test---------------------------
            testCatalog.SaveTests(resourceID, null);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(Directory.Exists(EnvironmentVariables.TestPath + "\\" + resourceID));
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_SaveTests")]
        public void TestCatalog_SaveTests_WhenEmptyList_ShouldDoNothing()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            //------------Execute Test---------------------------
            testCatalog.SaveTests(resourceID, new List<IServiceTestModelTO>());
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(Directory.Exists(EnvironmentVariables.TestPath + "\\" + resourceID));
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_SaveTests")]
        public void TestCatalog_SaveTests_WhenNotEmptyList_ShouldSaveTestsAsFiles()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };
            //------------Execute Test---------------------------
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            //------------Assert Results-------------------------
            var path = EnvironmentVariables.TestPath + "\\" + resourceID;
            NUnit.Framework.Assert.IsTrue(Directory.Exists(path));
            var testFiles = Directory.EnumerateFiles(path).ToList();
            var test1FilePath = path + "\\" + "Test 1.test";
            NUnit.Framework.Assert.AreEqual(test1FilePath, testFiles[0]);
            var test2FilePath = path + "\\" + "Test 2.test";
            NUnit.Framework.Assert.AreEqual(test2FilePath, testFiles[1]);

            var test1String = File.ReadAllText(test1FilePath);
            var serializer = new Dev2JsonSerializer();
            var test1 = serializer.Deserialize<IServiceTestModelTO>(test1String);
            NUnit.Framework.Assert.AreEqual("Test 1", test1.TestName);
            NUnit.Framework.Assert.IsTrue(test1.Enabled);

            var test2String = File.ReadAllText(test2FilePath);
            var test2 = serializer.Deserialize<IServiceTestModelTO>(test2String);
            NUnit.Framework.Assert.AreEqual("Test 2", test2.TestName);
            NUnit.Framework.Assert.IsFalse(test2.Enabled);
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_SaveTest")]
        public void TestCatalog_SaveTests_WhenNoResourceIdList_ShouldSaveTestAsFiles()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();

            var testToSave = new ServiceTestModelTO
            {
                Enabled = false,
                TestName = "Test 1"
            };
            
            //------------Execute Test---------------------------
            testCatalog.SaveTest(resourceID, testToSave);
            //------------Assert Results-------------------------
            var path = EnvironmentVariables.TestPath + "\\" + resourceID;
            NUnit.Framework.Assert.IsTrue(Directory.Exists(path));
            var testFiles = Directory.EnumerateFiles(path).ToList();
            var test1FilePath = path + "\\" + "Test 1.test";
            NUnit.Framework.Assert.AreEqual(test1FilePath, testFiles[0]);
          
            var test1String = File.ReadAllText(test1FilePath);
            var serializer = new Dev2JsonSerializer();
            var test1 = serializer.Deserialize<IServiceTestModelTO>(test1String);
            NUnit.Framework.Assert.AreEqual("Test 1", test1.TestName);
            NUnit.Framework.Assert.IsFalse(test1.Enabled);
            
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_SaveTest")]
        public void TestCatalog_SaveTests_WhenResourceIdList_ShouldSaveTestAsAddToList()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();

            var testToSave = new ServiceTestModelTO
            {
                Enabled = false,
                TestName = "Test 1"
            };

            var testToSave2 = new ServiceTestModelTO
            {
                Enabled = false,
                TestName = "Test 2"
            };

            testCatalog.SaveTest(resourceID, testToSave);

            //------------Execute Test---------------------------
            testCatalog.SaveTest(resourceID, testToSave2);
            //------------Assert Results-------------------------
            var path = EnvironmentVariables.TestPath + "\\" + resourceID;
            NUnit.Framework.Assert.IsTrue(Directory.Exists(path));
            var testFiles = Directory.EnumerateFiles(path).ToList();
            var test2FilePath = path + "\\" + "Test 2.test";
            NUnit.Framework.Assert.AreEqual(test2FilePath, testFiles[1]);
          
            var test2String = File.ReadAllText(test2FilePath);
            var serializer = new Dev2JsonSerializer();
            var test1 = serializer.Deserialize<IServiceTestModelTO>(test2String);
            NUnit.Framework.Assert.AreEqual("Test 2", test1.TestName);
            NUnit.Framework.Assert.IsFalse(test1.Enabled);

            var testInList = testCatalog.FetchTest(resourceID, "Test 2");
            NUnit.Framework.Assert.IsNotNull(testInList);
            NUnit.Framework.Assert.AreEqual("Test 2",testInList.TestName);
            
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_SaveTest")]
        public void TestCatalog_SaveTests_WhenResourceIdListHasTest_ShouldSaveTestUpdateToList()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();

            var testToSave = new ServiceTestModelTO
            {
                Enabled = false,
                TestName = "Test 1"
            };

            var testToSave2 = new ServiceTestModelTO
            {
                Enabled = false,
                TestName = "Test 2"
            };

            testCatalog.SaveTest(resourceID, testToSave);
            testCatalog.SaveTest(resourceID, testToSave2);

            //------------Assert Preconditions-------------------
            var path = EnvironmentVariables.TestPath + "\\" + resourceID;
            NUnit.Framework.Assert.IsTrue(Directory.Exists(path));
            var testFiles = Directory.EnumerateFiles(path).ToList();
            var test1FilePath = path + "\\" + "Test 1.test";
            var test2FilePath = path + "\\" + "Test 2.test";
            NUnit.Framework.Assert.AreEqual(test1FilePath, testFiles[0]);
            NUnit.Framework.Assert.AreEqual(test2FilePath, testFiles[1]);

            var test1String = File.ReadAllText(test2FilePath);
            var serializer = new Dev2JsonSerializer();
            var test2 = serializer.Deserialize<IServiceTestModelTO>(test1String);
            NUnit.Framework.Assert.AreEqual("Test 2", test2.TestName);
            NUnit.Framework.Assert.IsFalse(test2.Enabled);

            var testInList = testCatalog.FetchTest(resourceID, "Test 2");
            NUnit.Framework.Assert.IsNotNull(testInList);
            NUnit.Framework.Assert.AreEqual("Test 2", testInList.TestName);
            //------------Execute Test---------------------------
            var testToSaveUpdate = new ServiceTestModelTO
            {
                Enabled = true,
                TestName = "Test 2"
            };

            testCatalog.SaveTest(resourceID, testToSaveUpdate);
            //------------Assert Results-------------------------
            var test2StringUpdated = File.ReadAllText(test2FilePath);
            var test2Updated = serializer.Deserialize<IServiceTestModelTO>(test2StringUpdated);
            NUnit.Framework.Assert.AreEqual("Test 2", test2Updated.TestName);
            NUnit.Framework.Assert.IsTrue(test2Updated.Enabled);

            var testInListUpdated = testCatalog.FetchTest(resourceID, "Test 2");
            NUnit.Framework.Assert.IsNotNull(testInListUpdated);
            NUnit.Framework.Assert.AreEqual("Test 2", testInListUpdated.TestName);
            NUnit.Framework.Assert.IsTrue(testInListUpdated.Enabled);

        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_DeleteTest")]
        public void TestCatalog_DeleteTest_WhenResourceIdTestName_ShouldDeleteTest()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            //------------Assert Preconditions-------------------
            var path = EnvironmentVariables.TestPath + "\\" + resourceID;
            NUnit.Framework.Assert.IsTrue(Directory.Exists(path));
            var testFiles = Directory.EnumerateFiles(path).ToList();
            var test1FilePath = path + "\\" + "Test 1.test";
            NUnit.Framework.Assert.AreEqual(test1FilePath, testFiles[0]);
            var test2FilePath = path + "\\" + "Test 2.test";
            NUnit.Framework.Assert.AreEqual(test2FilePath, testFiles[1]);
            var modelTO = testCatalog.Tests.Select(pair => pair.Value.Single(to => to.TestName == "Test 2")).Single();
            NUnit.Framework.Assert.IsNotNull(modelTO);
            var test1String = File.ReadAllText(test1FilePath);
            var serializer = new Dev2JsonSerializer();
            var test1 = serializer.Deserialize<IServiceTestModelTO>(test1String);
            NUnit.Framework.Assert.AreEqual("Test 1", test1.TestName);
            NUnit.Framework.Assert.IsTrue(test1.Enabled);

            var test2String = File.ReadAllText(test2FilePath);
            var test2 = serializer.Deserialize<IServiceTestModelTO>(test2String);
            NUnit.Framework.Assert.AreEqual("Test 2", test2.TestName);
            NUnit.Framework.Assert.IsFalse(test2.Enabled);
            //------------Execute Test---------------------------
            testCatalog.DeleteTest(resourceID, "Test 2");
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsTrue(File.Exists(test1FilePath));
            NUnit.Framework.Assert.IsFalse(File.Exists(test2FilePath));
            modelTO = testCatalog.Tests.Select(pair => pair.Value.SingleOrDefault(to => to.TestName == "Test 2")).SingleOrDefault();
            NUnit.Framework.Assert.IsNull(modelTO);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_DeleteTest")]
        public void TestCatalog_DeleteAllTests_WhenResourceIdTestName_ShouldDeleteTestFolder()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            //------------Assert Preconditions-------------------
            var path = EnvironmentVariables.TestPath + "\\" + resourceID;
            NUnit.Framework.Assert.IsTrue(Directory.Exists(path));
            var testFiles = Directory.EnumerateFiles(path).ToList();
            var test1FilePath = path + "\\" + "Test 1.test";
            NUnit.Framework.Assert.AreEqual(test1FilePath, testFiles[0]);
            var test2FilePath = path + "\\" + "Test 2.test";
            NUnit.Framework.Assert.AreEqual(test2FilePath, testFiles[1]);
            var modelTO = testCatalog.Tests.Select(pair => pair.Value.Single(to => to.TestName == "Test 2")).Single();
            NUnit.Framework.Assert.IsNotNull(modelTO);
            var test1String = File.ReadAllText(test1FilePath);
            var serializer = new Dev2JsonSerializer();
            var test1 = serializer.Deserialize<IServiceTestModelTO>(test1String);
            NUnit.Framework.Assert.AreEqual("Test 1", test1.TestName);
            NUnit.Framework.Assert.IsTrue(test1.Enabled);

            var test2String = File.ReadAllText(test2FilePath);
            var test2 = serializer.Deserialize<IServiceTestModelTO>(test2String);
            NUnit.Framework.Assert.AreEqual("Test 2", test2.TestName);
            NUnit.Framework.Assert.IsFalse(test2.Enabled);
            //------------Execute Test---------------------------
            testCatalog.DeleteAllTests(resourceID);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsFalse(File.Exists(test1FilePath));
            NUnit.Framework.Assert.IsFalse(File.Exists(test2FilePath));
            modelTO = testCatalog.Tests.Select(pair => pair.Value.SingleOrDefault(to => to.TestName == "Test 2")).SingleOrDefault();
            NUnit.Framework.Assert.IsNull(modelTO);
            NUnit.Framework.Assert.IsFalse(Directory.Exists(path));
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_Load")]
        public void TestCatalog_Load_WhenTests_ShouldLoadDictionaryWithResourceIdAndTests()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var resourceID2 = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };


            var res2ServiceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 21"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 22"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            testCatalog.SaveTests(resourceID2, res2ServiceTestModelTos);
            //------------Execute Test---------------------------
            testCatalog.Load();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(2, testCatalog.Tests.Count);
            var res1Tests = testCatalog.Tests[resourceID];
            NUnit.Framework.Assert.AreEqual(2, res1Tests.Count);
            NUnit.Framework.Assert.AreEqual("Test 1", res1Tests[0].TestName);
            NUnit.Framework.Assert.AreEqual("Test 2", res1Tests[1].TestName);
            var res2Tests = testCatalog.Tests[resourceID2];
            NUnit.Framework.Assert.AreEqual(2, res2Tests.Count);
            NUnit.Framework.Assert.AreEqual("Test 21", res2Tests[0].TestName);
            NUnit.Framework.Assert.AreEqual("Test 22", res2Tests[1].TestName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_Load")]
        public void TestCatalog_Reload_ShouldLoadDictionaryWithResourceIdAndTests()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var resourceID2 = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };


            var res2ServiceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 21"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 22"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            testCatalog.SaveTests(resourceID2, res2ServiceTestModelTos);
            testCatalog.Load();
            //------------Assert Preconditions-------------------
            NUnit.Framework.Assert.AreEqual(2, testCatalog.Tests.Count);
            var res1Tests = testCatalog.Tests[resourceID];
            NUnit.Framework.Assert.AreEqual(2, res1Tests.Count);
            NUnit.Framework.Assert.AreEqual("Test 1", res1Tests[0].TestName);
            NUnit.Framework.Assert.AreEqual("Test 2", res1Tests[1].TestName);
            var res2Tests = testCatalog.Tests[resourceID2];
            NUnit.Framework.Assert.AreEqual(2, res2Tests.Count);
            NUnit.Framework.Assert.AreEqual("Test 21", res2Tests[0].TestName);
            NUnit.Framework.Assert.AreEqual("Test 22", res2Tests[1].TestName);
            DirectoryWrapperInstance().CleanUp(EnvironmentVariables.TestPath);
            Directory.CreateDirectory(EnvironmentVariables.TestPath);
            //------------Execute Test---------------------------
            testCatalog.ReloadAllTests();
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.AreEqual(0, testCatalog.Tests.Count);

        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_Fetch")]
        public void TestCatalog_Fetch_WhenResourceIdValid_ShouldReturnListOfTestsForResourceId()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var resourceID2 = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };


            var res2ServiceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 21"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 22"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            testCatalog.SaveTests(resourceID2, res2ServiceTestModelTos);
            testCatalog.Load();
            //------------Execute Test---------------------------
            var tests = testCatalog.Fetch(resourceID2);
            //------------Assert Results-------------------------
            var res2Tests = tests;
            NUnit.Framework.Assert.AreEqual(2, res2Tests.Count);
            NUnit.Framework.Assert.AreEqual("Test 21", res2Tests[0].TestName);
            NUnit.Framework.Assert.AreEqual("Test 22", res2Tests[1].TestName);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        [Category("TestCatalog_Fetch")]
        public void TestCatalog_Fetch_WhenPassResult_ShouldReturnListOfTestsForResourceIdWithCorrectPassResult()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var resourceID2 = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1",
                    TestFailing = true,
                    TestInvalid = true,
                    TestPassed = true,
                    TestPending = true
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2",
                    TestFailing = true,
                    TestInvalid = true,
                    TestPassed = true,
                    TestPending = true
                }
            };


            var res2ServiceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 21",
                        TestFailing = true,
                    TestInvalid = true,
                    TestPassed = true,
                    TestPending = true
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 22",
                        TestFailing = true,
                    TestInvalid = true,
                    TestPassed = true,
                    TestPending = true
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            testCatalog.SaveTests(resourceID2, res2ServiceTestModelTos);
            testCatalog.Load();
            //------------Execute Test---------------------------
            var tests = testCatalog.Fetch(resourceID2);
            //------------Assert Results-------------------------
            var res2Tests = tests;
            NUnit.Framework.Assert.AreEqual(2, res2Tests.Count);
            NUnit.Framework.Assert.AreEqual(true, res2Tests[0].TestFailing);
            NUnit.Framework.Assert.AreEqual(true, res2Tests[0].TestInvalid);
            NUnit.Framework.Assert.AreEqual(true, res2Tests[0].TestPending);
            NUnit.Framework.Assert.AreEqual(true, res2Tests[0].TestPassed);
            NUnit.Framework.Assert.AreEqual(true, res2Tests[1].TestPassed);
            NUnit.Framework.Assert.AreEqual(true, res2Tests[1].TestPassed);
            NUnit.Framework.Assert.AreEqual(true, res2Tests[1].TestPassed);
            NUnit.Framework.Assert.AreEqual(true, res2Tests[1].TestPassed);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_Fetch")]
        public void TestCatalog_Fetch_WhenResourceIdNotLoaded_ShouldReturnListOfTestsForResourceId()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var resourceID2 = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };


            var res2ServiceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 21"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 22"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            testCatalog.SaveTests(resourceID2, res2ServiceTestModelTos);
            //------------Execute Test---------------------------
            var tests = testCatalog.Fetch(resourceID2);
            //------------Assert Results-------------------------
            var res2Tests = tests;
            NUnit.Framework.Assert.AreEqual(2, res2Tests.Count);
            NUnit.Framework.Assert.AreEqual("Test 21", res2Tests[0].TestName);
            NUnit.Framework.Assert.AreEqual("Test 22", res2Tests[1].TestName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_Fetch")]
        public void TestCatalog_Fetch_WhenResourceIdNotValid_ShouldReturnListOfTestsForResourceId()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var resourceID2 = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };


            var res2ServiceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 21"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 22"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            testCatalog.SaveTests(resourceID2, res2ServiceTestModelTos);
            //------------Execute Test---------------------------
            var tests = testCatalog.Fetch(Guid.NewGuid());
            //------------Assert Results-------------------------
            var res2Tests = tests;
            NUnit.Framework.Assert.AreEqual(0, res2Tests.Count);
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_FetchTest")]
        public void TestCatalog_FetchTest_WhenResourceIdTestName_ShouldReturnTest()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            //------------Assert Preconditions-------------------           
            //------------Execute Test---------------------------
            var test = testCatalog.FetchTest(resourceID, "Test 2");
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(test);
            NUnit.Framework.Assert.AreEqual("Test 2",test.TestName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_FetchTest")]
        public void TestCatalog_FetchTest_WhenResourceIdInvalidTestName_ShouldReturnNull()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            //------------Assert Preconditions-------------------           
            //------------Execute Test---------------------------
            var test = testCatalog.FetchTest(resourceID, "Test 6");
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNull(test);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_FetchTest")]
        public void TestCatalog_FetchTest_WhenInvalidResourceIdTestName_ShouldReturnNull()
        {
            //------------Setup for test--------------------------
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1"
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2"
                }
            };
            testCatalog.SaveTests(Guid.NewGuid(), serviceTestModelTos);
            //------------Assert Preconditions-------------------           
            //------------Execute Test---------------------------
            var test = testCatalog.FetchTest(resourceID, "Test 6");
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNull(test);
        }



        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_UpdateTestsBasedOnIOChange")]
        public void TestCatalog_UpdateTestsBasedOnIOChange_WhenTestsFound_ShouldUpdateBasedOnChange_Scalars()
        {
            //------------Setup for test--------------------------
            var inputDefs = new List<IDev2Definition> { DataListFactory.CreateDefinition_Recordset("Age", "", "", "", false, "", false, "", false), DataListFactory.CreateDefinition_Recordset("Gender", "", "", "", false, "", false, "", false) };
            var outputDefs = new List<IDev2Definition> { DataListFactory.CreateDefinition_Recordset("MessageForUser", "", "", "", false, "", false, "", false) };
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1",
                    Inputs = new List<IServiceTestInput>
                    {
                        new ServiceTestInputTO
                        {
                            Variable = "Name"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "Age",
                            Value = "20"
                        }
                    },
                    Outputs = new List<IServiceTestOutput>
                    {
                        new ServiceTestOutputTO
                        {
                            Variable = "OutputMessage"
                        },
                        new ServiceTestOutputTO
                        {
                            Variable = "MessageForUser",
                            Value = "This is the message"
                        }
                    }
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2",
                    Inputs = new List<IServiceTestInput>
                    {
                        new ServiceTestInputTO
                        {
                            Variable = "Name"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "Age",
                            Value = "25"
                        }
                    }
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            //------------Execute Test---------------------------
            testCatalog.UpdateTestsBasedOnIOChange(resourceID, inputDefs,outputDefs );
            //------------Assert Results-------------------------
            var updatedTests = testCatalog.Fetch(resourceID);
            var updatedTest1 = updatedTests[0];
            var updatedTest2 = updatedTests[1];

            NUnit.Framework.Assert.AreEqual("Test 1",updatedTest1.TestName);
            NUnit.Framework.Assert.IsTrue(updatedTest1.TestInvalid);
            NUnit.Framework.Assert.IsFalse(updatedTest1.TestFailing);
            NUnit.Framework.Assert.IsFalse(updatedTest1.TestPassed);
            NUnit.Framework.Assert.IsFalse(updatedTest1.TestPending);
            NUnit.Framework.Assert.AreEqual(2,updatedTest1.Inputs.Count);
            NUnit.Framework.Assert.AreEqual("Age",updatedTest1.Inputs[0].Variable);
            NUnit.Framework.Assert.AreEqual("20",updatedTest1.Inputs[0].Value);
            NUnit.Framework.Assert.AreEqual("Gender", updatedTest1.Inputs[1].Variable);
            NUnit.Framework.Assert.AreEqual("", updatedTest1.Inputs[1].Value);
            NUnit.Framework.Assert.IsFalse(updatedTest1.Inputs[1].EmptyIsNull);

            NUnit.Framework.Assert.AreEqual(1, updatedTest1.Outputs.Count);
            NUnit.Framework.Assert.AreEqual("MessageForUser", updatedTest1.Outputs[0].Variable);
            NUnit.Framework.Assert.AreEqual("This is the message", updatedTest1.Outputs[0].Value);
            


            NUnit.Framework.Assert.AreEqual("Test 2",updatedTest2.TestName);
            NUnit.Framework.Assert.IsTrue(updatedTest2.TestInvalid);
            NUnit.Framework.Assert.IsFalse(updatedTest2.TestFailing);
            NUnit.Framework.Assert.IsFalse(updatedTest2.TestPassed);
            NUnit.Framework.Assert.IsFalse(updatedTest2.TestPending);
            NUnit.Framework.Assert.AreEqual(2, updatedTest2.Inputs.Count);
            NUnit.Framework.Assert.AreEqual("Age", updatedTest2.Inputs[0].Variable);
            NUnit.Framework.Assert.AreEqual("25", updatedTest2.Inputs[0].Value);
            NUnit.Framework.Assert.AreEqual("Gender", updatedTest2.Inputs[1].Variable);
            NUnit.Framework.Assert.AreEqual("", updatedTest2.Inputs[1].Value);
            NUnit.Framework.Assert.IsFalse(updatedTest2.Inputs[1].EmptyIsNull);

            NUnit.Framework.Assert.AreEqual(1, updatedTest2.Outputs.Count);
            NUnit.Framework.Assert.AreEqual("MessageForUser", updatedTest2.Outputs[0].Variable);
            NUnit.Framework.Assert.AreEqual("", updatedTest2.Outputs[0].Value);
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_UpdateTestsBasedOnIOChange")]
        public void TestCatalog_UpdateTestsBasedOnIOChange_WhenTestsFound_ShouldUpdateBasedOnChange_RecordSets()
        {
            //------------Setup for test--------------------------
            var inputDefs = new List<IDev2Definition> { DataListFactory.CreateDefinition_Recordset("Age", "", "", "", false, "", false, "", false), DataListFactory.CreateDefinition_Recordset("Gender", "", "", "", false, "", false, "", false), DataListFactory.CreateDefinition_Recordset("f", "", "", "rs", false, "", false, "", false), DataListFactory.CreateDefinition_Recordset("g", "", "", "rs", false, "", false, "", false), DataListFactory.CreateDefinition_Recordset("i", "", "", "rs", false, "", false, "", false), DataListFactory.CreateDefinition_Recordset("set", "", "", "rec", false, "", false, "", false) };
            var outputDefs = new List<IDev2Definition> { DataListFactory.CreateDefinition_Recordset("MessageForUser", "", "", "", false, "", false, "", false), DataListFactory.CreateDefinition_Recordset("out", "", "", "res", false, "", false, "", false) };
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1",
                    Inputs = new List<IServiceTestInput>
                    {
                        new ServiceTestInputTO
                        {
                            Variable = "Name"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "Age",
                            Value = "20"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "rs(1).f",
                            Value = "20"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "rs(1).g",
                            Value = "2"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "rs(1).h",
                            Value = "1"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "rs(2).f",
                            Value = "20"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "rs(2).g",
                            Value = "2"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "rs(2).h",
                            Value = "1"
                        }
                    },
                    Outputs = new List<IServiceTestOutput>
                    {
                        new ServiceTestOutputTO
                        {
                            Variable = "OutputMessage"
                        },
                        new ServiceTestOutputTO
                        {
                            Variable = "MessageForUser",
                            Value = "This is the message"
                        }
                    }
                },
                new ServiceTestModelTO
                {
                    Enabled = false,
                    TestName = "Test 2",
                    Inputs = new List<IServiceTestInput>
                    {
                        new ServiceTestInputTO
                        {
                            Variable = "Name"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "Age",
                            Value = "25"
                        }
                    }
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            //------------Execute Test---------------------------
            testCatalog.UpdateTestsBasedOnIOChange(resourceID, inputDefs,outputDefs );
            //------------Assert Results-------------------------
            var updatedTests = testCatalog.Fetch(resourceID);
            var updatedTest1 = updatedTests[0];
            var updatedTest2 = updatedTests[1];

            NUnit.Framework.Assert.AreEqual("Test 1",updatedTest1.TestName);
            NUnit.Framework.Assert.IsTrue(updatedTest1.TestInvalid);
            NUnit.Framework.Assert.IsFalse(updatedTest1.TestFailing);
            NUnit.Framework.Assert.IsFalse(updatedTest1.TestPassed);
            NUnit.Framework.Assert.IsFalse(updatedTest1.TestPending);
            NUnit.Framework.Assert.AreEqual(9,updatedTest1.Inputs.Count);
            NUnit.Framework.Assert.AreEqual("Age",updatedTest1.Inputs[0].Variable);
            NUnit.Framework.Assert.AreEqual("20",updatedTest1.Inputs[0].Value);
            NUnit.Framework.Assert.AreEqual("Gender", updatedTest1.Inputs[1].Variable);
            NUnit.Framework.Assert.AreEqual("", updatedTest1.Inputs[1].Value);
            NUnit.Framework.Assert.IsFalse(updatedTest1.Inputs[1].EmptyIsNull);

            NUnit.Framework.Assert.AreEqual(2, updatedTest1.Outputs.Count);
            NUnit.Framework.Assert.AreEqual("MessageForUser", updatedTest1.Outputs[0].Variable);
            NUnit.Framework.Assert.AreEqual("This is the message", updatedTest1.Outputs[0].Value);
            


            NUnit.Framework.Assert.AreEqual("Test 2",updatedTest2.TestName);
            NUnit.Framework.Assert.IsTrue(updatedTest2.TestInvalid);
            NUnit.Framework.Assert.IsFalse(updatedTest2.TestFailing);
            NUnit.Framework.Assert.IsFalse(updatedTest2.TestPassed);
            NUnit.Framework.Assert.IsFalse(updatedTest2.TestPending);
            NUnit.Framework.Assert.AreEqual(6, updatedTest2.Inputs.Count);
            NUnit.Framework.Assert.AreEqual("Age", updatedTest2.Inputs[0].Variable);
            NUnit.Framework.Assert.AreEqual("25", updatedTest2.Inputs[0].Value);
            NUnit.Framework.Assert.AreEqual("Gender", updatedTest2.Inputs[1].Variable);
            NUnit.Framework.Assert.AreEqual("", updatedTest2.Inputs[1].Value);
            NUnit.Framework.Assert.IsFalse(updatedTest2.Inputs[1].EmptyIsNull);

            NUnit.Framework.Assert.AreEqual(2, updatedTest2.Outputs.Count);
            NUnit.Framework.Assert.AreEqual("MessageForUser", updatedTest2.Outputs[0].Variable);
            NUnit.Framework.Assert.AreEqual("", updatedTest2.Outputs[0].Value);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("TestCatalog_UpdateTestsBasedOnIOChange")]
        public void TestCatalog_UpdateTestsBasedOnIOChange_WhenTestsFound_ShouldUpdateStepsToInvalid()
        {
            //------------Setup for test--------------------------
            var inputDefs = new List<IDev2Definition> { DataListFactory.CreateDefinition_Recordset("Age", "", "", "", false, "", false, "", false), DataListFactory.CreateDefinition_Recordset("Gender", "", "", "", false, "", false, "", false) };
            var outputDefs = new List<IDev2Definition> { DataListFactory.CreateDefinition_Recordset("MessageForUser", "", "", "", false, "", false, "", false) };
            var testCatalog = new TestCatalog();
            var resourceID = Guid.NewGuid();
            var serviceTestModelTos = new List<IServiceTestModelTO>
            {
                new ServiceTestModelTO
                {
                    Enabled = true,
                    TestName = "Test 1",
                    TestSteps = new List<IServiceTestStep>
                    {                     
                        new ServiceTestStepTO
                        {
                            StepOutputs = new System.Collections.ObjectModel.ObservableCollection<IServiceTestOutput>
                            {
                                new ServiceTestOutputTO
                                {
                                    Variable = "OutputMessage"
                                },
                                new ServiceTestOutputTO
                                {
                                    Variable = "MessageForUser",
                                    Value = "This is the message"
                                }
                            },
                            Result  = new TestRunResult
                            {
                                RunTestResult = RunResult.TestPassed
                            }
                        },
                        new ServiceTestStepTO
                        {
                            Result  = new TestRunResult
                            {
                                RunTestResult = RunResult.TestFailed
                            },
                            Children = new System.Collections.ObjectModel.ObservableCollection<IServiceTestStep>
                            {
                                new ServiceTestStepTO
                                {
                                    StepOutputs = new System.Collections.ObjectModel.ObservableCollection<IServiceTestOutput>
                                    {
                                        new ServiceTestOutputTO
                                        {
                                            Variable = "OutputMessage"
                                        },
                                        new ServiceTestOutputTO
                                        {
                                            Variable = "MessageForUser",
                                            Value = "This is the message"
                                        }
                                    },
                                    Result  = new TestRunResult
                                    {
                                        RunTestResult = RunResult.TestPassed
                                    }
                                }
                            }
                        }
                    },
                    Inputs = new List<IServiceTestInput>
                    {
                        new ServiceTestInputTO
                        {
                            Variable = "Name"
                        },
                        new ServiceTestInputTO
                        {
                            Variable = "Age",
                            Value = "20"
                        }
                    },
                    Outputs = new List<IServiceTestOutput>
                    {
                        new ServiceTestOutputTO
                        {
                            Variable = "OutputMessage"
                        },
                        new ServiceTestOutputTO
                        {
                            Variable = "MessageForUser",
                            Value = "This is the message"
                        }
                    }
                }
            };
            testCatalog.SaveTests(resourceID, serviceTestModelTos);
            //------------Execute Test---------------------------
            testCatalog.UpdateTestsBasedOnIOChange(resourceID, inputDefs, outputDefs);
            //------------Assert Results-------------------------
            var updatedTests = testCatalog.Fetch(resourceID);
            var updatedTest1 = updatedTests[0];

            NUnit.Framework.Assert.AreEqual("Test 1", updatedTest1.TestName);
            NUnit.Framework.Assert.IsTrue(updatedTest1.TestInvalid);
            NUnit.Framework.Assert.AreEqual(RunResult.TestInvalid, updatedTest1.TestSteps[0].Result.RunTestResult);
            NUnit.Framework.Assert.AreEqual(RunResult.TestInvalid, updatedTest1.TestSteps[0].StepOutputs[0].Result.RunTestResult);
            NUnit.Framework.Assert.AreEqual(RunResult.TestInvalid, updatedTest1.TestSteps[1].Result.RunTestResult);
            NUnit.Framework.Assert.AreEqual(RunResult.TestInvalid, updatedTest1.TestSteps[1].Children[0].StepOutputs[0].Result.RunTestResult);
        }
    }
}
