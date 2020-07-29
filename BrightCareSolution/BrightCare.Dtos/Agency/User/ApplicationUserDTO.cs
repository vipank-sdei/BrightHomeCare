using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Dtos.Agency
{
   public class ApplicationUserDTO
    {
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool IsValid { get; set; }
            public string IpAddress { get; set; }
            public string MacAddress { get; set; }
            public string BusinessToken { get; set; }
            public string DeviceToken { get; set; }
        
    }
}
