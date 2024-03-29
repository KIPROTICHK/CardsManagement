﻿namespace CardsManagement.Infrastructure.Repositories
{
    public class DataRepository :  IDataRepository
    {
        private readonly CoreContext _context;

        public DataRepository(CoreContext context)
        {
            _context = context;
        }

        public IQueryable<Card> GetCards()
        {
            return _context.Cards.AsQueryable();
        }
    }
}
