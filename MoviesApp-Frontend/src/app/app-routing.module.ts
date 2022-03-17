import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { MovieComponent } from './movies/movie/movie.component';
import { CategoryListComponent } from './categories/category-list/category-list.component';
import { CategoryComponent } from './categories/category/category.component';
import { DirectorListComponent } from './directors/director-list/director-list.component';
import { DirectorComponent } from './directors/director/director.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { LoginComponent } from './login/login.component';

const routes: Routes = [

  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent},
  {
    path: '', runGuardsAndResolvers: 'always', canActivate: [AuthGuard],
    children: 
    [ 
    { path: 'home', component: HomeComponent },
    { path: 'movies', component: MovieListComponent },
    { path: 'movie', component: MovieComponent },
    { path: 'movie/:id', component: MovieComponent },
    { path: 'categories', component: CategoryListComponent },
    { path: 'category', component: CategoryComponent },
    { path: 'category/:id', component: CategoryComponent },
    { path: 'directors', component: DirectorListComponent },
    { path: 'director', component: DirectorComponent },
    { path: 'director/:id', component: DirectorComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
   ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }