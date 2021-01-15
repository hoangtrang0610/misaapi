using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class CustomerService:BaseService<Customer>, ICustomerService
    {
        IBaseRepository<Customer> _baseRepository;
        ICustomerRepository _customerRepository;
        #region Constructor
        public CustomerService(IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _customerRepository = customerRepository;
        }

        //public override  int Add(Customer entity)
        //{
        //    //validate thông tin
        //    var isValid = true;
        //    //1 check mã khách hàng
        //    var customerDuplicate = _customerRepository.GetCustomerByCode(entity.CustomerCode);
        //    if(customerDuplicate != null)
        //    {
        //        isValid = false;
        //    }
        //    //login validate:
        //    if(isValid == true)
        //    {
        //        var res = base.Add(entity);
        //        return res;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //Thêm mới khách hàng
        //public ServiceResult AddCustomer(Customer customer)
        //{
        //    var serviceResult = new ServiceResult();
        //    //Validate dữ liệu:
        //    //Check trường bắt buộc nhập, nếu dữ liệu chứ hợp lệ trả về lỗi
        //    var customerCode = customer.CustomerCode;
        //    if (string.IsNullOrEmpty(customerCode))
        //    {
        //        var msg = new
        //        {
        //            devMsg = new { fieldName = "CustomerCode", msg = "Mã khách hàng không được để trống" },
        //            userMsg = "Mã khách hàng không được để trống",
        //            Code = MISACode.NotValid,
        //        };
        //        serviceResult.MISACode = MISACode.NotValid;
        //        serviceResult.Messenger = "Mã khách hàng không được phép để trống";
        //        serviceResult.Data = msg;
        //        return serviceResult;
        //    }
        //    //Check trùng mã
        //    var res = _customerRepository.GetCustomerByCode(customerCode);
        //    if (res != null)
        //    {
        //        var msg = new
        //        {
        //            devMsg = new { fieldName = "CustomerCode", msg = "Mã khách hàng đã tồn tại" },
        //            userMsg = "Mã khách hàng không được để trống",
        //            Code = MISACode.NotValid,
        //        };
        //        serviceResult.MISACode = MISACode.NotValid;
        //        serviceResult.Messenger = "Mã khách hàng đã tồn tại";
        //        serviceResult.Data = msg;
        //        return serviceResult;
        //    }


        //    //Thêm mới khi dữ liệu hợp lệ
        //    var rowAffects = _customerRepository.AddCustomer(customer);
        //    serviceResult.MISACode = MISACode.IsValid;
        //    serviceResult.Messenger = "Thêm thành công";
        //    serviceResult.Data = rowAffects;
        //    return serviceResult;
        //}

        public ServiceResult UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomerPaging(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomersByGroup(Guid departmantId)
        {
            throw new NotImplementedException();
        }
        //Sửa thông tin khách hàng

        //Xóa khách hàng
        #endregion
    }
}
