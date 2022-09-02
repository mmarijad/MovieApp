using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Domain.Models
{
    public class Movie: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }

        public int DirectorId { get; set; }
        public int CategoryId { get; set; }

        /* EF Relation */
        public Category Category { get; set; }
        public Director Director { get; set; }
    }
}
