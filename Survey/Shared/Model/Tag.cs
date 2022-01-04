using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Survey.Shared.Model
{
    public class Tag
    {
        [Key]
        public Guid Guid{ get; set; }

        public virtual string TagText { get; set; }

        public virtual IList<CardModel>? CardModel { get; set; }

    }
}
