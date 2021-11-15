using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public IdentityUser IdentityUser { get; set; } = default!;


        [Required]
        public CardModel CardModel { get; set; } = default!;

        public int RatingNummber { get; set; }



    }
}
