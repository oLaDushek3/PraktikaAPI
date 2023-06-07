using Microsoft.AspNetCore.Mvc;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class SupplyController : Controller
    {
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

        // POST api/<SupplyController>
        [HttpPost]
        [Route("api/[controller]/InsertSupply")]
        public IActionResult Post([FromBody] Supply model)
        {
            try
            {
                _supplyRepository.InsertSupply(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<SupplyController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateSupply")]
        public IActionResult Put([FromBody] Supply model)
        {
            try
            {
                _supplyRepository.UpdateSupply(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<SupplyController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteSupply/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _supplyRepository.DeleteSupply(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
