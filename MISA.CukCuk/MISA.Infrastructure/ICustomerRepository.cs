using MISA.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure
{
    /// <summary>
    /// Interface danh mục khách hàng
    /// </summary>
    /// CreatedBy: HTTrang(13/01/2021)
    
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy danh sách khách hàng
        /// </summary>
        /// <returns>Dánh sách khách hàng</returns>
        /// CreatedBy: HTTrang(13/01/2021)
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(Guid customerId);
        int AddCustomer(Customer customer);
        int UpdateCustomer(Customer customer);
        int DeleteCustomer(Guid customerId);
        Customer GetCustomerByCode(string customerCode);
    }
}
