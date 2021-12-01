using Survey.Shared.Model.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class CardModel : IRepliable
    {
        public virtual Guid Id { get; set; }
        [Required]
        public virtual string Title { get; set; } = default!;
        [Required]
        public virtual string Text { get; set; } = default!;

        public virtual IList<RatingModel> Rating { get; set; } = new List<RatingModel>();

        public virtual IList<Reply> Replies { get; set; } = new List<Reply>();


    }
}
