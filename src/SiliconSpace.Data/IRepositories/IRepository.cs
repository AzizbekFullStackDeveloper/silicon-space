namespace SiliconSpace.Data.IRepositories
{
    public interface IRepository<TEntity>
    {
        public Task<TEntity> AddAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task<bool> DeleteAsync(Guid Id);
        public Task<TEntity> SelectByIdAsync(Guid Id);
        public IQueryable<TEntity> SelectAll();
    }
}
