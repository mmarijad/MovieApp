import { Component, OnInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { MoviesService } from 'src/app/_services/movies.service';
import { ConfirmationDialogService } from 'src/app/_services/confirmation-dialog.service';
import { MoviesDetailsService } from 'src/app/_services/movies-details.service';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { Movie } from 'src/app/_models/Movie';
import { HttpClient, HttpParams } from "@angular/common/http";
import { MovieDetailsComponent } from 'src/app/movie-details/movie-details.component';

const APIKEY = "faeb787d";

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})

export class MovieListComponent implements OnInit {
  public movies: any;
  public listComplet: any;
  public searchTerm: string;
  public searchValueChanged: Subject<string> = new Subject<string>();
  movieDetails: any;
  name:string='';
  apiResponse: any;
  isSearching: boolean;
  isShowDiv: boolean;

  constructor(private router: Router,
              private service: MoviesService,
              private confirmationDialogService: ConfirmationDialogService,
              private movieDetailsService: MoviesDetailsService,
              private elementRef: ElementRef,
              private httpClient: HttpClient) {
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
        },
          err => {
          }))
      .catch(() => '');
  }

  public searchMovies() {
    this.searchValueChanged.next();
  }

  public getDetails(movie: Movie){
    this.name= movie.name;
    this.isShowDiv = false;
    this.httpClient.get(`http://www.omdbapi.com/?t=${movie.name}&apikey=${APIKEY}`)
    .subscribe(data=> {
    this.movieDetails=data;
    this.movieDetailsService.getDetails(this.movieDetails);
    })
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
