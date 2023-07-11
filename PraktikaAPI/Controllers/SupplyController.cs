using Microsoft.AspNetCore.Mvc;
using NLog;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class SupplyController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ISupplyRepository _supplyRepository;

        public SupplyController(PraktikaDbContext praktikaDbContext)
        {
            _supplyRepository = new SupplyRepository(praktikaDbContext);
        }

        // GET: api/<SupplyController>
        [HttpGet]
        [Route("api/[controller]/GetSupplys")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Supply> data = _supplyRepository.GetSupplys();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetSupplyProducts")]
        public IActionResult GetSupplyProducts()
        {
            try
            {
                IEnumerable<SupplyProduct> data = _supplyRepository.GetSupplyProducts();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<SupplyController>/5
        [HttpGet]
        [Route("api/[controller]/GetSupplyById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Supply? data = _supplyRepository.GetSupplyByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetSupplyProductById/{id}")]
        public IActionResult GetSupplyProduct(int id)
        {
            try
            {
                Supply? data = _supplyRepository.GetSupplyByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<SupplyController>
        [HttpPost]
        [Route("api/[controller]/InsertSupply/{userId}")]
        public IActionResult Post([FromBody] Supply model, int userId)
        {
            try
            {
                _supplyRepository.InsertSupply(model);
                logger.Info($"Создание поставки: Пользователь: {userId}; SupplyId: {model.SupplyId}; Date: {model.Date}");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Route("api/[controller]/InsertSupplyProduct/{userId}")]
        public IActionResult PostSupplyProduct([FromBody] SupplyProduct model, int userId)
        {
            try
            {
                _supplyRepository.InsertSupplyProduct(model);
                logger.Info($"Добавление продукции в поставке: Пользователь: {userId}; SupplyId: {model.SupplyId}; ProductId: {model.ProductId}");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<SupplyController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateSupply/{userId}")]
        public IActionResult Put([FromBody] Supply model, int userId)
        {
            try
            {
                _supplyRepository.UpdateSupply(model);
                logger.Info($"Обновление поставки: Пользователь: {userId}; SupplyId: {model.SupplyId}; Date: {model.Date}");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        [Route("api/[controller]/UpdateSupplyProduct/{userId}")]
        public IActionResult PutSupplyProduct([FromBody] SupplyProduct model, int userId)
        {
            try
            {
                _supplyRepository.UpdateSupplyProduct(model);
                logger.Info($"Обновление продукции в поставке: Пользователь: {userId}; SupplyId: {model.SupplyId}; ProductId: {model.ProductId}");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<SupplyController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteSupply/{id}/{userId}")]
        public IActionResult Delete(int id, int userId)
        {
            try
            {
                if (_supplyRepository.DeleteSupply(id))
                {
                    logger.Info($"Удаление поставки: Пользователь: {userId}; SupplyId: {id}");
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route("api/[controller]/DeleteSupplyProduct/{id}/{userId}")]
        public IActionResult DeleteSupplyProduct(int id, int userId)
        {
            try
            {
                if (_supplyRepository.DeleteSupplyProduct(id))
                {
                    logger.Info($"Удаление продукции из поставки: Пользователь: {userId}; SupplyProductId: {id}");
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
