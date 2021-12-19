﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using System;
using Survey.Shared.Model.Comment;
using Survey.Server.Services;

namespace Survey.Server.Controllers
{
    [ApiController]
    [Route(Survey.Shared.Constants.BACKEND_URL.API_CARD_URL)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardController : ControllerBase
    {

        private readonly SurveyDbContext _context = default!;
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
        [Route(Survey.Shared.Constants.BACKEND_URL.ACCESS_GUID + "/" + Survey.Shared.Constants.BACKEND_URL.UPDATE_CARD_RATING)]
        public async Task<int> OnChangeMethod([FromBody] CardRatingDto cardRatingDto)
        {
            IdentityUser user = ServerHelper.GetIdentityUserByName(_context, HttpContext);

            var myObject = user as BoardFiller;

            var cm = _context.CardModel.Include(x => x.Rating)
                .Where(x => x.Id == cardRatingDto.CardModel.Id)
                .FirstOrDefault();

            if (cm == null)
            {
                return 0;
            }


            if (cm.Rating.Any(x => x.IdentityUser == user))
            {

                if (cardRatingDto.RatingValue.HasValue)
                {
                    cm.Rating.Where(x => x.IdentityUser == user).First().RatingNumber = cardRatingDto.RatingValue.Value;
                }
                else
                {
                    var toDeleteScore = cm.Rating.Where(x => x.IdentityUser == user).First();
                    _context.Remove(toDeleteScore);
                }
            }
            else
            {
                if (cardRatingDto.RatingValue.HasValue)
                {
                    cm.Rating.Add(new RatingModel() { RatingNumber = cardRatingDto.RatingValue.Value, IdentityUser = user });
                }
            }

            _context.Update(cm);
            await _context.SaveChangesAsync();
            return cardRatingDto.RatingValue ?? 0;
        }



        [HttpPut]
        [Route(Survey.Shared.Constants.BACKEND_URL.ACCESS_GUID + "/" + Survey.Shared.Constants.BACKEND_URL.ADD_REPLY)]
        [Route(Survey.Shared.Constants.BACKEND_URL.ADD_REPLY)]
        public async Task AddRepy([FromBody] CardModel cardModel)
        {
            _context.Update(cardModel);
            await _context.SaveChangesAsync();

        }

        [HttpPut]
        [Route(Survey.Shared.Constants.BACKEND_URL.ACCESS_GUID + "/" + Survey.Shared.Constants.BACKEND_URL.ADD_REPLY_TO_REPLY)]
        [Route(Survey.Shared.Constants.BACKEND_URL.ADD_REPLY_TO_REPLY)]
        public async Task AddRepy([FromBody] Reply cardModel)
        {
            _context.Update(cardModel);
            await _context.SaveChangesAsync();

        }

        //create
        [HttpPost]
        [Route("{guidString}")]
        public async Task<Guid> AddCard([FromBody] CardModel cardModel, string guidString)
        {
            BoardModel? boardModel =
                _context.BoardModel.Include(x => x.Cards)
                .Where(x => x.Id.ToString() == guidString)
                .FirstOrDefault();

            boardModel?.Cards?.Add(cardModel);
            if (boardModel != null)
            {
                _context.Update(boardModel);
            }

            await _context.SaveChangesAsync();

            return cardModel.Id;
        }

        [HttpPost]
        [Route(Survey.Shared.Constants.BACKEND_URL.ACCESS_GUID + "/{guidString}")]
        public async Task<Guid> AddCard2([FromBody] CardModel cardModel, string guidString)
        {
            var user = await _userManager.FindByNameAsync(guidString);

            var boardModel = _context.BoardFillers
                .Include(x => x.BoardModel)
                .Include(x => x.BoardModel.Cards)
                .Where(x => x.UserName == user.UserName)
                .FirstOrDefault()?.BoardModel;

            boardModel?.Cards?.Add(cardModel);
            if (boardModel != null)
            {
                _context.Update(boardModel);
            }
            await _context.SaveChangesAsync();

            return cardModel.Id;
        }

        //delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,BoardAdmin")]
        public async Task<ActionResult> Delete(Guid id)
        {


            await new CardService(_context).DeleteCard(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
