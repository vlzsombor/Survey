﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey.Client.Unit;
using Survey.Shared.Model;
using Survey.Client.Pages.App.Card;
using Survey.Client.Repository;
using Survey.Client.Repository.Interfaces;
using Survey.Client.Auth;
using Survey.Shared.DTOs;
using Survey.Client.Shared;

namespace Survey.Client.Pages.App.Card
{
    public partial class MainPage : ComponentBase
    {
        public ICardRepository? cardRepository { get; set; }
        [Inject]
        public CardRepository CardRepository { get; set; } = default!;
        [Inject]
        public CardBoardFillerRepository CardBoardFillerRepository { get; set; } = default!;

        public IBoardRepository boardRepository { get; set; } = default!;

        [Inject]
        public BoardRepository BoardRepositoryImp { get; set; } = default!;
        [Inject]
        public BoardFillerRepository BoardBoardFillerRepository { get; set; } = default!;




        [Parameter]
        public string? BoardGuid { get; set; }
        [Parameter]
        public string? AccessGuid { get; set; }

        [CascadingParameter]
        public Error Error { get; set; } = default!;

        public List<CardModel>? CardList { get; set; } = new List<CardModel>();

        private CardModel cardModel = new CardModel();
        public BoardFillerDto BoardFillerDto { get; set; } = new BoardFillerDto();


        protected async override void OnInitialized()
        {
            if (AccessGuid != null)
            {
                BoardFillerDto.AccessGuid = AccessGuid;
                Guid = AccessGuid;
                boardRepository = BoardBoardFillerRepository;
                cardRepository = CardBoardFillerRepository;

            }
            else if (BoardGuid != null)
            {
                Guid = BoardGuid;
                boardRepository = BoardRepositoryImp;
                cardRepository = CardRepository;
            }

            await LoadCard();
        }

        public string? Guid { get; set; }
        private async void Create()
        {
            if (Guid != null && cardRepository != null)
            {
                await cardRepository.CreateCard(cardModel, Guid);
            }

            await LoadCard();
        }

        private async Task OnDelete(CardModel card)
        {
            if (cardRepository != null)
            {
                await cardRepository.DeleteCard(card);
                await LoadCard();
            }

        }



        public void OnRateChange(CardModel args)
        {
            if (cardRepository != null)
            {
                cardRepository.UpdateCardRating(args);
            }
        }

        public async Task LoadCard()
        {
            if (Guid != null)
            {
                try
                {
                    CardList = await boardRepository.GetAllCardsOfUser(Guid);
                }
                catch (ApplicationException ex)
                {
                    Error.ProcessError(ex);
                    return;
                }  
            }
            StateHasChanged();
        }
        public void GenerateAnonymousLink()
        {

        }


    }
}
