using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Common
{
    using System.ComponentModel.DataAnnotations;

    using Zaggoware.BugTracker.Locale;

    public enum AppType
    {
        [Display(Name = "WebApp", ResourceType = typeof(Labels))]
        Web,

        [Display(Name = "DesktopApp", ResourceType = typeof(Labels))]
        Desktop,

        [Display(Name = "PhoneApp", ResourceType = typeof(Labels))]
        Phone,
    }
}
