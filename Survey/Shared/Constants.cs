using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared
{
    public static class Constants
    {
        public static readonly IList<string> ROLE_NAMES = new List<string>(){
                "Admin",
                "BoardAdmin",
                "BoardFiller"
            };

        public static class URL
        {
            #region url
            public const string API = "api";
            public const string CREATE = "create";
            public const string LOGIN = "login";
            public const string CARDS = "cards";
            public const string UPDATE_CARD_RATING = "update-card-rating";

            #region controller url
            public const string SLASH = "/";
            public const string API_BOARD_URL = API + SLASH + "board";
            public const string API_CARD_URL = API + SLASH + "card";
            public const string API_ACCOUNT_URL = API + SLASH + "account";
            #endregion
            #endregion

        }


    }
}
