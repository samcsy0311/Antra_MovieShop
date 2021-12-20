using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
     public class GenreRepository : Repository<Genre>, IGenreRepository 
     {
          public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
          {

          }

          public async override Task<List<Genre>> GetAll()
          {
               return await _dbContext.Set<Genre>().OrderBy(g => g.Name).ToListAsync();
          }

          public async Task<IEnumerable<Movie>> GetGenreMovies(int Id)
          {
               var movie = await _dbContext.Movies.Where(
                    x => _dbContext.MovieGenres.Where(g => g.GenreID == Id).Select(g => g.MovieID).Contains(x.Id))
                    .ToListAsync();

               return movie;
          }
    }
}
