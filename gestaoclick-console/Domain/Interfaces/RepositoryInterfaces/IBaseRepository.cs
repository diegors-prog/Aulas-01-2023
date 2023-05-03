using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IBaseRepository<Entity> : IDisposable where Entity : class
    {
         Task<IList<Entity>> SearchAll(Expression<Func<Entity, bool>> predicate);
         Task<Entity> GetByIdAsync(int entityId);
         Task<IList<Entity>> GetAllAsync();
         void Save(Entity entity);
         bool Delete(int entityId);
         void Update(Entity entity);
    }
}