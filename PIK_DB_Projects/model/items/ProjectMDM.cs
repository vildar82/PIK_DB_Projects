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
    public class ProjectMDM
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
    }
}
