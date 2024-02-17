using CardsManagement.Domain;

namespace CardsManagement.Application.Repositories
{
    public interface IDataRepository
    {
        IQueryable<Card> GetCards();
    }
}
