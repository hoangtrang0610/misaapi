using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Api.Controllers
{
    public class EmployeesController : BaseEntityController<Employee>
    {
        IBaseService<Employee> _baseService;
        public EmployeesController(IBaseService<Employee> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
        [HttpGet("filter")]
        public IActionResult GetEmployeeFilter([FromQuery] string specs, [FromBody] Guid? departmentGroupId, Guid? positionGroupId)
        {
            return Ok(_baseService.GetEmployeesFilter(specs, departmentGroupId, positionGroupId));
        }
    }
}
