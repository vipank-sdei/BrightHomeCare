using BrightCare.Service.Interface.Agency.Login;
using System;
using System.Collections.Generic;
using System.Text;
using BrightCare.Dtos.Agency;
using BrightCare.Common.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using BrightCare.Common.Options;
using BrightCare.Common;
using BrightCare.Repository.Interface.Agency.Users;
using BrightCare.Repository.Interface.Agency.Staff;
using HC.Common;
using static BrightCare.Common.Enums.CommonEnum;
using AutoMapper;
using System.Security.Principal;
using BrightCare.Entity.Agency;

namespace BrightCare.Service.Agency.Login
{

    public class LoginService : ILoginServices
    {
        private readonly IUserRepository iUserRepository;
        private readonly IStaffRepository iStaffRepository;
        private readonly IMapper _mapper;

        public LoginService(IUserRepository iUserRepository, IStaffRepository iStaffRepository, IMapper mapper)
        {
            this.iUserRepository = iUserRepository;
            this.iStaffRepository = iStaffRepository;
            _mapper = mapper;

        }
        public JsonModel Login(ApplicationUserDTO applicationUser, JwtIssuerOptions _jwtOptions)
        {
            // check user by Username
            User dbuser = iUserRepository.GetFirstOrDefault(a => a.UserName == applicationUser.UserName);

 
            if (dbuser != null)
            {
                // get User info 
                Staffs staff = iStaffRepository.GetFirstOrDefault(a => a.UserID == dbuser.Id);
                if (staff == null)
                {
                    return new JsonModel(null, StatusMessage.StaffInfoNotFound, (int)HttpStatusCodes.NotFound);
                }
                // check user active or inactive
                else if (staff.IsActive == false)
                {
                    return new JsonModel(null, StatusMessage.AccountDeactivated, (int)HttpStatusCodes.Unauthorized);
                }
                else
                {
                    // check username and password
                    var identity = GetClaimsIdentity(applicationUser, dbuser);
                    if (identity != null)
                    {
                        return LoginUser(applicationUser, _jwtOptions, dbuser, staff, identity);
                    }
                }
            }
            return new JsonModel(null, StatusMessage.InvalidUserOrPassword, (int)HttpStatusCodes.Unauthorized);
        }

        private static ClaimsIdentity GetClaimsIdentity(ApplicationUserDTO user, User dbUser)
        {
            // check username and password
            var password = CommonMethods.Decrypt(dbUser.Password);
            if (dbUser != null && (user.UserName.ToUpper() == dbUser.UserName.ToUpper() && user.Password == CommonMethods.Decrypt(dbUser.Password)))
            {
                return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"),
                  new[]
                  {
                   new System.Security.Claims.Claim("HealthCare", "IAmAuthorized")
                  });
            }
            else
            {
                return null;
            }

        }

        private JsonModel LoginUser(ApplicationUserDTO applicationUser, JwtIssuerOptions _jwtOptions, User dbuser, Staffs userInfo, ClaimsIdentity identity)
        {
            // initialize claims for jwt token
            var claims = new[]
                   {
                        new System.Security.Claims.Claim("UserName",  applicationUser.UserName.ToString()),
                         new System.Security.Claims.Claim("UserID",dbuser.Id.ToString()),
                         new System.Security.Claims.Claim("StaffID",userInfo.Id.ToString()),
                        new System.Security.Claims.Claim("OrganizationID",dbuser.OrganizationID.ToString()),
                        new System.Security.Claims.Claim("RoleID",userInfo.RoleID.ToString()),
                        new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                        new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator()),
                        identity.FindFirst("HealthCare")
                };
            // initilize jwt params
            var jwt = new JwtSecurityToken(
               issuer: _jwtOptions.Issuer,
               audience: _jwtOptions.Audience,
               claims: claims,
               notBefore: _jwtOptions.NotBefore,
               expires: _jwtOptions.Expiration,
               signingCredentials: _jwtOptions.SigningCredentials);
            // Genrate Jwt token
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new JsonModel
            {
                access_token = encodedJwt,
                data = "User Authenticated",
            };
            return response;
        }
    }
}
