using CompanyPortal.Application.Abstractions.Repositories;
using CompanyPortal.Domain.Entities.Common;
using CompanyPortal.Persistance.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CompanyPortal.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            var result = entityEntry.State == EntityState.Added;
            await SaveAsync();
            return result;
        }
        public bool Add(T entity)
        {
            EntityEntry<T> entityEntry = Table.Add(entity);
            var result = entityEntry.State == EntityState.Added;
            Save();
            return result;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            await SaveAsync();
            return true;
        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            var result = entityEntry.State == EntityState.Deleted;
            Save();
            return result;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            T entity = await Table.FirstOrDefaultAsync(x => x.Id == id);
            var result = Remove(entity);
            await SaveAsync();
            return result;

        }

        public bool RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            Save();
            return true;
        }

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            var result = entityEntry.State == EntityState.Modified;
            Save();
            return result;
        }

        public bool UpdateRange(List<T> entities)
        {
            Table.UpdateRange(entities);
            Save();
            return true;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public int Save()
            => _context.SaveChanges();
    }
}
