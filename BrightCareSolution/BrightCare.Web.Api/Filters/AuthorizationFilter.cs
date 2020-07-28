
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BrightCare.Web.Api.Filters
{
    public sealed class AuthorizationFilter :IAuthorizationRequirement
    {
        public string AccessTokenValue { get; private set; }
        public AuthorizationFilter(string accessToken)
        {
            AccessTokenValue = accessToken;
        }
    }
}
