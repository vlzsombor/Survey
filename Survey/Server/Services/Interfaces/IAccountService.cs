using Microsoft.AspNetCore.Identity;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityUser> RegisterUser(Guid g, string password);
    }
}
