using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Net.Http;
using System.Text.Json;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using System.IdentityModel.Tokens.Jwt;

namespace Survey.Client.Auth
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    {
        private ISessionStorageService _sessionStorageService { get; set; }


        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        private HttpClient httpClient;
        private readonly string TOKENKEY = "TOKENKEY";
        private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public JWTAuthenticationStateProvider(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            this.httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _sessionStorageService.GetItemAsync<string>(TOKENKEY);

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

            return claims;
        }

        public async Task<AuthenticationState?> Login(string token)
        {
            await _sessionStorageService.SetItemAsStringAsync(TOKENKEY, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            return authState;
        }

        public async Task Logout()
        {
            await _sessionStorageService.RemoveItemAsync(TOKENKEY);

            httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));

        }
    }
}
