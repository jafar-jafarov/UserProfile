using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfile.Models.FormModel
{
    public class LoginFormModel
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
