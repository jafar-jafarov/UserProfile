using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace UserProfile.Models.FormModel
{
    public class PersonalSideFormModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil!")]
        public string EmailAddress { get; set; } = null!;

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile? File { get; set; }

        public string? FileTemp { get; set; }

    }
}
