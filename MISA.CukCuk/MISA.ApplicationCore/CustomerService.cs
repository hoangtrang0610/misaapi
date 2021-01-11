using MISA.ApplicationCore.Entities;
using MISA.Infrastructure;
using MISA.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore
{
    public class CustomerService
    {
        #region Method
        //Lấy danh sách khách hàng
        public IEnumerable<Customer> GetCustomers()
        {
            var customerContext = new CustomerContext();
            var customers = customerContext.GetCustomers();
            return customers;
        }

        //Thêm mới khách hàng
        public ServiceResult InsertCustomer(Customer customer)
        {
            var serviceResult = new ServiceResult();
            var customerContext = new CustomerContext();
            //Validate dữ liệu:
            //Check trường bắt buộc nhập, nếu dữ liệu chứ hợp lệ trả về lỗi
            var customerCode = customer.CustomerCode;
            if (string.IsNullOrEmpty(customerCode))
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Mã khách hàng không được để trống" },
                    userMsg = "Mã khách hàng không được để trống",
                    Code = 999,
                };
                serviceResult.MISACode = 900;
                serviceResult.Messenger = "Mã khách hàng không được phép để trống";
                serviceResult.Data = msg;
                return serviceResult;
            }
            //Check trùng mã
            var res = customerContext.GetCustomerByCode(customerCode);
            if (res != null)
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Mã khách hàng đã tồn tại" },
                    userMsg = "Mã khách hàng không được để trống",
                    Code = 999,
                };
                serviceResult.MISACode = 900;
                serviceResult.Messenger = "Mã khách hàngđã tồn tại";
                serviceResult.Data = msg;
                return serviceResult;
            }


            //Thêm mới khi dữ liệu hợp lệ
            var rowAffects = customerContext.InsertCustomer(customer);
            serviceResult.MISACode = 100;
            serviceResult.Messenger = "Thêm thành công";
            serviceResult.Data = rowAffects;
            return serviceResult;
        }
        //Sửa thông tin khách hàng

        //Xóa khách hàng
        #endregion
    }
}
