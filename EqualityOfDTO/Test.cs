namespace EqualityOfDTO
{
    public class Test
    {
        public int prop1;
        public char prop2;
        public string Prop3 { get; set; }
        public double Prop4 { get; set; }

        public Test (int prop1, char prop2, string prop3, double prop4)
        {
            this.prop1 = prop1;
            this.prop2 = prop2;
            this.Prop3 = prop3;
            this.Prop4 = prop4;
        }
    }
}
