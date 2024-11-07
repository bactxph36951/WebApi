using Datas.Data;
using Datas.Entities;

namespace Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);

    }
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly WebApiDbContext _context;

        public Repository(WebApiDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public async Task<int> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
