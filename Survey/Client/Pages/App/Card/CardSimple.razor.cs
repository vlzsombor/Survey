using Microsoft.AspNetCore.Components;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Pages.App.Card
{
    public partial class CardSimple : ComponentBase
    {
        [Parameter]
        public CardModel CardModel { get; set; }

        [Parameter]
        public EventCallback<CardModel> OnRatingChanges { get; set; }

        [Parameter]
        public EventCallback<CardModel> OnDelete { get; set; }

        private async void Delete()
        {
            await OnDelete.InvokeAsync(CardModel);
        }

        private async void OnChange(int value)
        {
            CardModel.Rating = value;

            await OnRatingChanges.InvokeAsync(CardModel);
        }
    }
}
