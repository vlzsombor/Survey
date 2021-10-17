using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Unit
{
    public static class StaticClass
    {
        private const string baseUrl = "api/";

        private const string card = baseUrl + "card";
        public const string API_ACCOUNT_URL = baseUrl + "account";
        public const string API_BOARD_URL = baseUrl + "board";
    }
}
