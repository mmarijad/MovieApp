import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/_services/category.service';
import { ConfirmationDialogService } from 'src/app/_services/confirmation-dialog.service';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { Category } from 'src/app/_models/Category';
import { ToastrService } from 'ngx-toastr';
import { Genre } from 'src/app/_models/MovieTmdb';
import { TmdbService } from 'src/app/_services/tmdb.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})

export class CategoryListComponent implements OnInit{
  public categories: Category[];
  public listComplet: any;
  public searchTerm: string;
  public genres: Genre[];
  public searchValueChanged: Subject<string> = new Subject<string>();

  constructor(private router: Router, private service: CategoryService,
            private confirmationDialogService: ConfirmationDialogService, 
            private toastr: ToastrService,
            private tmdbService: TmdbService) { }

  ngOnInit(): void {
      this.getGenres()
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

  private getGenres(){
    this.tmdbService.getGenres().subscribe(genres => {
      this.genres = genres;
      this.listComplet = genres;
    });
  }

  public addCategory() {
    this.router.navigate(['/category']);
  }

  public updateCategory(categoryId: number) {
    this.router.navigate(['/category/' + categoryId]);
  }
  
  public deleteCategory(categoryId: number){
    this.confirmationDialogService.confirm('Oprez', 'Jeste li sigurni da želite obrisati ovu kategoriju?')
      .then(() =>
        this.service.deleteCategory(categoryId).subscribe(() => {
          this.getCategories();
          this.toastr.success('Uspješno ste izbrisali kategoriju.');
        },
          error => {
            this.toastr.error('Došlo je do pogreške pri brisanju kategorije.');
          }))
      .catch(() => '');
  }

  public searchCategories() {
    this.searchValueChanged.next();
  }

  public search() {
     const value = this.searchTerm.toLowerCase();
     this.genres = this.listComplet.filter(
       genre => genre.name.toLowerCase().startsWith(value, 0))
   }
}