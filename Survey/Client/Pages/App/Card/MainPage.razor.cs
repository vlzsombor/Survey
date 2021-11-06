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

namespace Survey.Client.Pages.App.Card
{
    public partial class MainPage : ComponentBase
    {
        [Inject]
        public ICardRepository cardRepository { get; set; } = default!;

        public IBoardRepository boardRepository { get; set; } = default!; 

        [Inject]
        public BoardRepository BoardRepositoryImp { get; set; } = default!;
        [Inject]
        public BoardFillerRepository BoardBoardFillerRepository { get; set; } = default!;              


        [Inject]
        public ILoginService loginService { get; set; }

        [Inject]
        public IAccountsRepository accountsRepository { get; set; }

        public List<CardModel>? CardList { get; set; } = new List<CardModel>();

        private CardModel cardModel = new CardModel();

        private BoardFillerDto _boardFillerDto = new BoardFillerDto();
        private UserToken? userToken;

        [Parameter]
        public string? BoardGuid { get; set; }
        [Parameter]
        public string? AccessGuid { get; set; }


        public string? Guid { get; set; }
        private async void Create()
        {
            if (Guid != null)
            {
                await cardRepository.CreateCard(cardModel, Guid);
            }

            await LoadCard();
        }

        private async Task OnDelete(CardModel card)
        {
            await cardRepository.DeleteCard(card);
            await LoadCard();
        }

        protected async override void OnInitialized()
        {
            if (AccessGuid != null)
            {
                _boardFillerDto.AccessGuid = AccessGuid;
                Guid = AccessGuid;
                boardRepository = BoardBoardFillerRepository;

            }
            else if (BoardGuid != null)
            {
                Guid = BoardGuid;
                boardRepository = BoardRepositoryImp;
            }

            await LoadCard();
            Console.WriteLine(BoardGuid);
        }

        public void OnRateChange(CardModel args)
        {
            cardRepository.UpdateCardRating(args);
        }

        private async Task LoadCard()
        {
            if (Guid != null)
            {
                CardList = await boardRepository.GetAllCardsOfUser(Guid);
            }
            StateHasChanged();
        }
        public void GenerateAnonymousLink()
        {

        }

        private async Task LoginUser()
        {
            userToken = await accountsRepository.Login(_boardFillerDto);
            if (userToken?.Token != null)
            {
                await loginService.Login(userToken.Token);
            }
        }

    }
}
