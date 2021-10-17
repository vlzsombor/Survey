using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Survey.Server.Model;
using Survey.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;
using System.Linq;


namespace Survey.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardApiController : ControllerBase
    {

        private readonly SurveyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CardApiController(SurveyDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [HttpGet]
        [Route("Cards")]
        [AllowAnonymous]
        public List<CardModel> GetCardsAsync()
        {
            return _context.CardModel.ToList();
        }

        [HttpPut]
        [Route("UpdateCardRating")]
        public async Task<int> UpdateCardRating([FromBody] CardModel card)
        {
            _context.Update(card);
            await _context.SaveChangesAsync();
            return 0;
        }

        [HttpPost]
        public async Task<int> AddCard([FromBody] CardModel cardModel)
        {

            _context.Add(cardModel);
            await _context.SaveChangesAsync();

            return cardModel.Id;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = _context.CardModel.FirstOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Remove(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("test")]
        [Authorize]
        public async Task<string> test()
        {
            _context.Add(new CardModel { Rating = 2, Text = "dafasd", Title = "fasfdfs" });

            await _context.SaveChangesAsync();

            var a = _context.CardModel.ToList();


            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name);
            var userId = claim.Value;

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var aadsfdsa = _context.Users.Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            var asdsa = HttpContext.User.Identity;

            BoardModel b = new BoardModel()
            {
                Cards = new List<CardModel>() { new CardModel() { Text = "hello", Title = "title" } },
                OwnerUser = aadsfdsa,
            };

            _context.BoardModel.Add(b);

            _context.SaveChanges();
            return "hello";
        }

    }
}
