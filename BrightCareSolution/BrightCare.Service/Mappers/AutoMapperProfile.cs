using AutoMapper;
using BrightCare.Dtos.Agency;
using BrightCare.Dtos.Agency.LeaveReasons;
using BrightCare.Dtos.Agency.LeaveTypes;
using BrightCare.Dtos.Agency.MasterDocumentType;
using BrightCare.Dtos.Agency.MasterServices;
using BrightCare.Dtos.Agency.MasterServiceTypes;
using BrightCare.Dtos.Agency.UserRole;
using BrightCare.Entity.Agency;
using BrightCare.Dtos.Agency.StaffLeave;
using System;
using System.Collections.Generic;
using System.Text;
using BrightCare.Dtos.Agency.MasterAllergy;
using BrightCare.Dtos.Agency.MasterAllergyReaction;
using BrightCare.Dtos.Agency.DiagnosisCodes;
using BrightCare.Dtos.Agency.Document;
using BrightCare.Dtos.Agency.Staff;

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

            CreateMap<UserDocument,UserDocumentDTO>();
            CreateMap<UserDocument, UserDocumentResponseDTO>().ReverseMap();

            CreateMap<StaffExperience,StaffExperienceDTO>();
            CreateMap<StaffExperience, StaffExperienceDTO>().ReverseMap();

            CreateMap<MasterServices, MasterServicesDTO>().ReverseMap();
            CreateMap<MasterServiceType, MasterServiceTypeDTO>().ReverseMap();

            CreateMap<UserRoles, UserRoleDTO>().ReverseMap();

            CreateMap<MasterDocumentTypes, MasterDocumentTypeDTO>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();
            CreateMap<LeaveReason, LeaveReasonDTO>().ReverseMap();
            CreateMap<MasterAllergies, MasterAllergyDTO>().ReverseMap();
            CreateMap<MasterAllergiesReaction, MasterAllergyReactionDTO>().ReverseMap();
            CreateMap<DiagnosisCode, DiagnosisCodesDTO>().ReverseMap();

        }
    }
}
