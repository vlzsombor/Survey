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

namespace Survey.Client.Auth
{
    //public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    //{
    //    private IJSRuntime js;
    //    private HttpClient httpClient;
    //    private readonly string TOKENKEY = "TOKENKEY";
    //    private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

    //    public JWTAuthenticationStateProvider(IJSRuntime js, HttpClient httpClient)
    //    {
    //        this.js = js;
    //        this.httpClient = httpClient;
    //    }


    //    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //    {
    //        var token = await js.GetFromLocalStorage(TOKENKEY);


    //        if (string.IsNullOrEmpty(token))
    //        {
    //            return Anonymous;
    //        }

    //        return BuildAuthenticationState(token);
    //    }
    //    public AuthenticationState BuildAuthenticationState(string token)
    //    {
    //        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
    //        //05:08 7-resz
    //        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    //    }

    //    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    //    {
    //        var claims = new List<Claim>();
    //        var payload = jwt.Split('.')[1];
    //        var jsonBytes = ParseBase64WithoutPadding(payload);
    //        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

    //        keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

    //        if(roles != null)
    //        {
    //            if(roles.ToString().Trim().StartsWith("["))
    //            {
    //                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

    //                foreach (var parsedRole in parsedRoles)
    //                {
    //                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
    //                }


    //            }
    //            else
    //            {
    //                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
    //            }
    //            keyValuePairs.Remove(ClaimTypes.Role);
    //        }
    //        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
    //        return claims;
    //    }

    //    private byte[] ParseBase64WithoutPadding(string base64)
    //    {
    //        switch (base64.Length % 4)
    //        {
    //            case 2: base64 += "=="; break;
    //            case 3: base64 += "="; break;
    //        }
    //        return Convert.FromBase64String(base64);
    //    }

    //    public async Task Login(string token)
    //    {
    //        await js.SetInLocalStorage(TOKENKEY, token);
    //        var authState = BuildAuthenticationState(token);
    //        NotifyAuthenticationStateChanged(Task.FromResult(authState));

    //    }

    //    public async Task Logout()
    //    {
    //        await js.RemoveItem(TOKENKEY);

    //        httpClient.DefaultRequestHeaders.Authorization = null;
    //        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));

    //    }
    //}
}
