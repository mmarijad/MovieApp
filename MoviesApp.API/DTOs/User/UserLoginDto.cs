using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.DTOs.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Polje {0} ne može biti prazno.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zapamti me")]
        public bool RememberMe { get; set; }
    }
}
