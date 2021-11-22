using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model.Comment
{
    public class Reply
    {

        public Guid Id { get; set; }

        public IList<Reply> Replies { get; set; } = new List<Reply>();
        public string? Text { get; set; }

    }
}
