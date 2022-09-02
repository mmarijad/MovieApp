import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieTmdb } from '../_models/MovieTmdb';
import { TmdbService } from '../_services/tmdb.service';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { MoviesDetailsService} from '../_services/movies-details.service';
import { UserService } from '../_services/user.service';
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

  constructor(  private tmdbService: TmdbService, 
                private userService: UserService,
                private movieDetailsService: MoviesDetailsService) {}

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
      this.movieDetailsService.getDetailsFromApi(this.name);
    } 
}
