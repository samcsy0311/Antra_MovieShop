using ApplicationCore.RepositoryInterfaces;
using Infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
     public class Repository<T> : IRepository<T> where T : class
     {
          protected readonly MovieShopDbContext _dbContext;

          public Repository(MovieShopDbContext dbContext)
          {
               _dbContext = dbContext;
          }

          public async Task<T> Add(T entity)
          {
               _dbContext.Set<T>().Add(entity);
               await _dbContext.SaveChangesAsync();
               return entity;
          }

          public async Task<T> Delete(int id)
          {
               var entity = await GetById(id);
               _dbContext.Set<T>().Remove(entity);
               await _dbContext.SaveChangesAsync();
               return entity;
          }

          public virtual async Task<List<T>> GetAll()
          {
               return await _dbContext.Set<T>().ToListAsync();
          }

          public virtual async Task<T> GetById(int id)
          {
               var entity = await _dbContext.Set<T>().FindAsync(id);
               return entity;
          }

          public virtual async Task<T> Update(T entity)
          {
               throw new NotImplementedException();
          }
     }
}
