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
        //public async Task<PagedList<NicheBeneficiary>> GetNicheBeneficiaries(NicheBeneficiariesRequest request)
        //{
        //    var data = _dataRepository.GetNicheBeneficiaries();
        //    #region filtering data
        //    data = data
        //        .WhereIf(!string.IsNullOrEmpty(request.ProgrammeCode), x => x.Programme.Code == request.ProgrammeCode);
        //    //    .WhereIf(!string.IsNullOrEmpty(request.AccountType), x => x.AccountType == request.AccountType);
        //    ////    .WhereIf(request.Ids != null && request.Ids.Count > 0, x => request.Ids.Contains(x.Id));

        //    int OriginalCount = data.Count();
        //    if (!string.IsNullOrEmpty(request.SearchTerm))
        //    {

        //        string SearchTerm = request?.SearchTerm?.Trim()?.ToLower();
        //        data = data.WhereIf(true,
        //          x =>
        //          x.EligibleMembers.ToString().ToLower().Contains(SearchTerm)
        //          || x.TopUpAmount.ToString().ToLower().Contains(SearchTerm)
        //          //|| x.Email.ToLower().Contains(SearchTerm)
        //          //|| x.PhoneNumber.ToLower().Contains(SearchTerm)
        //          //|| x.PhysicalAddress.ToLower().Contains(SearchTerm)
        //          || x.ExitedOn.ToString().ToLower().Contains(SearchTerm)
        //          || x.ExitOn.ToString().ToLower().Contains(SearchTerm)

        //          );

        //    }

        //    if (!string.IsNullOrEmpty(request.SortColumn))
        //    {
        //        data = data.OrderByDynamic(request.SortColumn, request.SortColumnDirection);

        //    }

        //    #endregion

        //    data = data.AsNoTrackingWithIdentityResolution();

        //    data = data.OrderByDescending(x => x.RegisteredOn).ThenByDescending(x => x.ExitedOn);
        //    return await Task.FromResult(PagedList<NicheBeneficiary>.ToPagedList(data, request.PageNumber, request.PageSize, OriginalCount));

        //}
    }
}
