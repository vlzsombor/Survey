using Microsoft.AspNetCore.Identity;
using Survey.Server.Model;
using Survey.Server.Services.Interfaces;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<BoardFiller?> RegisterUser(BoardModel bm, string password, Survey.Shared.Constants.ROLE_NAMES? role)
        {
            BoardFiller user = new BoardFiller() { UserName = Guid.NewGuid().ToString(), BoardModel = bm };
            IdentityResult? result = await _userManager.CreateAsync(user, password);


            if (result.Succeeded)
            {
                IdentityResult roleIdentityResult = await _userManager.AddToRoleAsync(user, role.ToString());

                if (roleIdentityResult.Succeeded)
                {
                    return user;
                }
            }

            return null;
        }

    }
}
