using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class BoardFiller : IdentityUser
    {

        [Required]
        public virtual BoardModel BoardModel { get; set; } = default!;
    }


}
