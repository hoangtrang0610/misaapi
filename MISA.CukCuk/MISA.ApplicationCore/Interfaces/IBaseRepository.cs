 using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseRepository<TEntity>
    {

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Dánh sách khách hàng</returns>
        /// CreatedBy: HTTrang(13/01/2021)
        IEnumerable<TEntity> GetEntities();
        TEntity GetEntityById(Guid entityId);
        int Add(TEntity entity);
        int Update(TEntity cuentitystomer);
        int Delete(Guid entityId);
        TEntity GetEntityByProperty(TEntity entity, PropertyInfo property);
    }
}
