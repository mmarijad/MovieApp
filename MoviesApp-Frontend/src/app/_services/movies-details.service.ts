import { Injectable } from '@angular/core';
import { async } from '@angular/core/testing';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { MovieDetailsComponent } from '../movie-details/movie-details.component';
import { MovieOmdb } from '../_models/MovieOmdb';
import { MovieTmdb } from '../_models/MovieTmdb';

@Injectable()

export class MoviesDetailsService {

    constructor(private modalService: NgbModal) {}

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
}