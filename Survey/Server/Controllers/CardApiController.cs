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
    public class CardApiController : ControllerBase
    {

        private readonly SurveyDbContext _context;

        public CardApiController(SurveyDbContext context)
        {
            _context = context;
        }


        List<CardModel> CardList = Program.CardList;

        [HttpGet]
        [Route("Cards")]
        public List<CardModel> GetCardsAsync()
        {
            return _context.CardModel.ToList();
        }
        [HttpGet]
        [Route("Card")]
        public CardModel GetCardAsync(int numberOfPost, int starindex)
        {
            return CardList[1];
        }
        [HttpPut]
        [Route("UpdateCardRating")]
        public async Task<int> UpdateCardRating([FromBody] CardModel card)
        {
            Program.CardList.Where(x => x.Id == card.Id).FirstOrDefault().Rating = card.Rating;

            Program.CardList.ForEach(x => Console.WriteLine(x.Rating));

            

            return 0;
        }

        [HttpPost]
        public async Task<int> AddCard([FromBody] CardModel cardModel)
        {

            Program.CardList.Add(cardModel);

            return cardModel.Id;
        }



        [HttpGet]
        [Route("test")]
        public async Task<string> test()
        {
            _context.Add(new CardModel { Rating = 2, Text = "dafasd", Title = "fasfdfs" });

            await _context.SaveChangesAsync();

            var a = _context.CardModel.ToList();

            return "hello";
        }

    }
}
