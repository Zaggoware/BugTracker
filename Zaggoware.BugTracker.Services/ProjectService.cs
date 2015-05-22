using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Services
{
    using Zaggoware.BugTracker.Common;
    using Zaggoware.BugTracker.Common.Helpers;
    using Zaggoware.BugTracker.Data;
    using Zaggoware.BugTracker.Services.DataMappers;

    public class ProjectService : IProjectService
    {
        private readonly IDbContext context;

        public ProjectService(IDbContext context)
        {
            this.context = context;
        }

        public Project CreateProject(int organizationId, string name, AppType appType, bool copyUserGroupTemplates)
        {
            var project = this.context.Projects.Create();
            project.OrganizationId = organizationId;
            project.Name = name;
            project.Url = UrlHelper.CreateUrlFromName(name, true);
            project.CreationDate = DateTime.Now;
            project.ModificationDate = DateTime.Now;

            this.context.Projects.Add(project);
            this.context.SaveChanges();

            return project.Map();
        }

        public Project GetProjectById(int id)
        {
            return null;
        }

        public Project GetProjectByUrl(int organizationId, string url)
        {
            return
                this.context.Projects.FirstOrDefault(
                    p =>
                    p.OrganizationId == organizationId && p.Url.Equals(url, StringComparison.InvariantCultureIgnoreCase))
                    .Map();
        }
    }
}
