using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UserProfile.Models.Entities;

namespace UserProfile.Models.FormModel
{
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [EmailAddress]
        public string Email { get; set; }
        public virtual List<ImageFiles>? Images { get; set; }
        [NotMapped]
        public ImageItemFormModel[] Files { get; set; }


        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Şifrələr eyni olmalıdır!")]
        public string ConfirmPassword { get; set; }
    }
}
