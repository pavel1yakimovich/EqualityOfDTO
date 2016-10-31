namespace ComparerLibrary
{
    using System.Reflection;

    public static class CustomComparer<T>
    {
        /// <summary>
        /// Method that compares 2 DTO
        /// </summary>
        /// <param name="elem1">1st DTO</param>
        /// <param name="elem2">2nd DTO</param>
        /// <returns>true - equal, false - not equal</returns>
        public static bool Compare(T elem1, T elem2)
        {
            var propertiesNames = elem1.GetType().GetProperties();

            foreach (PropertyInfo property in propertiesNames)
            {
                if (!elem1.GetType().GetProperty(property.Name).GetValue(elem1).Equals(
                    elem2.GetType().GetProperty(property.Name).GetValue(elem2)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
