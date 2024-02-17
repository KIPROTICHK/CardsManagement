using CardsManagement.Application.ViewModels;
using CardsManagement.Domain;

namespace CardsManagement.Application.Service.Interface
{
    public interface ICardsManagementService
    {
        Task<GeneralResponseModel<CardResponse>> AddCard(AddCardRequest request,Guid Owner);
        Task<PagedList<Card>> GetCards(FilterCardsRequest request, Guid? OwnerId=null);
        Task<GeneralResponseModel<object>> RemoveCard(Guid? Id);
        Task<GeneralResponseModel<CardResponse>> UpdateCard(EditCardRequest request, Guid? Id);
    }
}