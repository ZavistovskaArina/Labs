using BLL.DTO;
using BLL.Interfases;
using ProjectDB;
using ProjectDB.Interfaces;
using ProjectDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLL.DTO.TaskDTO;

namespace BLL.Services
{
    public class ProjectServise : IProjectService
    {
        IUnitOfWork database;

        public ProjectServise()
        {
            database = new UnitOfWorkEntities();
        }
        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            IEnumerable<Project> project = database.Project.GetAll();
            List<ProjectDTO> p = new List<ProjectDTO>();
            foreach (var item in project)
            {
                p.Add(new ProjectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            };
            return p;
        }
        public IEnumerable<TaskDTO> GetAllTasks()
        {
            IEnumerable<ProjectDB.Task> task = database.Task.GetAll();
            List<TaskDTO> t = new List<TaskDTO>();
            foreach (var item in task)
            {
                t.Add(new TaskDTO()
                {
                    Id = item.Id,
                    Description = item.Description,
                    Time = item.Time,
                    Priority = item.Priority,
                    State = (States)item.State,
                    ProjectId = item.ProjectId,
                    EmployeeId = item.EmployeeId
                });
            };
            return t;
        }
        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            IEnumerable<Employee> emp = database.Employee.GetAll();
            List<EmployeeDTO> e = new List<EmployeeDTO>();
            foreach (var item in emp)
            {
                e.Add(new EmployeeDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Employment = item.Employment
                });
            };
            return e;
        }
        public void AddTask(string description, int time, int priority, ProjectDB.Task.States state, int projectId, int employeeId)
        {
            database.Task.Create(new ProjectDB.Task() { Description = description, Time = time, Priority = priority, State = state, ProjectId = projectId, EmployeeId = employeeId });
            database.Save();
        }
        public void UpdateTask(int id, string description, int time, int priority, ProjectDB.Task.States state, int projectId, int employeeId)
        {
            var obj = database.Task.Find(o => o.Id == id);
            foreach(var item in obj)
            {
                item.Description = description;
                item.Time = time;
                item.Priority = priority;
                item.State = state;
                item.ProjectId = projectId;
                item.EmployeeId = employeeId;
            }
            database.Save();
        }
        public void DeleteTask(int employeeId, int projectId)
        {
            var id = database.Task.Find(x => x.ProjectId == projectId && x.EmployeeId == employeeId);
            database.Task.Delete(id.FirstOrDefault().Id);
            database.Save();
        }
        public void QueryTask(ProjectDB.Task.States state)
        {
            IEnumerable<ProjectDB.Task> tsk = database.Task.GetAll();
            var query = from t in tsk
                        where t.State == state
                        select t;
            foreach (var t in query)
            {
                Console.WriteLine("{0} '{1}' {2}", t.Id, t.Description, t.Employee.Name);
            }
        }
        public void Dispose()
        {
            database.Dispose();
        }
    }
}
