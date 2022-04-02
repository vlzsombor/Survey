using System.Net.Http;
using Survey.Shared;

namespace Survey.Client.Repository
{
    public class CardBoardFillerRepository : CardRepository
    {
        public CardBoardFillerRepository(HttpClient httpClient) : base(httpClient)
        {
            base._baseUrl = Constants.BACKEND_URL.API_CARD_URL + "/" + Constants.BACKEND_URL.ACCESS_GUID;
        }
    }
}
