using Newtonsoft.Json;
using Survey.Client.Repository.Interfaces;
using Survey.Shared;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Survey.Client.Repository
{
    public class BoardFillerRepository : BoardRepository
    {

        public BoardFillerRepository(HttpClient httpClient) : base(httpClient)
        {
            base._baseUrl = Constants.BACKEND_URL.API_BOARD_URL + "/" + Constants.BACKEND_URL.ACCESS_GUID;
        }


    }
}
