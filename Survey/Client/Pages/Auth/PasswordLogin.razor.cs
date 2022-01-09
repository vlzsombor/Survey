using Microsoft.AspNetCore.Components;
using Survey.Client.Auth;
using Survey.Client.Repository.Interfaces;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System.Threading.Tasks;

namespace Survey.Client.Pages.Auth
{
    public partial class PasswordLogin : ComponentBase
    {
        [Inject]
        public IAccountsRepository accountsRepository { get; set; } = default!;
        private UserToken? userToken;

        [Inject]
        public ILoginService loginService { get; set; } = default!;

        [Parameter]
        public BoardFillerDto BoardFillerDto { get; set; } = new BoardFillerDto();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public EventCallback LoadCardEventCallBack { get; set; }
        private async Task LoginUser()
        {

            BoardFillerDto.AccessGuid = Guid;


            userToken = await accountsRepository.Login(BoardFillerDto);
            if (userToken?.Token != null)
            {

                var a = await loginService.Login(userToken.Token);

                if (a.User != null)
                {
                    
                    NavigationManager.NavigateTo(NavigationManager.BaseUri +
                        Survey.Shared.Constants.FRONTEND_URL.BOARD + "/"+ Guid);
                }

            }
        }
    }
}