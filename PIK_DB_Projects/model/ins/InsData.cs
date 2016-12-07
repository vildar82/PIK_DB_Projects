using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIK_DB_Projects.Ins
{
    /// <summary>
    /// Данные для записи инсоляции
    /// </summary>
    public class InsData
    {
        /// <summary>
        /// Название для данных
        /// </summary>
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<InsObject> Objects { get; set; }

        public override string ToString()
        {
            return $"{Date} - {Name} ({Objects?.Count}секц.)";
        }

        /// <summary>
        /// Открыть в Excel
        /// </summary>
        public void ToExcel()
        {
            var tempFile = Path.GetTempFileName().Replace(".tmp", ".xlsx");
            using (var exc = new ExcelPackage(new FileInfo(tempFile)))
            {
                var sheet = exc.Workbook.Worksheets.Add("Ins");
                foreach (var obj in Objects)
                {
                    foreach (var pt in obj.Points)
                    {
                        var cell = sheet.Cells[pt.Row, pt.Column];
                        cell.Value = pt.InsValue;
                        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(GetColor(pt.InsValue));
                    }
                }
                exc.Save();
            }
            Process.Start(tempFile);
        }

        private System.Drawing.Color GetColor(string insValue)
        {
            switch (insValue)
            {
                case "A":
                    return System.Drawing.Color.Red;
                case "B":
                    return System.Drawing.Color.Yellow;
                case "C":
                    return System.Drawing.Color.Orange;
                case "D":
                    return System.Drawing.Color.Blue;
                default:
                    return System.Drawing.Color.Gray;
            }
        }
    }
}
