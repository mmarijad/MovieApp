import { Injectable } from '@angular/core';
import { HttpClient, HttpParams  } from '@angular/common/http';
import { Movie } from '../_models/Movie';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})

export class MoviesService {
    private baseUrl: string = environment.baseUrl + 'api/';

    constructor( private http: HttpClient ) {}
    
    public addMovie(movie: Movie) {
        return this.http.post(this.baseUrl + 'movies', movie);
    }

    public updateMovie(id: number, movie: Movie) {
        return this.http.put(this.baseUrl + 'movies/' + id, movie);
    }

    public getMovies(): Observable<Movie[]> {
        return this.http.get<Movie[]>(this.baseUrl + `movies`);
    }

    public deleteMovie(id: number) {
        return this.http.delete(this.baseUrl + 'movies/' + id);
    }

    public getMovieById(id: number): Observable<Movie> {
        return this.http.get<Movie>(this.baseUrl + 'movies/' + id);
    }

    public getMovieByName(name:string): Observable<Movie> {
        return this.http.get<Movie>(this.baseUrl + 'movies/get-movies-by-name/' + name);
    }

    public searchMoviesWithCategoryAndDirector(searchedValue: string): Observable<Movie[]> {
        return this.http.get<Movie[]>(`${this.baseUrl}movies/search-movies-with-category-and-director/${searchedValue}`);
    }
}
