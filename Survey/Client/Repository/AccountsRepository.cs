using Survey.Client.Repository.Interfaces;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Threading.Tasks;
using Survey.Client.Util;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using Survey.Shared;
namespace Survey.Client.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly string baseURL = Constants.BACKEND_URL.API_ACCOUNT_URL;
        private HttpClient _httpClient { get; set; }

        public AccountsRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserToken?> Register(UserInfo userInfo)
        {
            var response = await _httpClient.PostAsJsonAsync(baseURL + "/" + Constants.BACKEND_URL.CREATE, userInfo);

            if (!response.IsSuccessStatusCode)
            {
                var errorsDictionarySerialized = await response.Content.ReadAsStringAsync();

                var errorDictionary = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(errorsDictionarySerialized);


                return new UserToken()
                {
                    ErrorDict = errorDictionary
                };
            }
            return JsonConvert.DeserializeObject<UserToken?>(await response.Content.ReadAsStringAsync());
        }

        public async Task<UserToken?> Login(UserInfo userInfo)
        {
            var response = await _httpClient.PostAsJsonAsync(baseURL + "/" + Constants.BACKEND_URL.LOGIN, userInfo);

            if (!response.IsSuccessStatusCode)
            {
                var errorsDictionarySerialized = await response.Content.ReadAsStringAsync();
                var errorDictionary = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(errorsDictionarySerialized);

                return new UserToken()
                {
                    ErrorDict = errorDictionary
                };
            }
            return JsonConvert.DeserializeObject<UserToken?>(await response.Content.ReadAsStringAsync());
        }

        public async Task<UserToken?> Login(BoardFillerDto boardFillerDto)
        {
            var response = await _httpClient.PostAsJsonAsync(baseURL + "/" + Constants.BACKEND_URL.BOARD_FILLER_LOGIN, boardFillerDto);

            if (!response.IsSuccessStatusCode)
            {
                var errorsDictionarySerialized = await response.Content.ReadAsStringAsync();
                var errorDictionary = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(errorsDictionarySerialized);

                return new UserToken()
                {
                    ErrorDict = errorDictionary
                };
            }
            return JsonConvert.DeserializeObject<UserToken?>(await response.Content.ReadAsStringAsync());
        }
    }
}
