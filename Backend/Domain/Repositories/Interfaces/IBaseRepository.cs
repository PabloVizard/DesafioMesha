
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Interfaces
{
    public interface IBaseRepository<Entity> where Entity : BaseEntity
    {
        bool Any(Expression<Func<Entity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate);

        IQueryable<Entity> AsNoTracking();

        Entity Find(int id);
        Task<Entity> FindAsync(int id);

        Entity FindBy(Expression<Func<Entity, bool>> predicate);
        Task<Entity> FindByAsync(Expression<Func<Entity, bool>> predicate);

        List<Entity> List();
        Task<List<Entity>> ListAsync();

        List<Entity> List(Expression<Func<Entity, bool>> predicate);
        Task<List<Entity>> ListAsync(Expression<Func<Entity, bool>> predicate);

        IQueryable<Entity> Query();

        Task<Entity> Add(Entity entity);
        void AddRange(IEnumerable<Entity> entities);

        void Remove(Entity entity);
        void RemoveRange(IEnumerable<Entity> entities);

        void Update(Entity entity);

        void SaveChanges();
        Task SaveChangesAsync();

    }
}
