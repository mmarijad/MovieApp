import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Category } from 'src/app/_models/Category';
import { CategoryService } from 'src/app/_services/category.service';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
}) 

export class CategoryComponent implements OnInit {
  categoryForm: FormGroup;
  validationErrors: string[] = [];
  public formData: Category;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  id: number;

  constructor(private service: CategoryService, private formBuilder: FormBuilder, 
              private router: Router,  private route: ActivatedRoute) { }

  ngOnInit() {

    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
      id: ['']
  });

  if (!this.isAddMode) {
      this.service.getCategoryById(this.id)
          .pipe(first())
          .subscribe(x => this.categoryForm.patchValue(x));
  }
}

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.categoryForm.invalid) {
        return;
    }

    this.loading = true;
    if (this.isAddMode) {
        this.insertCategory();
    } else {
        this.updateCategory();
    }
}

  private resetForm(form?: FormGroup) {
    if (form != null) {
      form.reset();
    }
  }
  
  insertCategory(){
    this.service.addCategory(this.categoryForm.value).subscribe(response => {
      this.router.navigateByUrl('/categories');
    }, error => {
      this.validationErrors = error;
    })
  }

 updateCategory() {
   
    this.service.updateCategory(this.categoryForm.value.id, this.categoryForm.value).subscribe(() => {
      this.resetForm(this.categoryForm);
      this.router.navigate(['/categories']);
    }, () => {

    });
  }

  public cancel() {
    this.router.navigate(['/categories']);
  }
}