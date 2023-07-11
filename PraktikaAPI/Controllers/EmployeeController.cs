using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Fluent;
using PraktikaAPI.DAL;
using PraktikaAPI.Models;
using System.IO;
using System.Reflection;

namespace PraktikaAPI.Controllers
{
    public class EmployeeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
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

        // GET api/<EmployeeController>/role
        [HttpGet]
        [Route("api/[controller]/GetRoles")]
        public IActionResult GetRoles()
        {
            try
            {
                IEnumerable<Role> data = _employeeRepository.GetRoles();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        [Route("api/[controller]/InsertEmployee/{userId}")]
        public IActionResult Post([FromBody] Employee model, int userId)
        {
            try
            {
                Employee? employee = _employeeRepository.GetEmployeeByLogin(model.Login);
                if (employee == null)
                {
                    _employeeRepository.InsertEmployee(model);
                    logger.Info($"Создание сотрудника: Пользователь: {userId}; EmployeeId: {model.EmployeeId}; Login: {model.Login}; FullName: {model.FullName}");
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
        [Route("api/[controller]/UpdateEmployee/{userId}")]
        public IActionResult Put([FromBody] Employee model, int userId)
        {
            try
            {
                _employeeRepository.UpdateEmployee(model);
                logger.Info($"Обновление сотрудника: Пользователь: {userId}; EmployeeId: {model.EmployeeId}; Login: {model.Login}; FullName: {model.FullName}");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteEmployee/{id}/{userId}")]
        public IActionResult Delete(int id, int userId)
        {
            try
            {
                _employeeRepository.DeleteEmployee(id);
                logger.Info($"Удаление сотрудника: Пользователь: {userId}; EmployeeId: {id}");
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
                logger.Info($"Попытка авторизации: Логин: {login}; Пароль: {password}");

                Employee? loginEmployee = _employeeRepository.GetEmployeeByLogin(login);
                if (loginEmployee == null)
                    return Unauthorized("Invalid login");
                else
                {
                    if (_employeeRepository.LoginEmployee(loginEmployee, password))
                        return Ok(loginEmployee);
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
