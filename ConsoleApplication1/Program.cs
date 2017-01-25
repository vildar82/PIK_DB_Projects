using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIK_DB_Projects;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main (string[] args)
        {
            using (var ent = new TestDB.SAPREntities())
            {
                var data = ent.C_Sections.ToList();
            }            

            //var adapter = new TestDB.DataSet1TableAdapters.C_SectionsTableAdapter();
            //var data = adapter.GetData();
            //data = adapter.GetData();
        }
    }
}
