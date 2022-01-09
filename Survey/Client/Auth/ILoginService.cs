using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Auth
{
    public interface ILoginService
    {
        Task<AuthenticationState?> Login(string token);
        Task Logout();
    }
}
