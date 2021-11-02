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
using Microsoft.EntityFrameworkCore;

namespace Survey.Server.Controllers
{
    [ApiController]
    [Route(Survey.Shared.Constants.BACKEND_URL.API_CARD_URL)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardController : ControllerBase
    {

        private readonly SurveyDbContext _context;

        public CardController(SurveyDbContext context)
        {
            _context = context;
        }

        //read
        [HttpGet]
        [Route(Survey.Shared.Constants.BACKEND_URL.CARDS)]
        [AllowAnonymous]
        public List<CardModel> GetCardsAsync(string? boardGuid)
        {

            return _context.CardModel.ToList();
        }



        //update, partly
        [HttpPut]
        [Route(Survey.Shared.Constants.BACKEND_URL.UPDATE_CARD_RATING)]
        public async Task<int> UpdateCardRating([FromBody] CardModel card)
        {
            _context.Update(card);
            await _context.SaveChangesAsync();
            return 0;
        }

        //create
        [HttpPost]
        [Route("{guidString}")]
        public async Task<int> AddCard([FromBody] CardModel cardModel, string guidString)
        {
            BoardModel? boardModel = 
                _context.BoardModel.Include(x=>x.Cards)
                .Where(x => x.Id.ToString() == guidString)
                .FirstOrDefault();

            boardModel?.Cards?.Add(cardModel);

            _context.Update(boardModel);
            await _context.SaveChangesAsync();

            return cardModel.Id;
        }
        //delete
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
