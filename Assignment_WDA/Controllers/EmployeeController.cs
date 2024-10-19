using Assignment_WDA.Entities;
using Assignment_WDA.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_WDA.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAllEmployees().ToList();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            var department = await _departmentRepository.GetAllDepartments();
            ViewBag.Departments = new SelectList(department, "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            var department = await _departmentRepository.GetAllDepartments();
            ViewBag.Departments = new SelectList(department, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var department = await _departmentRepository.GetAllDepartments();

            ViewBag.Departments = new SelectList(department, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _employeeRepository.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            var department = await _departmentRepository.GetAllDepartments();

            ViewBag.Departments = new SelectList(department, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            _employeeRepository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        
    }
}
