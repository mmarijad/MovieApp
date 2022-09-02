import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Director } from '../_models/Director';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class DirectorService{
    private baseUrl: string = environment.baseUrl + 'api/';

    constructor(private http: HttpClient) { }

    public addDirector(director: Director){
        return this.http.post(this.baseUrl+'directors', director);
    }

    public updateDirector(id: number, director:Director){
        return this.http.put(this.baseUrl+'directors/'+id,director);
    }

    public deleteDirector(id:number){
        return this.http.delete(this.baseUrl+'directors/'+id);
    }

    public getDirectors(): Observable<Director[]>{
        return this.http.get<Director[]>(this.baseUrl+'directors');
    }

    public getDirectorById(id): Observable<Director>{
        return this.http.get<Director>(this.baseUrl + 'directors/' + id);
    }

    public searchDirectors(name: string){
        return this.http.get<Director[]>(`${this.baseUrl}directors/search/${name}`);
    }
}