
using Microsoft.EntityFrameworkCore;
using Survey.Server.Model;
using Survey.Shared.Model.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Services;

public class CardService
{
    private readonly SurveyDbContext _context;

    public CardService(SurveyDbContext context)
    {
        _context = context;
    }

    async Task RemoveChildren(IList<Reply> children)
    {
        foreach (var child in children)
        {
            await RemoveChildren(child.Replies);
            _context.Remove(child);
        }
    }


    public async Task DeleteCard(Guid id)
    {
        var card = _context.CardModel.Include(x => x.Rating).FirstOrDefault(x => x.Id == id);
        if (card == null)
        {
            return;
        }


        if (card.Replies.Any())
        {
            await RemoveChildren(card.Replies);
        }

        _context.Remove(card);
    }


}