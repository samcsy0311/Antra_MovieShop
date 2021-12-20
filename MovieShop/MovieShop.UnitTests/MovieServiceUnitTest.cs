using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace MovieShop.UnitTests
{
     [TestClass]
     public class MovieServiceUnitTest
     {
          private MovieService _sut;
          private static List<Movie> _movies;
          private Mock<IMovieRepository> _mockMovieRepository;

          [TestInitialize]
          // [OneTimeSetUp] in NUnit
          public void OneTimeSetUp()
          {
               _mockMovieRepository = new Mock<IMovieRepository>();

               // SUT System under test 
               // MovieService => GetTopRevenueMovies

               _sut = new MovieService(_mockMovieRepository.Object);
               _mockMovieRepository.Setup(m => m.Get30HighestGrossingMovies()).ReturnsAsync(_movies);

              
          }

          [ClassInitialize]
          public static void SetUp(TestContext context)
          {
               _movies = new List<Movie>
               {
                    new Movie { Id = 1, Title = "Avengers: Infinity War", Budget = 1200000 },
                    new Movie { Id = 16, Title = "Furious 7", Budget = 1200000}
               };
          }

          [TestMethod]
          public async void TestListOfHighestGrossingMoviesFromFakeData()
          {
               // SUT System under Test
               // MovieService -> GetHighestGrossingMovies()

               // Arrange 
               // mock objects, data, methods, etc.  
               // _sut = new MovieService(new MockMovieRepository());

               // Act
               var movies = await _sut.GetHighestGrossingMovies();

               // check the actual output with expected data
               // AAA
               // Arrange, Act, Assert

               // Assert
               Assert.IsNotNull(movies);
               Assert.IsInstanceOfType(movies, typeof(IEnumerable<MovieCardResponseModel>));
               Assert.AreEqual(16, movies.Count());
          }
     }

     
}