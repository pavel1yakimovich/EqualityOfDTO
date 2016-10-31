using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparerLibrary
{
    /// <summary>
    /// Attribute that forbids comparison of property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotComparableAttribute : Attribute
    {
    }
}
