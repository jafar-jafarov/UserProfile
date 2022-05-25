using AutoMapper;
using Identity.Intro.WebApplicationUI.Models.Entity.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfile.Models.FormModel;

namespace UserProfile.AppCode.AutoMapper.Profiles
{
    public class PersonalSideProfile:Profile
    {
        public PersonalSideProfile()
        {
            CreateMap<AppUser, PersonalSideFormModel>()
            .ForMember(src => src.Username, dest => dest.MapFrom(map => map.UserName))
            .ForMember(src => src.EmailAddress, dest => dest.MapFrom(map => map.Email));


            CreateMap<PersonalSideFormModel, AppUser>()
                    .ForMember(src => src.UserName, dest => dest.MapFrom(map => map.Username))
                    .ForMember(src => src.Email, dest => dest.MapFrom(map => map.EmailAddress))
                    .ForMember(src => src.NormalizedUserName, dest => dest.MapFrom(map => map.Username.ToUpper()))
                    .ForMember(src => src.NormalizedEmail, dest => dest.MapFrom(map => map.EmailAddress.ToUpper()));
        }
    }
}
