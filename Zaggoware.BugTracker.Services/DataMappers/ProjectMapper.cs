using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Zaggoware.BugTracker.Common;
using Zaggoware.BugTracker.Common.Helpers;

namespace Zaggoware.BugTracker.Services.DataMappers
{
    public static class ProjectMapper
    {
        public static Project Map(this Data.Entities.Project project)
        {
            return Map(project, null);
        }

        public static Project Map(this Data.Entities.Project project, Organization organizationReference)
        {
            if (project == null)
            {
                return null;
            }

            var mapped = new Project
                             {
                                 Id = project.Id,
                                 Name = project.Name,
                                 AppType = EnumHelper.ParseOrDefault(project.AppType, AppType.Web),
                                 Url = project.Url,
                                 CreationDate = project.CreationDate,
                                 ModificationDate = project.ModificationDate,
                                 BugReports = Enumerable.Empty<BugReport>()
                             };
            mapped.Organization = organizationReference ?? project.Organization.Map(mapped);

            return mapped;
        }
    }
}
