using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Zaggoware.BugTracker.Common;

namespace Zaggoware.BugTracker.Services.DataMappers
{
    public static class OrganizationMapper
    {
        public static Organization Map(this Data.Entities.Organization organization)
        {
            return Map(organization, null);
        }

        public static Organization Map(this Data.Entities.Organization organization, Project projectReference)
        {
            if (organization == null)
            {
                return null;
            }

            var mapped = new Organization { Id = organization.Id, Name = organization.Name };
            mapped.Projects =
                organization.Projects.Select(
                    p => projectReference != null && projectReference.Id == p.Id ? projectReference : p.Map(mapped));

            return mapped;
        }
    }
}
