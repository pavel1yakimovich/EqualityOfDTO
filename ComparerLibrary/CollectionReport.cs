using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparerLibrary
{
    public class CollectionReport<T>
    {
        public List<T> Added { get; set; }

        public List<T> Removed { get; set; }

        public Dictionary<T, List<PropertyTree>> Updated { get; set; }

        public bool Empty
        {
            get
            {
                return Added.Count + Removed.Count + Updated.Count == 0;
            }
        }

        public CollectionReport()
        {
            this.Added = new List<T>();
            this.Removed = new List<T>();
            this.Updated = new Dictionary<T, List<PropertyTree>>();
        }
    }
}
