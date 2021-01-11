using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MISA.ApplicationCore;
using MISA.Infrastructure.Models;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// lấy toàn bộ khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: HTTrang(08/01/2021)
        [HttpGet]
        public IActionResult Get()
        {
            var customerService = new CustomerService();
            var customers = customerService.GetCustomers();
            return Ok(customers);
        }

        /// <summary>
        /// lấy danh sách khách hàng theo id và tên
        /// </summary>
        /// <param name="id">id của khách hàng</param>
        /// <param name="name">tên của khách ahngf</param>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: HTTrang(08/01/2021) 
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var connectionString = "User Id=nvmanh;Host=103.124.92.43;Port=3306;Password=12345678;Database=MISACukCuk_MF657_HTTRANG;";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            var customers = dbConnection.Query<Customer>("Proc_GetCustomerById", new { CustomerCode = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return Ok(customers);
        }

        /// <summary>
        /// thêm 1 khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: HTTrang(09/01/2021)
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            var customerService = new CustomerService();
            var serviceResult = customerService.InsertCustomer(customer);
            if (serviceResult.MISACode == 900)
            {
                return BadRequest(serviceResult.Data);
            }
            else if (serviceResult.MISACode == 100 && (int)serviceResult.Data > 0)
            {
                return Created("abc", customer);
            }
            else
            {
                return NoContent();
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(1);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("delete");
        }
    }
}

