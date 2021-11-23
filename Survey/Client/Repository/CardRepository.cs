using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Survey.Client.Repository.Interfaces;
using Survey.Shared;
using Survey.Shared.DTOs;
using Survey.Shared.Model.Comment;

namespace Survey.Client.Repository
{
    public class CardRepository : ICardRepository
    {
        protected string _baseUrl = Constants.BACKEND_URL.API_CARD_URL;
        private HttpClient _httpClient { get; set; }

        public CardRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCard(CardModel card, string guid)
        {
            var response = await _httpClient.PostAsJsonAsync<CardModel>(_baseUrl + "/" + guid, card);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteCard(CardModel card)
        {
            var response = await _httpClient.DeleteAsync(_baseUrl + "/" + card.Id);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }

        }

        public async Task UpdateCardRating(int value, CardModel cardmodel)
        {
            var response = await _httpClient.PutAsJsonAsync<CardRatingDto>(_baseUrl + "/" + Constants.BACKEND_URL.UPDATE_CARD_RATING, new CardRatingDto(value, cardmodel));
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task AddReply(string comment, IRepliable cardmodel)
        {
            var a = cardmodel as CardModel;

            if (a != null)
            {
                var response = await _httpClient.PutAsJsonAsync<CardModel>(_baseUrl + "/" + Constants.BACKEND_URL.ADD_REPLY, a);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(await response.Content.ReadAsStringAsync());
                }

            }

            var a2 = cardmodel as Reply;

            if (a2 != null)
            {
                var response = await _httpClient.PutAsJsonAsync<Reply>(_baseUrl + "/" + Constants.BACKEND_URL.ADD_REPLY_TO_REPLY, a2);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(await response.Content.ReadAsStringAsync());
                }

            }

        }
        public async Task AddReplyReply(string comment, IRepliable cardmodel)
        {
            var a = cardmodel as Reply;

            if (a != null)
            {
                var response = await _httpClient.PutAsJsonAsync<Reply>(_baseUrl + "/" + Constants.BACKEND_URL.ADD_REPLY_TO_REPLY, a);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(await response.Content.ReadAsStringAsync());
                }

            }

        }
    }
}
