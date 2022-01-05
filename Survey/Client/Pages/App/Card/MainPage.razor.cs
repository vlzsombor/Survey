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
using Microsoft.AspNetCore.WebUtilities;

namespace Survey.Client.Pages.App.Card
{
    public partial class MainPage : ComponentBase
    {
        [Parameter, EditorRequired]
        public ICardRepository? cardRepository { get; set; }
        [Parameter, EditorRequired]
        public IBoardRepository boardRepository { get; set; } = default!;


        [Parameter, EditorRequired]
        public string Guid { get; set; } = default!;

        [CascadingParameter]
        public Error Error { get; set; } = default!;

        [Parameter, EditorRequired]
        public List<CardRatingDto>? CardList { get; set; } = new List<CardRatingDto>();
        public List<CardRatingDto>? CardListToShow { get; set; } = new List<CardRatingDto>();

        private CardModel cardModel = new CardModel();

        [Parameter]
        public BoardFillerDto BoardFillerDto { get; set; } = new BoardFillerDto();

        [Inject]
        public NavigationManager navigationManager { get; set; } = default!;

        [Parameter, EditorRequired]
        public EventCallback<Task> SendMessage { get; set; }
        public string? SearchTag { get; set; }



        public async void NavigateCommand(string tagText)
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

            if (!QueryHelpers.ParseQuery(uri.Query).TryGetValue("tag", out var param) || param != tagText)
            {
                var query = new Dictionary<string, string> { { "tag", tagText } };
                navigationManager.NavigateTo(navigationManager.GetUriWithQueryParameter("tag",tagText));
            }
            SearchTag = tagText;
            await LoadCard();

        }
        private async void Create()
        {
            if (Guid != null && cardRepository != null)
            {

                cardModel.Tags = Tags.Select(x => new Tag() { TagText= x }).ToList();

                await cardRepository.CreateCard(cardModel, Guid);
                await SendMessage.InvokeAsync();

            }
            await LoadCard();
        }

        private async Task OnDelete(CardModel card)
        {
            if (cardRepository != null)
            {
                await cardRepository.DeleteCard(card);
                await LoadCard();
                await SendMessage.InvokeAsync();
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
                await SendMessage.InvokeAsync();
            }
        }

        public async Task LoadCard()
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

            if (Guid != null)
            {
                try
                {
                    if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("tag", out var param))
                    {
                        CardListToShow = CardList.Where(x => x.CardModel.Tags.All(x => x.TagText == param)).ToList();
                    }
                    else
                    {
                        SearchTag = null;
                        CardList = await boardRepository.GetAllCardsOfUser(Guid);
                        CardListToShow = CardList;
                    }
                }
                catch (ApplicationException ex)
                {
                    Error.ProcessError(ex);
                    return;
                }
            }
            StateHasChanged();
        }


    }
}
