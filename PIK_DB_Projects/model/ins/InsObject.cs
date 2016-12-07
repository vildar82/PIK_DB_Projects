using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIK_DB_Projects.Ins
{
    /// <summary>
    /// Объект с данными по инсоляции
    /// </summary>
    public class InsObject
    {
        /// <summary>
        /// Идентификатор объекта - из таблицы Objects
        /// </summary>
        public int Id { get; set; }
        public List<InsPoint> Points { get; set; }
    }
}
