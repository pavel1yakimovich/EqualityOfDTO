namespace ComparerLibraryTests
{
    using ComparerLibrary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CustomComparerTests
    {
        [TestMethod]
        public void CompareTwoSameObijects_True()
        {
            var prop5 = new TestClass(2, 'e', "1234", 1.65, null);
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, prop5);
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, prop5);

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoObijectsWithSameProperties()
        {
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoObijectsWithDifferentFields()
        {
            var var1 = new TestClass(1, 'w', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null));
            var var2 = new TestClass(2, 'q', "qwerty", 2.3, new TestClass(3, 'r', "1234", 1.65, null));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoDifferentObjects()
        {
            var var1 = new TestClass(1, 'w', "qwerty", 2.3, new TestClass(2, 'w', "1234", 2.65, null));
            var var2 = new TestClass(2, 'q', "qwerty", 2.3, new TestClass(3, 'r', "1234", 1.65, null));

            Assert.IsFalse(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoObijectsThreeObjectsInDepth()
        {
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, new TestClass(3, 't', "1234", 1.65, null)));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, new TestClass(5, 'u', "1234", 1.65, null)));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }
    }
}
