using ProjectDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.Repositories
{
    class EmployeeRepository : IRepository<Employee>
    {
        private ProjectDBContext db;

        public EmployeeRepository(ProjectDBContext context)
        {
            this.db = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return db.Employees.Include(o => o.Tasks).Include(p => p.Projects);
        }
        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }
        public void Create(Employee emp)
        {
            db.Employees.Add(emp);
        }
        public void Update(Employee emp)
        {
            db.Entry(emp).State = EntityState.Modified;
        }
        public IEnumerable<Employee> Find(Func<Employee, Boolean> predicate)
        {
            return db.Employees.Include(o => o.Tasks).Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Employee emp = db.Employees.Find(id);
            if (emp != null)
                db.Employees.Remove(emp);
        }
    }
}
