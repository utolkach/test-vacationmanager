using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VacationManager.ApplicationServices
{
    public static class Helpers
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> entities, string propertyName, bool? desc)
        {
            if (!entities.Any() || string.IsNullOrEmpty(propertyName))
                return entities;

            var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                throw new Exception($"Object has no property {propertyName}");
            }

            if (desc.HasValue && desc.Value)
            {
                return entities.OrderByDescending(e => propertyInfo.GetValue(e, null));

            }
            return entities.OrderBy(e => propertyInfo.GetValue(e, null));
        }
    }
}