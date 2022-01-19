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

            string pinCode = "Bb123456!";


            if (await _featureManager.IsEnabledAsync("FeatureActuallySendEmail"))
            {
                pinCode = ServerHelper.RandomString(_userManager);

            }

            BoardModel? boardModel = _context.BoardModel.Where(x => x.Id.ToString() == boardGuid).FirstOrDefault();
            if (boardModel == null)
            {
                return "unknown boardmodel";
            }

            List<(string, string, string)> emailUserList = new List<(string, string, string)>();
            foreach (var email in boardFillerGenerationDto.Emails)
            {
                BoardFiller? user = await _accountService.RegisterUser(boardModel,
                pinCode,
                Survey.Shared.Constants.ROLE_NAMES.BoardFiller);
                if (user != null)
                {
                    emailUserList.Add((email, pinCode, user.UserName));

                }
            }


            if (await _featureManager.IsEnabledAsync("FeatureActuallySendEmail"))
            {
                var emailRow = emailUserList.Select(x => x.Item1);
                var pinRow = emailUserList.Select(x => x.Item2);
                var usersList = emailUserList.Select(x => x.Item3);


                await EmailService.SendEmail(emailUserList);
            }
            else
            {


                var a = string.Join(Environment.NewLine +" ", emailUserList.Select(x=>x.Item3).ToArray());


                return a ?? "not found";

            }

            return "Adding was successful";

        }

    }
}

