namespace ApplicationCore.Entities
{
     public class MovieCrew
     {
          public int MovieId { get; set; }
          public int CrewID { get; set; }
          public string Department { get; set; }
          public string Job { get; set; }

          public Movie Movie { get; set; }
          public Crew Crew { get; set; }
     }
}
