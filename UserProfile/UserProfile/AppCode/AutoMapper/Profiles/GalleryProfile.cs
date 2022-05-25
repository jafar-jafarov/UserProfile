using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfile.Models.Entities;
using UserProfile.Models.ViewModel;

namespace UserProfile.AppCode.AutoMapper.Profiles
{
    public class GalleryProfile:Profile
    {
        public GalleryProfile()
        {
            CreateMap<GalleryVM, ImageFiles>();
        }
    }
}
