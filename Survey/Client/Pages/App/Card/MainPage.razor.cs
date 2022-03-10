using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey.Client.Util;
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
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;

namespace Survey.Client.Pages.App.Card
{
    public partial class MainPage : ComponentBase, IDisposable
    {
        [Parameter, EditorRequired]
        public ICardRepository? cardRepository { get; set; }
        [Parameter, EditorRequired]
        public IBoardRepository boardRepository { get; set; } = default!;
        
        [Parameter]
        public DateTime? ExpTime { get; set; }

        [Parameter, EditorRequired]
        public string Guid { get; set; } = default!;

        [CascadingParameter]
        public Error Error { get; set; } = default!;

        [Parameter, EditorRequired]
        public List<CardRatingDto>? CardList { get; set; } = new List<CardRatingDto>();

        private CardModel cardModel = new CardModel();

        [Parameter]
        public BoardFillerDto BoardFillerDto { get; set; } = new BoardFillerDto();

        [Inject]
        public NavigationManager navigationManager { get; set; } = default!;


        [Inject] 
        private IAuthorizationService AuthorizationService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private ILoginService LoginService { get; set; }

        [Parameter, EditorRequired]
        public EventCallback<Task> SendMessage { get; set; }

        public void NavigateCommand(string tagText)
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);


            //navigationManager.NavigateTo(navigationManager.GetUriWithQueryParameters(
            //    new Dictionary<string, object?>() {
            //        { "tag", new []{tagText,"fasdf" } }
            //    }));

            var query = new Dictionary<string, string> {
                { "tag", tagText }
            };

            navigationManager.NavigateTo(QueryHelpers
                .AddQueryString(navigationManager.Uri,query));


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



        protected override void OnInitialized()
        {

            navigationManager.LocationChanged += NavigationManager_LocationChanged;

            base.OnInitialized();
        }
        private async Task<IEnumerable<string>> SearchMethod(string searchText)
        {
            var a = CardList?
                .SelectMany(x=>
                x.CardModel.Tags.Select(x=>x.TagText)
                .Where(x=>x.ToLower().Contains(searchText.ToLower()))).Distinct();
            return a ?? Enumerable.Empty<string>();
        }

        protected async override Task OnParametersSetAsync()
        {
            await LoadCard();
        }
        private async void NavigationManager_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
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

        public async Task UpdateCardRating((int? value, bool? vote, CardModel cm) args)
        {
            if (cardRepository != null)
            {
                await cardRepository.UpdateCardRating(args.value, args.vote, args.cm);
                await LoadCard();
            }
        }

        public async Task AddReply((string comment, IRepliable cm) args)
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
                    CardList = await boardRepository.GetAllCardsOfUser(Guid);

                    if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("tag", out var param))
                    {
                        CardList = CardList?.Where(z => z.CardModel.Tags.Any(x => param.Any(y => y == x.TagText))).ToList();
                    }


                }
                catch (ApplicationException ex)
                {
                    //Error.ProcessError(ex);
                    throw ex;
                }
            }
            StateHasChanged();
        }

        public async void Dispose()
        {
            navigationManager.LocationChanged -= NavigationManager_LocationChanged;


            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.IsInRole("BoardFiller"))
            {
                await LoginService.Logout();
            }

        }
    }
}
