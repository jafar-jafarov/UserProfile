using AutoMapper;
using Identity.Intro.WebApplicationUI.Models.Entity.Membership;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserProfile.AppCode.Extensions;
using UserProfile.AppCode.Infrastructure;
using UserProfile.Models.DbContext;
using UserProfile.Models.Entities;
using UserProfile.Models.FormModel;
using UserProfile.Models.ViewModel;

namespace UserProfile.Controllers
{
    public class GalleryController : Controller
    {
        readonly UserDbContext db;
        readonly IActionContextAccessor ctx;
        readonly IMapper mapper;
        readonly IHostEnvironment env;

        public GalleryController(UserDbContext db, IActionContextAccessor ctx, IMapper mapper, IHostEnvironment env)
        {
            this.db = db;
            this.ctx = ctx;
            this.mapper = mapper;
            this.env = env;
        }
        public IActionResult Index()
        {
            var query = db.ImageFiles.OrderByDescending(n => n.CreatedTime).ToList();

            return View(query);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("File")] GalleryVM request)
        {
            CommandJsonResponse response = new();

            if (request.File == null)
            {
                ctx.AddModelError("", "Şəkil seçilməyib!");
            }
            if (!ctx.IsValid())
            {
                response.Error = true;
                response.Message = "Məlumat düzgün göndərilməyib!";
            }

            if (ctx.IsValid())
            {
                string fullPath = null;

                string ext = Path.GetExtension(request.File.FileName);
                string fileName = $"user-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "userGallery", fileName);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await request.File.CopyToAsync(fs);
                }

                request.ImagePath = fileName;
                ImageFiles imageFiles = mapper.Map<ImageFiles>(request);
                imageFiles.CreatedByUserId = ctx.GetUserId();


                await db.ImageFiles.AddAsync(imageFiles);
                await db.SaveChangesAsync();

                response.Error = false;
                response.Message = "Məlumatlar uğurla yadda saxlanıldı!";
            }
            return Json(response);
        }
    }
}
