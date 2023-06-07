using Microsoft.EntityFrameworkCore;
using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private PraktikaDbContext _context;

        public EmployeeRepository(PraktikaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();
            list = _context.Employees.
                Include(b => b.Orders).ToList();
            return list;
        }

        public Employee? GetEmployeeByID(int employeeId)
        {
            return _context.Employees.
                Include(b => b.Orders).FirstOrDefault(b => b.EmployeeId == employeeId);
        }
        public Employee? GetEmployeeByLogin(string employeeLogin)
        {
            return _context.Employees.
                Include(b => b.Orders).FirstOrDefault(b => b.Login == employeeLogin);
        }

        public void InsertEmployee(Employee employee)
        {
            employee.Password = EncryptionPassword.HashPassword(employee.Password);
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(int employeeId)
        {
            Employee? employee = _context.Employees.
                Include(b => b.Orders).FirstOrDefault(b => b.EmployeeId == employeeId);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool LoginEmployee(Employee loginEmployee, string password)
        {
            if (EncryptionPassword.VerifyHashedPassword(loginEmployee.Password, password))
                return true;
            else
                return false;

        }
    }
}
