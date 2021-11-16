using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class BoardFiller : IdentityUser
    {

        [Required]
        public BoardModel BoardModel { get; set; } = default!;

        public BoardFiller(string userName, BoardModel boardModel): base(userName)
        {
            this.BoardModel = boardModel;
        }

        
        public BoardFiller(string userName): base(userName)
        {
        }


    }


}
