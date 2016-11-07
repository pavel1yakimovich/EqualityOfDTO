using System;
using ComparerLibrary;

namespace ComparerLibraryTests
{
    public class TestClassWrongAttrs
    {
        [Accuracy(0.002)]
        public string Prop { get; set; }

        public TestClassWrongAttrs(string str)
        {
            this.Prop = str;
        }
    }
}
