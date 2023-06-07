using Microsoft.AspNetCore.Mvc;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController(PraktikaDbContext praktikaDbContext)
        {
            _productRepository = new ProductRepository(praktikaDbContext);
        }

        // GET: api/<ProductController>
        [HttpGet]
        [Route("api/[controller]/GetProducts")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Product> data = _productRepository.GetProducts();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet]
        [Route("api/[controller]/GetProductById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Product? data = _productRepository.GetProductByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        [Route("api/[controller]/InsertProduct")]
        public IActionResult Post([FromBody] Product model)
        {
            try
            {
                _productRepository.InsertProduct(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateProduct")]
        public IActionResult Put([FromBody] Product model)
        {
            try
            {
                _productRepository.UpdateProduct(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteProduct/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
