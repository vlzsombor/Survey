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

namespace Survey.Client.Repository
{
    public class CardBoardFillerRepository : CardRepository
    {
        public CardBoardFillerRepository(HttpClient httpClient):base(httpClient)
        {
            base._baseUrl = Constants.BACKEND_URL.API_CARD_URL + "/" + Constants.BACKEND_URL.ACCESS_GUID;
        }


    }
}
