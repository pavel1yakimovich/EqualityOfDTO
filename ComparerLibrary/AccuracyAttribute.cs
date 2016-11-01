namespace ComparerLibrary
{
    using System;

    public enum DateItem
    {
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second,
        Millisecond
    }

    /// <summary>
    /// Attribute for setting the accuracy of comparison for double numbers and dates
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AccuracyAttribute : Attribute
    {
        /// <summary>
        /// Sets number of digits after coma to compare
        /// </summary>
        /// <param name="digits">number of digits</param>
        public AccuracyAttribute(int digits)
        {
            this.Digits = digits;
        }

        /// <summary>
        /// Sets the smallest parameter for date to compare
        /// </summary>
        /// <param name="date">the smallest parameter</param>
        public AccuracyAttribute(DateItem date)
        {
            this.Date = date;
        }

        public int Digits { get; set; }

        public DateItem Date { get; set; }
    }
}
