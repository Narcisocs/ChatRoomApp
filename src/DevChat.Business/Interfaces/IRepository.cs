using DevChat.Business.Models;
using System.Linq.Expressions;

namespace DevChat.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(long id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Delete(long id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
