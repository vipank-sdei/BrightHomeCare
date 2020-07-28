using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrightCare.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExceptionController : ControllerBase
    {
        

        public ExceptionController()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("addException")]
        public IActionResult Get()
        {
            // Retrieve error information in case of internal errors
            var path = HttpContext
                      .Features
                      .Get<IExceptionHandlerPathFeature>();            

            // Use the information about the exception 
            var exception = path.Error;
            var pathString = path.Path;

        ////// save exception to DB///////////
        ///

            return  BadRequest(new { message = exception.Message, code = HttpStatusCode.BadRequest });
        }
    }
}
