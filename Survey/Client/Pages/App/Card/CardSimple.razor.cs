using Microsoft.AspNetCore.Components;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using Survey.Shared.Model.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Pages.App.Card
{
    public partial class CardSimple : ComponentBase
    {
        public CardSimple()
        {
        }

        [Parameter]
        public CardRatingDto CardModel { get; set; } = default!;

        [Parameter]
        public EventCallback<(int, CardModel)> OnChange { get; set; }

        [Parameter]
        public EventCallback<CardModel> OnDelete { get; set; }

        [Parameter]
        public EventCallback<(string, IRepliable)> AddReply { get; set; }

        public string Reply { get; set; }


        //[Parameter]
        public int Value
        {
            get { return 3; }
            set { Value = value; }
        }

        protected override void OnInitialized()
        {

            if (CardModel.CardModel.Rating.Any())
            {
                var a = CardModel.CardModel.Rating.Average(x => x.RatingNumber);
            }
            base.OnInitialized();
        }

        private async void Delete()
        {
            await OnDelete.InvokeAsync(CardModel.CardModel);
        }


        public void alma(IRepliable repliable)
        {
            AddReply.InvokeAsync((Reply, repliable));
        }

    }
}
