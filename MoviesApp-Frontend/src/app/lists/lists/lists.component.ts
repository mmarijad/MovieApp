import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { List } from 'src/app/_models/List';
import { ListService } from 'src/app/_services/list.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  listForm: FormGroup;
  validationErrors: string[] = [];
  public formData: List;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  id: number;
  model: NgbDateStruct;
  date: {year: number};
  username: string;
  listTitle: string;

  constructor(private service: ListService, private formBuilder: FormBuilder, 
              private router: Router,  private route: ActivatedRoute, 
              private toastr: ToastrService, private userService: UserService) {}

  ngOnInit(): void {
    this.username = this.userService.decodedToken?.unique_name;
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.listTitle = "Novi popis"
    
    this.listForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      userId: [this.username],
      id: Number,
  });

  if (!this.isAddMode) {
    this.service.getListById(this.id)
        .pipe(first())
        .subscribe(x => {
          this.listForm.patchValue(x);
          this.listTitle = x.title;
      });    
    }
  }

  onSubmit() {
    this.submitted = true;
    if (this.listForm.invalid) {
        return;
    }

  this.loading = true;
  if (this.isAddMode) {
      this.insertList();
    } 
  else {
      this.updateList();
    }
  }

  private resetForm(form?: FormGroup) {
      if (form != null) {
        form.reset();
      }
    }

  insertList(){
    this.service.addList(this.listForm.value, this.username).subscribe(response => {
      this.router.navigateByUrl('/lists/'+ this.username);
      this.toastr.success('Uspješno ste dodali popis filmova.');
    }, error => {
      this.validationErrors = error;
      this.toastr.error('Došlo je do pogreške pri dodavanju popisa filmova.');
    })
  }

  updateList() {
    this.service.updateList(this.listForm.value.id, this.listForm.value).subscribe(() => {
      this.resetForm(this.listForm);
      this.router.navigate(['/lists/'+this.username]);
      this.toastr.success('Uspješno ste uredili popis filmova.');
    },  error => {
      this.validationErrors = error;
      this.toastr.error('Došlo je do pogreške pri uređivanju popisa filmova.');
    });
  }

  public cancel() {
    this.router.navigate(['/lists/'+this.username]);
  }
}
