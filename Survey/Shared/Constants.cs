using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared
{
    public static class Constants
    {


        public enum ROLE_NAMES
        {
            Admin,
            BoardAdmin,
            BoardFiller
        }

        public static class BACKEND_URL
        {
            #region url
            public const string API = "api";
            public const string CREATE = "create";
            public const string LOGIN = "login";
            public const string BOARD_FILLER_LOGIN = "board-filler/login";
            public const string CARDS = "cards";
            public const string UPDATE_CARD_RATING = "update-card-rating";
            #endregion

            #region controller url
            public const string SLASH = "/";
            public const string API_BOARD_URL = API + SLASH + "board";
            public const string ACCESS_GUID = "acces-guid";
            public const string API_CARD_URL = API + SLASH + "card";
            public const string API_ACCOUNT_URL = API + SLASH + "account";
            public const string GENERATE_BOARD_FILLER = "generate-board-filler";
            #endregion

        }
        public static class FRONTEND_URL
        {

            public const string BOARD = "board";
            public const string MANAGER = "manager";


            public const string test = "test";
            public const string LOGIN = "login";
            public const string LOGOUT = "logout";
            public const string REGISTER = "register";
            public const string BOARD_MANAGER = BOARD + "/manager";
            public const string ADD_BOARDFILLERS = BOARD + "/add-boardfillers";




        }


    }
}
