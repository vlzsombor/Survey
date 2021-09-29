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
        [Parameter] 
        public CardModel cardModel { get; set; }
        [Parameter] 
        public EventCallback OnValidSubmit { get; set; }
    }
}
