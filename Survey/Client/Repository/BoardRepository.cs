using Survey.Client.Helpers;
using Survey.Client.Repository.Interfaces;
using Survey.Client.Unit;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IHttpService httpService;
        private readonly string baseURL = StaticClass.API_BOARD_URL;

        public BoardRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<List<BoardModel>?> GetAllCards()
        {
            var response = await httpService.Get<List<BoardModel>>(baseURL);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task CreateBoard(BoardModel bm)
        {
            var response = await httpService.Post(baseURL, bm);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
