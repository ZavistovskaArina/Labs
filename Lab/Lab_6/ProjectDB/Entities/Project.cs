using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Task> Tasks { get; set; }
        public virtual List<Employee> Employees { get; set; }
        public Project()
        {
            Tasks = new List<Task>();
            Employees = new List<Employee>();
        }
        public override string ToString()
        {
            return $"{this.Id}  {this.Name}";
        }
    }
}
