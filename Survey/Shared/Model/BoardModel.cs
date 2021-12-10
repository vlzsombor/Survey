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
        public virtual Guid Id { get; set; }
        public virtual IdentityUser? OwnerUser { get; set; }
        // todo kitolto user tipust letrehozni atnevezni
        //public ICollection<User> surveyFillingUser { get; set; }
        public virtual IList<CardModel> Cards { get; set; } = new List<CardModel>(); 

        //// todo from the other side it should be required, so if BoardFiller exist, then the table must exist
        //// but not the other way around
        //public ICollection<BoardFiller>? BoardFillers { get; set; }



    }
}
