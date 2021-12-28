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
        public virtual IList<CardModel> Cards { get; set; } = new List<CardModel>();
        public virtual DateTime ExpDate { get; set; }
    }
}
