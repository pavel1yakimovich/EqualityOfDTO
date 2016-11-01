namespace ComparerLibrary
{
    using System;

    /// <summary>
    /// Attribute that forbids comparison of property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotComparableAttribute : Attribute
    {
    }
}
