import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ListService } from '../_services/list.service';
import { ListMovieService } from '../_services/list-movie.service';
import { MoviesService } from '../_services/movies.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../_services/user.service';
import { List } from '../_models/List';
import { Movie } from '../_models/Movie';
import { MovieOmdb } from '../_models/MovieOmdb';
import { Category } from '../_models/Category';
import { Director } from '../_models/Director';
import { DirectorService } from '../_services/director.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})

export class MovieDetailsComponent implements OnInit {
  
    @Input() movieDetails: MovieOmdb;
    @Input() btnCancelText: string;
    @Input() btnAddText: string;
    movieListForm: FormGroup;
    movieForm: FormGroup;
    lists: List[];
    username: string;
    submitted: boolean;
    movie: Movie;
    category: Category;
    director: Director;
    directorForm: FormGroup;
    directorNames: string[];

    constructor(  private activeModal: NgbActiveModal, 
                  private listService: ListService,
                  private listMovieService: ListMovieService, 
                  private movieService: MoviesService,
                  private formBuilder: FormBuilder, 
                  private userService: UserService,
                  private directorService: DirectorService) {}

  ngOnInit() {

    this.movieForm = this.formBuilder.group({
      name: this.movieDetails.Title,
      description: this.movieDetails.Plot,
      year: this.movieDetails.Year,
      categoryId: 1,
      directorId: 1,
      id: 1,
  });

    this.movieListForm = this.formBuilder.group({
      listId: [Number, Validators.required],
      movieId: [Number, Validators.required],
      id: [Number, Validators.required]
    });

    this.directorForm = this.formBuilder.group({
      name: 'nan',
      lastname: 'nan'
    });

    this.username = this.userService.decodedToken?.unique_name;
    this.listService.getLists(this.username).subscribe(lists => {
      this.lists = lists;
    }, err => {
    });
  }
  
    public decline() {
      this.activeModal.close(false);
    }
  
    public dismiss() {
      this.activeModal.dismiss();
    }

    public onSubmit() {
      this.movieListForm.value.listId = Number(this.movieListForm.value.listId);
      this.submitted = true;
      if (this.movieListForm.invalid) 
        return;


      this.directorNames = this.movieDetails.Director.split(',');
      this.directorNames.forEach(element => {
          this.directorForm.value.name = element;
          this.directorForm.value.lastname = element;
          this.directorService.getDirectorByName(element).subscribe(
            response=>{
              this.movieForm.value.directorId = response['id'];
              this.getMovieByName();
            },
            error =>{
              if (error.status === 404){
                this.directorService.addDirector(this.directorForm.value).subscribe(
                  response =>{
                    this.movieForm.value.directorId = response['id'];
                    this.getMovieByName();
                  });
              }
            });
          }
      );
  }
  
    public getMovieByName(){
      // Provjera postoji li film već u bazi, ako da, dohvati njegov id i spremi u tablicu movie-list
      // Ako ne, dodaj ga i dohvati njegov id

    this.movieService.getMovieByName(this.movieForm.value.name).subscribe(
      response =>{
        this.movieService.getMovieByName(this.movieForm.value.name).subscribe(
          response =>{
            this.movieListForm.value.movieId = response['id'];
            this.addMovieToList();
          });
      },
      error => {
        if (error.status === 404)
        {
          this.movieService.addMovie(this.movieForm.value).subscribe(response => {
            response = response;
            this.movieListForm.value.movieId = response['id'];
            this.addMovieToList();
            });
        }
      });
    }
    private resetForm(form?: FormGroup) {
        if (form != null) {
          form.reset();
        }
      }

    public addMovieToList(){
      this.listMovieService.addMovieList(this.movieListForm.value).subscribe(() => {
        this.resetForm(this.movieListForm);
        this.resetForm(this.movieForm);
    });
  }      
}
  