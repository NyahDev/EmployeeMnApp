using EmployeeMnApp.Models;

namespace EmployeeMnApp.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> GetEmployeeByLastName(string Lastname);
        Task<Employee> GetEmployeeByLocation(string Location);
        Task<Employee> GetEmployeeByDateEmployed(DateTime dateEmployed);
        Task CreateEmployee (Employee employee);
        Task UpdateEmployee (Employee employee);
        Task DeleteEmployee(int id);
    }
}
