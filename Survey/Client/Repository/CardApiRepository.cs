using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Survey.Client.Helpers;
using Survey.Client.Repository.Interfaces;

namespace Survey.Client.Repository
{
    public class CardApiRepository : ICardRepository
    {


        private readonly IHttpService httpService;
        private HttpClient _httpClient { get; set; }

        private string url = "api/cardapi";


        public CardApiRepository(IHttpService httpService, HttpClient httpClient)
        {
            this.httpService = httpService;
            _httpClient = httpClient;
        }

        public async Task CreateCard(CardModel card)
        {
            var response = await _httpClient.PostAsJsonAsync<CardModel>(url, card);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteCard(CardModel card)
        {
            var response = await httpService.Delete($"{url}/{card.Id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

        }

        public async Task<List<CardModel>?> GetAllCards()
        {
            var response = await _httpClient.GetAsync(url + "/Cards");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }


            return JsonConvert.DeserializeObject<List<CardModel>?>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());

            //return response.;
        }

        public async Task Test()
        {
            var response = await httpService.Get<List<CardModel>>("/CardApi/test");
        }

        public async Task UpdateCardRating(CardModel card)
        {
            var response = await httpService.Put<CardModel>(url + "/UpdateCardRating", card);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

    }
}
