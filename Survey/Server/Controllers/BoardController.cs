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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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


        // create
        // POST api/<BoardController>
        [HttpPost]
        public void Post([FromBody] BoardModel bm)
        {
            IdentityUser user = ServerHelper.GetIdentityUserByName(_context, HttpContext);
            //bm.Cards = _context.CardModel.ToList();
            bm.OwnerUser = user;
            _context.BoardModel.Add(bm);
            _context.SaveChanges();

        }

        [HttpGet(Constants.BACKEND_URL.ACCESS_GUID + "/{boardFillerGuid}")]
        public List<CardRatingDto>? GenerateTempUserId(string boardFillerGuid)
        {
            BoardFiller? boardFiller = _context.BoardFillers
                .Where(x => x.UserName == boardFillerGuid)
                .FirstOrDefault();



            IdentityUser user = ServerHelper.GetIdentityUserByName(_context, HttpContext);
            if (boardFiller?.UserName != user.UserName)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return null;
            }

            List<CardRatingDto> cardRatingDto = new List<CardRatingDto>();

            foreach (var item in boardFiller?.BoardModel?.Cards ?? Enumerable.Empty<CardModel>())
            {
                cardRatingDto.Add(new CardRatingDto(item.Rating.Where(x => x.IdentityUser == user).FirstOrDefault()?.RatingNumber ?? 0, item));
            }

            return cardRatingDto;
        }

        //read
        [HttpGet("{guidString}")]
        public ICollection<CardRatingDto>? GetByGuid(string guidString)
        {
            var user = ServerHelper.GetIdentityUserByName(_context, HttpContext);

            //_context.BoardModel.Load();


            BoardModel? a = _context.BoardModel
                .Where(board =>
                    board.OwnerUser == user &&
                    board.Id.ToString() == guidString)
                .FirstOrDefault();



            List<CardRatingDto> cardRatingDto = new List<CardRatingDto>();
            if (a != null)
            {
                foreach (var item in a.Cards)
                {
                    cardRatingDto.Add(new CardRatingDto(item.Rating.Where(x => x.IdentityUser == user).FirstOrDefault()?.RatingNumber ?? 0, item));
                }
            }
            return cardRatingDto;
        }



        [HttpPost(Constants.BACKEND_URL.GENERATE_BOARD_FILLER)]
        [AllowAnonymous]
        public async Task<string?> GenerateTempUserId([FromBody] BoardFillerGenerationDto boardFillerGenerationDto)
        {
            return await boardService.HandleBoardFillerGeneration(boardFillerGenerationDto);

            // tick generate a random guid (lets call this G) and password (P)
            // this G,P should be saved to the db !alert P should definetily be hashed
            // if a request comes in with the G and corret password typed, then the page is shown
        }

        [HttpDelete(Constants.BACKEND_URL.DELETE_BOARD + "/{guid}")]
        public async Task<bool> DeleteBoard(Guid guid)
        {

            var a = _context.BoardModel.FirstOrDefault(board => board.Id == guid);

            if (a == null)
            {
                return false;
            }
            
            foreach (var item in a.Cards ?? Enumerable.Empty<CardModel>() )
            {
                await new CardService(_context).DeleteCard(item.Id);
            }


            _context.BoardModel.Remove(a);
            _context.SaveChanges();

            return true;
        }
    }
}
