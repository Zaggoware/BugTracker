using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common.Helpers
{
    public static class UrlHelper
    {
        public static string CreateUrlFromName(string name, bool convertToLower)
        {
            var url = name;

            if (convertToLower)
            {
                url = url.ToLower();
            }

            return url.Replace(" ", "-");
        }
    }
}
