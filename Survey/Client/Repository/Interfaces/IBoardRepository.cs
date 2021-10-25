using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Repository.Interfaces
{
    interface IBoardRepository
    {
        Task<bool> CreateBoard(BoardModel bm);
        Task<List<BoardModel>?> GetBoardOfUser();
    }
}
