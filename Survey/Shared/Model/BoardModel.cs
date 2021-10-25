using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class BoardModel
    {
        [Key]
        public Guid Id { get; set; }
        public virtual IdentityUser? OwnerUser { get; set; }
        // todo kitolto user tipust letrehozni atnevezni
        //public ICollection<User> surveyFillingUser { get; set; }
        public ICollection<CardModel>? Cards { get; set; }


    }
}
