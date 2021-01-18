using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
 
    public class DepartmentGroupsController : BaseEntityController<DepartmentGroup>
    {
        IBaseService<DepartmentGroup> _employeeService;
        public DepartmentGroupsController(IBaseService<DepartmentGroup> employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
    }
}
