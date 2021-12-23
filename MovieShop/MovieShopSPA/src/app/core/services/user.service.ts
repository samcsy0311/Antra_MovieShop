import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserDetails } from 'src/app/shared/models/userDetails';
import { UserFavorites } from 'src/app/shared/models/userFavorites';
import { UserPurchases } from 'src/app/shared/models/userPurchases';
import { UserReviews } from 'src/app/shared/models/userReviews';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  // get user details by id
  getUserDetails(id: number) : Observable<UserDetails>{
    return this.http.get<UserDetails>(`${environment.apiBaseUrl}Account/${id}`);
  }

  // get movie purchased by user
  getUserPurchases(id: number) : Observable<UserPurchases>{
    return this.http.get<UserPurchases>(`${environment.apiBaseUrl}User/${id}/purchases`);
  }

  // get favorites by user
  getUserFavorites(id: number) : Observable<UserFavorites>{
    return this.http.get<UserFavorites>(`${environment.apiBaseUrl}User/${id}/favorites`);
  }

  //get reviews by user
  getUserReviews(id: number) : Observable<UserReviews>{
    return this.http.get<UserReviews>(`${environment.apiBaseUrl}User/${id}/reviews`);
  }
}
