using System.ComponentModel.DataAnnotations;

namespace Assignment_WDA.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string Location { get; set; }
        public int NumberOfPersonals { get; set; }
    }
}
