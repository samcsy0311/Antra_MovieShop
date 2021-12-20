using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
     public class MovieService : IMovieService
     {
          //private MovieRepository _movieRepository;

          //public MovieService()
          //{
          //     _movieRepository = new MovieRepository();
          //}
          private readonly IMovieRepository _movieRepository;

          //Constructor Injection
          public MovieService(IMovieRepository movieRepository)
          {
               _movieRepository = movieRepository; // can be anything implementing the IMovieRepo
          }

          public async Task<IEnumerable<MovieCardResponseModel>> GetHighestGrossingMovies()
          {
               // call my MovieRepository and get the data 
               var movies = await _movieRepository.Get30HighestGrossingMovies();

               // 3rd party Automapper from 
               // manual way
               var movieCards = new List<MovieCardResponseModel>();
               foreach (var movie in movies)
               {
                    movieCards.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
               }

               return movieCards;
          }

          public async Task<MovieDetailsResponseModel> GetMovieDetailsById(int id)
          {
               var movie = await _movieRepository.GetById(id);

               // map movie entity into Movie Details Model
               // Automapper that can be used for mapping one object to another object

               var movieDetails = new MovieDetailsResponseModel
               {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title,
                    OriginalLanguage = movie.OriginalLanguage,
                    Overview = movie.Overview,
                    Rating = movie.Rating,
                    Tagline = movie.Tagline,
                    RunTime = movie.RunTime,
                    BackdropUrl = movie.BackdropUrl,
                    TmdbUrl = movie.TmdbUrl,
                    ImdbUrl = movie.ImdbUrl,
                    Price = movie.Price,
                    ReleaseDate = movie.ReleaseDate,
                    Revenue = movie.Revenue,
                    Budget = movie.Budget
               };

               foreach (var movieCast in movie.MoviesCasts)
               {
                    movieDetails.Casts.Add(new CastResponseModel
                    {

                         Id = movieCast.CastId,
                         Character = movieCast.Character,
                         Name = movieCast.Cast.Name,
                         PosterUrl = movieCast.Cast.ProfilePath
                    });
               }

               foreach (var trailer in movie.Trailers)
               {
                    movieDetails.Trailers.Add(new TrailerResponseModel
                    {
                         Id = trailer.Id, MovieId = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl
                    });
               }

               foreach (var movieGenres in movie.GenresOfMovie)
               {
                    movieDetails.Genres.Add(new GenreModel { Id = movieGenres.GenreID, Name = movieGenres.Genre.Name });
               }

               return movieDetails;

          }
     }
}
