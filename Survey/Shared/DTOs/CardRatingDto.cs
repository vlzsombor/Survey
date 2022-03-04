using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.DTOs
{
    public class CardRatingDto
    {
        public int? RatingValue { get; set; }

        public CardModel CardModel { get; set; }
        public bool? Smiley { get; set; }

        public CardRatingDto(int? ratingValue, bool? smileyVote, CardModel cardModel)
        {
            RatingValue = ratingValue;
            CardModel = cardModel;
            Smiley = smileyVote;
        }

    }
}
