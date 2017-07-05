using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class TaskDTO
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
        public ProjectDTO Project { get; set; }

        public int? EmployeeId { get; set; }
        public EmployeeDTO Employee { get; set; }
        public TaskDTO() { }
        public TaskDTO(ProjectDB.Task task)
        {
            if (task != null)
            {
                Id = task.Id;
                Description = task.Description;
                Time = task.Time;
                Priority = task.Priority;
                State = (States)task.State;
                Project = new ProjectDTO(task.Project);
                Employee = new EmployeeDTO(task.Employee);
            }
        }
        public override string ToString()
        {
            return $"{this.Id}  {this.Description} {this.Time} {this.Priority} {this.State} {this.ProjectId} {this.EmployeeId}";
        }
    }
}
