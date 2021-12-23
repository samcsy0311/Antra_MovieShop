import { Component, Input, OnInit } from '@angular/core';
import { MovieCard } from '../../models/movieCard';
import { MovieDetails } from '../../models/movieDetails';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {

  @Input() movieCards!: MovieCard;
  @Input() testInfo!: string;
  @Input() movieDetails!: MovieDetails;
  constructor() { }


  ngOnInit(): void {
    console.log('inside the child component');
    console.log(this.movieCards);
    console.log(this.testInfo);
  }

}
