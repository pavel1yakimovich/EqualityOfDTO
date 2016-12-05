using System;
using System.Collections.Generic;
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
        public void CompareTwoObjectsWithAccuracyAttributesDifferentDates()
        {
            var var1 = new TestClassWithDate(2, 'e', new DateTime(2016, 01, 01, 9, 13, 14, 16), 1.65);
            var var2 = new TestClassWithDate(2, 'e', new DateTime(2016, 01, 01, 11, 11, 12, 15), 1.65);

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyAttributesSameNumbers()
        {
            var var1 = new TestClass(1, 'q', "qwe", 2.3675, null, new TestStruct(2, 'w', "2345", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwe", 2.3683, null, new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(!Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyAttributesDifferentNumbers()
        {
            var var1 = new TestClass(1, 'q', "qwe", 2.3775, null, new TestStruct(2, 'w', "2345", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwe", 2.3683, null, new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyAttributesVeryDifferentNumbers()
        {
            var var1 = new TestClass(1, 'q', "qwe", 2.39, null, new TestStruct(2, 'w', "2345", 1.65m, null));
            var var2 = new TestClass(1, 'q', "qwe", 2.3683, null, new TestStruct(2, 'w', "1234", 1.65m, null));

            Assert.IsTrue(Compare(var1, var2).Any());
        }

        [TestMethod]
        public void CompareTwoObjectsWithAccuracyAttributesVeryDifferentDecimalNumbers()
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

        [TestMethod]
        public void CompareTwoDifferentSimpleObjectsDepthTwo()
        {
            var var1 = new SimpleTestClass() { IntProp = 1, StringProp = "q", CompositeProp = new SimpleTestClass() { IntProp = 1, StringProp = "q" } };
            var var2 = new SimpleTestClass() { IntProp = 2, StringProp = "q", CompositeProp = new SimpleTestClass() { IntProp = 2, StringProp = "w" } };

            var expected = new List<PropertyTree>() { new PropertyTree() { Info = var1.GetType().GetProperty("IntProp"), Tree = new List<PropertyTree>() }, new PropertyTree() { Info = var1.GetType().GetProperty("CompositeProp"), Tree = new List<PropertyTree>() { new PropertyTree() { Info = var1.GetType().GetProperty("IntProp"), Tree = new List<PropertyTree>() }, new PropertyTree() { Info = var1.GetType().GetProperty("StringProp"), Tree = new List<PropertyTree>() } } } };

            var actualData = Compare(var1, var2);

            var flag = expected[0].Info.Equals(actualData[0].Info) && !actualData[0].Tree.Any()
                && expected[1].Info.Equals(actualData[1].Info) && expected[1].Tree[0].Info.Equals(actualData[1].Tree[0].Info)
                && !actualData[1].Tree[0].Tree.Any() && expected[1].Tree[1].Info.Equals(actualData[1].Tree[1].Info)
                && !actualData[1].Tree[1].Tree.Any();

            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void CompareTwoDifferentSimpleObjectsDepthThree()
        {
            var var1 = new SimpleTestClass()
            {
                IntProp = 1,
                StringProp = "q",
                CompositeProp = new SimpleTestClass()
                {
                    IntProp = 1,
                    StringProp = "q",
                    CompositeProp = new SimpleTestClass()
                    {
                        IntProp = 1,
                        StringProp = "q"
                    }
                },
                DoubleProp = 4.3,
                StructProp = new SimpleTestStruct()
                {
                    IntProp = 1,
                    StringProp = "q"
                }
            };
            var var2 = new SimpleTestClass()
            {
                IntProp = 2,
                StringProp = "q",
                CompositeProp = new SimpleTestClass()
                {
                    IntProp = 2,
                    StringProp = "w",
                    CompositeProp = new SimpleTestClass()
                    {
                        IntProp = 1,
                        StringProp = "w"
                    }
                },
                DoubleProp = 2.3,
                StructProp = new SimpleTestStruct()
                {
                    IntProp = 1,
                    StringProp = "w"
                }
            };
            
            var actualData = Compare(var1, var2);
        }

        [TestMethod]
        public void CompareClassWithList()
        {
            var var1 = new TestClassWithList();
            var var2 = new TestClassWithList();

            var1.list.Add(new SimpleTestClass() { IntProp = 1, StringProp = "qwerty", DoubleProp = 2.2 });
            var1.list.Add(new SimpleTestClass() { IntProp = 3, StringProp = "qwerty", DoubleProp = 2.2 });
            var2.list.Add(new SimpleTestClass() { IntProp = 1, StringProp = "qq", DoubleProp = 2.2 });
            var2.list.Add(new SimpleTestClass() { IntProp = 2, StringProp = "qq", DoubleProp = 2.2 });

            Compare(var1, var2);
        }
    }
}
