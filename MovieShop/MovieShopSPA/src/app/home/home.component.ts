import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/movieCard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  // data that I need to expose to the template/view
  movieCards!: MovieCard[];
  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    // one of the most important life cycle hooks methods in Angular
    // we use this method to make any API calls and initialize the data object
    this.movieService.getTopGrossingMovies().subscribe(
      m => {
        this.movieCards = m;
        //console.log("inside the subscription");
        //console.log(this.movieCards);
      }
    );
  }

  // get the json data of the revenue movies and send the data to the view
  // models

}
