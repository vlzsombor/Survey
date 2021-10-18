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
    public static class Constants
    {
        /// <summary>
        /// Order dependent, the default Admin user is linked to the zeroth element of the List in the
        /// <see cref="SeedAdministratorAndUser.SeedAdministratorUser"/>
        /// </summary>
        public static readonly IList<string> ROLE_NAMES = new List<string>(){
                "Admin",
                "BoardAdmin",
                "BoardFiller"
            };


        public static IdentityUser GetIdentityUserByEmail(SurveyDbContext _context, HttpContext httpContext) => _context.Users.Where(x => x.Email == httpContext.User.Identity.Name).FirstOrDefault();

    }
}
