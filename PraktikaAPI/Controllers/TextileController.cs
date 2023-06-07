using Microsoft.AspNetCore.Mvc;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class TextileController : Controller
    {
        private ITextileRepository _textileRepository;

        public TextileController(PraktikaDbContext praktikaDbContext)
        {
            _textileRepository = new TextileRepository(praktikaDbContext);
        }

        // GET: api/<TextileController>
        [HttpGet]
        [Route("api/[controller]/GetTextiles")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Textile> data = _textileRepository.GetTextiles();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<TextileController>/5
        [HttpGet]
        [Route("api/[controller]/GetTextileById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Textile? data = _textileRepository.GetTextileByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<TextileController>
        [HttpPost]
        [Route("api/[controller]/InsertTextile")]
        public IActionResult Post([FromBody] Textile model)
        {
            try
            {
                _textileRepository.InsertTextile(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<TextileController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateTextile")]
        public IActionResult Put([FromBody] Textile model)
        {
            try
            {
                _textileRepository.UpdateTextile(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<TextileController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteTextile/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _textileRepository.DeleteTextile(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
