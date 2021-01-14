using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {
        IBaseService<TEntity> _baseService;
        public BaseEntityController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        /// <summary>
        /// lấy toàn bộ khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: HTTrang(08/01/2021)
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.GetEntities();
            return Ok(entities);
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
            var entity = _baseService.GetEntityById(Guid.Parse(id));
            return Ok(entity);
        }

        /// <summary>
        /// thêm 1 khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: HTTrang(09/01/2021)
        [HttpPost]
        public IActionResult Post(TEntity entity)
        {
            var rowEffects = _baseService.Add(entity);
            return Ok(rowEffects);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(1);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _baseService.Delete(id);
            return Ok("delete");
        }
    }
}
