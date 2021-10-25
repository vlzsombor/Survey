using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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


        private Exception? errorObject;
        private string? ex;

        public void ProcessError(HttpResponseMessage message)
        {

            if (message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ex = "Unauthorized please login";
                NavigationManager.NavigateTo("login");
            }

            toastServive.ShowError(ex);
            StateHasChanged();
        }

        public void ProcessError(Exception ex)
        {
            Logger.LogError("Error:ProcessError - Type: {Type} Message: {Message}",
                ex.GetType(), ex.Message);

            errorObject = ex;

            toastServive.ShowError("boom");
            StateHasChanged();
        }


    }
}
