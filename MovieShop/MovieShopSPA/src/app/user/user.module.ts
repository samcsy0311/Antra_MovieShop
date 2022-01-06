import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { ReviewsComponent } from './reviews/reviews.component';


@NgModule({
  declarations: [
    UserComponent,
    PurchasesComponent,
    FavoritesComponent,
    UserDetailsComponent,
    EditUserComponent,
    ReviewsComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule
  ]
})
export class UserModule { }
