using ProjectDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.Repositories
{
    class ProjectRepository : IRepository<Project>
    {
        private ProjectDBContext db;

        public ProjectRepository(ProjectDBContext context)
        {
            this.db = context;
        }
        public IEnumerable<Project> GetAll()
        {
            return db.Projects.Include(o => o.Tasks).Include(p => p.Employees);
        }
        public Project Get(int id)
        {
            return db.Projects.Find(id);
        }
        public void Create(Project project)
        {
            db.Projects.Add(project);
        }
        public void Update(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
        }
        public IEnumerable<Project> Find(Func<Project, Boolean> predicate)
        {
            return db.Projects.Include(o => o.Name).Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Project project = db.Projects.Find(id);
            if (project != null)
                db.Projects.Remove(project);
        }
    }
}
