import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './core/layout/header/header.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {path:"", component: HomeComponent},
  {path:"account/login", component: LoginComponent},
  {path:"account/register", component: RegisterComponent},

  // lazily load the movie modules only when user goes to http://localhost:4200/movies /details/2 /cast/2 path
  {path:"movies", loadChildren: () => import("./movies/movies.module").then(mod => mod.MoviesModule)},
  {path:"user", loadChildren:() => import("./user/user.module").then(mod => mod.UserModule), canLoad: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
