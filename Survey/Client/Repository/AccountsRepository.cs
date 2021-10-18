using Survey.Client.Helpers;
using Survey.Client.Repository.Interfaces;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Threading.Tasks;
using Survey.Client.Unit;

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
