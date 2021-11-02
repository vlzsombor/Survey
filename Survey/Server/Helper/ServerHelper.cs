using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Survey.Server.Data;
using Survey.Server.Model;

namespace Survey.Server
{
    public static class ServerHelper
    {
        /// <summary>
        /// Order dependent, the default Admin user is linked to the zeroth element of the List in the
        /// <see cref="SeedAdministratorAndUser.SeedAdministratorUser"/>
        /// </summary>

        public static IdentityUser GetIdentityUserByEmail(SurveyDbContext _context, HttpContext httpContext)
        {
            if (httpContext.User.Identity != null)
            {
                return _context.Users.Where(x => x.Email == httpContext.User.Identity.Name).First();
            }
            throw new UnauthorizedAccessException();
        }

        public static string GenerateRandomNo(Random random)
        {
            return random.Next(0, 9999).ToString("D4");
        }
    }
}
