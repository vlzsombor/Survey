using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Survey.Client.Helpers;
using System.Security.Claims;
using System.Net.Http;
using System.Text.Json;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;

namespace Survey.Client.Auth
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    {
        private ILocalStorageService _localStorageService { get; set; }
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        private HttpClient httpClient;
        private readonly string TOKENKEY = "TOKENKEY";
        private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public JWTAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonymous;
            }

            return BuildAuthenticationState(token);
        }
        public AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            JwtSecurityToken jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaims(jwtSecurityToken), "jwt")));
        }

        private IEnumerable<Claim> ParseClaims(JwtSecurityToken jwtSecurityToken)
        {
            IList<Claim> claims = jwtSecurityToken.Claims.ToList();
            //The value of tokenContent.Subject is the user's email

            //claims.Add(new Claim(ClaimTypes.Name, jwtSecurityToken.Subject));
            return claims;
        }

        public async Task Login(string token)
        {
            await _localStorageService.SetItemAsStringAsync(TOKENKEY, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));

        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(TOKENKEY);

            httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));

        }
    }
}
