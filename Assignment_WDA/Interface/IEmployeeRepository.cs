using Assignment_WDA.Entities;

namespace Assignment_WDA.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
        int GetEmployeeCountByDepartment(int departmentId);
    }
}
