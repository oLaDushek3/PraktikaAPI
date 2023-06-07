using Microsoft.AspNetCore.Mvc;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class ProductGroupController : Controller
    {
        private IProductGroupRepository _ProductGroupRepository;

        public ProductGroupController(PraktikaDbContext praktikaDbContext)
        {
            _ProductGroupRepository = new ProductGroupRepository(praktikaDbContext);
        }

        // GET: api/<ProductGroupController>
        [HttpGet]
        [Route("api/[controller]/GetProductGroups")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<ProductGroup> data = _ProductGroupRepository.GetProductGroups();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<ProductGroupController>/5
        [HttpGet]
        [Route("api/[controller]/GetProductGroupById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ProductGroup? data = _ProductGroupRepository.GetProductGroupByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ProductGroupController>
        [HttpPost]
        [Route("api/[controller]/InsertProductGroup")]
        public IActionResult Post([FromBody] ProductGroup model)
        {
            try
            {
                _ProductGroupRepository.InsertProductGroup(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ProductGroupController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateProductGroup")]
        public IActionResult Put([FromBody] ProductGroup model)
        {
            try
            {
                _ProductGroupRepository.UpdateProductGroup(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<ProductGroupController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteProductGroup/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _ProductGroupRepository.DeleteProductGroup(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
