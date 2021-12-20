using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
     public interface IGenreRepository : IRepository<Genre>
     {
          Task<IEnumerable<Movie>> GetGenreMovies(int Id);
     }
}
