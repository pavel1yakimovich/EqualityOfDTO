using System.Reflection;

namespace EqualityOfDTO
{
    public static class CustomComparer  <T>
    {
        public static bool Compare(T elem1, T elem2)
        {
            var propertiesNames = elem1.GetType().GetProperties();
            var fieldsNames = elem1.GetType().GetFields();

            foreach (PropertyInfo property in propertiesNames)
            {
                if (!elem1.GetType().GetProperty(property.Name).GetValue(elem1).Equals(
                    elem2.GetType().GetProperty(property.Name).GetValue(elem2)))
                    return false;
            }

            //are fields needed? cause DTO contain only properties
            foreach (FieldInfo field in fieldsNames)
            {
                if (!elem1.GetType().GetField(field.Name).GetValue(elem1).Equals(
                    elem2.GetType().GetField(field.Name).GetValue(elem2)))
                    return false;
            }

            return true;
        }
    }
}
