using Microsoft.AspNetCore.Components;
using Survey.Client.Repository;
using Survey.Client.Repository.Interfaces;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Survey.Client.Pages.App.Board
{
    public partial class BoardPage : ComponentBase
    {
        [Inject]
        private IBoardRepository _boardRepository { get; set; } = default!;

        public BoardPage()
        {
        }
        public async void Delete(BoardModel boardModel)
        {
            await _boardRepository.DeleteBoard(boardModel);
            await LoadCard();

        }
        private async void MakeNewSurveyBoard()
        {
            BoardModel boardModel = new BoardModel();

            bool ifSucceded = await _boardRepository.CreateBoard(new Survey.Shared.Model.BoardModel());
            if (ifSucceded)
            {
                BoardList?.Add(boardModel);
            }
            await LoadCard();
        }

        public List<BoardModel>? BoardList { get; set; } = new List<BoardModel>();

        private async Task LoadCard()
        {
            BoardList = await _boardRepository.GetBoardOfUser();
            StateHasChanged();
        }
        protected async override void OnInitialized()
        {
            await LoadCard();
        }
    }



}

