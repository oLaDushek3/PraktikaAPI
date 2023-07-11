using Microsoft.AspNetCore.Mvc;
using NLog;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class OrderController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IOrderRepository _OrderRepository;

        public OrderController(PraktikaDbContext praktikaDbContext)
        {
            _OrderRepository = new OrderRepository(praktikaDbContext);
        }

        // GET: api/<OrderController>
        [HttpGet]
        [Route("api/[controller]/GetOrders")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Order> data = _OrderRepository.GetOrders();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<OrderController>/5
        [HttpGet]
        [Route("api/[controller]/GetOrderById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Order? data = _OrderRepository.GetOrderByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<OrderController>
        [HttpPost]
        [Route("api/[controller]/InsertOrder/{userId}")]
        public IActionResult Post([FromBody] Order model, int userId)
        {
            try
            {
                _OrderRepository.InsertOrder(model);
                logger.Info($"Создание заказа: Пользователь: {userId}; OrderId: {model.OrderId}; BuyerId: {model.BuyerId}; Date: {model.Date}; Amount: {model.Amount};  Delivery: {model.Delivery};  Assembly: {model.Assembly};");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateOrder/{userId}")]
        public IActionResult Put([FromBody] Order model, int userId)
        {
            try
            {
                _OrderRepository.UpdateOrder(model);
                logger.Info($"Обновление заказа: Пользователь: {userId}; OrderId: {model.OrderId}; BuyerId: {model.BuyerId}; Date: {model.Date}; Amount: {model.Amount};  Delivery: {model.Delivery};  Assembly: {model.Assembly};");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<OrderController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteOrder/{id}/{userId}")]
        public IActionResult Delete(int id, int userId)
        {
            try
            {
                _OrderRepository.DeleteOrder(id);
                logger.Info($"Удаление заказа: Пользователь: {userId}; OrderId: {id}");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
