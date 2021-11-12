using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Survey.Shared;
using Survey.Client.Auth;

namespace Survey.Client.Shared
{
    public partial class Error : ComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Inject]
        private IToastService toastServive { get; set; } = default!;
        [Inject]
        private ILogger<Error> Logger { get; set; } = default!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public ILoginService loginService { get; set; } = default!;

        string generalErrorMessage = "Something went wrong please refresh the page";

        public void ProcessError(HttpResponseMessage message)
        {
            string exceptionMessage = generalErrorMessage;
            if (message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                exceptionMessage = "Unauthorized please login";
                NavigationManager.NavigateTo(Constants.FRONTEND_URL.LOGIN);
            }

            toastServive.ShowError(exceptionMessage);
            StateHasChanged();
        }

        public void ProcessError(Exception ex)
        {
            Logger.LogError("Error:ProcessError - Type: {Type} Message: {Message}",
                ex.GetType(), ex.Message);
            string exceptionMessage = generalErrorMessage;

            if (ex.Message == System.Net.HttpStatusCode.Unauthorized.ToString())
            {
                exceptionMessage = "Unauthorized please login";
                loginService.Logout();
            }

            loginService.Logout();
            Console.WriteLine(ex.Message);

            toastServive.ShowError(exceptionMessage);
            StateHasChanged();
        }


    }
}
