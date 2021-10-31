using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class BoardFiller
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BoardFillerGuid { get; set; }

        [Required]
        public string PinCode { get; set; } = default!;
        // todo which side should the class be? is it important?
        [Required]
        public BoardModel BoardModel { get; set; } = default!;
        


        public BoardFiller(Guid accessGuid, string pinCode, BoardModel boardModel)
        {
            BoardFillerGuid = accessGuid;
            PinCode = pinCode;
            BoardModel = boardModel;
        }
        
        private BoardFiller(Guid id)
        {
            Id = id;
        }
    }


}
