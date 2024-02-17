using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsManagement.Application.Repositories
{
    public interface IBaseRepository
    {
        Task CustomSaveChanges();
        Task CustomRemoveRange(IEnumerable<object> entities);
        Task CustomUpdateRange(IEnumerable<object> entities);
        Task CustomAddRange(IEnumerable<object> entities);
        Task<dynamic> CustomBeginTransaction();
        Task CustomAttachRange(IEnumerable<object> entities);
        Task CustomAdd(object entity);
        Task CustomUpdate(object entity);
        Task CustomAttach(object entity);
        Task CustomRemove(object entity);
        string GetConnectionstring();
    }
}
