using Microsoft.AspNetCore.Mvc;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class ProductTypeController : Controller
    {
        private IProductTypeRepository _ProductTypeRepository;

        public ProductTypeController(PraktikaDbContext praktikaDbContext)
        {
            _ProductTypeRepository = new ProductTypeRepository(praktikaDbContext);
        }

        // GET: api/<ProductTypeController>
        [HttpGet]
        [Route("api/[controller]/GetProductTypes")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<ProductType> data = _ProductTypeRepository.GetProductTypes();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<ProductTypeController>/5
        [HttpGet]
        [Route("api/[controller]/GetProductTypeById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ProductType? data = _ProductTypeRepository.GetProductTypeByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ProductTypeController>
        [HttpPost]
        [Route("api/[controller]/InsertProductType")]
        public IActionResult Post([FromBody] ProductType model)
        {
            try
            {
                _ProductTypeRepository.InsertProductType(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ProductTypeController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateProductType")]
        public IActionResult Put([FromBody] ProductType model)
        {
            try
            {
                _ProductTypeRepository.UpdateProductType(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<ProductTypeController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteProductType/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _ProductTypeRepository.DeleteProductType(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
