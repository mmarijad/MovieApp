using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        /* EF Relations */
        public IEnumerable<Movie> Movies { get; set; }
    }
}
