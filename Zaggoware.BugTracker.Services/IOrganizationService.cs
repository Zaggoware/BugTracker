using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaggoware.BugTracker.Common;

namespace Zaggoware.BugTracker.Services
{
    public interface IOrganizationService
    {
        Organization CreateOrganization(string name, bool copyUserGroupTemplates);

        Organization GetOrganizationById(string id);

        Organization GetOrganizationByName(string name);

        Organization GetOrganizationByUrl(string url);

        IEnumerable<Organization> GetOrganizations();
    }
}
