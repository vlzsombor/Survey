using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class UserInfo
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Your password must be between {2} and {1} characters.", MinimumLength = 6)]
        [Display]
        public string Password { get; set; }
    }
}
