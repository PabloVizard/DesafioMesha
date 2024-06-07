using Domain.Repositories.Interfaces;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BaseRepository<Entity> : IBaseRepository<Entity>, IDisposable where Entity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<Entity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }

        public bool Any(Expression<Func<Entity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public IQueryable<Entity> AsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public Entity Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<Entity> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Entity FindBy(Expression<Func<Entity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public async Task<Entity> FindByAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public List<Entity> List()
        {
            return _dbSet.ToList();
        }

        public async Task<List<Entity>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public List<Entity> List(Expression<Func<Entity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public async Task<List<Entity>> ListAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public IQueryable<Entity> Query()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<Entity> Add(Entity entity)
        {
            var save = await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return save.Entity;
        }

        public void AddRange(IEnumerable<Entity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Remove(Entity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Entity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(Entity entity)
        {
            _dbSet.Update(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion
    }
}
