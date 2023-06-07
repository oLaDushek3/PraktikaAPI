using Microsoft.AspNetCore.Mvc;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class OrderController : Controller
    {
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
        [Route("api/[controller]/InsertOrder")]
        public IActionResult Post([FromBody] Order model)
        {
            try
            {
                _OrderRepository.InsertOrder(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateOrder")]
        public IActionResult Put([FromBody] Order model)
        {
            try
            {
                _OrderRepository.UpdateOrder(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<OrderController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteOrder/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _OrderRepository.DeleteOrder(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
