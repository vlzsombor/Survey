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


namespace Survey.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardApiController : ControllerBase
    {

        private readonly SurveyDbContext _context;

        public CardApiController(SurveyDbContext context)
        {
            _context = context;
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


    }
}
