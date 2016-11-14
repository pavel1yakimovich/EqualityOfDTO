namespace ComparerLibraryTests
{
    public struct SimpleTestStruct
    {
        public int IntProp { get; set; }

        public string StringProp { get; set; }
    }

    public class SimpleTestClass
    {
        public int IntProp { get; set; }

        public string StringProp { get; set; }

        public SimpleTestClass CompositeProp { get; set; }
        
        public double DoubleProp { get; set; }

        public SimpleTestStruct StructProp { get; set; }
    }
}
