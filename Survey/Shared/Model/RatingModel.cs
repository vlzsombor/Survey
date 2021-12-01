using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class RatingModel
    {
        [Key]
        public virtual Guid Id { get; set; }

        public virtual IdentityUser? IdentityUser { get; set; } = default!;

        public virtual int? RatingNumber { get; set; }






    }
}
