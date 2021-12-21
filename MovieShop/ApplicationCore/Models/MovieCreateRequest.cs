using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
     public class MovieCreateRequest
     {
          public int? Id { get; set; }
          [MaxLength(150)]
          public string? Title { get; set; }
          [MaxLength(2084)]
          public string? Overview { get; set; }
          [MaxLength(2084)]
          public string? Tagline { get; set; }
          public decimal? Revenue { get; set; }
          public decimal? Budget { get; set; }
          public string? ImdbUrl { get; set; }
          public string? TmdbUrl { get; set; }
          public string? PosterUrl { get; set; }
          public string? BackdropUrl { get; set; }
          public string? OriginalLanguage { get; set; }
          public DateTime? ReleaseDate { get; set; }
          public int? RunTime { get; set; }
          public decimal? Price { get; set; }
          public List<int> Genre { get; set; }
     }
}
