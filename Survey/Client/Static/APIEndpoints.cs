using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Static
{
    public static class APIEndpoints
    {
#if DEBUG
        internal const string ServerBaseUrl = "https://localhost:44379";
#else
        internal const string ServerBaseUrl = "https://localhost:44379";
#endif
        

        internal readonly static string s_register = $"{ServerBaseUrl}/api/account/register";
        internal readonly static string s_signIn = $"{ServerBaseUrl}/api/account/signin";
        internal readonly static string s_weatherForcast = $"{ServerBaseUrl}/weatherforecast";
    }
}
