using Microsoft.EntityFrameworkCore;
using SiliconSpace.Data.DbContexts;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Domain.Commons;
using SiliconSpace.Domain.Entities;

namespace SiliconSpace.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        AppDbContext dbContext;
        DbSet<TEntity> dbSet;
        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }    

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var result = await dbContext.Set<TEntity>().Where(a => a.Id == Id).FirstOrDefaultAsync();
            
            result.StatusId = 2;
            
            await dbContext.SaveChangesAsync();
            return true;
        }

        public IQueryable<TEntity> SelectAll()
         => this.dbSet;
        public async Task<TEntity> SelectByIdAsync(Guid Id)
        {
            var result = await this.dbSet.Where(e => e.Id == Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = (dbContext.Update(entity)).Entity;
            await dbContext.SaveChangesAsync();
            return result;
        }

    }
}
