using System;

namespace ComparerLibrary
{
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
        public double Epsilon { get; set; }

        public DateItem Date { get; set; }
        
        /// <summary>
        /// Sets tolerance to compare numbers
        /// </summary>
        /// <param name="epsilon">tolerance</param>
        public AccuracyAttribute(double epsilon)
        {
            this.Epsilon = epsilon;
        }

        /// <summary>
        /// Sets the smallest parameter for date to compare
        /// </summary>
        /// <param name="date">the smallest parameter</param>
        public AccuracyAttribute(DateItem date)
        {
            this.Date = date;
        }
    }
}
