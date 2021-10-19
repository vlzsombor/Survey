using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.DTOs
{
    public class UserToken
    {
        public string? Token { get; set; }
        public string? Error { get; set; }
        public JArray? ErrorList { get; set; }
        public IDictionary<string, object>? ErrorDict { get; set; }

    }
}
