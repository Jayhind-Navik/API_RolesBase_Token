using API_RolesBase_Token.Models;
using API_RolesBase_Token.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_RolesBase_Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Employee
        [HttpGet("user")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var employees = await _repository.GetAllAsync();
            return Ok(employees);
            //return Ok($"Welcome User: {User.Identity?.Name}");
        }

        // GET: api/Employee/5
        [HttpGet("admin/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            var created = await _repository.AddAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Update(int id, Employee employee)
        {
            var updated = await _repository.UpdateAsync(id, employee);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
