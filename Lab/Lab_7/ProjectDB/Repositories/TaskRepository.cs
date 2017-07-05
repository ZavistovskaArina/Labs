using ProjectDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.Repositories
{
    class TaskRepository : IRepository<Task>
    {
        private ProjectDBContext db;

        public TaskRepository(ProjectDBContext context)
        {
            this.db = context;
        }
        public IEnumerable<Task> GetAll()
        {
            return db.Tasks.Include(o => o.Employee);
        }
        public Task Get(int id)
        {
            return db.Tasks.Find(id);
        }
        public void Create(Task task)
        {
            db.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
        }
        public IEnumerable<Task> Find(Func<Task, Boolean> predicate)
        {
            return db.Tasks.Include(o => o.Employee).Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task != null)
                db.Tasks.Remove(task);
        }
    }
}
