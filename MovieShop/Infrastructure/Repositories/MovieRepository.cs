using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
     public class MovieRepository : Repository<Movie>, IMovieRepository
     {
          public MovieRepository (MovieShopDbContext dbContext):base(dbContext)
          {

          }

          public async Task<IEnumerable<Movie>> Get30HighestRatedMovies()
          {
               var movies = await _dbContext.Movies
                    .Where(m => _dbContext.Reviews.GroupBy(r => r.MovieId)
                    .OrderByDescending(r => r.Average(x => x.Rating))
                    .Select(r => r.Key)
                    .Take(30)
                    .Contains(m.Id))
                    .ToListAsync();
               return movies;
          }

          public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
          {
               // we need to go to database and get the movies using Dapper or EF Core
               
               //access the dbContext object and dbset of movies object to query the movies table

               var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
               return movies;
          }

          public async override Task<Movie> GetById(int id)
          {
              // call the Movie dbset and also include the navigation properties such as 
              // Genres, Trailers, cast 
              // Include method in EF will help us navigate to related tables and get data
               var movieDetails = await _dbContext.Movies.Include(m=> m.MoviesCasts).ThenInclude(m=> m.Cast)
                    .Include(m=> m.GenresOfMovie).ThenInclude(m=> m.Genre).Include(m=> m.Trailers)
                    .FirstOrDefaultAsync(m=> m.Id == id);

               if (movieDetails == null) return null;

               var rating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                    .AverageAsync(r => r == null ? 0 : r.Rating);
               movieDetails.Rating = rating;
               return movieDetails;
          }
     }
}
