namespace ComparerLibraryTests
{
    public class TestClass
    {
        private int prop1;
        private char prop2;
        
        public TestClass(int prop1, char prop2, string prop3, double prop4, TestClass prop5)
        {
            this.prop1 = prop1;
            this.prop2 = prop2;
            this.Prop3 = prop3;
            this.Prop4 = prop4;
            this.CompositeProp = prop5;
        }

        public string Prop3 { get; set; }

        public double Prop4 { get; set; }

        public TestClass CompositeProp { get; set; }
    }
}
