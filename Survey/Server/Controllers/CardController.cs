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
using Survey.Shared.DTOs;

namespace Survey.Server.Controllers
{
    [ApiController]
    [Route(Survey.Shared.Constants.BACKEND_URL.API_CARD_URL)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardController : ControllerBase
    {

        private readonly SurveyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CardController(SurveyDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

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
        public async Task<int> UpdateCardRating([FromBody] CardRatingDto card)
        {
            //_context.Update(card);
            //await _context.SaveChangesAsync();
            return 0;
        }

        //create
        [HttpPost]
        [Route("{guidString}")]
        public async Task<int> AddCard([FromBody] CardModel cardModel, string guidString)
        {
            BoardModel? boardModel =
                _context.BoardModel.Include(x => x.Cards)
                .Where(x => x.Id.ToString() == guidString)
                .FirstOrDefault();

            boardModel?.Cards?.Add(cardModel);

            _context.Update(boardModel);
            await _context.SaveChangesAsync();

            return cardModel.Id;
        }

        [HttpPost]
        [Route(Survey.Shared.Constants.BACKEND_URL.ACCESS_GUID + "/{guidString}")]
        public async Task<int> AddCard2([FromBody] CardModel cardModel, string guidString)
        {
            var user = await _userManager.FindByNameAsync(guidString);

            var boardModel = _context.BoardFillers
                .Include(x => x.BoardModel)
                .Include(x => x.BoardModel.Cards)
                .Where(x => x.UserName == user.UserName)
                .FirstOrDefault()?.BoardModel;

            boardModel?.Cards?.Add(cardModel);

            _context.Update(boardModel);
            await _context.SaveChangesAsync();

            return cardModel.Id;
        }
        //delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,BoardAdmin")]
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
