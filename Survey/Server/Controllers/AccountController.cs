using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace Survey.Server.Controllers
{
    [ApiController]
    [Route(Survey.Shared.Constants.BACKEND_URL.API_ACCOUNT_URL)]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost(Survey.Shared.Constants.BACKEND_URL.CREATE)]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            IdentityResult roleIdentityResult = await _userManager.AddToRoleAsync(user, Survey.Shared.Constants.ROLE_NAMES.BOARD_ADMIN);
            if (result.Succeeded && roleIdentityResult.Succeeded)
            {
                return Ok(await BuildToken(user));
            }
            else
            {
                Dictionary<string, string> errorsDictionary = new Dictionary<string, string>();

                foreach (var errors in result.Errors)
                {
                    errorsDictionary.Add(errors.Code, errors.Description);
                }
                
                return BadRequest(errorsDictionary);
            }
        }


        [HttpPost(Survey.Shared.Constants.BACKEND_URL.LOGIN)]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                IdentityUser identityUser = await _userManager.FindByNameAsync(userInfo.Email);

                return await BuildToken(identityUser);
            }
            else
            {
                Dictionary<string, string> errorsDictionary = new Dictionary<string, string>();

                errorsDictionary.Add("Bad credentials", "Please give correct credentials");
                //string errorsDictionarySerialized = JsonConvert.SerializeObject(errorsDictionary);
                return BadRequest(errorsDictionary);
            }
        }

        private async Task<UserToken> BuildToken(IdentityUser identityUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, identityUser.Email),
                new Claim(ClaimTypes.Email, identityUser.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            IList<string> roleNames = await _userManager.GetRolesAsync(identityUser);
            claims.AddRange(roleNames.Select(roleName => new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)));


            var expiration = DateTime.UtcNow.AddYears(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }
    }
}
