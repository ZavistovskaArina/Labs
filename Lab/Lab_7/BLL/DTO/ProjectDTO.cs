using ProjectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProjectDTO() { }
        public ProjectDTO(Project project)
        {
            if (project != null)
            {
                Id = project.Id;
                Name = project.Name;
            }
        }
        public override string ToString()
        {
            return $"{this.Id}  {this.Name}";
        }
    }
}
