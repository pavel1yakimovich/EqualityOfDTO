using System;
using ComparerLibrary;

namespace ComparerLibraryTests
{
    public class TestClassWrongAttrs
    {
        [Accuracy(2)]
        public string Prop { get; set; }

        public TestClassWrongAttrs(string str)
        {
            this.Prop = str;
        }
    }
}
