using NUnit.Framework;

namespace Dev2.Tests.Activities.ActivityComparerTests.CaseConvert
{
    [TestFixture]
    [SetUpFixture]
    public class CaseConvertToEqualityTests
    {
        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_EmptyTos_IsEqual()
        {
            //---------------Set up test pack-------------------
            var multiAssign = new CaseConvertTO();
            var multiAssign1 = new CaseConvertTO();
            //---------------Assert Precondition----------------
            Assert.IsNotNull(multiAssign);
            //---------------Execute Test ----------------------
            var @equals = multiAssign.Equals(multiAssign1);
            //---------------Test Result -----------------------
            Assert.IsTrue(@equals);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_Values_Object_Is_IsEqual()
        {
            //---------------Set up test pack-------------------
            var multiAssign = new CaseConvertTO("A", "A", "",  1);
            var multiAssign1 = new CaseConvertTO("A", "A", "",  1);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(multiAssign);
            //---------------Execute Test ----------------------
            var @equals = multiAssign.Equals(multiAssign1);
            //---------------Test Result -----------------------
            Assert.IsTrue(@equals);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_DiffentFieldNames_Is_NotIsEqual()
        {
            //---------------Set up test pack-------------------
            var multiAssign = new CaseConvertTO("A", "A", "",  1);
            var multiAssign1 = new CaseConvertTO("a", "A", "",  1);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(multiAssign);
            //---------------Execute Test ----------------------
            var @equals = multiAssign.Equals(multiAssign1);
            //---------------Test Result -----------------------
            Assert.IsFalse(@equals);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_DiffentFromType_Is_NotIsEqual()
        {
            //---------------Set up test pack-------------------
            var multiAssign = new CaseConvertTO("A", "A", "",  1);
            var multiAssign1 = new CaseConvertTO("A", "v", "",  1);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(multiAssign);
            //---------------Execute Test ----------------------
            var @equals = multiAssign.Equals(multiAssign1);
            //---------------Test Result -----------------------
            Assert.IsFalse(@equals);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_DiffentindexNumber_Is_NotIsEqual()
        {
            //---------------Set up test pack-------------------
            var multiAssign = new CaseConvertTO("A", "A", "",  1);
            var multiAssign1 = new CaseConvertTO("A", "A", "",  2);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(multiAssign);
            //---------------Execute Test ----------------------
            var @equals = multiAssign.Equals(multiAssign1);
            //---------------Test Result -----------------------
            Assert.IsFalse(@equals);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_DiffentExpression_Is_NotIsEqual()
        {
            //---------------Set up test pack-------------------
            var multiAssign = new CaseConvertTO("A", "A",  "", 1);
            var multiAssign1 = new CaseConvertTO("A", "A",  "a", 2);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(multiAssign);
            //---------------Execute Test ----------------------
            var @equals = multiAssign.Equals(multiAssign1);
            //---------------Test Result -----------------------
            Assert.IsFalse(@equals);
        }

        [Test]
        [Author("Nkosinathi Sangweni")]
        public void Equals_DiffentToType_Is_NotIsEqual()
        {
            //---------------Set up test pack-------------------
            var multiAssign = new CaseConvertTO("A", "A", "a",  1);
            var multiAssign1 = new CaseConvertTO("A", "A", "",  2);
            //---------------Assert Precondition----------------
            Assert.IsNotNull(multiAssign);
            //---------------Execute Test ----------------------
            var @equals = multiAssign.Equals(multiAssign1);
            //---------------Test Result -----------------------
            Assert.IsFalse(@equals);
        }
    }
}