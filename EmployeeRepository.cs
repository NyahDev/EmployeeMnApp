using EmployeeMnApp.Data;
using EmployeeMnApp.Interfaces;
using EmployeeMnApp.Models;

namespace EmployeeMnApp
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task CreateEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Add(employee);
            await _employeeDbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeByDateEmployed(DateTime dateEmployed)
        {
            return await _employeeDbContext.Employees.FindAsync(dateEmployed);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeDbContext.Employees.FindAsync(id);
        }

        public async Task<Employee> GetEmployeeByLastName(string Lastname)
        {
            return await _employeeDbContext.Employees.FindAsync(Lastname);
        }

        public async Task<Employee> GetEmployeeByLocation(string Location)
        {
            // return await _employeeDbContext.Employees.FindAsync(Location);
            var loca = await _employeeDbContext.Employees.FindAsync(Location);
            if(loca != null)
            {
                return loca;
            }
            return new Employee();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _employeeDbContext.Update(employee);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
