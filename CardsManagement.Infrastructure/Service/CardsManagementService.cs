using CardsManagement.Application.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardsManagement.Application.SharedDefinedValues;

namespace CardsManagement.Infrastructure.Service
{
    public class CardsManagementService: ICardsManagementService
    {
         

        private readonly IDataRepository _dataRepository;
        private readonly ICustomBaseRepository _customBaseRepository;

        public CardsManagementService(IDataRepository dataRepository
            ,ICustomBaseRepository customBaseRepository)
        {
            _dataRepository = dataRepository;
            _customBaseRepository = customBaseRepository;
        }
        public async Task<PagedList<Card>> GetCards(FilterCardsRequest request, Guid? OwnerId)
        {
            var data = _dataRepository
                .GetCards()
                .Include(x=>x.OwnerNavigation)
                .AsQueryable()
                ;
            #region filtering data
            data = data
            .WhereIf(OwnerId.HasValue, x => x.Owner == OwnerId);
            //    .WhereIf(!string.IsNullOrEmpty(request.AccountType), x => x.AccountType == request.AccountType);
            ////    .WhereIf(request.Ids != null && request.Ids.Count > 0, x => request.Ids.Contains(x.Id));

            int OriginalCount = data.Count();

            #region search items


            data = data

            .WhereIf(!string.IsNullOrEmpty(request.Name),
              x => x.Name.ToLower().Contains(request.Name.ToLower()))

             .WhereIf(!string.IsNullOrEmpty(request.Color),
              x => x.Color.ToLower().Contains(request.Color.ToLower()))

             .WhereIf(!string.IsNullOrEmpty(request.Status),
              x => x.Status.ToLower().Contains(request.Name.ToLower()))

             .WhereIf(request.DateCreated.HasValue,
              x => x.DateCreated.ToString().ToLower().Contains(request.DateCreated.ToString().ToLower()));

            #endregion

            if (!string.IsNullOrEmpty(request.SortColumn))
            {
                data = data.OrderByDynamic(request.SortColumn, request.SortColumnDirection);

            }
            else
            {
                data = data.OrderBy(x => x.Id);

            }

            #endregion

            data = data.AsNoTrackingWithIdentityResolution();

            return await Task.FromResult(PagedList<Card>.ToPagedList(data, request.PageNumber, request.PageSize, OriginalCount));

        }

        public async Task<GeneralResponseModel<CardResponse>> AddCard(AddCardRequest request,Guid Owner)
        {
            GeneralResponseModel<CardResponse> res = new();

            Card data = request.CastMyObject<Card>();
            data.Owner = Owner;
            data.DateCreated = DateTime.Now;
 
            try
            {
                await _customBaseRepository.CustomAdd(data);
                res.Message = $"Successfully saved Data";
                res.Success = true;
                res.Data = data.CastMyObject<CardResponse>();

            }
            catch (Exception ex)
            {
                res.Message = $"Unable to save Card '{request.Name}'.\nError: {ex.InnerException?.Message ?? ex.Message}";


            }
            return res;
        }

        public async Task<GeneralResponseModel<CardResponse>> UpdateCard(EditCardRequest request,Guid? Id)
        {
            GeneralResponseModel<CardResponse> res = new();

            switch (request.Status)
            {
                case CardStatuses.ToDo:
                case CardStatuses.InProgress:
                case CardStatuses.Done:

                    break;


                default:
                    res.Message = $"Card Status {request.Status} is not valid!";
                    return res;
            }

            var data = await _dataRepository.GetCards()
                .FirstOrDefaultAsync(x => x.Id == Id);
             

            if (data == null)
            {
                res.Message = "Card details does not exists";
                return res;
            }
             

            #region update  

            data.Name = request.Name;
            data.Color = request.Color;
            data.Description = request.Description;
            data.Status = request.Status;
            #endregion

            #region save data

            try
            {
                await _customBaseRepository.CustomSaveChanges();
                res.Message = $"Successfully saved Card '{data.Name}'";
                res.Success = true;
                res.Data = data.CastMyObject<CardResponse>();
            }
            catch(Exception ex)
            {
                res.Message = $"Unable to save Card '{data.Name}'.\nError: {ex.InnerException?.Message ?? ex.Message}";

            }

            #endregion


            return res;
        }

        public async Task<GeneralResponseModel<object>> RemoveCard(Guid? Id)
        {
            GeneralResponseModel<object> res = new();
            var data = await _dataRepository.GetCards()
                .FirstOrDefaultAsync(x => x.Id == Id);


            if (data == null)
            {
                res.Message = "Card information does not exists, check & try again";
                return res;
            }

            try
            {
                await _customBaseRepository.CustomRemove(data);
                res.Message = $"Successfully removed Data '{data.Name}'";
                res.Success = true;

            }
            catch (Exception ex)
            {
                res.Message = $"Unable to remove Data.\nError: {ex.InnerException?.Message ?? ex.Message}";

            }

            return res;
        }
    }
}
