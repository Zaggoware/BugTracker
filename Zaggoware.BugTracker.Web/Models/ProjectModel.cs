using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Web.Models
{
    using System.Web.Mvc;

    using Zaggoware.BugTracker.Common;
    using Zaggoware.BugTracker.Common.Helpers;

    public class ProjectModel
    {
        public string Name { get; set; }

        public AppType AppType { get; set; }

        public bool CopyGroupTemplates { get; set; }

        public IEnumerable<SelectListItem> AppTypes
        {
            get
            {
                var enumValues = Enum.GetValues(typeof(AppType));

                return from AppType enumValue in enumValues
                       select
                           new SelectListItem
                               {
                                   Text = enumValue.GetDisplayName(),
                                   Value = enumValue.ToString(),
                                   Selected = this.AppType == enumValue
                               };
            }
        }
    }
}
