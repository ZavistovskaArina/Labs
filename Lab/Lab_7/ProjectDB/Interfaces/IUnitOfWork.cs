using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Project { get; }
        IRepository<Task> Task { get; }
        IRepository<Employee> Employee { get; }
        void Save();
    }
}
