using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Dtos.Agency.StaffLeave
{
   public class StaffLeaveDTO
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveType { get; set; }
        public int LeaveReasonId { get; set; }
        public string LeaveReason { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        //public decimal TotalRecords { get; set; }
    }
}
