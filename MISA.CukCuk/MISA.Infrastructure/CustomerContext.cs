using Dapper;
using MISA.Infrastructure.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class CustomerContext
    {
        #region Method
        //Lấy toàn bộ danh sách khách hàng:
        /// <summary>
        /// lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// createdBy: HTTrang(11/01/1021)
        public IEnumerable<Customer> GetCustomers(){
            //Kết nối tới CSDL
            var connectionString = "User Id=nvmanh;Host=103.124.92.43;Port=3306;Password=12345678;Database=MISACukCuk_MF657_HTTRANG;";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            //Khởi tạo các commandText:
            var customers = dbConnection.Query<Customer>("Proc_GetCustomers", commandType: CommandType.Text);
            //Trả về dữ liệu
            return customers;
        }
        //Lấy thông tin khách hàng theo mã khách hàng

        /// <summary>
        /// Thêm mưới khách hàng
        /// </summary>
        /// <param name="customer">Object khách hàng</param>
        /// <returns>Số bản ghi bị ảnh hưởng(thêm mới được)</returns>
        /// CreatedBy HTTrang(11/01/2021)
        public int InsertCustomer(Customer customer)
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
        /// <summary>
        /// Lấy khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="customerCode">Mã khách hàng</param>
        /// <returns>object khách hàng đầu tiên lấy được</returns>
        /// CreatedBy HTTrang(11/01/2021)
        public Customer GetCustomerByCode(string customerCode)
        {
            var connectionString = "User Id=nvmanh;Host=103.124.92.43;Port=3306;Password=12345678;Database=MISACukCuk_MF657_HTTRANG;";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            var res = dbConnection.Query<Models.Customer>("Proc_GetCustomerByCode", new { CustomerCode = customerCode }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }
        //Thêm mới khách hàng

        //Sửa thông tin khách hàng

        //Xóa khách hàng theo khóa chính
        #endregion
    }
}
