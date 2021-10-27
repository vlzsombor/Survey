using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey.Client.Unit;
using Survey.Shared.Model;
using Survey.Client.Pages.App.Card;
using Survey.Client.Repository;
using Survey.Client.Repository.Interfaces;

namespace Survey.Client.Pages.App.Card
{
    public partial class MainPage : ComponentBase
    {
        [Inject]
        public ICardRepository cardRepository { get; set; } = default!;

        public List<CardModel>? CardList { get; set; } = new List<CardModel>();

        private CardModel cardModel = new CardModel();

        [Parameter]
        public string boardGuid { get; set; } = default!;

        private async void Create()
        {
            await cardRepository.CreateCard(cardModel, boardGuid);
            await LoadCard();
        }

        private async Task OnDelete(CardModel card)
        {
            await cardRepository.DeleteCard(card);
            await LoadCard();
        }

        protected async override void OnInitialized()
        {
            await LoadCard();
            Console.WriteLine(boardGuid);

        }

        public void OnRateChange(CardModel args)
        {
            cardRepository.UpdateCardRating(args);
        }

        private async Task LoadCard()
        {
            CardList = await cardRepository.GetAllCardsOfUser(boardGuid);
            StateHasChanged();
        }

    }
}
