import { Component, OnInit } from '@angular/core';
import { ListMovieService } from 'src/app/_services/list-movie.service';
import { ActivatedRoute } from '@angular/router';
import { DirectorService } from 'src/app/_services/director.service';
import { Director } from 'src/app/_models/Director';
import { HttpClient } from '@angular/common/http';
import { MoviesDetailsService } from 'src/app/_services/movies-details.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';
import { first} from 'rxjs/operators';
import { MovieOmdb } from 'src/app/_models/MovieOmdb';
import { ConfirmationDialogService } from 'src/app/_services/confirmation-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { MoviesService } from 'src/app/_services/movies.service';
import { Movie } from 'src/app/_models/Movie';

@Component({
  selector: 'app-director-details',
  templateUrl: './director-details.component.html',
  styleUrls: ['./director-details.component.css']
})
export class DirectorDetailsComponent implements OnInit {
  movies: Movie[];
  directorId: number;
  directorName: string;
  director: Director;
  name: string;
  isShowDiv: boolean;
  movieDetails: MovieOmdb;
  username: string;

  constructor(private route: ActivatedRoute,
              private directorService: DirectorService,
              private movieDetailsService: MoviesDetailsService, 
              private movieService: MoviesService) { }

  ngOnInit(): void {
    this.getValues();
  }

  getValues(){
    this.directorId = this.route.snapshot.params['id'];
    this.directorService.getDirectorById(this.directorId)
        .pipe(first())
        .subscribe(x => {
          this.directorName = x.name;
          this.director = x;
    });
            
    this.movieService.getMoviesByDirector(this.directorId).subscribe(movies => {
      this.movies = movies;
    });;
  }

  public getDetails(movieName: string){
    this.name = movieName;
    this.movieDetailsService.getDetailsFromApi(this.name);
    } 

}
