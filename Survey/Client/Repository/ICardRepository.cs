﻿using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Repository
{
    public interface ICardRepository
    {
        public Task<List<CardModel>> GetAllCards();
        public Task CreateCard(CardModel card);
        public Task UpdateCardRating(CardModel card);
        public Task DeleteCard(CardModel card);
    }
}
