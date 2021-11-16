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
        public Guid Id { get; set; }

        [Required]
        public IdentityUser IdentityUser { get; set; } = default!;

        public int RatingNumber { get; set; }

        public RatingModel(int ratingNumber, IdentityUser identityUser)
        {
            RatingNumber = ratingNumber;
            IdentityUser = identityUser;
        }


        public RatingModel(Guid id, IdentityUser identityUser, int ratingNumber)
        {
            Id = id;
            IdentityUser = identityUser;
            RatingNumber = ratingNumber;
        }
        public RatingModel()
        {

        }

    }
}
