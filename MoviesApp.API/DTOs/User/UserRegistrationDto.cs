using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.DTOs.User
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        [StringLength(20, ErrorMessage = "Polje {0} mora sadržavati minimalno {2}, a maksimalno {1} znakova.", MinimumLength = 2)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        [StringLength(150, ErrorMessage = "Polje {0} mora sadržavati minimalno {2}, a maksimalno {1} znakova.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        [StringLength(150, ErrorMessage = "Polje {0} mora sadržavati minimalno {2}, a maksimalno {1} znakova.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        [StringLength(50, ErrorMessage = "Polje {0} mora sadržavati minimalno {2}, a maksimalno {1} znakova.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", 
            ErrorMessage = "Lozinka mora sadržavati najmanje 8 znakova od čega najmanje jedno veliko slovo, jedno malo slovo, jednu znamenku i jedan poseban znak.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju")]
        public string ConfirmPassword { get; set; }
    }
}
