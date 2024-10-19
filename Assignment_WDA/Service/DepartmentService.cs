using Assignment_WDA.Entities;
using Assignment_WDA.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace Assignment_WDA.Service
{
    public class DepartmentService : IDepartmentRepository
    {
        private readonly DataContext _context;
        public DepartmentService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetAllDepartments() => await _context.Departments.ToListAsync();

        public async Task<Department> GetDepartmentById(int id) => await _context.Departments.FindAsync(id);

        public async Task AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
