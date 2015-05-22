using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
    using Zaggoware.BugTracker.Common;

    public interface IProjectService
    {
        Project CreateProject(int organizationId, string name, AppType appType, bool copyUserGroupTemplates);

        Project GetProjectById(int id);

        Project GetProjectByUrl(int organizationId, string url);
    }
}
