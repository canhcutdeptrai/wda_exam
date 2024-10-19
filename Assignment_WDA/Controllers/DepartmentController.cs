using Assignment_WDA.Entities;
using Assignment_WDA.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_WDA.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentController(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAllDepartments();
            return View(departments);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.AddDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.UpdateDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            if (department == null) return NotFound();
            await _departmentRepository.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Report(int id)
        {
                var department = await _departmentRepository.GetDepartmentById(id);

                if (department == null)
                {
                    return NotFound();
                }

                var employeeCount = _employeeRepository.GetEmployeeCountByDepartment(id);

                ViewBag.DepartmentName = department.DepartmentName;
                ViewBag.EmployeeCount = employeeCount;

                return View();
        }


    }
}
