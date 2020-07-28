using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrightCare.Entity.SuperAdmin
{
    public class SuperUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsBlock { get; set; }
        public DateTime? BlockDateTime { get; set; }
        
    }
}
