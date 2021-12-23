import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { HttpClient } from '@angular/common/http'
import { environment} from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }

  // Home component will call this method
  // Return list of movie card model
  // Observatables (Kind of like a Linq; async)
  getTopGrossingMovies() : Observable<MovieCard[]> {
    // we need to make a call to the API https://localhost:7224/api/Movies/toprevenue
    // HttpClient class comes from HttpClientModule in angular

    return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}Movies/toprevenue`);;
  }

}

// Dependency Injection
// var movies = _dbContext.Movies.Where(x => x.revenue > 10000).ToListAsync();

// YouTube Channel => ABC => posts videos
// You wanna get the notification when new video is posted

// two types of observatables => 1. Finite   2. Infinite
// 1. Http Call
// 2. Keep on sending stream of data until you catch the data (e.g. gaming)
