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
        Organization CreateOrganization(string name);

        Organization CreateOrganization(string name, bool createDefaultUserGroups);

        Organization GetOrganizationById(string id);

        Organization GetOrganizationByName(string name);


    }
}
