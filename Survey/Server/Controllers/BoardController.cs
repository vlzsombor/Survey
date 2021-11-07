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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Server.Controllers
{
    [Route(Constants.BACKEND_URL.API_BOARD_URL)]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                .Include(r => r.OwnerUser)
                .Where(x => x.OwnerUser == ServerHelper.GetIdentityUserByEmail(_context, HttpContext))
                .ToList();
        }


        // create
        // POST api/<BoardController>
        [HttpPost]
        public void Post([FromBody] BoardModel bm)
        {
            IdentityUser user = ServerHelper.GetIdentityUserByEmail(_context, HttpContext);
            bm.Cards = _context.CardModel.ToList();
            bm.OwnerUser = user;
            _context.BoardModel.Add(bm);
            _context.SaveChanges();

        }

        [HttpGet(Constants.BACKEND_URL.ACCESS_GUID + "/{boardFillerGuid}")]
        public List<CardModel>? GenerateTempUserId(string boardFillerGuid)
        {
            Guid guid = Guid.Parse(boardFillerGuid);
            BoardFiller? boardFiller = _context.BoardFillers
                .Include(x => x.BoardModel)
                .Include(x => x.BoardModel.Cards)
                .Where(x => x.identityUser.UserName == boardFillerGuid).FirstOrDefault();

            return boardFiller?.BoardModel?.Cards?.ToList();
        }

        //read
        [HttpGet("{guidString}")]
        public ICollection<CardModel>? GetByGuid(string guidString)
        {
            BoardModel? a = _context.BoardModel
                .Include(b => b.Cards)
                .Where(board =>
                board.OwnerUser == ServerHelper.GetIdentityUserByEmail(_context, HttpContext) &&
                board.Id.ToString() == guidString).FirstOrDefault();


            return a?.Cards;
        }

        [HttpPost("test")]
        public async Task<string?> GenerateTempUserId([FromBody] BoardFillerGenerationDto boardFillerGenerationDto)
        {
            return await boardService.HandleBoardFillerGeneration(boardFillerGenerationDto);

            // tick generate a random guid (lets call this G) and password (P)
            // this G,P should be saved to the db !alert P should definetily be hashed
            // if a request comes in with the G and corret password typed, then the page is shown
        }



    }
}
