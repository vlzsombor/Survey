using Newtonsoft.Json;
using Survey.Client.Repository.Interfaces;
using Survey.Client.Unit;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Survey.Shared;


namespace Survey.Client.Repository
{
    public class BoardRepository : IBoardRepository
    {
        protected string _baseUrl = Constants.BACKEND_URL.API_BOARD_URL;
        private HttpClient _httpClient { get; set; }

        public BoardRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BoardModel>?> GetBoardOfUser()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<List<BoardModel>?>(await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> CreateBoard(BoardModel bm)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, bm);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }

            return true;
        }

        public async Task<List<CardModel>?> GetAllCardsOfUser(string guid)
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/" + guid);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<List<CardModel>?>(await response.Content.ReadAsStringAsync());
        }
    }
}
