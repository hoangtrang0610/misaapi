using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseService<TEntity>
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
        TEntity GetEntityByCode(string entityCode);
    }
}
