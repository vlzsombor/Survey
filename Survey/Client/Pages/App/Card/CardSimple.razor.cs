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
        public CardSimple()
        {
        }

        [Parameter]
        public CardModel CardModel { get; set; } = default!;

        [Parameter]
        public EventCallback<(int, CardModel)> OnChange { get; set; }

        [Parameter]
        public EventCallback<CardModel> OnDelete { get; set; }

        private async void Delete()
        {
            await OnDelete.InvokeAsync(CardModel);
        }


    }
}
