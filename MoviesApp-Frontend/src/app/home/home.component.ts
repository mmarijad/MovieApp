import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MovieTmdb } from '../_models/MovieTmdb';
import { TmdbService } from '../_services/tmdb.service';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { MoviesDetailsService} from '../_services/movies-details.service';
import { UserService } from '../_services/user.service';
import { map } from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { MovieOmdb } from '../_models/MovieOmdb';
const APIKEY = environment.omdbApi;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  movies: Observable<MovieTmdb[]>;
  public tmdbMovies: MovieTmdb[];
  public searchTerm: string;
  public searchValueChanged: Subject<string> = new Subject<string>();
  name: string = '';
  apiResponse: Object;
  isSearching: boolean;
  isShowDiv: boolean;
  movieDetails: MovieOmdb;
  username: string;
  currentPage: number;
  private url = 'https://api.themoviedb.org/3/movie/';
  private searchUrl = 'https://api.themoviedb.org/3/search/movie';
  private language = 'en';


  constructor(private router: Router, private tmdbService: TmdbService, private userService: UserService,
    private movieDetailsService: MoviesDetailsService, private http: HttpClient) {}

  ngOnInit(): void {
    this.getMovies();
    this.username = this.userService.decodedToken?.unique_name;
    this.currentPage = 1;
    this.searchValueChanged.pipe(debounceTime(1000))
    .subscribe(() => {
      this.search();
    });
  }

    getMovies() {
      this.movies = this.tmdbService.getMovies();
    }

    public nextPage(){
      if (this.currentPage!==499){
        this.currentPage = this.currentPage+1;
        this.searchMovies();
      }
    }

    public previousPage(){
      if (this.currentPage!==1){
        this.currentPage = this.currentPage-1;
        this.searchMovies();
      }
    }

    public searchMovies() {
      this.searchValueChanged.next();
    }

    private search() {
      if (this.searchTerm !== '') {
       this.movies = this.tmdbService.searchMovies(this.searchTerm, this.currentPage);
      } else {
        this.movies = this.tmdbService.getMovies();
      }
    }

  public getDetails(movie: MovieTmdb){
    this.name = movie.title;
    this.isShowDiv = false;
    this.http.get(`http://www.omdbapi.com/?t=${this.name}&apikey=${APIKEY}`).pipe(
      map((data: MovieOmdb) => {return data as MovieOmdb})).subscribe(data => { 
          this.movieDetailsService.getDetails(data);
      });
    } 
}
