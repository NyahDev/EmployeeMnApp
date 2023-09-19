using EmployeeMnApp.Data;
using EmployeeMnApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMnApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeesController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_employeeDbContext.Employees == null)
            {
                return NotFound();
            }
            return await _employeeDbContext.Employees.ToListAsync();
        }

        [HttpGet("{Id}")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        //[ProducesResponseType(400)]
        public async Task<ActionResult<Employee>> GetEmployeesById(int Id)
        {
            if (_employeeDbContext.Employees == null)
            {
                return NotFound();
            }
            var employee = await _employeeDbContext.Employees.FindAsync(Id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost("Employee")]
        //[ProducesResponseType(204, Type = typeof(IEnumerable<Employee>))]
        //[ProducesResponseType(400)]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (_employeeDbContext.Employees == null)
            {
                return Problem("Entity set 'BookStoreDbContext.Users'  is null.");
            }
            _employeeDbContext.Employees.Add(employee);
            await _employeeDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public async Task<IActionResult> PutEmployee( int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _employeeDbContext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _employeeDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("Delete")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_employeeDbContext.Employees == null)
            {
                return NotFound();
            }
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        private bool EmployeeExists(int id)
        {
            return (_employeeDbContext.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
