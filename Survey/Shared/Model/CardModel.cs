﻿using Survey.Shared.Model.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class CardModel : IRepliable
    {
        [Key]
        public virtual Guid Id { get; set; }
        [Required]
        public virtual string Title { get; set; } = default!;
        [Required]
        public virtual string Text { get; set; } = default!;

        public virtual IList<RatingModel> Rating { get; set; } = new List<RatingModel>();

        public virtual IList<Reply> Replies { get; set; } = new List<Reply>();
        [NotMapped]
        public virtual IList<string> Tags { get; set; } = new List<string>();


    }
}
