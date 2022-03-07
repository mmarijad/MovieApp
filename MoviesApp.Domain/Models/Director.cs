﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Domain.Models
{
    public class Director : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        /* EF Relations */
        public IEnumerable<Movie> Movies { get; set; }
    }
}