import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse  } from '@angular/common/http';
import { Genre, MovieTmdb } from '../_models/MovieTmdb';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'
import { environment } from 'src/environments/environment';
const API_KEY = environment.tmdbApi;

@Injectable({
    providedIn: 'root'
})

export class TmdbService {
    private url =              'https://api.themoviedb.org/3/movie/';
    private searchUrl =        'https://api.themoviedb.org/3/search/movie';
    private genresUrl =        'https://api.themoviedb.org/3/genre/movie';
    private moviesByGenreUrl = 'https://api.themoviedb.org/3/discover/movie';
    private language = 'en-US'
    constructor( private http: HttpClient ) {}

    public getMovies(): Observable<MovieTmdb[]> {
        let moviesUrl = `${this.url}popular?api_key=${API_KEY}&language=${this.language}`;

        return this.http.get(moviesUrl).pipe(
            map(res => res['results'] as MovieTmdb[]|| [])
         ); 
      }

      searchMovies(query: string, page: number) {
        let searchUrl = `${this.searchUrl}?api_key=${API_KEY}&language=${this.language}&query=${query}&page=${page}`;
    
        return this.http.get(searchUrl).pipe(map((res) => res['results'] as MovieTmdb[]|| []))
      }

     public getGenres(): Observable<Genre[]> {
       let genresUrl =  `${this.genresUrl}/list?api_key=${API_KEY}&language=${this.language}`;

       return this.http.get(genresUrl).pipe(
        map(res => res['genres'] as Genre[]|| [])
     ); 
    }

    public getMoviesByGenres(id: number, page: number): Observable<MovieTmdb[]>{
      let moviesByGenreUrl = `${this.moviesByGenreUrl}?api_key=${API_KEY}&with_genres=${id}&page=${page}`;
      return this.http.get(moviesByGenreUrl).pipe(
        map(res => res['results'] as MovieTmdb[]|| [])
     ); 
  }
}
