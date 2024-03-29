﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.DTOs.Movie
{
    public class MovieAddDto
    {
   
        public int CategoryId { get; set; }

        public int DirectorId { get; set; }

        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        [StringLength(150, ErrorMessage = "Polje {0} mora sadržavati minimalno {2}, a maksimalno {1} znakova.", MinimumLength = 2)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Year { get; set; }
    }
}
