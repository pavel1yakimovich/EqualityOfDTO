namespace EqualityOfDTO
{
    using System;
    using ConparerLibrary;

    public class Program
    {
        public static void Main(string[] args)
        {
            Test var1 = new Test(1, 'q', "qwerty", 2.3);
            Test var2 = new Test(1, 'q', "qwerty", 2.3);

            Console.WriteLine(CustomComparer<Test>.Compare(var1, var2));

            Console.Read();
        }
    }
}
