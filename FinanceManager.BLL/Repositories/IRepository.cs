using System.Linq.Expressions;

namespace FinanceManager.BLL.Repositories
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll(params Expression<Func<T, object>>[] includes);
        T? GetById(int id); //може повернути й  null
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
