using Identity.Intro.WebApplicationUI.Models.Entity.Membership;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfile.Models.Entities
{
    public class ImageFiles
    {
        public int Id { get; set; }
        public int CreatedByUserId { get; set; }

        public virtual AppUser CreatedByUser { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
