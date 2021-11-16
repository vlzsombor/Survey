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
        public string Title { get; set; } = default!;
        [Required]
        public string Text { get; set; } = default!;

        public IList<RatingModel> Rating { get; set; } = new List<RatingModel>();


        public CardModel(int id, string title, string text, IList<RatingModel> rating)
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
