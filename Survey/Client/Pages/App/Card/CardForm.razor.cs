using Microsoft.AspNetCore.Components;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Pages.App.Card
{
    public partial class CardForm : ComponentBase
    {
        public CardForm()
        {
        }

        [Parameter]
        public CardModel cardModel { get; set; } = default!;
        [Parameter] 
        public EventCallback OnValidSubmit { get; set; }
    }
}
