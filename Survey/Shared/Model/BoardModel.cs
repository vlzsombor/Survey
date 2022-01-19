using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    [DataContract]

    public class BoardModel
    {
        [Key]
        [DataMember]

        public virtual Guid Id { get; set; }
        [DataMember]
        public virtual string? Name { get; set; }
        public virtual IdentityUser? OwnerUser { get; set; }
        [DataMember]
        public virtual IList<CardModel> Cards { get; set; } = new List<CardModel>();
        [DataMember]
        public virtual DateTime ExpDate { get; set; }
    }
}
