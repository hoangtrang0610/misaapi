using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Infrastructure.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        #region DECLARE
        IConfiguration _configuration;
        #endregion
        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Method
        public int AddCustomer(Customer customer)
        {
            //Khởi tạo kết nối với Db:
            var connectionString = "User Id=nvmanh;Host=103.124.92.43;Port=3306;Password=12345678;Database=MISACukCuk_MF657_HTTRANG;";
            var dbConnection = new MySqlConnection(connectionString);
            var properties = customer.GetType().GetProperties();// lấy tất cả properties của customer
            var parameters = new DynamicParameters();
            //Xử lý các kiểu dữ liệu (mapping dataType)
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(customer);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            //Thực thi commandText
            var rowAffects = dbConnection.Execute("Proc_InsertCustomer", parameters, commandType: CommandType.StoredProcedure);
            //tả về kết quả(số bản ghi thêm mới được)
            return rowAffects;
        }

        public Customer DeleteCustomer(Guid customerId)
        {
            //Kết nối tới CSDL
            var connectionString = "User Id=nvmanh;Host=103.124.92.43;Port=3306;Password=12345678;Database=MISACukCuk_MF657_HTTRANG;";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            //Khởi tạo các commandText:
            var customers = dbConnection.Query<Customer>("Proc_GetCustomerbyId",new { CustomerId = customerId}, commandType: CommandType.Text).FirstOrDefault();
            //Trả về dữ liệu
            return customers;
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            var connectionString = "User Id=nvmanh;Host=103.124.92.43;Port=3306;Password=12345678;Database=MISACukCuk_MF657_HTTRANG;";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            var res = dbConnection.Query<Customer>("Proc_GetCustomerByCode",new {CustomerCode = customerCode }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }

        public Customer GetCustomerById(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            //Kết nối tới CSDL
            var connectionString = "User Id=nvmanh;Host=103.124.92.43;Port=3306;Password=12345678;Database=MISACukCuk_MF657_HTTRANG;";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            //Khởi tạo các commandText:
            var customers = dbConnection.Query<Customer>("Proc_GetCustomers", commandType: CommandType.Text);
            //Trả về dữ liệu
            return customers;
        }

        public int UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        int ICustomerRepository.DeleteCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
