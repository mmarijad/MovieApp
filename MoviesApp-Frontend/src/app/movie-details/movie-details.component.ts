import { Component, OnInit, ElementRef, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

const APIKEY = "faeb787d";

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})

export class MovieDetailsComponent implements OnInit {
  
    @Input() movieDetails: any;
    @Input() btnCancelText: string;

    constructor(private activeModal: NgbActiveModal){}

  ngOnInit() {}
  
    public decline() {
      this.activeModal.close(false);
    }
  
    public dismiss() {
      this.activeModal.dismiss();
    }
  }
  