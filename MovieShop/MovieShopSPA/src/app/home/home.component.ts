import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { UserService } from '../core/services/user.service';
import { MovieCard } from '../shared/models/movieCard';
import { MovieDetails } from '../shared/models/movieDetails';
import { UserDetails } from '../shared/models/userDetails';
import { UserFavorites } from '../shared/models/userFavorites';
import { UserPurchases } from '../shared/models/userPurchases';
import { UserReviews } from '../shared/models/userReviews';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  // data that I need to expose to the template/view
  movieCards!: MovieCard[];
  movieDetails!: MovieDetails;
  userDetails!: UserDetails;
  userPurchases!: UserPurchases;
  userFavorites!: UserFavorites;
  userReviews!: UserReviews;
  constructor(private movieService: MovieService, private userService: UserService) { }

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


    // call movie service get details for testing
    this.movieService.getMovieDetails(1).subscribe(
      m => {
        this.movieDetails = m;
      }
    );

    this.userService.getUserDetails(49822).subscribe(
      u => {
        this.userDetails = u;
      }
    );

    this.userService.getUserPurchases(49822).subscribe(
      u => {
        this.userPurchases = u;
      }
    );

    this.userService.getUserFavorites(49822).subscribe(
      u => {
        this.userFavorites = u;
      }
    );

    this.userService.getUserReviews(49822).subscribe(
      u => {
        this.userReviews = u;
      }
    );



  }

  // get the json data of the revenue movies and send the data to the view
  // models

}
