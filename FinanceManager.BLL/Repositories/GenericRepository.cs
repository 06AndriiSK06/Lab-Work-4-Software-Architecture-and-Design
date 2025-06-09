using FinanceManager.DB;
using Microsoft.EntityFrameworkCore;
using PlacesDB;
using System.Linq.Expressions;

namespace FinanceManager.BLL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly FinanceContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(FinanceContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public List<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            return query.ToList();
        }


        public T? GetById(int id) => _dbSet.Find(id);

        public void Add(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
    }
}
