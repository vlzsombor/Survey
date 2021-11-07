using Microsoft.AspNetCore.Identity;
using Survey.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Services.Interfaces
{
    public interface IBoardService
    {
        Task<string> HandleBoardFillerGeneration(BoardFillerGenerationDto boardFillerGenerationDto);
        Task<IdentityResult?> HandleBoardFillerGeneration2(BoardFillerGenerationDto boardFillerGenerationDto);
    }
}   
