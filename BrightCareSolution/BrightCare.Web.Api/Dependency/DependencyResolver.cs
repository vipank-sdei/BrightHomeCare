using BrightCare.Persistence;
using BrightCare.Repository.Agency;
using BrightCare.Repository.Agency.MasterDocumentType;
using BrightCare.Repository.Agency.MasterService;
using BrightCare.Repository.Agency.MasterServiceTypes;
using BrightCare.Repository.Agency.Organizations;
using BrightCare.Repository.Agency.UserRole;
using BrightCare.Repository.Interface.Agency;
using BrightCare.Repository.Interface.Agency.MasterDocumentType;
using BrightCare.Repository.Interface.Agency.MasterService;
using BrightCare.Repository.Interface.Agency.MasterServiceTypes;
using BrightCare.Repository.Interface.Agency.Organizations;
using BrightCare.Repository.Interface.Agency.UserRole;
using BrightCare.Repository.Interface.SuperAdmin;
using BrightCare.Repository.Interface.SuperAdmin.Organizations;
using BrightCare.Repository.SuperAdmin;
using BrightCare.Repository.SuperAdmin.Organizations;
using BrightCare.Service.Agency.MasterDocumentType;
using BrightCare.Service.Agency.MasterService;
using BrightCare.Service.Agency.MasterServiceTypes;
using BrightCare.Service.Agency.Organizations;
using BrightCare.Service.Agency.UserRole;
using BrightCare.Service.Interface.Agency.MasterDocumentType;
using BrightCare.Service.Interface.Agency.MasterServices;
using BrightCare.Service.Interface.Agency.MasterServiceTypes;
using BrightCare.Service.Interface.Agency.Organizations;
using BrightCare.Service.Interface.Agency.UserRole;
using BrightCare.Service.Interface.SuperAdmin.Organizations;
using BrightCare.Service.SuperAdmin.Organizations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            //MasterService
            services.AddScoped<IMasterServicesRepository, MasterServicesRepository>();
            //MasterServiceType
            services.AddScoped<IMasterServiceTypeRepository, MasterServiceTypeRepository>();
            // UserRoles
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            //MasterDocumentType
            services.AddScoped<IMasterDocumentTypeRepository, MasterDocumentTypeRepository>();


            /////////////services///////////////
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IMasterServices, MasterServicesService>();
            services.AddTransient<IMasterServiceType, MasterServiceTypeService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IMasterDocumentTypeService, MasterDocumentTypeService>();
            #endregion



        }
    }
}
