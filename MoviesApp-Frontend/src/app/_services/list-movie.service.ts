import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MovieList } from '../_models/MovieList';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})

export class ListMovieService {
    private baseUrl: string = environment.baseUrl + 'api/';

    constructor( private http: HttpClient ) {}
    
    public addMovieList(list: MovieList) {
        return this.http.post(this.baseUrl + 'movielist', list);
    }

    public updateMovieList(id: number, list: MovieList) {
        return this.http.put(this.baseUrl + 'movielist/' + id, list);
    }

    public getMovieLists(listId: number): Observable<MovieList[]> {
        return this.http.get<MovieList[]>(this.baseUrl + 'movielist/'+ listId);
    }

    public deleteMovieList(id: number) {
        return this.http.delete(this.baseUrl + 'movielist/' + id);
    }
}
