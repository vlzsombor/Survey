using Survey.Client.Helpers;
using Survey.Client.Static;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Survey.Client.Helpers.Providers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace Survey.Client.Repository
{

    // todo
    //public class AccountsRepository : IAccountsRepository
    //{
    //    public HttpClient HttpClient { get; set; }
    //    public ILocalStorageService LocalStorageService { get; set; }
    //    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    //    private readonly IHttpService httpService;
    //    private readonly string baseURL = "api/account";

    //    public AccountsRepository(IHttpService httpService, HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
    //    {
    //        this.httpService = httpService;
    //        this.HttpClient = httpClient;
    //        this.LocalStorageService = localStorageService;
    //        this.AuthenticationStateProvider = AuthenticationStateProvider;
    //    }

    //    public async Task<UserToken> Register(UserInfo userInfo)
    //    {
    //        var response = await httpService.Post<UserInfo, UserToken>($"{baseURL}/create", userInfo);

    //        if (!response.Success)
    //        {
    //            throw new ApplicationException(await response.GetBody());
    //        }

    //        return response.Response;

    //    }
    //    public async Task<UserToken> Login(UserInfo userInfo)
    //    {
    //        var response = await httpService.Post<UserInfo, UserToken>($"{baseURL}/login", userInfo);

    //        if (!response.Success)
    //        {
    //            throw new ApplicationException(await response.GetBody());
    //        }

    //        return response.Response;

    //    }

    //    public async Task<bool> SignIn(User userToSignIn)
    //    {
    //        HttpResponseMessage httpResponseMessage = await HttpClient.PostAsJsonAsync(APIEndpoints.s_signIn, userToSignIn);

    //        if (httpResponseMessage.IsSuccessStatusCode)
    //        {
    //            string jsonWebToken = await httpResponseMessage.Content.ReadAsStringAsync();

    //            await LocalStorageService.SetItemAsStringAsync("bearerToken", jsonWebToken);

    //            await ((AppAuthenticationStateProvider)AuthenticationStateProvider).SignIn();

    //            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", jsonWebToken);

    //            return true;
    //        }

    //        return false;
    //    }

    //    public Task<UserToken> Register2(UserInfo userInfo)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
