using System;
using System.Linq;
using ComparerLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ComparerLibrary.CustomComparer;

namespace ComparerLibraryTests
{
    [TestClass]
    public class CustomComparerTests
    {
        [TestMethod]
        public void CompareTwoSameObjects_True()
        {
            var prop5 = new TestClass(2, 'e', "1234", 1.65, null, null);
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, prop5, null);
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, prop5, null);

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithSameProperties()
        {
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null, null), new TestStruct(2, 'w', "1234", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null, null), new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithDifferentFields()
        {
            var var1 = new TestClass(1, 'w', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null, null), new TestStruct(2, 'w', "1234", 1.65m, null));
            var var2 = new TestClass(2, 'q', "qwerty", 2.3, new TestClass(3, 'r', "1234", 1.65, null, null), new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoDifferentObjects()
        {
            var var1 = new TestClass(1, 'w', "qwerty", 2.3, new TestClass(2, 'w', "1234", 2.65, null, null), null);
            var var2 = new TestClass(2, 'q', "qwerty", 2.3, new TestClass(3, 'r', "1234", 1.65, null, null), null);

            Assert.IsTrue(Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsThreeObjectsInDepth()
        {
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, new TestClass(3, 't', "1234", 1.65, null, new TestStruct(2, 'w', "1234", 1.65m, null)), null), new TestStruct(2, 'w', "1234", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, new TestClass(5, 'u', "1234", 1.65, null, new TestStruct(2, 'w', "1234", 1.65m, null)), null), new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithDifferentPropertiesWithAttributes()
        {
            var prop5 = new TestClass(2, 'e', "1234", 1.65, null, new TestStruct(2, 'w', "1234", 1.65m, null));
            var var1 = new TestClass(1, 'q', "qwe", 2.3, prop5, new TestStruct(2, 'w', "2345", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, prop5, new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyhAttributesDifferentDates()
        {
            var var1 = new TestClassWithDate(2, 'e', new DateTime(2016, 01, 01, 9, 13, 14, 16), 1.65);
            var var2 = new TestClassWithDate(2, 'e', new DateTime(2016, 01, 01, 11, 11, 12, 15), 1.65);

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyhAttributesSameNumbers()
        {
            var var1 = new TestClass(1, 'q', "qwe", 2.3675, null, new TestStruct(2, 'w', "2345", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwe", 2.3683, null, new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyhAttributesDifferentNumbers()
        {
            var var1 = new TestClass(1, 'q', "qwe", 2.3775, null, new TestStruct(2, 'w', "2345", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwe", 2.3683, null, new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyhAttributesVeryDifferentNumbers()
        {
            var var1 = new TestClass(1, 'q', "qwe", 2.39, null, new TestStruct(2, 'w', "2345", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwe", 2.3683, null, new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyhAttributesVeryDifferentDecimalNumbers()
        {
            var var1 = new TestClass(1, 'q', "qwe", 2.39, null, new TestStruct(2, 'w', "2345", 1.6544m, null));
            var var2 = new TestClass(1, 'q', "qwe", 2.3683, null, new TestStruct(2, 'w', "1234", 1.612m, null));

            Assert.IsTrue(Compare(var1, var2).Any());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongAccuracyUsageException))]
        public void CompareTwoObjectsWithWrongAccuracyAttrs()
        {
            var var1 = new TestClassWrongAttrs("qwerty");
            var var2 = new TestClassWrongAttrs("qwerty");

            Compare(var1, var2);
        }
    }
}
