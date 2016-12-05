using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComparerLibrary
{
    public struct SimpleTestStruct
    {
        public int IntProp { get; set; }

        public string StringProp { get; set; }
    }

    public class SimpleTestClass
    {
        [IsKey]
        public int IntProp { get; set; }

        public string StringProp { get; set; }

        public SimpleTestClass CompositeProp { get; set; }

        public double DoubleProp { get; set; }

        public SimpleTestStruct StructProp { get; set; }
    }

    public class TestClassWithList
    {
        public List<SimpleTestClass> list { get; set; }

        public TestClassWithList()
        {
            list = new List<SimpleTestClass>();
        }
    }

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

                if (prop1.GetType().GetInterfaces().Where(i => i.Name == typeof(IEnumerable<T>).Name).Any() && !(prop1 is string))
                {
                    CompareCollection(prop1 as IEnumerable<SimpleTestClass>, prop2 as IEnumerable<SimpleTestClass>);
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

        private static CollectionReport<T> CompareCollection<T>(IEnumerable<T> oldCollection, IEnumerable<T> newCollection)
        {
            var keys = typeof(T).GetProperties().Where(p => p.GetCustomAttributes<IsKeyAttribute>().Any());

            var result = new CollectionReport<T>();

            var comparer = new CollectionPropertyComparer<T>(keys);

            foreach (T item in oldCollection)
            {
                var props = item.GetType().GetProperties().Where(p => keys.Contains(p));

                comparer.Equals(item, newCollection.First());

                if (!newCollection.Contains(item, comparer))
                {
                    result.Removed.Add(item);
                }
                else
                {
                    var newItem = newCollection.FirstOrDefault(i => comparer.Equals(i, item));

                    var comparisonResult = Compare(item, newItem);

                    if (comparisonResult.Any())
                    {
                        result.Updated.Add(item, comparisonResult);
                    }
                }
            }

            foreach (T item in newCollection)
            {
                if (!oldCollection.Contains(item, comparer))
                {
                    result.Added.Add(item);
                }
            }

            return result;
        }
    }

    class CollectionPropertyComparer<T> : IEqualityComparer<T>
    {
        private IEnumerable<PropertyInfo> keys;
        
        public CollectionPropertyComparer(IEnumerable<PropertyInfo> keys)
        {
            this.keys = keys;
        }

        public bool Equals(T x, T y)
        {
            try
            {
                foreach (var prop in keys)
                {
                    if (CustomComparer.Compare(prop.GetValue(x), prop.GetValue(y)).Any()) //if int it's false
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetHashCode(T obj)
        {
            int hash = 0;

            foreach (var prop in keys)
            {
                hash += prop.GetValue(obj).GetHashCode();
            }

            return hash;
        }
    }
}
