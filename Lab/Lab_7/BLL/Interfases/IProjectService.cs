using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfases
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetAllProjects();
        IEnumerable<TaskDTO> GetAllTasks();
        IEnumerable<EmployeeDTO> GetAllEmployees();
        void AddTask(string description, int time, int priority, ProjectDB.Task.States state, int projectId, int employeeId);
        void UpdateTask(int id, string description, int time, int priority, ProjectDB.Task.States state, int projectId, int employeeId);
        void DeleteTask(int employeeId, int projectId);
        void QueryTask(ProjectDB.Task.States state);
        void Dispose();
    }
}
