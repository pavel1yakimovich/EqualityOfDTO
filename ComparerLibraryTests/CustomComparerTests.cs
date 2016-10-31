namespace ComparerLibraryTests
{
    using ComparerLibrary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CustomComparerTests
    {
        [TestMethod]
        public void CompareTwoSameTestClassObijects_True()
        {
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'e', "1234", 1.65, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'e', "1234", 1.65, null));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }

        [TestMethod]
        public void CompareTwoTestClassObijectsWithSameProperties()
        {
            var var1 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'e', "1234", 1.65, null));
            var var2 = new TestClass(1, 'q', "qwerty", 2.3, new TestClass(2, 'w', "1234", 1.65, null));

            Assert.IsTrue(CustomComparer<TestClass>.Compare(var1, var2));
        }
    }
}
