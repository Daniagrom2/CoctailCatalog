using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BaseRepository<T> where T: class
    {
        private DbContext context;
        private DbSet<T> dbSet;
        public BaseRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll() => dbSet;
        public async Task<IEnumerable<T>> GetAllAsync() => await Task.Run(() => GetAll());
        public T Get(int id) => dbSet.Find(id);

        public async Task<T> GetAsync(int id)
        {
            return await Task<T>.Run(() => Get(id));
        }
        public void CreateOrUpdate(T entity) => dbSet.AddOrUpdate(entity);
        public async Task CreateOrUpdateAsync(T entity) => await Task.Run(() => CreateOrUpdate(entity));
        public void Delete(T entity) => dbSet.Remove(entity);
        public Task DeleteAsync(T entity)
        {
            Task.Run(() => Delete(entity));
            return Task.CompletedTask;
        }

        public void Save() => context.SaveChanges();
        public async Task SaveAsync() => await Task.Run(() => Save());
    }
}