import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ListService } from 'src/app/_services/list.service';
import { ConfirmationDialogService } from 'src/app/_services/confirmation-dialog.service';
import { List } from 'src/app/_models/List';
import { HttpClient, HttpParams } from "@angular/common/http";
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-lists-lists',
  templateUrl: './lists-lists.component.html',
  styleUrls: ['./lists-lists.component.css']
})
export class ListsListsComponent implements OnInit {

  public lists: List[];
  public listComplet: List[];
  username: string;

  constructor(private router: Router,
              private service: ListService,
              private confirmationDialogService: ConfirmationDialogService,
              private httpClient: HttpClient, private userService: UserService,
              private toastr: ToastrService) {
              }

  ngOnInit() {
    this.username = this.userService.decodedToken?.unique_name;
    this.getValues();
  }

  private getValues() {
    this.service.getLists(this.username).subscribe(lists => {
      this.lists = lists;
      this.listComplet = lists;
    });
  }

  public deleteList(listId: number) {
    this.confirmationDialogService.confirm('', 'Jeste li sigurni da želite obrisati ovaj film?')
      .then(() =>
        this.service.deleteList(listId).subscribe(() => {
          this.getValues();
          this.toastr.success('Uspješno ste izbrisali listu filmova.');
        },
          error => {
            this.toastr.error('Došlo je do pogreške pri brisanju liste filmova.');
          }))
      .catch(() => '');
  }

  public addList() {
    this.router.navigate(['/list']);
  }

  public updateList(listId: number) {
    this.router.navigate(['/list/' + listId]);
  }

  public listDetails(listId: number){
    this.router.navigate(['/list-details/' + listId]);
  }
}
