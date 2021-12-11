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

        private readonly IBoardRepository _boardRepository = default!;

        public BoardPage()
        {
        }

        public BoardPage(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }
        public async void Delete(BoardModel boardModel)
        {
            await _boardRepository.DeleteBoard(boardModel);
            await LoadCard();

        }
    }
}
