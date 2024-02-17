using Microsoft.EntityFrameworkCore;

namespace CardsManagement.Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async Task CustomSaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        public async Task CustomRemoveRange(IEnumerable<object> entities)
        {
            _context.RemoveRange(entities);
            await CustomSaveChanges();
        }

        public async Task CustomRemove(object entity)
        {
            _context.Remove(entity);
            await CustomSaveChanges();
        }

        public async Task CustomUpdateRange(IEnumerable<object> entities)
        {
            _context.UpdateRange(entities);
            await CustomSaveChanges();
        }
        public async Task CustomAttachRange(IEnumerable<object> entities)
        {
            _context.AttachRange(entities);
            await CustomSaveChanges();
        }

        public async Task CustomAddRange(IEnumerable<object> entities)
        {
            _context.AddRange(entities);
            await CustomSaveChanges();
        }

        public async Task CustomAdd(object entity)
        {
            _context.Add(entity);
            await CustomSaveChanges();
        }

        public async Task CustomUpdate(object entity)
        {
            _context.Update(entity);
            await CustomSaveChanges();
        }
        public async Task CustomAttach(object entity)
        {
            _context.Attach(entity);
            await CustomSaveChanges();
        }

        public async Task<dynamic> CustomBeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public string GetConnectionstring()
        {
            return _context.Database.GetConnectionString();
        }
    }
}
