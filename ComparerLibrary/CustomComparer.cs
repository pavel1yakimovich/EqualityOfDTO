using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComparerLibrary
{
    public static class CustomComparer
    {
        /// <summary>
        /// Method that compares 2 DTO
        /// </summary>
        /// <param name="obj1">1st DTO</param>
        /// <param name="obj2">2nd DTO</param>
        /// <returns>List of different PropertyTrees. Empty if DTO are equal</returns>
        public static List<PropertyTree> Compare<T>(T obj1, T obj2)
        {
            var result = new List<PropertyTree>();

            if (ReferenceEquals(obj1, obj2))
            {
                return result;
            }

            if (obj1.Equals(obj2))
            {
                return result;
            }

            var propertiesNames = obj1.GetType().GetProperties();

            foreach (var property in propertiesNames)
            {
                if (property.GetCustomAttributes(typeof(NotComparableAttribute), false).Length != 0)
                {
                    continue;
                }

                var prop1 = property.GetValue(obj1);
                var prop2 = property.GetValue(obj2);

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

                if (property.PropertyType.GetProperties().Length == 0 || property.PropertyType == typeof(string))
                {
                    result.Add(new PropertyTree { Info = property, Tree = new List<PropertyTree>() });
                    continue;
                }
                
                var comparison = Compare(prop1, prop2);

                if (!comparison.Any())
                {
                    continue;
                }
                else
                {
                    result.Add(new PropertyTree { Info = property, Tree = comparison });
                }
            }

            return result;
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
