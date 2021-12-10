using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey.Client.Unit;
using Survey.Shared.Model;
using Survey.Client.Pages.App.Card;
using Survey.Client.Repository;
using Survey.Client.Repository.Interfaces;
using Survey.Client.Auth;
using Survey.Shared.DTOs;
using Survey.Client.Shared;
using Survey.Shared.Model.Comment;
using Microsoft.AspNetCore.SignalR.Client;



namespace Survey.Client.Pages.App.Board
{
    public partial class AdminBoardPage : ComponentBase
    {
        //public List<CardRatingDto>? CardList { get; set; } = new List<CardRatingDto>();

        //public async Task LoadCard()
        //{
        //    if (Guid != null)
        //    {
        //        try
        //        {
        //            CardList = await boardRepository.GetAllCardsOfUser(Guid);
        //        }
        //        catch (ApplicationException ex)
        //        {
        //            Error.ProcessError(ex);
        //            return;
        //        }
        //    }
        //    StateHasChanged();
        //}
    }
}
