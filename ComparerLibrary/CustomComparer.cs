namespace ComparerLibrary
{
    using System;
    using System.Reflection;

    public static class CustomComparer<T>
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

            var propertiesNames = elem1.GetType().GetProperties();

            foreach (PropertyInfo property in propertiesNames)
            {
                if (CheckAttribute(property))
                {
                    continue;
                }

                var prop1 = elem1.GetType().GetProperty(property.Name).GetValue(elem1);
                var prop2 = elem2.GetType().GetProperty(property.Name).GetValue(elem2);

                if (ReferenceEquals(prop1, prop2))
                {
                    continue;
                }

                if (prop1.GetType().IsClass && prop1.GetType() != string.Empty.GetType())
                {
                    if (!Compare(prop1, prop2))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!prop1.Equals(prop2))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool CheckAttribute (PropertyInfo property)
        {
            var attrs = property.CustomAttributes;

            foreach (var item in attrs)
            {
                if (item.AttributeType.FullName == "ComparerLibrary.NotComparableAttribute") // bydlokod. cahnge!!!
                {
                    return true;
                }
            }

            return false;
        }
    }
}
