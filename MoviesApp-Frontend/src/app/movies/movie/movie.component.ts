import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Movie } from 'src/app/_models/Movie';
import { MoviesService } from 'src/app/_services/movies.service';
import { CategoryService } from 'src/app/_services/category.service';
import { DirectorService } from 'src/app/_services/director.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  public formData: Movie;
  public categories: any;
  public directors: any;

  constructor(public service: MoviesService,
              private categoryService: CategoryService,
              private directorService: DirectorService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.resetForm();
    let id;
    this.route.params.subscribe(params => {
      id = params['id'];
    });

    if (id != null) {
      this.service.getMovieById(id).subscribe(movie => {
        this.formData = movie;
        }, err => {
      });
    } else {
      this.resetForm();
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

  public onSubmit(form: NgForm) {
    form.value.categoryId = Number(form.value.categoryId);
    form.value.directorId = Number(form.value.directorId);
    if (form.value.id === 0) {
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }
  }

  public insertRecord(form: NgForm) {
    this.service.addMovie(form.form.value).subscribe(() => {
      this.resetForm(form);
      this.router.navigate(['/movies']);
    }, () => {
    });
  }

  public updateRecord(form: NgForm) {
    this.service.updateMovie(form.form.value.id, form.form.value).subscribe(() => {
      this.resetForm(form);
      this.router.navigate(['/movies']);
    }, () => {
    });
  }

  public cancel() {
    this.router.navigate(['/movies']);
  }

  private resetForm(form?: NgForm) {
    if (form != null) {
      form.form.reset();
    }

    this.formData = {
      id: 0,
      name: '',
      description: '',
      categoryId: null,
      directorId: null,
      year: null
    };
  }
}