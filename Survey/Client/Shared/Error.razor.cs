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

        private Exception? errorObject;
        private string? ex;

        public void ProcessError(HttpResponseMessage message)
        {
            System.Net.HttpStatusCode a = message.StatusCode;

            if (a == System.Net.HttpStatusCode.Unauthorized)
            {
                ex = "Unauthorized please login";
            }


            StateHasChanged();
        }
        public void ProcessError(Exception ex)
        {
            Logger.LogError("Error:ProcessError - Type: {Type} Message: {Message}",
                ex.GetType(), ex.Message);

            errorObject = ex;
            StateHasChanged();
        }
    }
}
