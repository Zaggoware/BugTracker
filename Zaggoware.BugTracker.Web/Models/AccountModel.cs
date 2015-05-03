using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaggoware.BugTracker.Common;
using Zaggoware.BugTracker.Locale;

namespace Zaggoware.BugTracker.Web.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
        }

        public AccountModel(User user)
            : this()
        {
            if (user == null)
            {
                return;
            }

            this.EmailAddress = user.EmailAddress;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }

        [Display(Name = "EmailAddress", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceName = "EmailAddressRequired", ErrorMessageResourceType = typeof(Errors))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailAddressInvalid", ErrorMessageResourceType = typeof(Errors))]
        public string EmailAddress { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Labels))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Labels))]
        public string LastName { get; set; }

        [Display(Name = "CurrentPassword", ResourceType = typeof(Labels))]
        public string CurrentPassword { get; set; }

        [Display(Name = "NewPassword", ResourceType = typeof(Labels))]
        public string NewPassword { get; set; }

        [Display(Name = "NewPasswordConfirm", ResourceType = typeof(Labels))]
        public string NewPasswordConfirm { get; set; }
    }
}
