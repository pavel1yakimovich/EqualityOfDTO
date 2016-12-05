using System.Collections.Generic;
using System.Reflection;

namespace ComparerLibrary
{
    /// <summary>
    /// Class for describing different properties
    /// </summary>
    public class PropertyTree
    {
        /// <summary>
        /// PropertyInfo of differenet properties
        /// </summary>
        public PropertyInfo Info { get; set; }

        /// <summary>
        /// List of different sub-properties
        /// </summary>
        public List<PropertyTree> Tree { get; set; }

        //public CollectionReport<T> CollectionInfo { get; set; }
    }
}
