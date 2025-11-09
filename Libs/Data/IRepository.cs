using System.Linq.Expressions;

namespace Libs.Data
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        int Count(Expression<Func<T, bool>> where);
        T GetById(object id);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int skip = 0,
            int take = 0);
        T GetByCondition(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        bool Any(Expression<Func<T, bool>> where);
        void Save();
        Task<T?> GetByIdAsync(Guid id);

    }
}
