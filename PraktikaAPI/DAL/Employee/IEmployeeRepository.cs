using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee? GetEmployeeByID(int employeeId);
        Employee? GetEmployeeByLogin(string employeeLogin);
        void InsertEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
        void UpdateEmployee(Employee employee);
        bool LoginEmployee(Employee loginEmployee, string password);
    }
}
