using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Survey.Client.Auth
{
    public class DummyAuthProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //await Task.Delay(3000);aaaaa
            var anonymous = new ClaimsIdentity(new List<Claim>()
            {
                new Claim("key1", "value1"),
                new Claim(ClaimTypes.Name, "Zsombor"),
                new Claim(ClaimTypes.Role, "Admin2")
            });
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}
