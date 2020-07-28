using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrightCare.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightCare.Web.Api.Controllers
{
   
   
    //[Authorize(Policy = "AuthorizedUser")]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public TokenModel GetToken(HttpContext httpContext)
        {
            return CommonMethods.GetTokenDataModel(httpContext);
        }
        //[NonAction]
        //public TokenModel GetBussinessToken(HttpContext httpContext, ITokenService tokenService)
        //{
        //    var bussinessName = CommonMethods.Decrypt(httpContext.Request.Headers["businessToken"].ToString());
        //    DomainToken domainToken = new DomainToken
        //    {
        //        BusinessToken = bussinessName
        //    };
        //    DomainToken tokenData = tokenService.GetDomain(domainToken);

        //    TokenModel token = new TokenModel
        //    {
        //        Request = httpContext,
        //        OrganizationID = tokenData.OrganizationId
        //    };
        //    return token;
        //}
    }
}