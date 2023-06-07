using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;

namespace PraktikaAPI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(PraktikaDbContext praktikaDbContext, IConfiguration config)
        {
            _employeeRepository = new EmployeeRepository(praktikaDbContext);
            _config = config;
        }

        // GET api/<EmployeeController>
        [HttpGet]
        [Route("api/[controller]/GetEmployees")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Employee> data = _employeeRepository.GetEmployees();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet]
        [Route("api/[controller]/GetEmployeeById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Employee data = _employeeRepository.GetEmployeeByID(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }        

        // GET api/<EmployeeController>/login
        [HttpGet]
        [Route("api/[controller]/GetEmployeeByLogin/{login}")]
        public IActionResult Get(string login)
        {
            try
            {
                Employee data = _employeeRepository.GetEmployeeByLogin(login);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        [Route("api/[controller]/InsertEmployee")]
        public IActionResult Post([FromBody] Employee model)
        {
            try
            {
                Employee? employee = _employeeRepository.GetEmployeeByLogin(model.Login);
                if (employee == null)
                {
                    _employeeRepository.InsertEmployee(model);
                    return Ok();
                }
                else
                    return BadRequest("Login busy");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateEmployee")]
        public IActionResult Put([FromBody] Employee model)
        {
            try
            {
                _employeeRepository.UpdateEmployee(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteEmployee/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeRepository.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/[controller]/LoginEmployee/{login}/{password}")]
        public IActionResult Login(string login, string password)
        {
            try
            {
                Employee? loginEmployee = _employeeRepository.GetEmployeeByLogin(login);
                if (loginEmployee == null)
                    return Unauthorized("Invalid login");
                else
                {
                    if (_employeeRepository.LoginEmployee(loginEmployee, password))
                        return Ok();
                    else
                        return Unauthorized("Invalid password");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
