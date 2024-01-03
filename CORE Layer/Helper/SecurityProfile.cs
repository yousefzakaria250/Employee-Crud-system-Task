using AutoMapper;
using CORE_Layer.Dtos;
using Db_Builder.Models.User_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Helper
{
    public class SecurityProfile :Profile
    {
       public SecurityProfile() {

            CreateMap<AppUser, AddUserDto>().ReverseMap();
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();
            CreateMap<AppUser, GetUserDto>()
                .ForMember(D => D.State, O => O.MapFrom(S => S.DegreeState.State));
        }
    }
}
