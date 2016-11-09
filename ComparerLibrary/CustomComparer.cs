using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ComparerLibrary
{
    public static class CustomComparer
    {
        /// <summary>
        /// Method that compares 2 DTO
        /// </summary>
        /// <param name="elem1">1st DTO</param>
        /// <param name="elem2">2nd DTO</param>
        /// <returns>true - equal, false - not equal</returns>
        public static IEnumerable<PropertyInfo> Compare(object elem1, object elem2)
        {
            if (ReferenceEquals(elem1, elem2))
            {
                return new List<PropertyInfo>();
            }

            if (elem1.Equals(elem2))
            {
                return new List<PropertyInfo>();
            }
            else if (elem1.GetType().GetProperties().Length == 0)
            {
                var anon = new { property = elem1 }.GetType().GetProperties()[0];
                return new List<PropertyInfo>() { anon };
            }

            var propertiesNames = elem1.GetType().GetProperties();

            foreach (var property in propertiesNames)
            {
                if (property.GetCustomAttributes(typeof(NotComparableAttribute), false).Length != 0)
                {
                    continue;
                }

                var prop1 = elem1.GetType().GetProperty(property.Name).GetValue(elem1);
                var prop2 = elem2.GetType().GetProperty(property.Name).GetValue(elem2);

                if (CheckAccuracyAttribute(property, prop1, prop2))
                {
                    continue;
                }
                
                if (ReferenceEquals(prop1, prop2))
                {
                    continue;
                }

                if (prop1.Equals(prop2))
                {
                    continue;
                }

                if (!Compare(prop1, prop2).Any())
                {
                    continue;
                }

                return new List<PropertyInfo>() { property };
            }

            return new List<PropertyInfo>();
        }

        private static bool CheckAccuracyAttribute(PropertyInfo property, object obj1, object obj2)
        {
            var attr = (AccuracyAttribute)property.GetCustomAttribute(typeof(AccuracyAttribute), false);
            if (ReferenceEquals(attr, null))
            {
                return false;
            }

            if (obj1 is DateTime)
            {
                var accuracy = attr.Date;
                return CompareDates((DateTime)obj1, (DateTime)obj1, accuracy);
            }

            if (obj1 is double || obj1 is float)
            {
                var accuracy = attr.Epsilon;
                return (double)obj1 - (double)obj2 < accuracy;
            }

            if (obj1 is decimal)
            {
                var accuracy = attr.Epsilon;
                return (decimal)obj1 - (decimal)obj2 < (decimal)accuracy;
            }

            throw new WrongAccuracyUsageException("Wrong type of property.");
        }

        private static bool CompareDates(DateTime date1, DateTime date2, DateItem parameter)
        {
            switch (parameter)
            {
                case DateItem.Year:
                    return date1.Year == date2.Year;
                case DateItem.Month:
                    return date1.Year == date2.Year && date1.Month == date2.Month;
                case DateItem.Day:
                    return date1.Year == date2.Year && date1.Month == date2.Month
                        && date1.Day == date2.Day;
                case DateItem.Hour:
                    return date1.Year == date2.Year && date1.Month == date2.Month
                        && date1.Day == date2.Day && date1.Hour == date2.Hour;
                case DateItem.Minute:
                    return date1.Year == date2.Year && date1.Month == date2.Month
                        && date1.Day == date2.Day && date1.Hour == date2.Hour
                        && date1.Minute == date2.Minute;
                case DateItem.Second:
                    return date1.Year == date2.Year && date1.Month == date2.Month
                         && date1.Day == date2.Day && date1.Hour == date2.Hour
                         && date1.Minute == date2.Minute && date1.Second == date2.Second;
                case DateItem.Millisecond:
                    return date1 == date2;

                default: throw new WrongAccuracyUsageException("Wrong DateItem.");
            }
        }
    }
}
