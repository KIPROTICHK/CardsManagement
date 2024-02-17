using CardsManagement.Application.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CardsManagement.Infrastructure.Service
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public async Task<PagedList<Card>> GetCards(FilterCardsRequest request)
        {
            var data = _dataRepository.GetCards();
            #region filtering data
            //data = data
            //.WhereIf(!string.IsNullOrEmpty(request.name), x => x.Programme.Code == request.ProgrammeCode);
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
    }
}
