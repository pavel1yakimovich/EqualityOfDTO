namespace ComparerLibraryTests
{
    using System;
    using ComparerLibrary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CustomComparerTests
    {
        [TestMethod]
        public void CompareTwoSameObjects_True()
        {
            var prop5 = new TestClass(2, 'e', "1234", 1.65, null, null);
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, prop5, null);
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, prop5, null);

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoObjectsWithSameProperties()
        {
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null, null), new TestStruct(2, 'w', "1234", 1.65, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null, null), new TestStruct(2, 'w', "1234", 1.65, null));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoObjectsWithDifferentFields()
        {
            var var1 = new TestClass(1, 'w', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null, null), new TestStruct(2, 'w', "1234", 1.65, null));
            var var2 = new TestClass(2, 'q', "qwerty", 2.3, new TestClass(3, 'r', "1234", 1.65, null, null), new TestStruct(2, 'w', "1234", 1.65, null));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoDifferentObjects()
        {
            var var1 = new TestClass(1, 'w', "qwerty", 2.3, new TestClass(2, 'w', "1234", 2.65, null, null), null);
            var var2 = new TestClass(2, 'q', "qwerty", 2.3, new TestClass(3, 'r', "1234", 1.65, null, null), null);

            Assert.IsFalse(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoObjectsThreeObjectsInDepth()
        {
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, new TestClass(3, 't', "1234", 1.65, null, new TestStruct(2, 'w', "1234", 1.65, null)), null), new TestStruct(2, 'w', "1234", 1.65, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, new TestClass(5, 'u', "1234", 1.65, null, new TestStruct(2, 'w', "1234", 1.65, null)), null), new TestStruct(2, 'w', "1234", 1.65, null));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoObjectsWithDifferentPropertiesWithAttributes()
        {
            var prop5 = new TestClass(2, 'e', "1234", 1.65, null, new TestStruct(2, 'w', "1234", 1.65, null));
            var var1 = new TestClass(1, 'q', "qwe", 2.3, prop5, new TestStruct(2, 'w', "2345", 1.65, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, prop5, new TestStruct(2, 'w', "1234", 1.65, null));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyhAttributesDifferentDates()
        {
            var var1 = new TestClassWithDate(2, 'e', new DateTime(2016, 01, 01, 9, 13, 14, 16), 1.65);
            var var2 = new TestClassWithDate(2, 'e', new DateTime(2016, 01, 01, 11, 11, 12, 15), 1.65);

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }
    }
}
