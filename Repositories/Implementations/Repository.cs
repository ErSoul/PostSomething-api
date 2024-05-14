using Microsoft.EntityFrameworkCore;
using PostSomething_api.Database;
using PostSomething_api.Repositories.Interface;
using System.Linq.Expressions;

namespace PostSomething_api.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly ApplicationContext _context;
        protected readonly ILogger<Repository<TEntity>> _logger;

        public Repository(ApplicationContext context, ILogger<Repository<TEntity>> _logger)
        {
            _context = context;
            this._logger = _logger;
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public IQueryable<TEntity> GetList()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> UpdateAsync(Guid gui, TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().FirstOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().Where(predicate).ToListAsync();
        }
    }
}