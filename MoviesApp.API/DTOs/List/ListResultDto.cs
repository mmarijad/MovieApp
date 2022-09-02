using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.DTOs.List
{
    public class ListResultDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
