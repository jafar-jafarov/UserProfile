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
using UserProfile.Models.FormModel;

namespace UserProfile.Controllers
{
    public class PersonalSideController : Controller
    {
        readonly UserDbContext db;
        readonly IActionContextAccessor ctx;
        readonly IMapper mapper;
        readonly IHostEnvironment env;
        public PersonalSideController(UserDbContext db, IActionContextAccessor ctx, IMapper mapper,IHostEnvironment env)
        {
            this.db = db;
            this.ctx = ctx;
            this.mapper = mapper;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            int userId = ctx.GetUserId();
            AppUser currentUser = await db.Users.FindAsync(userId);

            PersonalSideFormModel formModel = mapper.Map<PersonalSideFormModel>(currentUser);

            return View(formModel);
        }
        [Route("edit-profile-info")]
        public async Task<IActionResult> Edit()
        {
            int userId = ctx.GetUserId();
            AppUser currentUser = await db.Users.FindAsync(userId);

            PersonalSideFormModel formModel = mapper.Map<PersonalSideFormModel>(currentUser);

            return View(formModel);
        }
        [HttpPost("edit-profile-info")]
        async public Task<IActionResult> Edit(PersonalSideFormModel request)
        {
            CommandJsonResponse response = new();

            if (request.Id == null || request.Id <= 0)
            {
                response.Error = true;
                response.Message = "Məlumatın tamlığı qorunmayıb!";
            }

            AppUser currentUser = await db.AppUsers.FirstOrDefaultAsync(u => u.Id == request.Id);

            if (currentUser == null)
            {
                response.Error = true;
                response.Message = "Məlumat mövcud deyil!";
            }

            if (ctx.IsValid())
            {
                string fullPath = null;
                string currentPath = null;

                if (currentUser.ImagePath != null || (currentUser.ImagePath == null && request.File != null))
                {
                    if (request.File == null && !string.IsNullOrWhiteSpace(request.FileTemp))
                    {
                        request.ImagePath = currentUser.ImagePath;
                    }
                    else if (request.File == null)
                    {
                        currentPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "userImg", currentUser.ImagePath);
                    }
                    else if (request.File != null)
                    {
                        string ext = Path.GetExtension(request.File.FileName);
                        string fileName = $"user-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                        fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "userImg", fileName);

                        using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                        {
                            await request.File.CopyToAsync(fs);
                        }

                        request.ImagePath = fileName;
                    }
                }
               

                AppUser user = mapper.Map(request, currentUser);

                await db.SaveChangesAsync();

                response.Error = false;
                response.Message = "Məlumatlar uğurla yadda saxlanıldı!";
            }
            return Json(response);
        }
    }
}
