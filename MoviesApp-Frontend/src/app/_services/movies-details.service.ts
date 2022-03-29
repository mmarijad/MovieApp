import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MovieDetailsComponent } from '../movie-details/movie-details.component';
import { MovieOmdb } from '../_models/MovieOmdb';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
const APIKEY = environment.omdbApi;

@Injectable()

export class MoviesDetailsService {
    isShowDiv: boolean;
    constructor(private modalService: NgbModal,
        private httpClient: HttpClient) {}

    public getDetails(
        movieDetails: MovieOmdb,
        title: string = movieDetails.Title,
        btnCancelText: string = 'Zatvori',
        btnAddText: string = 'Dodajte na popis',
        dialogSize: 'sm'|'lg' = 'lg'): Promise<boolean> {
        const modalRef = this.modalService.open(MovieDetailsComponent, {size: dialogSize});
        modalRef.componentInstance.btnCancelText = btnCancelText;
        modalRef.componentInstance.btnAddText = btnAddText;
        modalRef.componentInstance.movieDetails = movieDetails;
        modalRef.componentInstance.Title = title;
        return modalRef.result;
    }

    public getDetailsFromApi(movieName: string){
        this.isShowDiv = false;
        this.httpClient.get(`http://www.omdbapi.com/?t=${movieName}&apikey=${APIKEY}`).pipe(
          map((data: MovieOmdb) => {return data as MovieOmdb})).subscribe(data => { 
              this.getDetails(data);
          });
        } 

}