using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey.Server.Data;


namespace Survey.Server
{
    public static class Constants
    {
        /// <summary>
        /// Order dependent, the default Admin user is linked to the zeroth element of the List in the
        /// <see cref="SeedAdministratorAndUser.SeedAdministratorUser"/>
        /// </summary>
        public static readonly IList<string> ROLE_NAMES = new List<string>(){
                "Admin",
                "BoardAdmin",
                "BoardFiller"
            };
    }
}
