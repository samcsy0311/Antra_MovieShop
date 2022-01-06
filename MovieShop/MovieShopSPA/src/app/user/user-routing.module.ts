import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../core/guards/auth.guard';
import { EditUserComponent } from './edit-user/edit-user.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserComponent } from './user.component';

const routes: Routes = [
  {
    path:'', component: UserComponent, canActivate: [AuthGuard],

    children: [
      {path:'purchases', component: PurchasesComponent},
      {path:'favorites', component: FavoritesComponent},
      {path:'reviews', component: ReviewsComponent},
      {path:'details/:id', component: UserDetailsComponent},
      {path:'edit/:id', component: EditUserComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
