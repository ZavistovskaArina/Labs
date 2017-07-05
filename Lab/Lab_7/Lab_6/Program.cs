using BLL.Interfases;
using BLL.Services;
using ProjectDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectDB.Task;

namespace Lab_6
{
    class Program
    {
        static void Main(string[] args)
        {
            IProjectService proj = new ProjectServise();
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Press 1 to see information about projects");
                Console.WriteLine("Press 2 to see information about tasks");
                Console.WriteLine("Press 3 to see information about employees");
                Console.WriteLine("Press 4 to add the task");
                Console.WriteLine("Press 5 to update the task");
                Console.WriteLine("Press 6 to delete the task");
                Console.WriteLine("Press 7 to see task list");
                
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        GetProject(proj);
                        break;
                    case 2:
                        GetTask(proj);
                        break;
                    case 3:
                        GetEmployee(proj);
                        break;
                    case 4:
                        AddTask(proj);
                        break;
                    case 5:
                        UpdateTask(proj);
                        break;
                    case 6:
                        DeleteTask(proj);
                        break;
                    case 7:
                        TaskList(proj);
                        break;
                }
            }
        }

        private static void TaskList(IProjectService proj)
        {
            Console.WriteLine("State (Received, Performing, Comlited)");
            string state_ = Console.ReadLine();
            States s_ = (States)Enum.Parse(typeof(States), state_);
            proj.QueryTask(s_);
            Console.ReadKey();
        }

        private static void DeleteTask(IProjectService proj)
        {
            Console.WriteLine("ProjectId: ");
            int projectid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("EmployeeId: ");
            int employeeid = Convert.ToInt32(Console.ReadLine());
            proj.DeleteTask(employeeid, projectid);
            Console.WriteLine("Deleted");
            Console.ReadKey();
        }
        private static void UpdateTask(IProjectService proj)
        {
            Console.WriteLine("Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Description: ");
            string descr = Console.ReadLine();
            Console.WriteLine("Time: ");
            int time = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Priority: ");
            int prior = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("State (Received, Performing, Comlited)");
            string state = Console.ReadLine();
            States s = (States)Enum.Parse(typeof(States), state);
            Console.WriteLine("ProjectId: ");
            int projid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("EmployeeId: ");
            int empid = Convert.ToInt32(Console.ReadLine());
            proj.UpdateTask(id, descr, time, prior, s, projid, empid);
            Console.WriteLine("Update successful");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        private static void AddTask(IProjectService proj)
        {
            Console.WriteLine("Description: ");
            string descr = Console.ReadLine();
            Console.WriteLine("Time: ");
            int time = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Priority: ");
            int prior = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("State (Received, Performing, Comlited)");
            string state = Console.ReadLine();
            States s = (States)Enum.Parse(typeof(States), state);
            Console.WriteLine("ProjectId: ");
            int projid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("EmployeeId: ");
            int empid = Convert.ToInt32(Console.ReadLine());
            proj.AddTask(descr, time, prior, s, projid, empid);
            Console.WriteLine("Add successful");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            
        }

        private static void GetEmployee(IProjectService proj)
        {
            var emp = proj.GetAllEmployees();
            foreach (var e in emp)
            {
                Console.WriteLine("{0} {1} {2}", e.Id, e.Name, e.Employment);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void GetTask(IProjectService proj)
        {
            var tasks = proj.GetAllTasks();
            foreach (var t in tasks)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", t.Id, t.Description, t.Time, t.Priority, t.State, t.ProjectId, t.EmployeeId);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void GetProject(IProjectService proj)
        {
            var projects = proj.GetAllProjects();
            foreach (var p in projects)
            {
                Console.WriteLine("{0} {1}", p.Id, p.Name);
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
