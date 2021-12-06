using Microsoft.AspNetCore.Identity;
using Microsoft.FeatureManagement;
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
        private readonly IFeatureManager _featureManager;

        public BoardService(SurveyDbContext surveyDbContext,
            IAccountService accountService,
            UserManager<IdentityUser> userManager,
            IFeatureManager featureManager)
        {
            _userManager = userManager;

            _context = surveyDbContext;
            _accountService = accountService;
            _featureManager = featureManager;

        }
        private Random _random = new Random();

        public async Task<string> HandleBoardFillerGeneration(BoardFillerGenerationDto boardFillerGenerationDto)
        {
            string boardGuid = boardFillerGenerationDto.BoardGuid.ToString();

            string pinCode2 = ServerHelper.GenerateRandomNo(_random) + "Aa123456!";
            string pinCode = "Bb123456!";


            BoardModel? boardModel = _context.BoardModel.Where(x => x.Id.ToString() == boardGuid).FirstOrDefault();
            if (boardModel == null)
            {
                return "unknown boardmodel";
            }


            BoardFiller? user = await _accountService.RegisterUser(boardModel,
                pinCode,
                Survey.Shared.Constants.ROLE_NAMES.BoardFiller);

            if (user != null)
            {

                if (await _featureManager.IsEnabledAsync("FeatureActuallySendEmail"))
                {
                    await EmailService.SendEmail("vl.zsombor@gmail.com", user.UserName);
                }
            }


            if (user != null)
            {
                //user.BoardModel = boardModel;
                //_context.Add(user);
                //_context.SaveChanges();
                return user.UserName;
            }

            return "Unsuccessful adding";

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

