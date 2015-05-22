using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaggoware.BugTracker.Common;

namespace Zaggoware.BugTracker.Services
{
    using Zaggoware.BugTracker.Data;
    using Zaggoware.BugTracker.Services.DataMappers;

    public class OrganizationService : IOrganizationService
    {
        private readonly IDbContext context;

        public OrganizationService(IDbContext context)
        {
            this.context = context;
        }

        public Organization CreateOrganization(string name, bool copyUserGroupTemplates)
        {
            var organization = this.context.Organizations.Create();
            organization.Name = name;
            organization.CreationDate = DateTime.Now;
            organization.ModificationDate = DateTime.Now;

            if (copyUserGroupTemplates)
            {
                // TODO: Copy user groups when copyUserGroupTemplates == true
            }

            this.context.Organizations.Add(organization);
            this.context.SaveChanges();

            return organization.Map();
        }

        public Organization GetOrganizationById(string id)
        {
            return this.context.Organizations.Find(id).Map();
        }

        public Organization GetOrganizationByName(string name)
        {
            return
                this.context.Organizations.FirstOrDefault(
                    o => o.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Map();
        }

        public Organization GetOrganizationByUrl(string url)
        {
            return
                this.context.Organizations.FirstOrDefault(
                    o => o.Url.Equals(url, StringComparison.InvariantCultureIgnoreCase)).Map();
        }

        public IEnumerable<Organization> GetOrganizations()
        {
            return this.context.Organizations.ToList().Select(o => o.Map());
        }
    }
}
