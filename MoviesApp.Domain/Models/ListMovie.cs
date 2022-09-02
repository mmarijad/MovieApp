using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Domain.Models
{
    public class ListMovie: Entity
    {
        public int MovieId { get; set; }
        public int ListId { get; set; }

        /* EF Relation */
        public Movie Movie { get; set; }
        public List List { get; set; }
    }
}
