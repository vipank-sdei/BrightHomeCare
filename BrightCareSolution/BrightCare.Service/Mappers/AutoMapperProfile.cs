using AutoMapper;
using BrightCare.Dtos.Agency;
using BrightCare.Dtos.Agency.MasterDocumentType;
using BrightCare.Dtos.Agency.MasterServices;
using BrightCare.Dtos.Agency.MasterServiceTypes;
using BrightCare.Dtos.Agency.UserRole;
using BrightCare.Entity.Agency;
using BrightCare.Dtos.Agency.StaffLeave;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            // CreateMap<EntityClass, DtoClass>();
            //CreateMap<EntityClass, DtoClass>().ReverseMap()

            CreateMap<User,UserDTO>();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Staffs, StaffsDTO>();
            CreateMap<Staffs, StaffsDTO>().ReverseMap();

            CreateMap<StaffLeaves, StaffLeaveDTO>();
            CreateMap<StaffLeaves, StaffLeaveDTO>().ReverseMap();

            CreateMap<MasterServices, MasterServicesDTO>().ReverseMap();
            CreateMap<MasterServiceType, MasterServiceTypeDTO>().ReverseMap();

            CreateMap<UserRoles, UserRoleDTO>().ReverseMap();

            CreateMap<MasterDocumentTypes, MasterDocumentTypeDTO>().ReverseMap();

        }

    }
}
