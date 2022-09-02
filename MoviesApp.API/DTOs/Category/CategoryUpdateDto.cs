using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.DTOs.Category
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        [StringLength(150, ErrorMessage = "Polje {0} mora sadržavati minimalno {2}, a maksimalno {1} znakova.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
