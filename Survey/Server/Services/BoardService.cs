using Microsoft.AspNetCore.Identity;
using Survey.Server.Model;
using Survey.Server.Services.Interfaces;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Services
{
    public class BoardService : IBoardService
    {
        private readonly SurveyDbContext _context;
        private readonly IAccountService _accountService;
        private readonly UserManager<IdentityUser> _userManager;

        public BoardService(SurveyDbContext surveyDbContext, IAccountService accountService, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

            this._context = surveyDbContext;
            _accountService = accountService;
        }
        private Random _random = new Random();

        public async Task HandleBoardFillerGeneration(BoardFillerGenerationDto boardFillerGenerationDto)
        {
            string boardGuid = boardFillerGenerationDto.BoardGuid.ToString();

            Guid g = Guid.NewGuid();

            // todo hash pin code
            string pinCode = ServerHelper.GenerateRandomNo(_random) + "Aa123456!";


            BoardModel? boardModel = _context.BoardModel.Where(x => x.Id.ToString() == boardGuid).FirstOrDefault();
            if (boardModel == null)
            {
                return;
            }
            //foreach email address{

            var a = await _accountService.RegisterUser(g, pinCode);

            if (a != null)
            {
                _context.Add(new BoardFiller() { identityUser = a, BoardModel = boardModel });

            }


            //string password = "Aa123456!";

            //IdentityUser user = new IdentityUser() { UserName= "korte2@a.hu" , Email = "korte2@a.hu" };
            //IdentityResult? result = await _userManager.CreateAsync(user, password);

            //BoardFiller boardFiller = new BoardFiller(g, pinCode, boardModel);
            //SendToEmail(null);
            //}


            //_context.Add(boardFiller);


            _context.SaveChanges();
        }
        public async Task<IdentityResult?> HandleBoardFillerGeneration2(BoardFillerGenerationDto boardFillerGenerationDto)
        {
            string password = "Aa123456!";

            var user = new IdentityUser("korte5@a.hu");
            IdentityResult? result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                Console.WriteLine("adsfads");
            }
            return result;
        }

        private void SendToEmail(string email, BoardFiller boardFiller)
        {

        }
    }
}

