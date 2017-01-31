using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIK_DB_Projects
{
    /// <summary>
    /// Проект.
    /// </summary>
    public class ProjectMDM : IEquatable<ProjectMDM>
    {
        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя проекта
        /// </summary>
        public string Name { get; set; }        

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(ProjectMDM other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(ProjectMDM prj1, ProjectMDM prj2)
        {
            if (ReferenceEquals(prj1, prj2)) return true;
            if (((object)prj1 == null) || ((object)prj2 == null)) return false;
            return prj1.Equals(prj2);
        }

        public static bool operator !=(ProjectMDM prj1, ProjectMDM prj2)
        {
            return !(prj1 == prj2);
        }
    }
}
