using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common.Helpers
{
    public static class ObjectHelper
    {
        public static Dictionary<string, object> ToDictionary(this object anonymousObject)
        {
            var type = anonymousObject.GetType();
            var properties = type.GetProperties();
            var pairs =
                properties.Select(p => new KeyValuePair<string, object>(p.Name, p.GetValue(anonymousObject, null)));

            return pairs.ToDictionary(p => p.Key, p => p.Value);
        }
    }
}
