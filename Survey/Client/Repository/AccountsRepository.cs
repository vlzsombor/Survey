using Survey.Client.Helpers;
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

namespace Survey.Client.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = StaticClass.API_ACCOUNT_URL;

        public AccountsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<UserToken?> Register(UserInfo userInfo)
        {
            var response = await httpService.Post<UserInfo, UserToken>($"{baseURL}/create", userInfo);

            if (!response.Success)
            {

                string a = "[\r\n  {\r\n    \"Code\": \"PasswordRequiresNonAlphanumeric\",\r\n    \"Description\": \"Passwords must have at least one non alphanumeric character.\"\r\n  },\r\n  {\r\n    \"Code\": \"PasswordRequiresDigit\",\r\n    \"Description\": \"Passwords must have at least one digit ('0'-'9').\"\r\n  },\r\n  {\r\n    \"Code\": \"PasswordRequiresUpper\",\r\n    \"Description\": \"Passwords must have at least one uppercase ('A'-'Z').\"\r\n  }\r\n]";
                //var a = await response.HttpResponseMessage.Content.ReadAsStringAsync();
                var b = await response.HttpResponseMessage.Content.ReadAsStringAsync();
                //var b = await response.GetBody();
                var asdfsda = "fasdfsa";
                
                //var values = (Newtonsoft.Json.Linq.JArray) JsonConvert.DeserializeObject(b);

                var fadsf = "adsfsd";
                var asdfdsa = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(b);

                //var aaadsfds = values["detail"];

                //foreach (var x in values)
                //{
                //    Console.WriteLine((x));
                //}
                foreach (KeyValuePair<string, string> entry in asdfdsa)
                {
                    Console.WriteLine(entry.Key);
                    Console.WriteLine(entry.Value);
                }

                return new UserToken()
                {
                    Error = "fadsfda",
                };


                throw new ApplicationException(await response.GetBody());

            }

            return response.Response;

        }

        public async Task<UserToken?> Login(UserInfo userInfo)
        {
            var response = await httpService.Post<UserInfo, UserToken>($"{baseURL}/login", userInfo);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;

        }
    }
}
