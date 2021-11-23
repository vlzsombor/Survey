using Survey.Shared.Model;
using Survey.Shared.Model.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Repository.Interfaces
{
    public interface ICardRepository
    {
        public Task CreateCard(CardModel card, string guid);
        public Task UpdateCardRating(int value, CardModel cardmodel);
        public Task AddReply(string reply, IRepliable cardmodel);
        public Task DeleteCard(CardModel card);
    }
}
