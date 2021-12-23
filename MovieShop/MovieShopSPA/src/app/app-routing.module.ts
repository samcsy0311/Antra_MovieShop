import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:"", component: HomeComponent},
  {path:"account/login", component: LoginComponent},
  {path:"account/register", component: RegisterComponent},

  // lazily load the movie modules only when user goes to http://localhost:4200/movies /details/2 /cast/2 path
  {path:"movies", loadChildren: () => import("./movies/movies.module").then(mod => mod.MoviesModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
