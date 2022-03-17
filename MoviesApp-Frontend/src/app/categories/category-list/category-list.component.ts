import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/_services/category.service';
import { ConfirmationDialogService } from 'src/app/_services/confirmation-dialog.service';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { Category } from 'src/app/_models/Category';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})

export class CategoryListComponent implements OnInit{
  public categories: Category[];
  public searchTerm: string;
  public searchValueChanged: Subject<string> = new Subject<string>();

  constructor(private router: Router, private service: CategoryService,
            private confirmationDialogService: ConfirmationDialogService) { }

  ngOnInit(): void {
      this.getCategories();
      this.searchValueChanged.pipe(debounceTime(1000))
      .subscribe(() => {
        this.search();
      });
    }
  
  private getCategories(){
    this.service.getCategories().subscribe(categories => {
      this.categories = categories;
    })
  }

  private search() {
    if (this.searchTerm !== '') {
      this.service.searchCategories(this.searchTerm).subscribe(category => {
        this.categories = category;
      }, error => {
        this.categories = [];
      });
    } else {
      this.service.getCategories().subscribe(categories => this.categories = categories);
    }
  }

  public addCategory() {
    this.router.navigate(['/category']);
  }

  public updateCategory(categoryId: number) {
    this.router.navigate(['/category/' + categoryId]);
  }
  
  public deleteCategory(categoryId: number){
    this.confirmationDialogService.confirm('Oprez', 'Jeste li sigurni da želite obrisati ovu kategoriju??')
      .then(() =>
        this.service.deleteCategory(categoryId).subscribe(() => {
          this.getCategories();
        },
          error => {
          }))
      .catch(() => '');
  }

  public searchCategories() {
    this.searchValueChanged.next();
  }  
}