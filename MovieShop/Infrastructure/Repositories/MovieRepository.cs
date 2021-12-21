using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Infrastructure.Repositories
{
     public class MovieRepository : Repository<Movie>, IMovieRepository
     {
          public MovieRepository (MovieShopDbContext dbContext):base(dbContext)
          {

          }

          public async override Task<Movie> Update(Movie movie)
          {
               var _movie = await _dbContext.Movies.Where(m => m.Id == movie.Id).FirstOrDefaultAsync();

               if (_movie == null) return null;

               if (movie.Title != null) _movie.Title = movie.Title;
               if (movie.Overview != null) _movie.Overview = movie.Overview;
               if (movie.Tagline != null) _movie.Tagline = movie.Tagline;
               if (movie.Revenue != 0) _movie.Revenue = movie.Revenue;
               if (movie.Budget != 0) _movie.Budget = movie.Budget;
               if (movie.ImdbUrl != null) _movie.ImdbUrl = movie.ImdbUrl;
               if (movie.TmdbUrl != null) _movie.TmdbUrl = movie.TmdbUrl;
               if (movie.PosterUrl != null) _movie.PosterUrl = movie.PosterUrl;
               if (movie.BackdropUrl != null) _movie.BackdropUrl = movie.BackdropUrl;
               if (movie.OriginalLanguage != null) _movie.OriginalLanguage = movie.OriginalLanguage;
               if (movie.ReleaseDate != null) _movie.ReleaseDate = movie.ReleaseDate;
               if (movie.RunTime != null) _movie.RunTime = movie.RunTime;
               if (movie.Price != null) _movie.Price = movie.Price;
               if (movie.GenresOfMovie != null) _movie.GenresOfMovie = movie.GenresOfMovie;

               await _dbContext.SaveChangesAsync();
               return _movie;
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
