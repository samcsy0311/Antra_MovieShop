using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
     public class GenreService : IGenreService
     {
          private readonly IGenreRepository _genreRepository;
          private readonly IMemoryCache _memoryCache;
          private static readonly string _genresCacheKey = "genres";
          private static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromDays(7);

          public GenreService (IGenreRepository genreRepository, IMemoryCache memoryCache)
          {
               _genreRepository = genreRepository;
               _memoryCache = memoryCache;
          }
          public async Task<IEnumerable<GenreModel>> GetAllGenres()
          {
               // this is the database call
               // first check the cache if the genres are already in the cache 

               // if the genre already present in the memory, then we don't need to go to database
               // just read the genre in the cache

               // if the genre is not present in the memory, then go to database and get the genres
               // then store them in the cache

               // make sure the cache is not expired then get the data
               // when we update/create any new genre, then we call the cache and delete from cache

               var genresFromCache = await _memoryCache.GetOrCreateAsync(_genresCacheKey, CacheFactory);
               // Func<int, int, string>     Last one is the return type
               // Action<int, int, string>   All parameters
               return genresFromCache;
          }

          public async Task<List<MovieCardResponseModel>> GetMovieOfGenre(int Id)
          {
               var movie = await _genreRepository.GetGenreMovies(Id);

               var movieCards = new List<MovieCardResponseModel>();
               foreach (var item in movie)
               {
                    movieCards.Add(new MovieCardResponseModel { Id = item.Id, PosterUrl = item.PosterUrl, Title = item.Title });
               }
               return movieCards;
          }

          private async Task<IEnumerable<GenreModel>> CacheFactory(ICacheEntry entry)
          {
               entry.SlidingExpiration = DefaultCacheDuration;
               var genres = await _genreRepository.GetAll();
               var genreModel = new List<GenreModel>();
               foreach (var genre in genres)
               {
                    genreModel.Add(new GenreModel { Id = genre.Id, Name = genre.Name });

               }
               return genreModel;
          }
    }
}
