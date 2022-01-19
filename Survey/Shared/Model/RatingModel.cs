using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    [DataContract]
    public class RatingModel
    {
        [Key]
        [DataMember]
        public virtual Guid Id { get; set; }
        [DataMember]

        public virtual int? RatingNumber { get; set; }
        [DataMember]

        public virtual bool? SmileyVote { get; set; }

        public virtual IdentityUser? IdentityUser { get; set; } = default!;


    }
}
