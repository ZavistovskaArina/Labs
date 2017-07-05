using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB
{
    public class Task
    {
        public enum States
        {
            Received,
            Performing,
            Complited
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public int Priority { get; set; }
        public States State { get; set; }

        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public override string ToString()
        {
            return $"{this.Id}  {this.Description} {this.Time} {this.Priority} {this.State} {this.ProjectId} {this.EmployeeId}";
        }
    }
}
