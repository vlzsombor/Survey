using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Survey.Server.Model;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Survey.Shared.DTOs;
using Survey.Server.Services.Interfaces;
using Survey.Shared;
using System.Net;
using Survey.Shared.Model.Comment;
using System.Threading;
using Survey.Server.Services;

namespace Survey.Server.Controllers
{
    [Route(Constants.BACKEND_URL.API_BOARD_URL)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BoardController : ControllerBase
    {
        private readonly SurveyDbContext _context;
        private readonly IBoardService boardService;
        private readonly UserManager<IdentityUser> _userManager;

        public BoardController(UserManager<IdentityUser> userManager, SurveyDbContext surveyDbContext, IBoardService boardService)
        {
            _userManager = userManager;

            this._context = surveyDbContext;
            this.boardService = boardService;
        }

        // read
        [HttpGet]
        public List<BoardModel> Get()
        {

            return _context.BoardModel
                .Where(x => x.OwnerUser == ServerHelper.GetIdentityUserByName(_context, HttpContext))
                .ToList();
        }


        [HttpGet(Constants.FRONTEND_URL.GET_EXP_TIME + "/"+"{guid}")]
        public DateTime GetExpTime(string guid)
        {

            return _context.BoardModel.Where(x => x.Id.ToString()== guid).First().ExpDate;
        }
        [HttpGet(Constants.BACKEND_URL.ACCESS_GUID +"/"+Constants.FRONTEND_URL.GET_EXP_TIME + "/"+"{guid}")]
        public DateTime GetExpTimeAccessGuid(string guid)
        {

            return _context.BoardFillers.Where(x => x.UserName == guid).Select(x => x.BoardModel.ExpDate).First();
        }

        // create
        // POST api/<BoardController>
        [HttpPost]
        public void Post([FromBody] BoardModel bm)
        {
            IdentityUser user = ServerHelper.GetIdentityUserByName(_context, HttpContext);
            //bm.Cards = _context.CardModel.ToList();
            bm.OwnerUser = user;
            _context.BoardModel.Update(bm);
            _context.SaveChanges();
        }

        [HttpGet(Constants.BACKEND_URL.ACCESS_GUID + "/{boardFillerGuid}")]
        public List<CardRatingDto>? GenerateTempUserId(string boardFillerGuid)
        {
            BoardFiller? boardFiller = _context.BoardFillers
                .Where(x => x.UserName == boardFillerGuid &&
                0 < DateTime.Compare(x.BoardModel.ExpDate, DateTime.Now))
                .FirstOrDefault();

            IdentityUser user = ServerHelper.GetIdentityUserByName(_context, HttpContext);
            if (boardFiller?.UserName != user.UserName)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return null;
            }

            List<CardRatingDto> cardRatingDto = new List<CardRatingDto>();

            foreach (var item in boardFiller?.BoardModel?.Cards ?? Enumerable.Empty<CardModel?>())
            {
                if (item != null)
                {
                    cardRatingDto.Add(new CardRatingDto(item.Rating.Where(x => x.IdentityUser == user).FirstOrDefault()?.RatingNumber ?? 0, item));

                }

            }

            return cardRatingDto;
        }

        //read
        [HttpGet("{guidString}")]
        public ICollection<CardRatingDto>? GetByGuid(string guidString)
        {
            var user = ServerHelper.GetIdentityUserByName(_context, HttpContext);

            BoardModel? boardModel = _context.BoardModel
                .Where(board =>
                    board.OwnerUser == user &&
                    board.Id.ToString() == guidString &&
                    0 < DateTime.Compare(board.ExpDate, DateTime.Now))
                .FirstOrDefault();



            List<CardRatingDto> cardRatingDto = new List<CardRatingDto>();
            if (boardModel != null)
            {
                foreach (var item in boardModel.Cards)
                {
                    if (item != null)
                    {
                        cardRatingDto.Add(new CardRatingDto(item.Rating.Where(x => x.IdentityUser == user).FirstOrDefault()?.RatingNumber ?? 0, item));
                    }

                }
            }
            return cardRatingDto;
        }



        [HttpPost(Constants.BACKEND_URL.GENERATE_BOARD_FILLER)]
        [AllowAnonymous]
        public async Task<string?> GenerateTempUserId([FromBody] BoardFillerGenerationDto boardFillerGenerationDto)
        {
            return await boardService.HandleBoardFillerGeneration(boardFillerGenerationDto);
        }

        [HttpDelete(Constants.BACKEND_URL.DELETE_BOARD + "/{guid}")]
        public async Task<bool> DeleteBoard(Guid guid)
        {

            var a = _context.BoardModel.FirstOrDefault(board => board.Id == guid);

            if (a == null)
            {
                return false;
            }

            foreach (var item in a.Cards ?? Enumerable.Empty<CardModel>())
            {
                await new CardService(_context).DeleteCard(item.Id);
            }


            _context.BoardModel.Remove(a);
            _context.SaveChanges();

            return true;
        }
    }
}
