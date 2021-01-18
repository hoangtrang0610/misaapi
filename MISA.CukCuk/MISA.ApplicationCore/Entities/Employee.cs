using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    /// CreatedBy: HTTrang(14/01/2020)
    public class Employee:BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? PositionGroupId { get; set; }
        public Guid? DepartmentGroupId { get; set; }
        public string PersonalTaxCode { get; set; }
        public string Salary { get; set; }
        public string JoinDate { get; set; }
        public string WorkStatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
