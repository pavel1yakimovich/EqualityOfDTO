using System;
using System.Reflection;
using static System.Math;

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
        public static bool Compare(object elem1, object elem2)
        {
            if (ReferenceEquals(elem1, elem2))
            {
                return true;
            }

            if (elem1.Equals(elem2))
            {
                return true;
            }
            else if (elem1.GetType().GetProperties().Length == 0)
            {
                return false;
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

                if (Compare(prop1, prop2))
                {
                    continue;
                }

                return false;
            }

            return true;
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
                var accuracy = attr.Digits;
                return Round((double)obj1, accuracy) == 
                    Round((double)obj2, accuracy);
            }

            if (obj1 is decimal)
            {
                var accuracy = attr.Digits;
                return decimal.Round((decimal)obj1, accuracy) ==
                       decimal.Round((decimal)obj2, accuracy);
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
