using System;
using System.Collections.Generic;
using System.Text;
using BrightCare.Dtos.Agency;
using BrightCare.Common.Model;
using BrightCare.Common.Options;
using BrightCare.Common;

namespace BrightCare.Service.Interface.Agency.Login
{
    public interface ILoginServices
    {
        JsonModel Login(ApplicationUser applicationUser, JwtIssuerOptions _jwtOptions);
    }
}
