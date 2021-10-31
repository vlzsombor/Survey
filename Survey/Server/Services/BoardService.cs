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
        public BoardService(SurveyDbContext surveyDbContext)
        {
            this._context = surveyDbContext;
        }
        private Random _random = new Random();

        public void HandleBoardFillerGeneration(BoardFillerGenerationDto boardFillerGenerationDto)
        {
            string boardGuid = boardFillerGenerationDto.BoardGuid.ToString();

            Guid g = Guid.NewGuid();

            // todo hash pin code
            string pinCode = ServerHelper.GenerateRandomNo(_random);


            BoardModel? boardModel = _context.BoardModel.Where(x => x.Id.ToString() == boardGuid).FirstOrDefault();
            if (boardModel == null)
            {
                return;
            }
            //foreach email address{
            BoardFiller boardFiller = new BoardFiller(g, pinCode, boardModel);
            //SendToEmail(null);
            //}
            
            
            _context.Add(boardFiller);


            _context.SaveChanges();
        }

        private void SendToEmail(string email, BoardFiller boardFiller)
        {

        }
    }
}

