using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Survey.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Survey.Server
{
    public static class Unit
    {

        public static IdentityUser GetIdentityUserByEmail(SurveyDbContext _context, HttpContext httpContext) => _context.Users.Where(x => x.Email == httpContext.User.Identity.Name).FirstOrDefault();
    }
}
