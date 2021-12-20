using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
     [Table("MovieGenre")]
     public class MovieGenre
     {
          public int MovieID { get; set; }
          public int GenreID { get; set; }
          public Movie Movie { get; set; }
          public Genre Genre { get; set; }
     }
}
