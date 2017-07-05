using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Employment { get; set; }

        public virtual List<Project> Projects { get; set; }
        public virtual List<Task> Tasks { get; set; }
        public Employee()
        {
            Projects = new List<Project>();
            Tasks = new List<Task>();
        }
        public override string ToString()
        {
            return $"{this.Id} {this.Name} {this.Employment}";
        }
    }
}
