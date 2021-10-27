using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Repository.Interfaces
{
    public interface ICardRepository
    {
        public Task<List<CardModel>?> GetAllCardsOfUser(string guid);
        public Task CreateCard(CardModel card, string guid);
        public Task UpdateCardRating(CardModel card);
        public Task DeleteCard(CardModel card);
    }
}
