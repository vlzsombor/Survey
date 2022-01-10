using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Survey.Client.Repository.Interfaces
{
    public interface IBoardRepository
    {
        Task<bool> CreateBoard(BoardModel bm);
        Task<List<CardRatingDto>?> GetAllCardsOfUser(string guid);
        Task<List<BoardModel>?> GetBoardOfUser();
        Task<HttpResponseMessage?> DeleteBoard(BoardModel boardModel);
        Task<DateTime> GetExpTime(string guid);


    }
}
