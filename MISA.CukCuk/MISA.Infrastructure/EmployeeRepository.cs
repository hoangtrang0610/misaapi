using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class EmployeeRepository :BaseReponsitory<Employee>, IEmployeeRepository
    {
    
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public Customer GetEmployeeByCode(string employeeCode)
        {
            var customerDuplicate = _dbConnection.Query<Customer>($"select * from employee where EmployeeCode = '{employeeCode}'", commandType: CommandType.Text).FirstOrDefault();
            return customerDuplicate;
        }

        public List<Employee> GetEmployeesFilter(string specs, Guid? departmentGroupId, Guid? positionGroupId)
        {
            //built tham số đầu vào cho store:
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", specs);
            parameters.Add("@FullName", specs);
            parameters.Add("@PhoneNumber", specs);
            parameters.Add("@DepartmentGroupId", departmentGroupId);
            parameters.Add("@PositionGroupId", positionGroupId);
            var employees = _dbConnection.Query<Employee>("Proc_GetEmployeePaging", parameters, commandType: CommandType.StoredProcedure).ToList();
            return null;
        }

        Employee IEmployeeRepository.GetEmployeeByCode(string employeeCode)
        {
            throw new NotImplementedException();
        }
    }
}
