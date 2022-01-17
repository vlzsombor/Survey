using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Survey.Server.Data;
using Survey.Server.Model;
using Survey.Shared.Model;

namespace Survey.Server
{
    public static class ServerHelper
    {
        /// <summary>
        /// Order dependent, the default Admin user is linked to the zeroth element of the List in the
        /// <see cref="SeedAdministratorAndUser.SeedAdministratorUser"/>
        /// </summary>

        public static IdentityUser GetIdentityUserByName(SurveyDbContext _context, HttpContext httpContext)
        {
            if (httpContext.User.Identity != null)
            {
                return _context.Users.Where(x => x.UserName == httpContext.User.Identity.Name).First();
            }
            throw new UnauthorizedAccessException();
        }

        public static string GenerateRandomNo(Random random)
        {
            return random.Next(0, 9999).ToString("D4");
        }

        public static string RandomString(UserManager<IdentityUser> _userManager)
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }


        public static int GetRating(CardModel cm, IdentityUser identityUser)
        {
            return cm.Rating.Where(x => x.IdentityUser == identityUser).FirstOrDefault()?.RatingNumber ?? default(int);
        }

        public static void WriteFile(string path, string text)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine(text);
                tw.Close();
            }
            else if (File.Exists(path))
            {
                TextWriter tw = new StreamWriter(path, true);
                tw.WriteLine(text);
                tw.Close();
            }
        }
    }
}
