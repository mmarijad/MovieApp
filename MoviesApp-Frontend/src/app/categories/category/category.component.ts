import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Category } from 'src/app/_models/Category';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  public formData: Category;

  constructor(public service: CategoryService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.resetForm();

    let id;
    this.route.params.subscribe(params => {
      id = params['id'];
    });

    if (id != null) {
      this.service.getCategoryById(id).subscribe(category => {
        this.formData = category;
      }, error => {
        
      });
    } else {
      this.resetForm();
    }
  }

 private resetForm(form?: NgForm) {
    if (form != null) {
      form.form.reset();
    }

    this.formData = {
      id: 0,
      name: ''
    };
  }

  public onSubmit(form: NgForm) {
    if (form.value.id === 0) {
      this.insertCategory(form);
    } else {
      this.updateCategory(form);
    }
  }

  public insertCategory(form: NgForm) {
    this.service.addCategory(form.form.value).subscribe(() => {
      
      this.resetForm(form);
      this.router.navigate(['/categories']);
    }, () => {
          });
  }

  public updateCategory(form: NgForm) {
    this.service.updateCategory(form.form.value.id, form.form.value).subscribe(() => {
      
      this.resetForm(form);
      this.router.navigate(['/categories']);
    }, () => {

    });
  }

  public cancel() {
    this.router.navigate(['/categories']);
  }
}
