using DevChat.Business.Interfaces;
using DevChat.Business.Models;

namespace DevChat.Business.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Entity, new()
    {
        protected readonly IRepository<TEntity> _repository;

        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task Add(TEntity entity)
        {
            await _repository.Add(entity);
        }

        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<TEntity>> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Find(predicate);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TEntity> GetById(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<int> SaveChanges()
        {
            return await _repository.SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            await _repository.Update(entity);
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
