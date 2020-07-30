using BrightCare.Persistence;
using BrightCare.Repository.Agency;
using BrightCare.Repository.Agency.Organizations;
using BrightCare.Repository.Interface.Agency;
using BrightCare.Repository.Interface.Agency.Users;
using BrightCare.Repository.Interface.Agency.Organizations;
using BrightCare.Repository.Interface.SuperAdmin;
using BrightCare.Repository.Interface.SuperAdmin.Organizations;
using BrightCare.Repository.SuperAdmin;
using BrightCare.Repository.SuperAdmin.Organizations;
using BrightCare.Service.Agency.Login;
using BrightCare.Service.Agency.Organizations;
using BrightCare.Service.Interface.Agency.Login;
using BrightCare.Service.Interface.Agency.Organizations;
using BrightCare.Service.Interface.SuperAdmin.Organizations;
using BrightCare.Service.SuperAdmin.Organizations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Repository.Agency.Users;
using BrightCare.Repository.Interface.Agency.Staff;
using BrightCare.Repository.Agency.Staff;
using BrightCare.Service.Interface.Agency.Staffs;
using BrightCare.Service.Agency.Staff;
using BrightCare.Service.Interface.Agency.Patients;
using BrightCare.Service.Agency.Patient;
using BrightCare.Repository.Interface.Agency.Patients;
using BrightCare.Repository.Agency.Patients;
using BrightCare.Repository.Interface.Agency.MasterService;
using BrightCare.Repository.Interface.Agency.MasterServiceTypes;
using BrightCare.Repository.Interface.Agency.UserRole;
using BrightCare.Repository.Interface.Agency.MasterDocumentType;
using BrightCare.Service.Interface.Agency.MasterServices;
using BrightCare.Service.Interface.Agency.MasterServiceTypes;
using BrightCare.Service.Interface.Agency.UserRole;
using BrightCare.Service.Interface.Agency.MasterDocumentType;
using BrightCare.Service.Agency.MasterDocumentType;
using BrightCare.Service.Agency.UserRole;
using BrightCare.Service.Agency.MasterServiceTypes;
using BrightCare.Service.Agency.MasterService;
using BrightCare.Repository.Agency.MasterService;
using BrightCare.Repository.Agency.MasterServiceTypes;
using BrightCare.Repository.Agency.UserRole;
using BrightCare.Repository.Agency.MasterDocumentType;
using BrightCare.Repository.Interface.Agency.StaffLeave;
using BrightCare.Service.Interface.Agency.StaffLeave;
using BrightCare.Service.Agency.StaffLeave;
using BrightCare.Repository.Agency.StaffLeave;
using BrightCare.Service.Interface.Agency.LeaveTypes;
using BrightCare.Repository.Interface.Agency.LeaveTypes;
using BrightCare.Repository.Agency.LeaveTypes;
using BrightCare.Repository.Interface.Agency.LeaveReasons;
using BrightCare.Repository.Agency.LeaveReasons;
using BrightCare.Service.Agency.LeaveTypes;
using BrightCare.Service.Interface.Agency.LeaveReasons;
using BrightCare.Service.Agency.LeaveReasons;
using BrightCare.Repository.Interface.Agency.MasterAllergy;
using BrightCare.Repository.Agency.MasterAllergy;
using BrightCare.Service.Interface.Agency.MasterAllergy;
using BrightCare.Service.Agency.MasterAllergy;
using BrightCare.Repository.Interface.Agency.MasterAllergyReaction;
using BrightCare.Repository.Agency.MasterAllergyReaction;
using BrightCare.Service.Interface.Agency.MasterAllergyReaction;
using BrightCare.Service.Agency.MasterAllergyReaction;
using BrightCare.Repository.Interface.Agency.DiagnosisCodes;
using BrightCare.Repository.Agency.DiagnosisCodes;
using BrightCare.Service.Interface.Agency.DiagnosisCodes;
using BrightCare.Service.Agency.DiagnosisCodes;

namespace BrightCare.Web.Api.Dependency
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            #region Others
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
          //  services.AddScoped<DbContext, HCOrganizationContext>();
            #endregion



            #region SuperAdmin
            /////////////Repository///////////////
            services.AddScoped(typeof(IMasterRepositoryBase<>), typeof(MasterRepositoryBase<>));
            //organization
            services.AddScoped<IMasterOrganizationRepository, MasterOrganizationRepository>();


            /////////////services///////////////
            services.AddTransient<IMasterOrganizationService, MasterOrganizationService>();
            
            #endregion

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            

            #region Agency
            /////////////Repository///////////////
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            //organization
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IPatientRepository,PatientRepository>();
            services.AddScoped<IStaffLeaveRepository, StaffLeaveRepository>();
            //MasterService
            services.AddScoped<IMasterServicesRepository, MasterServicesRepository>();
            //MasterServiceType
            services.AddScoped<IMasterServiceTypeRepository, MasterServiceTypeRepository>();
            // UserRoles
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            //MasterDocumentType
            services.AddScoped<IMasterDocumentTypeRepository, MasterDocumentTypeRepository>();
            //LeaveType
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            //LeaveReason
            services.AddScoped<ILeaveReasonReapository, LeaveReasonRepository>();
            //MasterAllergy
            services.AddScoped<IMasterAllergyRepository, MasterAllergyRepository>();
            //MasterAllergyReaction
            services.AddScoped<IMasterAllergyReactionRepository, MasterAllergyReactionRepository>();
            //DiagnosisCodes
            services.AddScoped<IDiagnosisCodesRepository, DiagnosisCodesRepository>();


            /////////////services///////////////
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<ILoginServices, LoginService>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IMasterServices, MasterServicesService>();
            services.AddTransient<IMasterServiceType, MasterServiceTypeService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IMasterDocumentTypeService, MasterDocumentTypeService>();
            services.AddTransient<IStaffLeaveService, StaffLeaveService>();

            services.AddTransient<ILeaveTypeService, LeaveTypeService>();
            services.AddTransient<ILeaveReasonService, LeaveReasonService>();
            services.AddTransient<IMasterAllergyService, MasterAllergyService>();
            services.AddTransient<IMasterAllergyReactionService, MasterAllergyReactionService>();
            services.AddTransient<IDiagnosisCodesService, DiagnosisCodesService>();
            #endregion
        }
    }
}
