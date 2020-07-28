using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrightCare.Common;
using BrightCare.Common.Options;
using Microsoft.Extensions.Options;
using BrightCare.Service.Interface.Agency.Login;
using BrightCare.Dtos.Agency;


namespace BrightCare.Web.Api.Controllers.Agency
{
    [Route("api/agency/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginServices _loginService;
        private readonly JwtIssuerOptions _jwtOptions;

        public LoginController(ILoginServices loginService, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            _loginService = loginService;
        }

       [HttpPost]
        public ActionResult login(ApplicationUser applicationUser)
        {
            return Ok(_loginService.Login(applicationUser, _jwtOptions));
        }
    }
}
