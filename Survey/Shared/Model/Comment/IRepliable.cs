using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model.Comment
{
    public interface IRepliable
    {
        public IList<Reply> Replies { get; set; }
    }
}
