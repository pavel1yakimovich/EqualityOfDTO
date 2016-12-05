using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparerLibrary
{
    public class CollectionReport<T>
    {
        public IEnumerable<T> Added { get; set; }

        public IEnumerable<T> Removed { get; set; }

        public IDictionary<T, PropertyTree> Updated { get; set; }
    }
}
