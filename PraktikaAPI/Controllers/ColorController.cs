using Microsoft.AspNetCore.Mvc;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class ColorController : Controller
    {
        private IColorRepository _colorRepository;

        public ColorController(PraktikaDbContext praktikaDbContext)
        {
            _colorRepository = new ColorRepository(praktikaDbContext);
        }

        // GET: api/<ColorController>
        [HttpGet]
        [Route("api/[controller]/GetColors")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Color> data = _colorRepository.GetColors();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<ColorController>/5
        [HttpGet]
        [Route("api/[controller]/GetColorById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Color? data = _colorRepository.GetColorByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ColorController>
        [HttpPost]
        [Route("api/[controller]/InsertColor")]
        public IActionResult Post([FromBody] Color model)
        {
            try
            {
                _colorRepository.InsertColor(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ColorController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateColor")]
        public IActionResult Put([FromBody] Color model)
        {
            try
            {
                _colorRepository.UpdateColor(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<ColorController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteColor/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _colorRepository.DeleteColor(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
