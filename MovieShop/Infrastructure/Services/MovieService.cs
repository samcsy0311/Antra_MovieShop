using ApplicationCore.Entities;
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

          public async Task<IEnumerable<Movie>> GetAllMovies()
          {
               return await _movieRepository.GetAll();
          }

          public async Task<int> AddMovie(MovieCreateRequest movieCreateRequest)
          {
               var getMovie = await _movieRepository.GetById(movieCreateRequest.Id.Value);
               if (getMovie != null) return -1;

               var genres = new List<MovieGenre>();
               foreach (var genre in movieCreateRequest.Genre)
               {
                    genres.Add(new MovieGenre
                    {
                         GenreID = genre
                    });
               }

               var movie = new Movie
               {
                    Title = movieCreateRequest.Title,
                    Overview = movieCreateRequest.Overview,
                    Tagline = movieCreateRequest.Tagline,
                    Revenue = movieCreateRequest.Revenue,
                    Budget = movieCreateRequest.Budget,
                    ImdbUrl = movieCreateRequest.ImdbUrl,
                    TmdbUrl = movieCreateRequest.TmdbUrl,
                    PosterUrl = movieCreateRequest.PosterUrl,
                    BackdropUrl = movieCreateRequest.BackdropUrl,
                    OriginalLanguage = movieCreateRequest.OriginalLanguage,
                    ReleaseDate = movieCreateRequest.ReleaseDate,
                    RunTime = movieCreateRequest.RunTime,
                    Price = movieCreateRequest.Price,
                    GenresOfMovie = genres
               };

               var newMovie = await _movieRepository.Add(movie);
               if (newMovie == null) return -1;
               return newMovie.Id;
          }

          public async Task<int> UpdateMovie(MovieCreateRequest movieCreateRequest) 
          {
               var getMovie = await _movieRepository.GetById(movieCreateRequest.Id.Value);
               if (getMovie == null) return await AddMovie(movieCreateRequest);

               var genres = new List<MovieGenre>();
               foreach (var genre in movieCreateRequest.Genre)
               {
                    genres.Add(new MovieGenre
                    {
                         MovieID = movieCreateRequest.Id.Value,
                         GenreID = genre
                    });
               }

               var movie = new Movie
               {
                    Id = movieCreateRequest.Id.Value,
                    Title = movieCreateRequest.Title,
                    Overview = movieCreateRequest.Overview,
                    Tagline = movieCreateRequest.Tagline,
                    Revenue = movieCreateRequest.Revenue,
                    Budget = movieCreateRequest.Budget,
                    ImdbUrl = movieCreateRequest.ImdbUrl,
                    TmdbUrl = movieCreateRequest.TmdbUrl,
                    PosterUrl = movieCreateRequest.PosterUrl,
                    BackdropUrl = movieCreateRequest.BackdropUrl,
                    OriginalLanguage = movieCreateRequest.OriginalLanguage,
                    ReleaseDate = movieCreateRequest.ReleaseDate,
                    RunTime = movieCreateRequest.RunTime,
                    Price = movieCreateRequest.Price,
                    GenresOfMovie = genres
               };

               var newMovie = await _movieRepository.Update(movie);
               return newMovie.Id;
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

          public async Task<IEnumerable<MovieCardResponseModel>> GetHighestRatedMovies()
          {
               var movies = await _movieRepository.Get30HighestRatedMovies();
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
