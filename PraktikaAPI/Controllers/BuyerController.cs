using Microsoft.AspNetCore.Mvc;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class BuyerController : Controller
    {
        private IBuyerRepository _buyerRepository;

        public BuyerController(PraktikaDbContext praktikaDbContext)
        {
            _buyerRepository = new BuyerRepository(praktikaDbContext);
        }

        // GET: api/<BuyerController>
        [HttpGet]
        [Route("api/[controller]/GetBuyers")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Buyer> data = _buyerRepository.GetBuyers();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<BuyerController>/5
        [HttpGet]
        [Route("api/[controller]/GetBuyerById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Buyer data = _buyerRepository.GetBuyerByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<BuyerController>
        [HttpPost]
        [Route("api/[controller]/InsertBuyer")]
        public IActionResult Post([FromBody] Buyer model)
        {
            try
            {
                _buyerRepository.InsertBuyer(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<BuyerController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateBuyer")]
        public IActionResult Put([FromBody] Buyer model)
        {
            try
            {
                _buyerRepository.UpdateBuyer(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<BuyerController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteBuyer/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _buyerRepository.DeleteBuyer(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
