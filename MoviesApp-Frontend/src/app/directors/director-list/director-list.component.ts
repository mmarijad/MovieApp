import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DirectorService } from 'src/app/_services/director.service';
import { ConfirmationDialogService } from 'src/app/_services/confirmation-dialog.service';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { Director } from 'src/app/_models/Director';

@Component({
  selector: 'app-director-list',
  templateUrl: './director-list.component.html',
  styleUrls: ['./director-list.component.css']
})

export class DirectorListComponent implements OnInit{
  public directors: Director[] ;
  public searchTerm: string;
  public searchValueChanged: Subject<string> = new Subject<string>();

  constructor(private router: Router, private service: DirectorService,
                private confirmationDialogService: ConfirmationDialogService) { }

  ngOnInit(): void {
      this.getDirectors();
      this.searchValueChanged.pipe(debounceTime(1000))
      .subscribe(() => {
        this.search();
      });
    }
  
  private getDirectors(){
    this.service.getDirectors().subscribe(directors => {
      this.directors = directors;
    })
  }

  private search() {
    if (this.searchTerm !== '') {
      this.service.searchDirectors(this.searchTerm).subscribe(director => {
        this.directors = director;
      }, error => {
        this.directors = [];
      });
    } else {
      this.service.getDirectors().subscribe(directors => this.directors = directors);
    }
  }

  public addDirector() {
    this.router.navigate(['/director']);
  }

  public updateDirector(directorId: number) {
    this.router.navigate(['/director/' + directorId]);
  }
  
  public deleteDirector(directorId: number){
    this.confirmationDialogService.confirm('Oprez', 'Jeste li sigurni da želite obrisati ovog redatelja?')
      .then(() =>
        this.service.deleteDirector(directorId).subscribe(() => {
          this.getDirectors();
        },
          error => {
          }))
      .catch(() => '');
  }

  public searchDirectors() {
    this.searchValueChanged.next();
  }  
}