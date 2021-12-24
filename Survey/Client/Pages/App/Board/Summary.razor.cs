using Microsoft.AspNetCore.Components;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Survey.Client.Pages.App.Board
{
    public partial class Summary : ComponentBase
    {

        public List<(CardModel, Dictionary<int, int>)> ToShowList = new List<(CardModel, Dictionary<int, int>)>();

        public void test2()
        {
            foreach (var cardItem in CardList ?? Enumerable.Empty<CardRatingDto>())
            {
                var b2 = cardItem.CardModel.Rating
                    .GroupBy(x => x.RatingNumber)
                    .ToDictionary(grp => grp.Key,
                     grp => grp.Count());


                for(int i = 0; i<7; i++)
                {
                        b2.TryAdd(i, 0);
                }


                Console.WriteLine(b2);

            }
        }
    }
}
