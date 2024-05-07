using System.Linq.Expressions;

namespace PostSomething_api.Repositories.Interface
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> AsQueryable();
        IQueryable<TEntity> GetList();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(Guid gui, TEntity entity);
        Task Delete(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate);
    }
}