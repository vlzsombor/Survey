using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class CardModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }


        public int Rating { get; set; }

        public CardModel(int id, string title, string text)
        {
            Title = title;
            Text = text;
            Id = id;
        }        
        public CardModel(int id, string title, string text, int rating)
        {
            Title = title;
            Text = text;
            Id = id;
            Rating = rating;
        }

        public CardModel()
        {

        }
    }
}
