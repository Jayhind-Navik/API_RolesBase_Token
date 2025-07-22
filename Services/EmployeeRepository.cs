using API_RolesBase_Token.Context;
using API_RolesBase_Token.Models;
using Microsoft.EntityFrameworkCore;

namespace API_RolesBase_Token.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateAsync(int id, Employee employee)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null) return null;

            existing.Name = employee.Name;
            //existing.LastName = employee.LastName;
            //existing.Email = employee.Email;
            existing.Department = employee.Department;
            //existing.DateOfJoining = employee.DateOfJoining;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null) return false;

            _context.Employees.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
