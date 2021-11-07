﻿using Microsoft.AspNetCore.Identity;
using Survey.Shared;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityUser?> RegisterUser(string g, string password, Constants.ROLE_NAMES role);
    }
}
