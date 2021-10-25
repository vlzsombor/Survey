using Survey.Client.Repository.Interfaces;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Threading.Tasks;
using Survey.Client.Unit;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace Survey.Client.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly string baseURL = StaticClass.API_ACCOUNT_URL;
        private HttpClient _httpClient { get; set; }

        public AccountsRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserToken?> Register(UserInfo userInfo)
        {
            var response = await _httpClient.PostAsJsonAsync($"{baseURL}/create", userInfo);

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
            var response = await _httpClient.PostAsJsonAsync($"{baseURL}/login", userInfo);

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
