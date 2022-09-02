using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.DTOs.MovieList
{
    public class MovieListAddDto
    {

        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        public int ListId { get; set; }

    }
}
