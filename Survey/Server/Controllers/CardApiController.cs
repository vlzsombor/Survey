using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Survey.Server.Model;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet]
        [Route("test")]
        public async Task<string> test()
        {
            _context.Add(new CardModel { Rating = 2, Text = "Contextes hozzaadas", Title = "fasfdfs" });

            _context.CardModel.Add(new CardModel { Rating = 1, Text = "CardModelel hozzadas", Title = "CardModelel hozzadas" });
            _context.SaveChanges();
            await _context.SaveChangesAsync();
            var a = _context.CardModel.ToList();

            BoardModel boardModel = new BoardModel()
            {
                Cards = a,
                OwnerUser = new User() { EmailAddress = "ezaz@gmail.com", Password="almafa" }
            };
            _context.BoardModel.Add(boardModel);

            _context.SaveChanges();
            return "hello";
        }

    }
}
