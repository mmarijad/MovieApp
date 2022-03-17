import { Component, OnInit } from '@angular/core';
import * as $ from "jquery";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    var api_key = "8f8f136e15a5f493451e82f334d5de84"; 
		var api_url = "https://api.themoviedb.org/3/movie/popular?api_key=" + api_key;
		$.getJSON( api_url, function( data ) {

			$.each( data.results, function( i, item ) {
				var posterFullUrl = "https://image.tmdb.org/t/p/w185//" + item.poster_path;
				$("<div class='col-3 mb-1'><img src='" + posterFullUrl + "'><h5>" + item.title + "</h5></div>").appendTo(".movies");
			});
		});

  }

}
