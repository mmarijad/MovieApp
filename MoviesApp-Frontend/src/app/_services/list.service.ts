import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { List } from '../_models/List';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})

export class ListService {
    private baseUrl: string = environment.baseUrl + 'api/';

    constructor( private http: HttpClient ) {}

    public addList(list: List, username: string) {
        return this.http.post(this.baseUrl + 'list', list);
    }
    
    public updateList(id: number, list: List) {
        return this.http.put(this.baseUrl + 'list/' + id, list);
    }

    public getLists(username: string): Observable<List[]> {
        return this.http.get<List[]>( this.baseUrl+'list/'+ username);
    }

    public deleteList(id: number) {
        return this.http.delete(this.baseUrl + 'list/' + id);
    }

    public getListById(id: number): Observable<List> {
        return this.http.get<List>(this.baseUrl + 'list/' + id);
    }
}
