using AutoMapper;
using BrightCare.Dtos.Agency.MasterDocumentType;
using BrightCare.Dtos.Agency.MasterServices;
using BrightCare.Dtos.Agency.MasterServiceTypes;
using BrightCare.Dtos.Agency.UserRole;
using BrightCare.Entity.Agency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Service.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MasterServices, MasterServicesDTO>().ReverseMap();
            CreateMap<MasterServiceType, MasterServiceTypeDTO>().ReverseMap();
            CreateMap<UserRoles, UserRoleDTO>().ReverseMap();
            CreateMap<MasterDocumentTypes, MasterDocumentTypeDTO>().ReverseMap();

            // CreateMap<EntityClass, DtoClass>();
            //CreateMap<EntityClass, DtoClass>().ReverseMap()

        }

    }
}
