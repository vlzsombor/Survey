using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared
{
    public class Constants
    {
        public static readonly IList<string> ROLE_NAMES = new List<string>(){
                "Admin",
                "BoardAdmin",
                "BoardFiller"
            };
    }
}
