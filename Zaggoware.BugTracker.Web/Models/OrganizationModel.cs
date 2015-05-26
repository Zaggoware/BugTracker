using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaggoware.BugTracker.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using Zaggoware.BugTracker.Common.Tagging;

    public class OrganizationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public TagList Managers { get; set; }
    }
}
