import { Component, OnInit } from '@angular/core';
import { ListMovieService } from 'src/app/_services/list-movie.service';
import { MovieList } from 'src/app/_models/MovieList';
import { ActivatedRoute } from '@angular/router';
import { ListService } from 'src/app/_services/list.service';
import { List } from 'src/app/_models/List';
import { HttpClient } from '@angular/common/http';
import { MoviesDetailsService } from 'src/app/_services/movies-details.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';
import { first, map } from 'rxjs/operators';
import { MovieOmdb } from 'src/app/_models/MovieOmdb';
import { environment } from 'src/environments/environment';
import { ConfirmationDialogService } from 'src/app/_services/confirmation-dialog.service';
import { ToastrService } from 'ngx-toastr';
const APIKEY = environment.omdbApi;

@Component({
  selector: 'app-list-details',
  templateUrl: './list-details.component.html',
  styleUrls: ['./list-details.component.css']
})
export class ListDetailsComponent implements OnInit {

  movies: MovieList[];
  listId: number;
  listTitle: string;
  list: List;
  name: string;
  isShowDiv: boolean;
  movieDetails: MovieOmdb;
  username: string;

  constructor(private movieListService: ListMovieService, private route: ActivatedRoute,
    private listService: ListService, private http: HttpClient, 
    private movieDetailsService: MoviesDetailsService, private router: Router,
    private userService: UserService, private confirmationDialogService: ConfirmationDialogService,
    private toastr: ToastrService ) { }
    

  ngOnInit(): void {
    this.getValues();
    this.username = this.userService.decodedToken?.unique_name;
  }

  getValues(){
    this.listId = this.route.snapshot.params['id'];
      this.listService.getListById(this.listId)
          .pipe(first())
          .subscribe(x => {
            this.listTitle = x.title;
            this.listTitle = x.title;
            this.list = x;
        });
            
    this.movieListService.getMovieLists(this.listId).subscribe(movies => {
      this.movies = movies;
    });;
  }

  public getDetails(movieName: string){
    this.name = movieName;
    this.isShowDiv = false;
    this.http.get(`http://www.omdbapi.com/?t=${this.name}&apikey=${APIKEY}`).pipe(
      map((data: MovieOmdb) => {return data as MovieOmdb})).subscribe(data => { 
          this.movieDetailsService.getDetails(data);
      });
    } 

  public delete(id: number) {
    this.confirmationDialogService.confirm('', 'Jeste li sigurni da želite obrisati ovaj film s popisa?')
      .then(() =>
        this.movieListService.deleteMovieList(id).subscribe(() => {
          this.getValues();
          this.toastr.success('Uspješno ste izbrisali film s popisa.');
        },
          error => {
            this.toastr.error('Došlo je do pogreške pri brisanju filma s popisa.');
          }))
      .catch(() => '');
  }

public goToHome(){
  this.router.navigate(['/home']);
}

  public cancel() {
    this.router.navigate(['/lists/'+this.username]);
  }
}

