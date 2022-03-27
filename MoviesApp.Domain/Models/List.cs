using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Domain.Models
{
    public class List: Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
      
        /* EF Relation */
        public User User { get; set; }

        public IEnumerable<ListMovie> MovieLists { get; set; }
    }
}
