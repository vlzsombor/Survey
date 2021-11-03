using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Repository.Interfaces
{
    public interface IAccountsRepository
    {
        public Task<UserToken?> Register(UserInfo userInfo);
        public Task<UserToken?> Login(UserInfo userInfo);
        public Task<UserToken?> Login(BoardFillerDto boardFillerDto);

    }
}
