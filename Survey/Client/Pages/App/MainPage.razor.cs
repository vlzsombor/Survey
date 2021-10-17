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

namespace Survey.Client.Pages.App
{
    public partial class MainPage : ComponentBase
    {
        [Inject]
        public ICardRepository cardRepository { get; set; }



        public List<CardModel> CardList { get; set; } = new List<CardModel>();

        private CardModel cardModel = new CardModel();

        private async void Create()
        {
            await cardRepository.CreateCard(cardModel);
            await LoadCard();
        }

        private async Task OnDelete(CardModel card)
        {
            //CardList.Remove(CardList.Where(cardLambda => cardLambda.Id == card.Id).FirstOrDefault());
            await cardRepository.DeleteCard(card);

            await LoadCard();
        }

        protected async override void OnInitialized()
        {
            //await cardRepository.Test();
            await LoadCard();
        }

        public void OnRateChange(CardModel args)
        {
            cardRepository.UpdateCardRating(args);
        }

        private async Task LoadCard()
        {
            CardList = await cardRepository.GetAllCards();
            StateHasChanged();
        }

    }
}
