﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Survey.Client.Auth;
using System.Net.Http;

namespace Survey.Client.Helpers.Providers
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public AppAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }


        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string savedToken = await _localStorageService.GetItemAsync<string>("bearerToken");

                if (string.IsNullOrWhiteSpace(savedToken))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                JwtSecurityToken jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
                DateTime expiry = jwtSecurityToken.ValidTo;

                if(expiry < DateTime.UtcNow)
                {
                    await _localStorageService.RemoveItemAsync("bearerToken");
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
                //Get claims from token and build authenticated user object
                var claims = ParseClaims(jwtSecurityToken);

                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                return new AuthenticationState(user);
            }
            catch (Exception)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        private IList<Claim> ParseClaims(JwtSecurityToken jwtSecurityToken)
        {
            IList<Claim> claims = jwtSecurityToken.Claims.ToList();
            //The value of tokenContent.Subject is the user's email

            claims.Add(new Claim(ClaimTypes.Name, jwtSecurityToken.Subject));
            return claims;
        }


        internal async Task SignIn()
        {
            string savedToken = await _localStorageService.GetItemAsync<string>("bearerToken");
            JwtSecurityToken jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = ParseClaims(jwtSecurityToken);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            Task<AuthenticationState> authentication = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authentication);
        }

        internal void SignOut()
        {
            ClaimsPrincipal nobody = new ClaimsPrincipal(new ClaimsIdentity());
            Task<AuthenticationState> authentication = Task.FromResult(new AuthenticationState(nobody));
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(authentication);
        }


    }
}
