using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIK_DB_Projects.Ins
{
    /// <summary>
    /// Инсоляционная точка - модуль/ячейка
    /// </summary>
    public class InsPoint
    {
        /// <summary>
        ///  Номер строки (с 1)
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// номер столбца (с 1)
        /// </summary>
        public int Column { get; set; }
        /// <summary>
        /// Значение инсоляции (A, B, C, D - лат.)
        /// </summary>
        public string InsValue { get; set; }

        public string GetPoint()
        {
            return $"{Row};{Column}";
        }
    }
}