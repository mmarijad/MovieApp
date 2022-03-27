﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.DTOs.MovieList
{
    public class MovieListResultDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        public int ListId { get; set; }

        public string MovieName { get; set; }

        public string MovieDescription { get; set; }
    }
}
