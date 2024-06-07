using AutoMapper;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class BaseService<Entity> : IBaseService<Entity> where Entity : BaseEntity
    {
        private readonly IBaseRepository<Entity> _repository;
        private readonly IMapper _mapper;

        protected BaseService(IBaseRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual bool Any(Expression<Func<Entity, bool>> predicate)
        {
            return _repository.Any(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _repository.AnyAsync(predicate);
        }
        public IQueryable<Entity> AsNoTracking()
        {
            return _repository.AsNoTracking();
        }

        public virtual Entity Find(int id)
        {
            return _repository.Find(id);
        }

        public virtual async Task<Entity> FindAsync(int id)
        {
            return await _repository.FindAsync(id);
        }

        public virtual Entity FindBy(Expression<Func<Entity, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public virtual async Task<Entity> FindByAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _repository.FindByAsync(predicate);
        }

        public virtual List<Entity> List()
        {
            return _repository.List();
        }

        public virtual async Task<List<Entity>> ListAsync()
        {
            return await _repository.ListAsync();
        }

        public virtual List<Entity> List(Expression<Func<Entity, bool>> predicate)
        {
            return _repository.List(predicate);
        }

        public virtual async Task<List<Entity>> ListAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _repository.ListAsync(predicate);
        }

        public virtual IQueryable<Entity> Query()
        {
            return _repository.Query();
        }

        public virtual async Task<Entity> Add(Entity entity)
        {
            return await _repository.Add(entity);
        }

        public virtual void AddRange(IEnumerable<Entity> entities)
        {
            _repository.AddRange(entities);
        }

        public virtual void Remove(Entity entity)
        {
            _repository.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<Entity> entities)
        {
            _repository.RemoveRange(entities);
        }

        public virtual void Update(Entity entity)
        {
            _repository.Update(entity);
        }

        public virtual async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }
    }
}

