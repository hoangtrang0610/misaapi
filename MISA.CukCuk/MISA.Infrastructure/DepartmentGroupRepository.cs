using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure
{
   public  class DepartmentGroupRepository : BaseReponsitory<DepartmentGroup>, IDepartmentGroupRepository
    {
        public DepartmentGroupRepository(IConfiguration configuration) : base(configuration)
        {

        }

    }
}
