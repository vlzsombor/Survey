using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Survey.Client.Repository.Interfaces;

namespace Survey.Client.Repository
{
    public class CardRepository : ICardRepository
    {


        private HttpClient _httpClient { get; set; }

        private string url = "api/cardapi";


        public CardRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCard(CardModel card, string guid)
        {
            var response = await _httpClient.PostAsJsonAsync<CardModel>(url+ "/"+guid, card);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteCard(CardModel card)
        {
            var response = await _httpClient.DeleteAsync($"{url}/{card.Id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }

        }

        public async Task<List<CardModel>?> GetAllCardsOfUser(string guid)
        {
            var response2 = await _httpClient.GetAsync(Survey.Shared.Constants.URL.API_CARD_URL+ "/" + guid);
            if (!response2.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response2.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<List<CardModel>?>(await response2.Content.ReadAsStringAsync());
        }

        public async Task UpdateCardRating(CardModel card)
        {
            var response = await _httpClient.PutAsJsonAsync<CardModel>(url + "/UpdateCardRating", card);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
        }

    }
}
