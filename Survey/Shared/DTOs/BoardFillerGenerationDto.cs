using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.DTOs
{
    public class BoardFillerGenerationDto
    {
        public List<string> Emails { get; set; }
        public string BoardGuid { get; set; }

        // todo is this class needed or anonymous should be used???
        public BoardFillerGenerationDto(string boardGuid, List<string> emails)
        {
            Emails = emails;
            BoardGuid = boardGuid;
        }        
        
    }
}
