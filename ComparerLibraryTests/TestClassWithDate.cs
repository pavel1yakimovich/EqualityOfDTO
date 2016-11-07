using System;
using ComparerLibrary;

namespace ComparerLibraryTests
{
    public struct TestStructWithDate
    {
        private int prop1;
        private char prop2;

        [Accuracy(DateItem.Hour)]
        public DateTime Prop3 { get; set; }

        [Accuracy(2)]
        public double Prop4 { get; set; }

        public TestStructWithDate(int prop1, char prop2, DateTime prop3, double prop4)
        {
            this.prop1 = prop1;
            this.prop2 = prop2;
            this.Prop3 = prop3;
            this.Prop4 = prop4;
        }
    }

    public class TestClassWithDate
    {
        private int prop1;
        private char prop2;

        [Accuracy(DateItem.Day)]
        public DateTime Prop3 { get; set; }

        [Accuracy(3)]
        public double Prop4 { get; set; }

        public TestClassWithDate(int prop1, char prop2, DateTime prop3, double prop4)
        {
            this.prop1 = prop1;
            this.prop2 = prop2;
            this.Prop3 = prop3;
            this.Prop4 = prop4;
        }
    }
}
