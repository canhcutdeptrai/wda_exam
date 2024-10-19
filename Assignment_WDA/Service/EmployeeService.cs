using Assignment_WDA.Entities;
using Assignment_WDA.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace Assignment_WDA.Service
{
    public class EmployeeService : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.Include(e => e.Department).ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public int GetEmployeeCountByDepartment(int departmentId)
        {
            return _context.Employees
                           .Where(e => e.DepartmentId == departmentId)
                           .Count();
        }
    }
}
