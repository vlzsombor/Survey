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

        //private readonly IBoardRepository _boardRepository;

        public BoardPage()
        {
        }

        public BoardPage(IBoardRepository boardRepository)
        {
            BoardRepository = boardRepository;
        }
        public async void Delete(BoardModel boardModel)
        {
            await BoardRepository.DeleteBoard(boardModel);
            await LoadCard();

        }
    }
}
