using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    public class PositionGroup:BaseEntity
    {
        public Guid PositionGroupId { get; set; }
        public string PositionGroupName { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModefiedDate { get; set; }
        public string ModefiedBy { get; set; }
    }
}
