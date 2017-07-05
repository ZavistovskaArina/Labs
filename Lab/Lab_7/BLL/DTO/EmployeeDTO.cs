using ProjectDB;

namespace BLL.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Employment { get; set; }

        public EmployeeDTO() { }
        public EmployeeDTO(Employee emp)
        {
            if (emp != null)
            {
                Id = emp.Id;
                Name = emp.Name;
                Employment = emp.Employment;
            }
        }
        public override string ToString()
        {
            return $"{this.Id} {this.Name} {this.Employment}";
        }
    }
}
