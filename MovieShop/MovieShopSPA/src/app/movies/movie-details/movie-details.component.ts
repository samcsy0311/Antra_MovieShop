import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieDetails } from 'src/app/shared/models/movieDetails';
import { MovieService } from 'src/app/core/services/movie.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  id: Number = 0;
  movie!: MovieDetails;
  constructor(private route:ActivatedRoute, private movieService:MovieService, public datepipe: DatePipe) { }

  ngOnInit(): void {
    // get the movie id from the current URL and call MovieService and show the movie details

    this.route.paramMap.subscribe(
      p => {
        this.id = Number(p.get('id'));
        console.log('MovieId: ' + this.id);
        // get the movie id from the current URL and call Movie Service and show the movie details
        this.movieService.getMovieDetails(this.id).subscribe(
          m => {
            this.movie = m;
            this.movie.rating = Math.round(m.rating*100)/100;
            this.movie.releaseDateWithoutTime = this.datepipe.transform(m.releaseDate, 'MMM d, yyyy') || undefined;
            this.movie.releaseYear = new Date(m.releaseDate).getFullYear();
            this.movie.revenue = Math.round(m.revenue*1000)/1000;
            this.movie.budget = Math.round(m.budget*1000)/1000;
            console.log(this.movie);
          }
        );
      }
    );
  }

}
