using Application.Applications.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Services.Interfaces;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class BaseApp<Entity, Model> : IBaseApp<Entity, Model> where Entity : BaseEntity where Model : BaseModel
    {
        private readonly IBaseService<Entity> _baseService;
        private readonly IMapper _mapper;

        public BaseApp(IBaseService<Entity> baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }

        public bool Any(Expression<Func<Entity, bool>> predicate)
        {
            return _baseService.Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _baseService.AnyAsync(predicate);
        }
        public IQueryable<Entity> AsNoTracking()
        {
            return _baseService.AsNoTracking();
        }


        public Entity Find(int id)
        {
            return _baseService.Find(id);
        }

        public async Task<Entity> FindAsync(int id)
        {
            return await _baseService.FindAsync(id);
        }

        public Entity FindBy(Expression<Func<Entity, bool>> predicate)
        {
            return _baseService.FindBy(predicate);
        }

        public async Task<Entity> FindByAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _baseService.FindByAsync(predicate);
        }

        public List<Entity> List()
        {
            return _baseService.List();
        }

        public async Task<List<Entity>> ListAsync()
        {
            return await _baseService.ListAsync();
        }

        public List<Entity> List(Expression<Func<Entity, bool>> predicate)
        {
            return _baseService.List(predicate);
        }

        public async Task<List<Entity>> ListAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _baseService.ListAsync(predicate);
        }

        public IQueryable<Entity> Query()
        {
            return _baseService.Query();
        }

        public virtual async Task<Entity> Add(Entity dado)
        {
            return await _baseService.Add(_mapper.Map<Entity>(dado));
        }

        public void AddRange(IEnumerable<Entity> entities)
        {
            _baseService.AddRange(entities);
        }

        public void Remove(Entity entity)
        {
            _baseService.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Entity> entities)
        {
            _baseService.RemoveRange(entities);
        }

        public void Update(Entity dado)
        {
            _baseService.Update((dado));
        }

        public async Task SaveChangesAsync()
        {
            await _baseService.SaveChangesAsync();
        }
    }
}
