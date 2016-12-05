using System;

namespace ComparerLibrary
{
    /// <summary>
    /// Attribute for setting keys in collection
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IsKeyAttribute : Attribute
    {
        public bool Flag { get; set; }

        public IsKeyAttribute() : this(true)
        {
        }

        public IsKeyAttribute(bool flag)
        {
            this.Flag = flag;
        }
    }
}
