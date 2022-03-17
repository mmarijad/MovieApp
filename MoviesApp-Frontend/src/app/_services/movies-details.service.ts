import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MovieDetailsComponent } from '../movie-details/movie-details.component';

@Injectable()

export class MoviesDetailsService {
    constructor(private modalService: NgbModal) {}

    public getDetails(
        movieDetails,
        title: string = movieDetails['Title'],
        btnCancelText: string = 'Zatvori',
        dialogSize: 'sm'|'lg' = 'lg'): Promise<boolean> {
        const modalRef = this.modalService.open(MovieDetailsComponent, {size: dialogSize});
        modalRef.componentInstance.btnCancelText = btnCancelText;
        modalRef.componentInstance.movieDetails = movieDetails;
        modalRef.componentInstance.Title = title;
        return modalRef.result;
    }
}