﻿using Microsoft.AspNetCore.Authorization;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Server.Controllers
{
    [Route(Survey.Shared.Constants.BACKEND_URL.API_BOARD_URL)]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BoardController : ControllerBase
    {
        private readonly SurveyDbContext _context;
        private readonly IBoardService boardService;

        public BoardController(SurveyDbContext surveyDbContext, IBoardService boardService)
        {
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

        [HttpGet("{boardFillerGuid}")]
        public List<CardModel>? GenerateTempUserId(string boardFillerGuid)
        {
            Guid guid = Guid.Parse(boardFillerGuid);
            BoardFiller? boardFiller = _context.BoardFillers
                .Include(x => x.BoardModel)
                .Include(x=>x.BoardModel.Cards)
                .Where(x => x.BoardFillerGuid.ToString() == boardFillerGuid).FirstOrDefault();

            return boardFiller?.BoardModel?.Cards?.ToList();

        }


        [HttpPost("test")]
        public string? GenerateTempUserId([FromBody] BoardFillerGenerationDto boardFillerGenerationDto)
        {

            //xxxxxxxxxxxxxxxx input parameter
            string email = "xyz@a.hu";
            string boardGuid = "c0145f46-b749-4fb2-75ab-08d99d3f0e38";
            //xxxxxxxxxxxxxxxx
            boardService.HandleBoardFillerGeneration(boardFillerGenerationDto);

            // tick generate a random guid (lets call this G) and password (P)
            // this G,P should be saved to the db !alert P should definetily be hashed
            // if a request comes in with the G and corret password typed, then the page is shown


            return "hey";
        }



    }
}
