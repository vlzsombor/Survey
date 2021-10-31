using Survey.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Services.Interfaces
{
    public interface IBoardService
    {
        void HandleBoardFillerGeneration(BoardFillerGenerationDto boardFillerGenerationDto);
    }
}   
