using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
     public interface IGenreService
     {
          Task<IEnumerable<GenreModel>> GetAllGenres();
          Task<List<MovieCardResponseModel>> GetMovieOfGenre(int Id);
     }
}
