import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';

import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { CategoryComponent } from './categories/category/category.component';
import { CategoryListComponent } from './categories/category-list/category-list.component';
import { DirectorComponent } from './directors/director/director.component';
import { DirectorListComponent } from './directors/director-list/director-list.component';
import { MovieComponent } from './movies/movie/movie.component';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';

import { MoviesService } from './_services/movies.service';
import { CategoryService } from './_services/category.service';
import { ConfirmationDialogService } from './_services/confirmation-dialog.service';
import { DirectorService } from './_services/director.service';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { MoviesDetailsService } from './_services/movies-details.service';
import { RegisterComponent } from './register/register.component';
import { FormComponent } from './form/form.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    CategoryListComponent,
    DirectorComponent,
    DirectorListComponent,
    MovieComponent,
    MovieListComponent,
    HomeComponent,
    NavComponent,
    ConfirmationDialogComponent,
    MovieDetailsComponent,
    RegisterComponent,
    FormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    MoviesService,
    CategoryService,
    DirectorService,
    ConfirmationDialogService,
    MoviesDetailsService,
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }