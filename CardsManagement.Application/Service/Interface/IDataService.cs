using CardsManagement.Application.ViewModels;
using CardsManagement.Domain;

namespace CardsManagement.Application.Service.Interface
{
    public interface IDataService
    {

        //Task<PagedList<NicheBeneficiary>> GetNicheBeneficiaries(NicheBeneficiariesRequest request);
        Task<PagedList<Card>> GetCards(FilterCardsRequest request);
    }
}
