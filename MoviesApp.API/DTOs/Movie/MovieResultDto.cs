using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.DTOs.Movie
{
    public class MovieResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string DirectorLastName { get; set; }
        public string CategoryName { get; set; }
        public string DirectorName { get; set; }
        public int CategoryId { get; set; }
        public int DirectorId { get; set; }

    }
}