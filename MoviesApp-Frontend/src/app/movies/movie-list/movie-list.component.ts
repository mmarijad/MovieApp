import { Component, OnInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { MoviesService } from 'src/app/_services/movies.service';
import { ConfirmationDialogService } from 'src/app/_services/confirmation-dialog.service';
import { MoviesDetailsService } from 'src/app/_services/movies-details.service';
import { Subject } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { Movie } from 'src/app/_models/Movie';
import { HttpClient, HttpParams } from "@angular/common/http";
import { ToastrService } from 'ngx-toastr';
import { TmdbService } from 'src/app/_services/tmdb.service';
import { MovieTmdb } from 'src/app/_models/MovieTmdb';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

const APIKEY = environment.omdbApi;

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})

export class MovieListComponent implements OnInit {
  public movies: Movie[];
  public listComplet: Movie[];
  public searchTerm: string;
  public tmdbMovies: Observable<MovieTmdb[]>;
  public searchValueChanged: Subject<string> = new Subject<string>();
  movieDetails: Object;
  name: string = '';
  apiResponse: Object;
  isSearching: boolean;
  isShowDiv: boolean;

  constructor(private router: Router,
              private service: MoviesService,
              private confirmationDialogService: ConfirmationDialogService,
              private movieDetailsService: MoviesDetailsService,
              private toastr: ToastrService) {
                this.isSearching = false;
                this.apiResponse = [];
                this.movieDetails = [];
              }
              
  ngOnInit() {
    this.getValues();
    this.searchValueChanged.pipe(debounceTime(1000))
    .subscribe(() => {
      this.search();
    });
  }

  private getValues() {
    this.service.getMovies().subscribe(movies => {
      this.movies = movies;
      this.listComplet = movies;
    });
  }

  public addMovie() {
    this.router.navigate(['/movie']);
  }

  public updateMovie(movieId: number) {
    this.router.navigate(['/movie/' + movieId]);
  }

  public deleteMovie(movieId: number) {
    this.confirmationDialogService.confirm('', 'Jeste li sigurni da želite obrisati ovaj film?')
      .then(() =>
        this.service.deleteMovie(movieId).subscribe(() => {
          this.getValues();
          this.toastr.success('Uspješno ste izbrisali film.');
        },
          error => {
            this.toastr.error('Došlo je do pogreške pri brisanju filma.');
          }))
      .catch(() => '');
  }

  public searchMovies() {
    this.searchValueChanged.next();
  }

public goToHome(){
  this.router.navigate(['/home']);
}

  public getDetails(movie: Movie){
    this.name = movie.name;
    this.isShowDiv = false;
    this.movieDetailsService.getDetailsFromApi(this.name);
    } 
    
  private search() {
    if (this.searchTerm !== '') {
      this.service.searchMoviesWithCategoryAndDirector(this.searchTerm).subscribe(movie => {
        this.movies = movie;
      }, error => {
        this.movies = [];
      });
    } else {
      this.service.getMovies().subscribe(movies => this.movies = movies);
    }
  }
}
