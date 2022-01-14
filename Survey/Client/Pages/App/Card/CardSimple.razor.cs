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
        public EventCallback<(int?, bool?, CardModel)> OnChange { get; set; }

        [Parameter]
        public EventCallback<CardModel> OnDelete { get; set; }

        [Parameter]
        public EventCallback<(string, IRepliable)> AddReply { get; set; }

        [Parameter]
        public EventCallback<string> NavigateCommand { get; set; }
        public string myStyle { get; set; } = "color:grey";

        public bool smileyOnOff { get; set; } = false;


        public Reply ReplyModel { get; set; } = new Reply();

        public void OnValidSubmit()
        {
            if (!string.IsNullOrEmpty(ReplyModel.Text))
            {
                AddReply.InvokeAsync((ReplyModel.Text, CardModel.CardModel));

            }
        }

        protected override void OnInitialized()
        {

            smileyOnOff = !CardModel.Smiley ?? false;


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


        public void OnAddReply(IRepliable repliable)
        {
            if (!string.IsNullOrEmpty(ReplyModel.Text))
            {
                AddReply.InvokeAsync((ReplyModel.Text, repliable));
            }
        }


        public void test()
        {
            smileyOnOff = !smileyOnOff;
            if (smileyOnOff)
            {
                myStyle = "color:yellow";
            }
            else
            {
                myStyle = "color:grey";
            }

            OnChange.InvokeAsync((null, smileyOnOff, CardModel.CardModel));
        }

    }
}
