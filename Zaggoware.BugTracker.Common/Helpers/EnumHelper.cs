using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common.Helpers
{
    using System.ComponentModel.DataAnnotations;

    public static class EnumHelper
    {
        public static string GetDisplayName<T>(this T enumValue) where T : struct
        {
            var fieldInfo = typeof(T).GetField(enumValue.ToString());
            var descriptionAttributes =
                fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null || !descriptionAttributes.Any())
            {
                return enumValue.ToString();
            }

            var attr = descriptionAttributes.First();

            if (attr.ResourceType != null)
            {
                var property = attr.ResourceType.GetProperty(attr.Name);

                if (property != null)
                {
                    var propertyValue = property.GetValue(attr) as string;

                    if (!string.IsNullOrEmpty(propertyValue))
                    {
                        return propertyValue;
                    }
                }
            }

            return !string.IsNullOrEmpty(attr.Name) ? attr.Name : enumValue.ToString();
        }

        public static T ParseOrDefault<T>(string value) where T : struct
        {
            return ParseOrDefault(value, default(T));
        }

        public static T ParseOrDefault<T>(string value, T defaultValue) where T : struct
        {
            T parsed;

            return Enum.TryParse(value, true, out parsed) ? parsed : defaultValue;
        }
    }
}
