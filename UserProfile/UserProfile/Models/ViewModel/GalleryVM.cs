using Identity.Intro.WebApplicationUI.Models.Entity.Membership;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfile.Models.Entities;
using UserProfile.Models.FormModel;

namespace UserProfile.Models.ViewModel
{
    public class GalleryVM
    {
        public int? Id { get; set; }
        public int? CreatedByUserId { get; set; }
        public AppUser? CreatedByUser { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile? File { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
