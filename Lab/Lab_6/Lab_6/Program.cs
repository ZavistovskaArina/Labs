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
            do
            {
                ProjectDBContext db = new ProjectDBContext();
                Case(db);
            } while (Console.ReadKey().Key == ConsoleKey.Escape);
        }

        private static void Case(ProjectDBContext db)
        {
            Console.WriteLine("Для выбора таблицы - 1");
            Console.WriteLine("Для работы с запросами - 2");
            int variant = Int32.Parse(Console.ReadLine());
            switch (variant)
            {
                case 1:
                    WorkwithTables(db);
                    break;
                case 2:
                    WorkQuery(db);
                    break;
            }
        }

        private static void WorkQuery(ProjectDBContext db)
        {
            Console.WriteLine(@"Выбирете запрос:
                        1 - Список задач в зависимости от состояния
                        2 - Список задач работника
                        3 - Список задач и работников проекта");
            int q = Int32.Parse(Console.ReadLine());
            switch (q)
            {
                case 1:
                    TaskQuery(db);
                    break;
                case 2:
                    EmployeeList(db);
                    break;
                case 3:
                    ProjectList(db);
                    break;
            }
        }

        private static void ProjectList(ProjectDBContext db)
        {
            Console.WriteLine("Введите Id проекта");
            int id = Int32.Parse(Console.ReadLine());
            var pr = db.Projects.Find(id);
            if (pr == null)
            {
                Console.WriteLine("Такого проекта нет. Выбирете проект: ");
                var query = from p in db.Projects
                            select p;
                foreach (var t in query)
                {
                    Console.WriteLine("{0} '{1}'", t.Id, t.Name);
                }
                Console.WriteLine("Введите Id работника: ");
                id = Int32.Parse(Console.ReadLine());
                pr = db.Projects.Find(id);
            }
            Console.WriteLine("'{0}'", pr.Name + " has tasks and employees: ");
            foreach (var task in pr.Tasks.ToList())
            {
                Console.WriteLine("Task: '{0}'", task.Description);
            }
            foreach (var r in pr.Employees.ToList())
            {
                Console.WriteLine("Employee: {0}", r.Name);
            }
        }

        private static void WorkwithTables(ProjectDBContext db)
        {
            Console.WriteLine(@"Выбирете таблицу:
                        1 - Project
                        2 - Task
                        3 - Employee");
            int tableNumber = Int32.Parse(Console.ReadLine());
            switch (tableNumber)
            {
                case 1:
                    ProjectCase(db);
                    break;
                case 2:
                    TaskCase(db);
                    break;
                case 3:
                    EmployeeCase(db);
                    break;
            }
        }

        private static void EmployeeList(ProjectDBContext db)
        {
            Console.WriteLine("Введите Id работника");
            int id = Int32.Parse(Console.ReadLine());
            var emp = db.Employees.Find(id);
            if (emp == null)
            {
                Console.WriteLine("Такого работника нет. Выбирете работника: ");
                var query = from e in db.Employees
                            select new
                            {
                                e.Id,
                                e.Name
                            };
                foreach (var t in query)
                {
                    Console.WriteLine("{0} {1}", t.Id, t.Name);
                }
                Console.WriteLine("Введите Id работника: ");
                id = Int32.Parse(Console.ReadLine());
                emp = db.Employees.Find(id);
            }
            Console.WriteLine(emp.Name + " has tasks: ");
            foreach (var p in emp.Tasks.ToList())
            {
                Console.WriteLine("{0} '{1}' {2} {3}", p.Id, p.Description, p.State, p.Time);
            }
        }

        private static void TaskQuery(ProjectDBContext db)
        {
            Console.WriteLine("Введите состояние задания (Received, Performing, Complited)");
            string state = Console.ReadLine();
            var task = (States)Enum.Parse(typeof(States), state);
            var query = from t in db.Tasks
                        where t.State == task
                        select t;
            foreach (var t in query)
            {
                Console.WriteLine("{0} '{1}' {3}", t.Id, t.Description, t.Employee.Name);
            }
        }

        private static void ProjectCase(ProjectDBContext db)
        {
            int action;
            Case();
            action = Int32.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    TableProjectView(db);
                    break;
                case 2:
                    AddProject(db);
                    break;
                case 3:
                    UpdateProject(db);
                    break;
                case 4:
                    DeleteProject(db);
                    break;
            }
        }

        private static void TaskCase(ProjectDBContext db)
        {
            int action;
            Case();
            action = Int32.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    TableTaskView(db);
                    break;
                case 2:
                    AddTask(db);
                    break;
                case 3:
                    UpdateTask(db);
                    break;
                case 4:
                    DeleteTask(db);
                    break;
            }
        }

        private static void EmployeeCase(ProjectDBContext db)
        {
            int action;
            Case();
            action = Int32.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    TableEmployeeView(db);
                    break;
                case 2:
                    AddEmployee(db);
                    break;
                case 3:
                    UpdateEmployee(db);
                    break;
                case 4:
                    DeleteEmployee(db);
                    break;
            }
        }

        private static void DeleteEmployee(ProjectDBContext db)
        {
            Console.WriteLine("Введите Id работника:");
            int id_ = Int32.Parse(Console.ReadLine());
            var emp = db.Employees.Find(id_);
            db.Employees.Remove(emp);
            db.SaveChanges();
            Console.Write("Запись удалена");
        }

        private static void UpdateEmployee(ProjectDBContext db)
        {
            Console.WriteLine("Введите Id работника:");
            int id_ = Int32.Parse(Console.ReadLine());
            var emp = db.Employees.Find(id_);
            Console.WriteLine(emp);
            Console.WriteLine("Введите имя работника: ");
            emp.Name = Console.ReadLine();
            Console.WriteLine("Введите занятость работника: ");
            emp.Employment = Int32.Parse(Console.ReadLine());
            db.SaveChanges();
            Console.Write("Запись обновлена");
        }
        private static void AddEmployee(ProjectDBContext db)
        {
            var eNew = new Employee();
            Console.WriteLine("Введите имя работника: ");
            eNew.Name = Console.ReadLine();
            Console.WriteLine("Введите занятость работника: ");
            eNew.Employment = Int32.Parse(Console.ReadLine());
            db.Employees.Add(eNew);
            db.SaveChanges();
            Console.Write("Запись добавлена");
        }
        private static void TableEmployeeView(ProjectDBContext db)
        {
            var employee = db.Employees;
            Console.WriteLine("Id  Name  Employment");
            foreach (Employee e in employee)
            {
                Console.WriteLine(e);
            }
        }
        private static void DeleteTask(ProjectDBContext db)
        {
            Console.WriteLine("Введите Id задания:");
            int id_ = Int32.Parse(Console.ReadLine());
            var tk = db.Tasks.Find(id_);
            db.Tasks.Remove(tk);
            db.SaveChanges();
            Console.Write("Запись удалена");
        }
        private static void UpdateTask(ProjectDBContext db)
        {
            Console.WriteLine("Введите Id задания: ");
            int id_ = Int32.Parse(Console.ReadLine());
            var tk = db.Tasks.Find(id_);
            Console.WriteLine(tk);
            Console.WriteLine("Введите описание задания: ");
            tk.Description = Console.ReadLine();
            Console.WriteLine("Введите время выполнения: ");
            tk.Time = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите приоритет задания: ");
            tk.Priority = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите состояние задания (Received, Performing, Comlited): ");
            string state = Console.ReadLine();
            tk.State = (States)Enum.Parse(typeof(States), state);
            Console.WriteLine("Введите Id проекта: ");
            tk.ProjectId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите Id worker: ");
            tk.EmployeeId = Int32.Parse(Console.ReadLine());
            db.SaveChanges();
            Console.Write("Запись обновлена");
        }

        private static void AddTask(ProjectDBContext db)
        {
            var tNew = new ProjectDB.Task();
            Console.WriteLine("Введите описание задания: ");
            tNew.Description = Console.ReadLine();
            Console.WriteLine("Введите время выполнения: ");
            tNew.Time = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите приоритет задания: ");
            tNew.Priority = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите состояние задания (Received, Performing, Complited): ");
            string state = Console.ReadLine();
            tNew.State = (States)Enum.Parse(typeof(States), state);
            Console.WriteLine("Введите Id проекта: ");
            tNew.ProjectId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите Id worker: ");
            int id = Int32.Parse(Console.ReadLine());
            var emp = db.Employees.Find(id);
            if (emp.Employment > 3)
            {
                Console.WriteLine("Работник очень занят. Выберете из доступных: ");
                var query = from e in db.Employees
                            where e.Employment <= 3
                            select new
                            {
                                e.Id,
                                e.Name,
                            };
                foreach (var t in query)
                {
                    Console.WriteLine("{0} {1}", t.Id, t.Name);
                }
                Console.WriteLine("Введите Id worker: ");
                tNew.EmployeeId = Int32.Parse(Console.ReadLine());
            }
            else
                tNew.EmployeeId = id;
            db.Tasks.Add(tNew);
            db.SaveChanges();
            Console.Write("Запись добавлена");
        }

        private static void TableTaskView(ProjectDBContext db)
        {
            var task = db.Tasks;
            Console.WriteLine("Id  Description  Time  Priority State ProjectId EmployeeId");
            foreach (ProjectDB.Task t in task)
            {
                Console.WriteLine(t);
            }
        }

        private static void DeleteProject(ProjectDBContext db)
        {
            Console.WriteLine("Введите Id проекта:");
            int id_ = Int32.Parse(Console.ReadLine());
            var pr = db.Projects.Find(id_);
            db.Projects.Remove(pr);
            db.SaveChanges();
            Console.Write("Запись удалена");
        }

        private static void UpdateProject(ProjectDBContext db)
        {
            Console.Write("Введите Id проекта:");
            int id_ = Int32.Parse(Console.ReadLine());
            var pr = db.Projects.Find(id_);
            Console.WriteLine(pr);
            Console.WriteLine("Введите название проекта: ");
            pr.Name = Console.ReadLine();
            db.SaveChanges();
            Console.Write("Запись обновлена");
        }

        private static void AddProject(ProjectDBContext db)
        {
            var pNew = new Project();
            Console.WriteLine("Введите название проекта: ");
            pNew.Name = Console.ReadLine();
            db.Projects.Add(pNew);
            db.SaveChanges();
            Console.WriteLine("Запись добавлена");
        }

        private static void TableProjectView(ProjectDBContext db)
        {
            var project = db.Projects;
            Console.WriteLine("Id  Name");
            foreach (Project p in project)
            {
                Console.WriteLine(p);
            }
        }

        static void Case()
        {
            Console.WriteLine(@"Выбирете действие:
                    1 - Отобразить таблицу
                    2 - Добавить запись
                    3 - Обновить запись
                    4 - Удалить запись");
        }
    }
}
