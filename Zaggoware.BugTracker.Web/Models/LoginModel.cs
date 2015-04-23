using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaggoware.BugTracker.Locale;

namespace Zaggoware.BugTracker.Web.Models
{
    public class LoginModel
    {
        [Display(Name = "UserName", ResourceType = typeof (Labels))]
        [Required(ErrorMessageResourceName = "UserNameRequired", ErrorMessageResourceType = typeof (Errors))]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof (Labels))]
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof (Errors))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberUserName", ResourceType = typeof(Labels))]
        public bool RememberUserName { get; set; }
    }
}
