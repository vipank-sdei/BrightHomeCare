using System;
using System.ComponentModel.DataAnnotations;

namespace BrightCare.Entity.Agency
{
    public class ExceptionLog
    {
        [Key] 
        public int ExceptionId { get; set; }
        public int UserId { get; set; }
        public int AgencyId { get; set; }        
        public string Host { get; set; }        
        public string Source { get; set; }
        public string Message { get; set; }     
        public string StackTrace { get; set; }
        public int StatusCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
