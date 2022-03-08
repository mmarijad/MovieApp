import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Director } from 'src/app/_models/Director';
import { DirectorService } from 'src/app/_services/director.service';

@Component({
  selector: 'app-director',
  templateUrl: './director.component.html',
  styleUrls: ['./director.component.css']
})
export class DirectorComponent implements OnInit {
  public formData: Director;

  constructor(public service: DirectorService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.resetForm();

    let id;
    this.route.params.subscribe(params => {
      id = params['id'];
    });

    if (id != null) {
      this.service.getDirectorById(id).subscribe(director => {
        this.formData = director;
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
      name: '',
      lastname: ''
    };
  }

  public onSubmit(form: NgForm) {
    if (form.value.id === 0) {
      this.insertDirector(form);
    } else {
      this.updateDirector(form);
    }
  }

  public insertDirector(form: NgForm) {
    this.service.addDirector(form.form.value).subscribe(() => {
      this.resetForm(form);
      this.router.navigate(['/directors']);
    }, () => {
    });
  }

  public updateDirector(form: NgForm) {
    this.service.updateDirector(form.form.value.id, form.form.value).subscribe(() => {
      this.resetForm(form);
      this.router.navigate(['/directors']);
    }, () => {
    });
  }

  public cancel() {
    this.router.navigate(['/directors']);
  }
}
