import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from 'src/app/_models/Movie';
import { Category } from 'src/app/_models/Category';
import { Director} from 'src/app/_models/Director';
import { MoviesService } from 'src/app/_services/movies.service';
import { CategoryService } from 'src/app/_services/category.service';
import { DirectorService } from 'src/app/_services/director.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import {NgbDateStruct, NgbCalendar} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css'],
}) 

export class MovieComponent implements OnInit {
  movieForm: FormGroup;
  validationErrors: string[] = [];
  public formData: Movie;
  public categories: Category[];
  public directors: Director[];
  isAddMode: boolean;
  loading = false;
  submitted = false;
  id: number;
  model: NgbDateStruct;
  date: {year: number};

  constructor(private service: MoviesService,  private categoryService: CategoryService,
              private directorService: DirectorService, private formBuilder: FormBuilder, 
              private router: Router,  private route: ActivatedRoute, 
              private calendar: NgbCalendar, private toastr: ToastrService) { }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    
    this.movieForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      categoryId: ['', Validators.required],
      directorId: ['', Validators.required],
      year: [''],
      id: [''],
  });

  this.model = {
    "year": 2000,
    "month": 1,
    "day": 1
  }        

  if (!this.isAddMode) {
      this.service.getMovieById(this.id)
          .pipe(first())
          .subscribe(x => {
            this.movieForm.patchValue(x);
            this.model = {
              "year": Number(x.year),
              "month": 1,
              "day": 1
            }
        });    
  }
  
  this.categoryService.getCategories().subscribe(categories => {
    this.categories = categories;
  }, err => {
  });
  
  this.directorService.getDirectors().subscribe(directors => {
    this.directors = directors;
  }, err => {
  });
}

onSubmit() {
    this.submitted = true;
    this.movieForm.value.categoryId = Number(this.movieForm.value.categoryId);
    this.movieForm.value.directorId = Number(this.movieForm.value.directorId);
    this.movieForm.value.year = String(this.date.year);
 
    if (this.movieForm.invalid) {
        return;
    }

    this.loading = true;
    if (this.isAddMode) {
        this.insertMovie();
    } else {
        this.updateMovie();
    }
}

  private resetForm(form?: FormGroup) {
    if (form != null) {
      form.reset();
    }
  }
  
  insertMovie(){
    this.service.addMovie(this.movieForm.value).subscribe(response => {
      this.router.navigateByUrl('/movies');
      this.toastr.success('Uspješno ste dodali film.');
    }, error => {
      this.validationErrors = error;
      this.toastr.error('Došlo je do pogreške pri dodavanju filma.');
    })
  }

  updateMovie() {
    this.service.updateMovie(this.movieForm.value.id, this.movieForm.value).subscribe(() => {
      this.resetForm(this.movieForm);
      this.router.navigate(['/movies']);
      this.toastr.success('Uspješno ste uredili film.');
    },  error => {
      this.validationErrors = error;
      this.toastr.error('Došlo je do pogreške pri uređivanju filma.');
    });
  }

  public cancel() {
    this.router.navigate(['/movies']);
  }
}