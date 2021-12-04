
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model.Comment
{
    public class Reply : IRepliable
    {
        [Key]
        public virtual Guid Id { get; set; }

        public virtual IList<Reply> Replies { get; set; } = new List<Reply>();

        [Required(AllowEmptyStrings = false)]
        public virtual string Text { get; set; } = default!;

    }
}
