import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Director } from 'src/app/_models/Director';
import { DirectorService } from 'src/app/_services/director.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-director',
  templateUrl: './director.component.html',
  styleUrls: ['./director.component.css']
}) 

export class DirectorComponent implements OnInit {
  directorForm: FormGroup;
  validationErrors: string[] = [];
  public formData: Director;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  id: number;

  constructor(private service: DirectorService, private formBuilder: FormBuilder, 
              private router: Router,  private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit() {

    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    
    this.directorForm = this.formBuilder.group({
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      id: ['']
  });

  if (!this.isAddMode) {
      this.service.getDirectorById(this.id)
          .pipe(first())
          .subscribe(x => this.directorForm.patchValue(x));
  }
}

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.directorForm.invalid) {
        return;
    }

    this.loading = true;
    if (this.isAddMode) {
        this.insertDirector();
    } else {
        this.updateDirector();
    }
}

  private resetForm(form?: FormGroup) {
    if (form != null) {
      form.reset();
    }
  }
  
  insertDirector(){
    this.service.addDirector(this.directorForm.value).subscribe(response => {
      this.router.navigateByUrl('/directors');
      this.toastr.success('Uspješno ste dodali redatelja.');
    }, error => {
      this.validationErrors = error;
      this.toastr.error('Došlo je do pogreške pri dodavanju redatelja.');
    })
  }

 updateDirector() {
    this.service.updateDirector(this.directorForm.value.id, this.directorForm.value).subscribe(() => {
      this.resetForm(this.directorForm);
      this.router.navigate(['/directors']);
      this.toastr.success('Uspješno ste uredili redatelja.');
    }, error => {
      this.validationErrors = error;
      this.toastr.error('Došlo je do pogreške pri uređivanju redatelja.');
    });
  }

  public cancel() {
    this.router.navigate(['/directors']);
  }
}