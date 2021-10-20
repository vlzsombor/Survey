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

namespace Survey.Client.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly string baseURL = StaticClass.API_BOARD_URL;
        private HttpClient _httpClient { get; set; }

        public BoardRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BoardModel>?> GetAllCards()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(baseURL);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<List<BoardModel>?>(await response.Content.ReadAsStringAsync());
        }
        public async Task CreateBoard(BoardModel bm)
        {
            var response = await _httpClient.PostAsJsonAsync(baseURL, bm);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
