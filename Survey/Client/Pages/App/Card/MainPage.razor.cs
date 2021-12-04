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
using Survey.Client.Auth;
using Survey.Shared.DTOs;
using Survey.Client.Shared;
using Survey.Shared.Model.Comment;
using Microsoft.AspNetCore.SignalR.Client;

namespace Survey.Client.Pages.App.Card
{
    public partial class MainPage : ComponentBase, IAsyncDisposable
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

        public List<CardRatingDto>? CardList { get; set; } = new List<CardRatingDto>();

        private CardModel cardModel = new CardModel();
        public BoardFillerDto BoardFillerDto { get; set; } = new BoardFillerDto();

        private HubConnection hubConnection = default!;
        [Inject]
        public NavigationManager navigationManager { get; set; }

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


            hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
                .Build();


            hubConnection.On("ReceiveCm", async () =>
            {
                await LoadCard();
                StateHasChanged();
            });

            await hubConnection.StartAsync();

            await LoadCard();
        }


        Task SendMessage() => hubConnection.SendAsync("SendCardModel");
        public bool IsConnected =>
            hubConnection.State == HubConnectionState.Connected;


        public string? Guid { get; set; }
        private async void Create()
        {
            if (Guid != null && cardRepository != null)
            {
                if (IsConnected) await SendMessage();


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


        public async void UpdateCardRating((int value, CardModel cm) args)
        {
            if (cardRepository != null)
            {
                await cardRepository.UpdateCardRating(args.value, args.cm);
                await LoadCard();

            }

        }

        public async void AddReply((string comment, IRepliable cm) args)
        {
            if (cardRepository != null)
            {
                args.cm.Replies.Add(new Survey.Shared.Model.Comment.Reply() { Text = args.comment });

                await cardRepository.AddReply(args.comment, args.cm);
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
        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }

    }
}
