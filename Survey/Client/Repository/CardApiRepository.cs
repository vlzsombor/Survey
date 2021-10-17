using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Survey.Client.Helpers;

namespace Survey.Client.Repository
{
    public class CardApiRepository : ICardRepository
    {
        private readonly IHttpService httpService;

        private string url = "api/cardapi";

        public CardApiRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task CreateCard(CardModel card)
        {
            var response = await httpService.Post(url, card);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
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

        public async Task<List<CardModel>> GetAllCards()
        {
            var response = await httpService.Get<List<CardModel>>(url + "/Cards");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
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
